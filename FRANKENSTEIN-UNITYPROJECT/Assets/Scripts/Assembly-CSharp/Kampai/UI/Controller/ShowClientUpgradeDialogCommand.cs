namespace Kampai.UI.Controller
{
	public class ShowClientUpgradeDialogCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.UI.View.IGUIService guiService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		public override void Execute()
		{
			string storeUrl = "market://details?id=com.ea.gp.minions";
			ShowUnityDialog(storeUrl);
		}

		private void ShowNativeDialog()
		{
			global::System.EventHandler<NativeAlertManager.NativeAlertEventArgs> onClick = null;
			onClick = delegate(object sender, NativeAlertManager.NativeAlertEventArgs eventArgs)
			{
				string buttonText = eventArgs.ButtonText;
				if (buttonText == "OK")
				{
					logger.Info("Going to store...");
					NativeAlertManager.AlertClicked -= onClick;
					global::UnityEngine.Application.OpenURL("market://details?id=com.ea.gp.minions");
				}
			};
			NativeAlertManager.AlertClicked += onClick;
			NativeAlertManager.ShowAlert("Client Upgrade", "A game update is available.  Press OK to download it from the app store.", "OK", "Cancel");
		}

		private void ShowUnityDialog(string storeUrl = "")
		{
			global::Kampai.UI.View.IGUICommand iGUICommand = guiService.BuildCommand(global::Kampai.UI.View.GUIOperation.Load, "popup_NudgeUpgrade");
			iGUICommand.skrimScreen = "ClientUpgradeSkrim";
			iGUICommand.darkSkrim = true;
			iGUICommand.Args.Add(storeUrl);
			guiService.Execute(iGUICommand);
		}
	}
}
