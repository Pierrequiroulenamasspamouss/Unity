namespace Kampai.Game.Controller
{
	public class AppFocusGainedCommand : global::strange.extensions.command.impl.Command
	{
		public override void Execute()
		{
			bool autorotateToLandscapeRight = (global::UnityEngine.Screen.autorotateToLandscapeLeft = global::Kampai.Util.Native.AutorotationIsOSAllowed());
			global::UnityEngine.Screen.autorotateToLandscapeRight = autorotateToLandscapeRight;
		}
	}
}
