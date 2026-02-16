namespace Kampai.UI.View
{
	public class HUDSettingsButtonMediator : global::strange.extensions.mediation.impl.Mediator
	{
		[Inject]
		public global::Kampai.UI.View.HUDSettingsButtonView view { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal soundFXSignal { get; set; }

		[Inject(global::Kampai.Game.SocialServices.FACEBOOK)]
		public global::Kampai.Game.ISocialService facebookService { get; set; }

		[Inject]
		public global::Kampai.UI.View.UpdateFacebookStateSignal updateFacebookDialogState { get; set; }

		[Inject]
		public global::Kampai.UI.View.CloseAllOtherMenuSignal closeAllMenuSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.StopAutopanSignal stopAutopanSignal { get; set; }

		[Inject]
		public global::Kampai.Common.PickControllerModel model { get; set; }

		public override void OnRegister()
		{
			view.ClickedSignal.AddListener(ButtonClicked);
		}

		public override void OnRemove()
		{
			view.ClickedSignal.RemoveListener(ButtonClicked);
		}

		private void ButtonClicked()
		{
			if (view.display.IsActive())
			{
				view.display.gameObject.SetActive(false);
				return;
			}
			model.Enabled = false;
			stopAutopanSignal.Dispatch();
			soundFXSignal.Dispatch("Play_menu_popUp_01");
			updateFacebookDialogState.Dispatch(facebookService.isLoggedIn);
			closeAllMenuSignal.Dispatch(view.display.gameObject);
			view.display.gameObject.SetActive(true);
		}
	}
}
