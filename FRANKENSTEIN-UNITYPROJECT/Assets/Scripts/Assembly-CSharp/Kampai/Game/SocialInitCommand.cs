namespace Kampai.Game
{
	public class SocialInitCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Game.ISocialService socialService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public ILocalPersistanceService localPersistanceService { get; set; }

		[Inject]
		public global::Kampai.Game.SocialInitSuccessSignal initSuccess { get; set; }

		[Inject]
		public global::Kampai.Game.SocialInitFailureSignal initFailure { get; set; }

		public override void Execute()
		{
			logger.Debug("Social Init Command Called With {0}", socialService.type.ToString());
			socialService.Init(initSuccess, initFailure);
		}
	}
}
