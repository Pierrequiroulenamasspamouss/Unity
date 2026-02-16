namespace Kampai.UI.View
{
	public class MignetteCallMinionsView : global::Kampai.UI.View.PopupMenuView
	{
		public global::Kampai.UI.View.ButtonView leftArrow;

		public global::Kampai.UI.View.ButtonView rightArrow;

		public global::UnityEngine.UI.Text modalName;

		public global::UnityEngine.UI.Text minionsNeeded;

		public global::UnityEngine.UI.Text mignetteDescription;

		public global::Kampai.UI.View.ButtonView callMinionsButton;

		public global::UnityEngine.UI.Text rewardsString;

		public global::UnityEngine.UI.Text availableString;

		public global::UnityEngine.UI.Text minionsAvailable;

		public global::System.Collections.Generic.List<global::Kampai.UI.View.MignetteRuleViewObject> mignetteRulesList;

		private global::Kampai.Game.MignetteBuilding mignetteBuilding;

		internal global::Kampai.Main.ILocalizationService localizationService;

		internal global::Kampai.Game.IPlayerService playerService;

		public void Init(global::Kampai.Game.MignetteBuilding building, global::Kampai.Main.ILocalizationService localization, global::Kampai.Game.IPlayerService playerServ)
		{
			base.Init();
			localizationService = localization;
			playerService = playerServ;
			mignetteBuilding = building;
			SetUpView();
			Open();
		}

		private void organizeRulesPanel()
		{
			global::Kampai.Game.MignetteBuildingDefinition mignetteBuildingDefinition = mignetteBuilding.MignetteBuildingDefinition;
			int count = mignetteRulesList.Count;
			int count2 = mignetteBuildingDefinition.MignetteRules.Count;
			for (int i = 0; i < count; i++)
			{
				if (i < count2)
				{
					mignetteRulesList[i].gameObject.SetActive(true);
					mignetteRulesList[i].RenderRule(mignetteBuildingDefinition.MignetteRules[i]);
					mignetteRulesList[i].AmountLabel.text += localizationService.GetString("MignettePoints");
				}
				else
				{
					mignetteRulesList[i].gameObject.SetActive(false);
				}
			}
		}

		private int GetIdleMinionCount()
		{
			global::System.Collections.Generic.List<global::Kampai.Game.Minion> instancesByType = playerService.GetInstancesByType<global::Kampai.Game.Minion>();
			int num = 0;
			foreach (global::Kampai.Game.Minion item in instancesByType)
			{
				if (item.State == global::Kampai.Game.MinionState.Idle || item.State == global::Kampai.Game.MinionState.Selectable || item.State == global::Kampai.Game.MinionState.Selected || item.State == global::Kampai.Game.MinionState.Leisure)
				{
					num++;
				}
			}
			return num;
		}

		internal void SetArrowButtonsState(bool enable)
		{
			leftArrow.GetComponent<global::UnityEngine.UI.Button>().interactable = enable;
			rightArrow.GetComponent<global::UnityEngine.UI.Button>().interactable = enable;
		}

		internal void SetArrowButtonsVisibleAndActive(bool active)
		{
			leftArrow.gameObject.SetActive(active);
			rightArrow.gameObject.SetActive(active);
		}

		private void SetUpView()
		{
			global::Kampai.Game.MignetteBuildingDefinition mignetteBuildingDefinition = mignetteBuilding.MignetteBuildingDefinition;
			modalName.text = localizationService.GetString(mignetteBuildingDefinition.LocalizedKey);
			mignetteDescription.text = localizationService.GetString(mignetteBuildingDefinition.Description);
			int minionSlotsOwned = mignetteBuilding.GetMinionSlotsOwned();
			minionsNeeded.text = minionSlotsOwned.ToString();
			int idleMinionCount = GetIdleMinionCount();
			minionsAvailable.text = idleMinionCount.ToString();
			availableString.text = localizationService.GetString("MignetteMinionsAvailable");
			rewardsString.text = localizationService.GetString("MignetteRewards");
			organizeRulesPanel();
		}

		internal void RecreateModal(global::Kampai.Game.MignetteBuilding building)
		{
			mignetteBuilding = building;
			SetUpView();
		}
	}
}
