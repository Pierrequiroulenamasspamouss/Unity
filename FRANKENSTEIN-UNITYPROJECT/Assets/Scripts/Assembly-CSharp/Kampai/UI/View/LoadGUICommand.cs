namespace Kampai.UI.View
{
	public class LoadGUICommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.UI.View.IGUIService guiService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		public override void Execute()
		{
			logger.EventStart("LoadGUICommand.Execute");
			if (global::Kampai.Util.GameConstants.StaticConfig.DEBUG_ENABLED)
			{
				guiService.Execute(global::Kampai.UI.View.GUIOperation.LoadStatic, "DebugConsoleButton");
			}
			global::UnityEngine.GameObject o = guiService.Execute(global::Kampai.UI.View.GUIOperation.LoadStatic, "screen_HUD");
			base.injectionBinder.Bind<global::UnityEngine.GameObject>().ToValue(o).ToName(global::Kampai.UI.View.UIElement.HUD);
			logger.EventStart("LoadGUICommand.Execute");
		}
	}
}
