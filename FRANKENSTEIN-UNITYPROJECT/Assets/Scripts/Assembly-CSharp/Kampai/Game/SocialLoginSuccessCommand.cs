namespace Kampai.Game
{
	public class SocialLoginSuccessCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Game.ISocialService socialService { get; set; }

		[Inject]
		public global::Kampai.Game.SocialInitSuccessSignal socialInitSuccess { get; set; }

		[Inject]
		public global::Kampai.Common.Service.HealthMetrics.IClientHealthService clientHealth { get; set; }

		public override void Execute()
		{
			logger.Debug("Social Login Success");
			socialService.SendLoginTelemetry("MANUAL");
			socialInitSuccess.Dispatch(socialService);
		}
	}
}
