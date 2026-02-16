namespace Kampai.Game
{
	public class SocialLoginCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Game.ISocialService socialService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Game.SocialLoginSuccessSignal loginSuccess { get; set; }

		[Inject]
		public global::Kampai.Game.SocialLoginFailureSignal loginFailure { get; set; }

		public override void Execute()
		{
			logger.Debug("Social Login Command Called With {0}", socialService.type.ToString());
			if (!socialService.isLoggedIn)
			{
				socialService.Login(loginSuccess, loginFailure);
			}
			else
			{
				logger.Debug("Already logged on, you must log out first");
			}
		}
	}
}
