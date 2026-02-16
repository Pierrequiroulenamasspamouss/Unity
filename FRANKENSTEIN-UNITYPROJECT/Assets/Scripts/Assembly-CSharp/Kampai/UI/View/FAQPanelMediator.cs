namespace Kampai.UI.View
{
	public class FAQPanelMediator : global::Kampai.UI.View.UIStackMediator<global::Kampai.UI.View.FAQPanelView>
	{
		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal soundFXSignal { get; set; }

		public override void OnRegister()
		{
			base.view.closeButton.ClickedSignal.AddListener(Close);
			base.OnRegister();
		}

		public override void OnRemove()
		{
			base.view.closeButton.ClickedSignal.RemoveListener(Close);
			base.OnRemove();
		}

		protected override void Close()
		{
			soundFXSignal.Dispatch("Play_button_click_01");
			base.gameObject.SetActive(false);
		}
	}
}
