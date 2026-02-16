namespace Kampai.UI.View
{
	public class ButtonView : global::strange.extensions.mediation.impl.View
	{
		public bool PlaySoundOnClick = true;

		public global::strange.extensions.signal.impl.Signal ClickedSignal = new global::strange.extensions.signal.impl.Signal();

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal playSFXSignal { get; set; }

		public virtual void OnClickEvent()
		{
			if (PlaySoundOnClick)
			{
				playSFXSignal.Dispatch("Play_button_click_01");
			}
			ClickedSignal.Dispatch();
		}
	}
}
