namespace Kampai.Game
{
	public class SelectLandExpansionCommand : global::strange.extensions.command.impl.Command
	{
		private global::Kampai.Game.PurchasedLandExpansion purchasedLandExpansion;

		private global::Kampai.Game.LandExpansionDefinition definition;

		[Inject]
		public int buildingID { get; set; }

		[Inject]
		public global::Kampai.Game.ILandExpansionService landExpansionService { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService localService { get; set; }

		[Inject]
		public global::Kampai.Game.CameraAutoMoveSignal AutoMoveSignal { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		[Inject]
		public global::Kampai.Game.PurchaseLandExpansionSignal purchaseSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.PopupMessageSignal popupMessageSignal { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal playSFXSignal { get; set; }

		[Inject]
		public global::Kampai.Game.HighlightLandExpansionSignal highlightSignal { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal sfxSignal { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.UI.View.IGUIService guiService { get; set; }

		public override void Execute()
		{
			int expansionByItemID = landExpansionService.GetExpansionByItemID(buildingID);
			purchasedLandExpansion = playerService.GetByInstanceId<global::Kampai.Game.PurchasedLandExpansion>(354);
			definition = FindLandExpansion(expansionByItemID);
			if (ExpansionIsPurchaseable(expansionByItemID))
			{
				int minimumLevel = definition.MinimumLevel;
				if (minimumLevel > playerService.GetQuantity(global::Kampai.Game.StaticItem.LEVEL_ID))
				{
					Error(localService.GetString("LandLockedByLevel", minimumLevel));
					return;
				}
				global::UnityEngine.GameObject forSaleSign = landExpansionService.GetForSaleSign(expansionByItemID);
				global::UnityEngine.Vector3 type = forSaleSign.transform.position + global::Kampai.Util.GameConstants.CAMERA_OFFSET_LANDEXPANSION;
				highlightSignal.Dispatch(expansionByItemID, true);
				AutoMoveSignal.Dispatch(type, 0.8f, new global::Kampai.Game.CameraMovementSettings(global::Kampai.Game.CameraMovementSettings.Settings.None, null, null));
				ShowMenu(expansionByItemID);
			}
			else if (!HasPurchased(expansionByItemID))
			{
				Error(localService.GetString("LandLockedByOtherLand"));
			}
		}

		private void Error(string message)
		{
			sfxSignal.Dispatch("Play_action_locked_01");
			popupMessageSignal.Dispatch(message);
		}

		private bool HasPurchased(int expansionID)
		{
			return purchasedLandExpansion.HasPurchased(expansionID);
		}

		private bool ExpansionIsPurchaseable(int expansionID)
		{
			return purchasedLandExpansion.IsUnpurchasedAdjacentExpansion(expansionID);
		}

		private void ShowMenu(int expansionID)
		{
			playSFXSignal.Dispatch("Play_menu_popUp_01");
			global::Kampai.UI.View.IGUICommand iGUICommand = guiService.BuildCommand(global::Kampai.UI.View.GUIOperation.Load, "popup_Confirmation_Expansion");
			iGUICommand.skrimScreen = "LandExpansionSkrim";
			iGUICommand.darkSkrim = false;
			iGUICommand.Args.Add(expansionID);
			iGUICommand.Args.Add(global::Kampai.UI.View.RushDialogView.RushDialogType.LAND_EXPANSION);
			guiService.Execute(iGUICommand);
		}

		private global::Kampai.Game.LandExpansionDefinition FindLandExpansion(int expansionId)
		{
			global::System.Collections.Generic.List<global::Kampai.Game.LandExpansionDefinition> all = definitionService.GetAll<global::Kampai.Game.LandExpansionDefinition>();
			for (int i = 0; i < all.Count; i++)
			{
				if (all[i].ExpansionID == expansionId)
				{
					return all[i];
				}
			}
			logger.Fatal(global::Kampai.Util.FatalCode.DS_NO_SUCH_LAND_EXPANSION, expansionId);
			return null;
		}
	}
}
