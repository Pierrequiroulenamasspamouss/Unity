namespace Kampai.UI.View
{
	public class HelpPanelMediator : global::strange.extensions.mediation.impl.Mediator
	{
		[Inject]
		public global::Kampai.UI.View.HelpPanelView view { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal soundFXSignal { get; set; }

		[Inject(global::Kampai.Main.MainElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable mainContext { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService localizationService { get; set; }

		[Inject]
		public global::Kampai.Game.IConfigurationsService configurationsService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		public override void OnRegister()
		{
			view.moreHelpButton.ClickedSignal.AddListener(MoreHelpClicked);
			Init();
		}

		public override void OnRemove()
		{
			view.moreHelpButton.ClickedSignal.RemoveListener(MoreHelpClicked);
		}

		private void Init()
		{
			view.helpIdText.text = string.Format("{0}{1}", localizationService.GetString("playerid"), playerService.ID);
			view.moreHelpText.text = localizationService.GetString("MoreHelp");
		}

		private void MoreHelpClicked()
		{
			soundFXSignal.Dispatch("Play_button_click_01");
			mainContext.injectionBinder.GetInstance<global::Kampai.Main.OpenHelpSignal>().Dispatch();
		}

		private void OnEnable()
		{
			logger.Info("wwce killswitch :{0}", configurationsService.isKillSwitchOn(global::Kampai.Game.KillSwitch.WWCE));
			view.moreHelpButton.gameObject.SetActive(!configurationsService.isKillSwitchOn(global::Kampai.Game.KillSwitch.WWCE));
		}
	}
}
