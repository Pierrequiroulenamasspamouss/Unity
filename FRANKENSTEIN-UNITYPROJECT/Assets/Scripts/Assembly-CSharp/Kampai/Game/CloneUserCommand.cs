namespace Kampai.Game
{
	public class CloneUserCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public long UserId { get; set; }

		[Inject]
		public global::Kampai.Game.IUserSessionService userSessionService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Main.ReloadGameSignal reloadGameSignal { get; set; }

		public override void Execute()
		{
			global::Kampai.Game.UserSession userSession = userSessionService.UserSession;
			string text = string.Format("{0}/rest/gamestate/{1}/{2}", global::Kampai.Util.GameConstants.StaticConfig.SERVER_URL, UserId, userSession.UserID);
			logger.Log(global::Kampai.Util.Logger.Level.Info, true, string.Format("Cloning: {0}", text));
			global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest request = new global::Ea.Sharkbite.HttpPlugin.Http.Impl.DefaultRequest(text).WithHeaderParam("user_id", userSession.UserID).WithHeaderParam("session_key", userSession.SessionID);
			global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse response = null;
			try
			{
				response = request.Execute();
				if (response.Success)
				{
					reloadGameSignal.Dispatch();
				}
				else
				{
					logger.Log(global::Kampai.Util.Logger.Level.Error, true, string.Format("Server request failed: [{0}]{1}", response.Code, response.Body));
				}
			}
			catch (global::System.Exception ex)
			{
				logger.Log(global::Kampai.Util.Logger.Level.Error, true, string.Format("Error: {0}", ex.Message));
			}
		}
	}
}
