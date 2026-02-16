namespace Kampai.Game
{
	public class SocialInitAllServicesCommand : global::strange.extensions.command.impl.Command
	{
		[Inject(global::Kampai.Game.SocialServices.FACEBOOK)]
		public global::Kampai.Game.ISocialService facebookService { get; set; }

		[Inject(global::Kampai.Game.SocialServices.GOOGLEPLAY)]
		public global::Kampai.Game.ISocialService gpService { get; set; }

		[Inject]
		public global::Kampai.Game.SocialInitSignal socialInitSignal { get; set; }

		[Inject]
		public global::Kampai.Common.ICoppaService coppaService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		public override void Execute()
		{
			logger.EventStart("SocialInitAllServicesCommand.Execute");
			socialInitSignal.Dispatch(facebookService);
			if (!coppaService.Restricted())
			{
				socialInitSignal.Dispatch(gpService);
			}
			logger.EventStop("SocialInitAllServicesCommand.Execute");
		}
	}
}
