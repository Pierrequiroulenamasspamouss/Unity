namespace Kampai.UI.View
{
	public class COPPAAgeGatePanelView : global::strange.extensions.mediation.impl.View
	{
		public global::UnityEngine.UI.Slider AgeSlider;

		public global::UnityEngine.UI.Text AgeText;

		public global::Kampai.UI.View.ButtonView TOSButton;

		public global::Kampai.UI.View.ButtonView EULAButton;

		public global::Kampai.UI.View.ButtonView PrivacyButton;

		public global::Kampai.UI.View.ButtonView DeclineButton;

		public global::Kampai.UI.View.ButtonView AcceptButton;

		private float lastSoundPlayed;

		private global::Kampai.Main.PlayGlobalSoundFXSignal soundFXSignal;

		private global::Kampai.Game.ITimeService timeService;

		internal void Init(global::Kampai.Main.PlayGlobalSoundFXSignal soundSignal, global::Kampai.Game.ITimeService time)
		{
			soundFXSignal = soundSignal;
			timeService = time;
		}

		public void OnSliderChanged()
		{
			if (AgeSlider != null && AgeText != null)
			{
				float num = timeService.SecondsSinceAppStartAsFloat() - lastSoundPlayed;
				if (num >= 0.17f)
				{
					soundFXSignal.Dispatch("Play_minion_confirm_select_02");
					lastSoundPlayed = timeService.SecondsSinceAppStartAsFloat();
				}
				AgeText.text = AgeSlider.value.ToString();
				bool flag = AgeSlider.value > 0f;
				global::UnityEngine.UI.Button component = AcceptButton.gameObject.GetComponent<global::UnityEngine.UI.Button>();
				if (component.interactable != flag)
				{
					component.interactable = flag;
				}
			}
		}
	}
}
