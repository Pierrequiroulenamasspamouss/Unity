namespace Kampai.UI.View
{
	public class CompositeBuildingMenuMediator : global::Kampai.UI.View.UIStackMediator<global::Kampai.UI.View.CompositeBuildingMenuView>
	{
		private global::Kampai.Game.CompositeBuilding compositeBuilding;

		[Inject]
		public global::Kampai.UI.View.IGUIService guiService { get; set; }

		[Inject]
		public global::Kampai.UI.View.HideSkrimSignal hideSkrim { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService localizationService { get; set; }

		[Inject]
		public global::Kampai.Game.ShuffleCompositeBuildingPiecesSignal shuffleCompositeBuildingPiecesSignal { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal soundFXSignal { get; set; }

		public override void OnRegister()
		{
			base.OnRegister();
			base.view.gameObject.SetActive(false);
			base.view.ShuffleButton.ClickedSignal.AddListener(OnShuffleClicked);
			base.view.MignettesButton.ClickedSignal.AddListener(OnMignettesClicked);
		}

		public override void OnRemove()
		{
			base.OnRemove();
			base.view.ShuffleButton.ClickedSignal.RemoveListener(OnShuffleClicked);
			base.view.MignettesButton.ClickedSignal.RemoveListener(OnMignettesClicked);
		}

		public override void Initialize(global::Kampai.UI.View.GUIArguments args)
		{
			compositeBuilding = args.Get<global::Kampai.Game.CompositeBuilding>();
			base.view.Init(compositeBuilding, localizationService);
			base.view.gameObject.SetActive(true);
		}

		protected override void Close()
		{
			soundFXSignal.Dispatch("Play_menu_disappear_01");
			hideSkrim.Dispatch("BuildingSkrim");
			guiService.Execute(global::Kampai.UI.View.GUIOperation.Unload, PrefabName);
		}

		private void OnShuffleClicked()
		{
			shuffleCompositeBuildingPiecesSignal.Dispatch(compositeBuilding.ID);
		}

		private void OnMignettesClicked()
		{
			Close();
			global::Kampai.UI.View.IGUICommand iGUICommand = guiService.BuildCommand(global::Kampai.UI.View.GUIOperation.Load, "screen_TownHall");
			iGUICommand.skrimScreen = "TownHallSkrim";
			iGUICommand.darkSkrim = true;
			guiService.Execute(iGUICommand);
		}
	}
}
