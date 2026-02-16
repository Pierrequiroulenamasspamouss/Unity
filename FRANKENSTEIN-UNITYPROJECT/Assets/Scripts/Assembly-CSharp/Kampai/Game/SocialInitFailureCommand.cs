namespace Kampai.Game
{
	public class SocialInitFailureCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Game.ISocialService socialService { get; set; }

		[Inject]
		public global::Kampai.Main.TriggerUpsightPromoSignal triggerUpsightPromoSignal { get; set; }

		[Inject]
		public ILocalPersistanceService localPersistanceService { get; set; }

		public override void Execute()
		{
			logger.Log(global::Kampai.Util.Logger.Level.Error, socialService.type.ToString() + " Init Failed");
			if (!localPersistanceService.GetData("UpsightTriggeredAtGameLaunch").Equals("True"))
			{
				triggerUpsightPromoSignal.Dispatch(global::Kampai.Game.UpsightPromoTrigger.Placement.GameLaunch);
				localPersistanceService.PutData("UpsightTriggeredAtGameLaunch", "True");
			}
		}
	}
}
