namespace Kampai.Game
{
	public class SocialInitSuccessCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Game.IUserSessionService userSessionService { get; set; }

		[Inject]
		public global::Kampai.Game.ISocialService socialService { get; set; }

		[Inject]
		public global::Kampai.Game.LinkAccountSignal linkAccountSignal { get; set; }

		[Inject]
		public global::Kampai.Common.Service.HealthMetrics.IClientHealthService clientHealth { get; set; }

		[Inject]
		public global::Kampai.Main.TriggerUpsightPromoSignal triggerUpsightPromoSignal { get; set; }

		[Inject]
		public ILocalPersistanceService localPersistanceService { get; set; }

		[Inject]
		public global::Kampai.Game.UpdateMarketplaceSlotStateSignal updateMarketplaceSlotSignal { get; set; }

		public override void Execute()
		{
			global::Kampai.Game.SocialServices type = socialService.type;
			switch (type)
			{
			case global::Kampai.Game.SocialServices.FACEBOOK:
				clientHealth.MarkMeterEvent("External.Facebook.Login");
				break;
			case global::Kampai.Game.SocialServices.GAMECENTER:
				clientHealth.MarkMeterEvent("External.GameCenter.Login");
				break;
			case global::Kampai.Game.SocialServices.GOOGLEPLAY:
				clientHealth.MarkMeterEvent("External.Google.Login");
				break;
			}
			logger.Debug("In {0} Init Success", type.ToString());
			if (userSessionService.UserSession != null)
			{
				updateMarketplaceSlotSignal.Dispatch();
				CheckLoggedIn(type);
				if (!localPersistanceService.GetData("UpsightTriggeredAtGameLaunch").Equals("True"))
				{
					triggerUpsightPromoSignal.Dispatch(global::Kampai.Game.UpsightPromoTrigger.Placement.GameLaunch);
					localPersistanceService.PutData("UpsightTriggeredAtGameLaunch", "True");
				}
			}
		}

		private void CheckLoggedIn(global::Kampai.Game.SocialServices socialType)
		{
			bool flag = false;
			if (!socialService.isLoggedIn)
			{
				return;
			}
			socialService.LoginSource = null;
			socialService.SendLoginTelemetry("AUTOMATIC");
			logger.Debug("{0} Logged into looking into links", socialType.ToString());
			foreach (global::Kampai.Game.UserIdentity socialIdentity in userSessionService.UserSession.SocialIdentities)
			{
				if (socialIdentity.Type.ToString().ToLower().Equals(socialType.ToString().ToLower()))
				{
					flag = true;
					if (!(socialIdentity.ExternalID == socialService.userID))
					{
						logger.Debug("Calling link from {0} Init", socialType.ToString());
						linkAccountSignal.Dispatch(socialService, false);
					}
					return;
				}
			}
			if (!flag)
			{
				logger.Debug("Calling link from {0} Init", socialType.ToString());
				linkAccountSignal.Dispatch(socialService, false);
			}
		}
	}
}
