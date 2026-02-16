namespace Kampai.UI.View
{
	public class SocialPartyNoEventMediator : global::Kampai.UI.View.UIStackMediator<global::Kampai.UI.View.SocialPartyNoEventView>
	{
		[Inject]
		public global::Kampai.Main.ILocalizationService localService { get; set; }

		[Inject]
		public global::Kampai.UI.View.IGUIService guiService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal globalSFX { get; set; }

		[Inject]
		public global::Kampai.UI.View.HideSkrimSignal hideSkrimSignal { get; set; }

		public override void OnRegister()
		{
			base.OnRegister();
			base.view.Init();
			base.view.TitleText.text = localService.GetString("socialpartynoeventtitle");
			base.view.MessageText.text = localService.GetString("socialpartynoeventdescription");
			base.view.YesButtonText.text = localService.GetString("socialpartynoeventbutton");
			base.view.YesButton.ClickedSignal.AddListener(OkButtonPressed);
			base.view.OnMenuClose.AddListener(OnClose);
		}

		public override void OnRemove()
		{
			base.OnRemove();
			base.view.YesButton.ClickedSignal.RemoveListener(OkButtonPressed);
			base.view.OnMenuClose.RemoveListener(OnClose);
		}

		protected override void Close()
		{
			OkButtonPressed();
		}

		public void OkButtonPressed()
		{
			base.view.Close();
			OnClose();
		}

		public void OnClose()
		{
			guiService.Execute(global::Kampai.UI.View.GUIOperation.Unload, "popup_SocialParty_NoEvent");
			hideSkrimSignal.Dispatch("SocialSkrim");
		}
	}
}
