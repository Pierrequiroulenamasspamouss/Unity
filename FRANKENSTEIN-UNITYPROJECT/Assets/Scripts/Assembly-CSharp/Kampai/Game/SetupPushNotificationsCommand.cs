namespace Kampai.Game
{
	public class SetupPushNotificationsCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Game.IPushNotificationService pushNotificationService { get; set; }

		[Inject]
		public global::Kampai.Game.IUserSessionService userSessionService { get; set; }

		[Inject]
		public ILocalPersistanceService localPersistanceService { get; set; }

		[Inject]
		public global::Kampai.Common.ICoppaService coppaService { get; set; }

		public override void Execute()
		{
			if (!"true".Equals(localPersistanceService.GetDataPlayer("AllowPushNotifications")))
			{
				logger.Debug("[PN] SetupPushNotificationsCommand: PNs were not enabled by user.");
				return;
			}
			logger.Debug("[PN] SetupPushNotificationsCommand: prepare args for PN service");
			global::Kampai.Game.UserSession userSession = userSessionService.UserSession;
			if (userSession == null)
			{
				logger.Error("[PN] User session is null");
				return;
			}
			string userID = userSession.UserID;
			if (string.IsNullOrEmpty(userID))
			{
				logger.Error("[PN] User alias is invalid");
				return;
			}
			global::System.DateTime birthdate;
			if (!coppaService.GetBirthdate(out birthdate))
			{
				logger.Info("[PN] Coppa birthdate is unknown at the moment");
				return;
			}
			logger.Debug("[PN] Start push notification service.");
			pushNotificationService.Start(userID, birthdate.Year, birthdate.Month);
		}
	}
}
