namespace Kampai.Game
{
	public class SocialLoginFailureCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Game.ISocialService socialService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		public override void Execute()
		{
			logger.Debug("{0} Login Failed", socialService.type.ToString());
		}
	}
}
