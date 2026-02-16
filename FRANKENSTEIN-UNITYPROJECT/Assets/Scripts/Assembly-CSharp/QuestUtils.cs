public static class QuestUtils
{
	public static string SetupTaskDescText(global::Kampai.Game.IDefinitionService definitionService, global::Kampai.Main.ILocalizationService localService, global::Kampai.Game.QuestStepDefinition stepDefinition)
	{
		string result = string.Empty;
		global::Kampai.Game.BuildingDefinition buildingDefinition = null;
		global::Kampai.Game.ItemDefinition itemDefinition = null;
		string text = null;
		switch (stepDefinition.Type)
		{
		case global::Kampai.Game.QuestStepType.Mignette:
			buildingDefinition = definitionService.Get<global::Kampai.Game.BuildingDefinition>(stepDefinition.ItemDefinitionID);
			text = localService.GetString(buildingDefinition.LocalizedKey);
			result = localService.GetString("mignetteTaskDescWrap", text, localService.GetString("mignetteTaskDesc*", stepDefinition.ItemAmount));
			break;
		case global::Kampai.Game.QuestStepType.OrderBoard:
			buildingDefinition = definitionService.Get<global::Kampai.Game.BuildingDefinition>(3022);
			text = localService.GetString(buildingDefinition.LocalizedKey);
			result = localService.GetString("orderboardTaskDesc", stepDefinition.ItemAmount);
			break;
		case global::Kampai.Game.QuestStepType.Construction:
			buildingDefinition = definitionService.Get<global::Kampai.Game.BuildingDefinition>(stepDefinition.ItemDefinitionID);
			text = localService.GetString(buildingDefinition.LocalizedKey + "*", stepDefinition.ItemAmount);
			result = localService.GetString("constructTaskDesc", text);
			break;
		case global::Kampai.Game.QuestStepType.Delivery:
			itemDefinition = definitionService.Get<global::Kampai.Game.ItemDefinition>(stepDefinition.ItemDefinitionID);
			text = localService.GetString(itemDefinition.LocalizedKey + "*", stepDefinition.ItemAmount);
			result = localService.GetString("deliverTaskDesc", text);
			break;
		case global::Kampai.Game.QuestStepType.Harvest:
			itemDefinition = definitionService.Get<global::Kampai.Game.ItemDefinition>(stepDefinition.ItemDefinitionID);
			text = localService.GetString(itemDefinition.LocalizedKey + "*", stepDefinition.ItemAmount);
			result = localService.GetString("harvestTaskDesc", text);
			break;
		case global::Kampai.Game.QuestStepType.BridgeRepair:
			result = localService.GetString("repairBridgeDesc");
			break;
		case global::Kampai.Game.QuestStepType.StageRepair:
			result = localService.GetString("repairStageDesc");
			break;
		case global::Kampai.Game.QuestStepType.CabanaRepair:
			result = localService.GetString(string.Format("{0}{1}", "CabanaRepair", stepDefinition.ItemAmount));
			break;
		case global::Kampai.Game.QuestStepType.WelcomeHutRepair:
			result = localService.GetString("repairWelcomeDesc");
			break;
		case global::Kampai.Game.QuestStepType.FountainRepair:
			result = localService.GetString("repairFountainDesc");
			break;
		}
		return result;
	}

	public static string SetupStepAction(global::Kampai.Main.ILocalizationService localService, global::Kampai.Game.QuestStepDefinition stepDefinition)
	{
		string result = string.Empty;
		switch (stepDefinition.Type)
		{
		case global::Kampai.Game.QuestStepType.BridgeRepair:
		case global::Kampai.Game.QuestStepType.CabanaRepair:
		case global::Kampai.Game.QuestStepType.WelcomeHutRepair:
		case global::Kampai.Game.QuestStepType.FountainRepair:
			result = localService.GetString("repairAction");
			break;
		case global::Kampai.Game.QuestStepType.StageRepair:
			result = localService.GetString("buildAction");
			break;
		case global::Kampai.Game.QuestStepType.Construction:
			result = localService.GetString("constructAction");
			break;
		case global::Kampai.Game.QuestStepType.Mignette:
			result = localService.GetString("mignetteAction");
			break;
		case global::Kampai.Game.QuestStepType.Delivery:
			result = localService.GetString("deliveryAction");
			break;
		case global::Kampai.Game.QuestStepType.OrderBoard:
			result = localService.GetString("orderBoardAction");
			break;
		case global::Kampai.Game.QuestStepType.Harvest:
			result = localService.GetString("haveAction");
			break;
		}
		return result;
	}
}
