namespace Kampai.UI.View
{
	public class TownHallDialogMediator : global::Kampai.UI.View.UIStackMediator<global::Kampai.UI.View.TownHallDialogView>
	{
		[Inject]
		public global::Kampai.Game.MignetteCollectionService mignetteCollectionService { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.UI.View.IGUIService guiService { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService localizationService { get; set; }

		[Inject]
		public global::Kampai.UI.View.HideSkrimSignal hideSkrim { get; set; }

		[Inject]
		public global::Kampai.Game.ILandExpansionService landExpansionService { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal soundFXSignal { get; set; }

		public override void OnRegister()
		{
			base.OnRegister();
			soundFXSignal.Dispatch("Play_menu_popUp_01");
			base.view.CloseButton.ClickedSignal.AddListener(Close);
			global::System.Collections.Generic.List<int> list = new global::System.Collections.Generic.List<int>();
			global::System.Collections.Generic.IList<global::Kampai.Game.MignetteBuilding> instancesByType = playerService.GetInstancesByType<global::Kampai.Game.MignetteBuilding>();
			foreach (global::Kampai.Game.MignetteBuilding item in instancesByType)
			{
				int iD = item.Definition.ID;
				if (!list.Contains(iD))
				{
					list.Add(iD);
					base.view.AddMignetteScoreSummary(item);
				}
			}
			global::System.Collections.Generic.List<global::Kampai.Game.MignetteBuilding> list2 = new global::System.Collections.Generic.List<global::Kampai.Game.MignetteBuilding>();
			global::System.Collections.Generic.IList<global::Kampai.Game.Building> allAspirationalBuildings = landExpansionService.GetAllAspirationalBuildings();
			foreach (global::Kampai.Game.Building item2 in allAspirationalBuildings)
			{
				global::Kampai.Game.MignetteBuilding mignetteBuilding = item2 as global::Kampai.Game.MignetteBuilding;
				int iD = mignetteBuilding.Definition.ID;
				if (mignetteBuilding != null && !list.Contains(iD))
				{
					list.Add(iD);
					list2.Add(mignetteBuilding);
					base.view.AddMignetteScoreSummary(mignetteBuilding);
				}
			}
			base.view.LeftAlignContent();
		}

		public override void OnRemove()
		{
			base.OnRemove();
			base.view.CloseButton.ClickedSignal.RemoveListener(Close);
		}

		protected override void Close()
		{
			hideSkrim.Dispatch("TownHallSkrim");
			guiService.Execute(global::Kampai.UI.View.GUIOperation.Unload, "screen_TownHall");
		}
	}
}
