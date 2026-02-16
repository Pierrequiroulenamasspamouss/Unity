namespace Kampai.Game
{
	public class DCNService : global::Kampai.Game.IDCNService
	{
		private global::Kampai.Game.DCNModel dcnModel = new global::Kampai.Game.DCNModel();

		private global::System.Collections.Generic.IList<global::System.Func<global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest>> requests = new global::System.Collections.Generic.List<global::System.Func<global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest>>();

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Common.ICoppaService coppaService { get; set; }

		[Inject]
		public global::Kampai.Download.IDownloadService downloadService { get; set; }

		[Inject]
		public global::Kampai.Game.DCNTokenSignal dcnTokenSignal { get; set; }

		public void Perform(global::System.Func<global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest> request, bool isTokenRequest = false)
		{
			if (!coppaService.Restricted())
			{
				if ((dcnModel.Token == null || !dcnModel.Token.IsValid()) && !isTokenRequest)
				{
					requests.Add(request);
					dcnTokenSignal.Dispatch();
				}
				else
				{
					downloadService.Perform(request());
				}
			}
		}

		public void SetToken(global::Kampai.Game.DCNToken token)
		{
			dcnModel.Token = token;
			if (requests.Count > 0)
			{
				Perform(requests[0]);
				requests.RemoveAt(0);
			}
		}

		public string GetToken()
		{
			if (dcnModel == null)
			{
				return string.Empty;
			}
			if (dcnModel.Token == null)
			{
				return string.Empty;
			}
			return dcnModel.Token.Token;
		}

		public void SetFeaturedContentId(int featuredContentId)
		{
			dcnModel.FeaturedContentId = featuredContentId;
		}

		public int GetFeaturedContentId()
		{
			return dcnModel.FeaturedContentId;
		}
	}
}
