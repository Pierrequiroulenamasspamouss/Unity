namespace Kampai.UI.View
{
	public class PopupMessageCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public string localizedText { get; set; }

		[Inject]
		public global::Kampai.UI.View.IGUIService guiService { get; set; }

		public override void Execute()
		{
			global::Kampai.UI.View.IGUICommand iGUICommand = guiService.BuildCommand(global::Kampai.UI.View.GUIOperation.LoadStatic, "popup_MessageBox");
			iGUICommand.Args.Add(localizedText);
			guiService.Execute(iGUICommand);
		}
	}
}
