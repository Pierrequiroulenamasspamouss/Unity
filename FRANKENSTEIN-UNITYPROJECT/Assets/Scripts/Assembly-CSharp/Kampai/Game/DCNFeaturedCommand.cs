namespace Kampai.Game
{
	public class DCNFeaturedCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Game.IDCNService dcnService { get; set; }

		[Inject]
		public global::Kampai.Game.DCNEventSignal eventSignal { get; set; }

		public override void Execute()
		{
			dcnService.Perform(Request);
		}

		private global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest Request()
		{
			string uri = string.Format("{0}{1}", global::Kampai.Util.GameConstants.DCN.SERVER, "/contents/featured");
			global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse> signal = new global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse>();
			signal.AddListener(Response);
			return new global::Ea.Sharkbite.HttpPlugin.Http.Impl.DefaultRequest(uri).WithMethod(global::Ea.Sharkbite.HttpPlugin.Http.Impl.DefaultRequest.METHOD_GET).WithHeaderParam("X-DCN-TOKEN", dcnService.GetToken()).WithResponseSignal(signal);
		}

		private void Response(global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse response)
		{
			if (response == null)
			{
				logger.Log(global::Kampai.Util.Logger.Level.Error, "DCNFeaturedCommand response is null");
			}
			else if (!response.Success)
			{
				logger.Log(global::Kampai.Util.Logger.Level.Error, string.Format("DCNFeaturedCommand failed with response code: {0}", response.Code));
			}
			else
			{
				Deserialize(response.Body);
			}
		}

		private void Deserialize(string json)
		{
			global::Kampai.Game.DCNContent dCNContent = null;
			try
			{
				dCNContent = global::Newtonsoft.Json.JsonConvert.DeserializeObject<global::Kampai.Game.DCNContent>(json);
			}
			catch (global::Newtonsoft.Json.JsonSerializationException e)
			{
				HandleJsonException(e);
			}
			catch (global::Newtonsoft.Json.JsonReaderException e2)
			{
				HandleJsonException(e2);
			}
			if (dCNContent == null)
			{
				logger.Log(global::Kampai.Util.Logger.Level.Error, "Error: content is null");
				return;
			}
			dcnService.SetFeaturedContentId(dCNContent.Id);
			string value;
			if (dCNContent.Urls.TryGetValue("html5", out value))
			{
				string value2 = global::UnityEngine.WWW.EscapeURL("minions:\\dcn");
				global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder(value).Append("&token=").Append(dcnService.GetToken()).Append("&return_name=")
					.Append("Minions")
					.Append("&return_url=")
					.Append(value2);
				global::UnityEngine.Application.OpenURL(stringBuilder.ToString());
				eventSignal.Dispatch();
			}
			else
			{
				logger.Log(global::Kampai.Util.Logger.Level.Warning, "HTML5 URL does not exist in the response!");
			}
		}

		private void HandleJsonException(global::System.Exception e)
		{
			logger.Error("[Error]\n{0}", e.Message);
			logger.Error("[StackTrace]\n{0}", e.StackTrace);
		}
	}
}
