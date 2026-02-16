namespace Kampai.UI.View
{
	public class ShowBridgeBuildingUICommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		[Inject]
		public global::Kampai.UI.View.IGUIService guiService { get; set; }

		[Inject]
		public int BridgeBuildingId { get; set; }

		[Inject]
		public global::Kampai.UI.View.PopupMessageSignal popupMessageSignal { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService localService { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Game.ILandExpansionConfigService landExpansionConfigService { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal sfxSignal { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		public override void Execute()
		{
			global::Kampai.Game.BridgeBuilding byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.BridgeBuilding>(BridgeBuildingId);
			uint quantity = playerService.GetQuantity(global::Kampai.Game.StaticItem.LEVEL_ID);
			if (byInstanceId.UnlockLevel > quantity)
			{
				ShowLockMessage(byInstanceId.Definition.AspirationalMessage, byInstanceId.UnlockLevel);
			}
			else
			{
				if (byInstanceId.BridgeId == 0)
				{
					return;
				}
				global::Kampai.Game.BridgeDefinition bridgeDefinition = definitionService.Get(byInstanceId.BridgeId) as global::Kampai.Game.BridgeDefinition;
				if (bridgeDefinition == null)
				{
					return;
				}
				global::Kampai.Game.LandExpansionConfig expansionConfig = landExpansionConfigService.GetExpansionConfig(bridgeDefinition.LandExpansionID);
				if (expansionConfig == null)
				{
					return;
				}
				bool flag = false;
				foreach (int adjacentExpansionId in expansionConfig.adjacentExpansionIds)
				{
					if (playerService.IsExpansionPurchased(adjacentExpansionId))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					ShowLockMessage("BridgeNotAvailable");
					return;
				}
				if (byInstanceId.State == global::Kampai.Game.BuildingState.Idle)
				{
					ShowLockMessage("MustPrestigeBobKey");
					return;
				}
				sfxSignal.Dispatch("Play_menu_popUp_01");
				global::Kampai.UI.View.IGUICommand iGUICommand = guiService.BuildCommand(global::Kampai.UI.View.GUIOperation.Load, "popup_Confirmation_Expansion");
				iGUICommand.skrimScreen = "BridgeSkrim";
				iGUICommand.darkSkrim = true;
				iGUICommand.Args.Add(BridgeBuildingId);
				iGUICommand.Args.Add(global::Kampai.UI.View.RushDialogView.RushDialogType.BRIDGE_QUEST);
				guiService.Execute(iGUICommand);
			}
		}

		private void ShowLockMessage(string key, int unlockLevel = -1)
		{
			string type = ((unlockLevel == -1) ? localService.GetString(key) : localService.GetString(key, unlockLevel));
			popupMessageSignal.Dispatch(type);
			sfxSignal.Dispatch("Play_action_locked_01");
		}
	}
}
