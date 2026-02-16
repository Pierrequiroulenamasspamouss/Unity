namespace Kampai.Game.View
{
	public class CameraAutoMoveToMignetteCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public int definitionId { get; set; }

		[Inject]
		public global::Kampai.Game.PanInstructions panInstructions { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Game.CameraAutoMoveToBuildingSignal moveToBuildingSignal { get; set; }

		[Inject]
		public global::Kampai.Game.CameraAutoMoveToBuildingDefSignal moveToBuildingDefSignal { get; set; }

		[Inject]
		public global::Kampai.Game.PanAndOpenModalSignal panAndOpenModalSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.PopupMessageSignal popupMessageSignal { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal globalSFXSignal { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService localizationService { get; set; }

		public override void Execute()
		{
			global::Kampai.Game.Building firstInstanceByDefinitionId = playerService.GetFirstInstanceByDefinitionId<global::Kampai.Game.Building>(definitionId);
			if (firstInstanceByDefinitionId != null)
			{
				panAndOpenModalSignal.Dispatch(firstInstanceByDefinitionId.ID);
				return;
			}
			global::System.Collections.Generic.IList<global::Kampai.Game.AspirationalBuildingDefinition> all = definitionService.GetAll<global::Kampai.Game.AspirationalBuildingDefinition>();
			int i = 0;
			for (int count = all.Count; i < count; i++)
			{
				global::Kampai.Game.AspirationalBuildingDefinition aspirationalBuildingDefinition = all[i];
				if (aspirationalBuildingDefinition.BuildingDefinitionID != definitionId)
				{
					continue;
				}
				global::Kampai.Game.BuildingDefinition buildingDefinition = definitionService.Get<global::Kampai.Game.BuildingDefinition>(aspirationalBuildingDefinition.BuildingDefinitionID);
				if (buildingDefinition != null)
				{
					global::Kampai.Game.MignetteBuildingDefinition mignetteBuildingDefinition = definitionService.Get<global::Kampai.Game.MignetteBuildingDefinition>(aspirationalBuildingDefinition.BuildingDefinitionID);
					if (mignetteBuildingDefinition != null)
					{
						string aspirationalMessage = mignetteBuildingDefinition.AspirationalMessage;
						global::Kampai.Game.Location location = aspirationalBuildingDefinition.Location;
						moveToBuildingDefSignal.Dispatch(buildingDefinition, location, panInstructions);
						globalSFXSignal.Dispatch("Play_action_locked_01");
						popupMessageSignal.Dispatch(localizationService.GetString(aspirationalMessage));
						return;
					}
				}
			}
			logger.Error("CameraAutoMoveToMignetteCommand: Failed to find mignette with definition ID {0}", definitionId);
		}
	}
}
