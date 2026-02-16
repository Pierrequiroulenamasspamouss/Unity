namespace Kampai.Game
{
	public class PopulateBuildingCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Game.PlaceBuildingSignal placeBuildingSignal { get; set; }

		[Inject]
		public global::Kampai.Game.DebugUpdateGridSignal DebugUpdateGridSignal { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		public override void Execute()
		{
			global::UnityEngine.Debug.Log("[PopulateBuildingCommand] ========== EXECUTE STARTED ==========");
			global::Kampai.Game.IPlayerService instance = base.injectionBinder.GetInstance<global::Kampai.Game.IPlayerService>();
			global::System.Collections.Generic.List<global::Kampai.Game.Building> buildings = new global::System.Collections.Generic.List<global::Kampai.Game.Building>(instance.GetInstancesByType<global::Kampai.Game.Building>());
			global::UnityEngine.Debug.Log("[PopulateBuildingCommand] Found " + buildings.Count + " buildings in player data");
			
			int placedCount = 0;
			foreach (global::Kampai.Game.Building item in buildings)
			{
				if (item.State != global::Kampai.Game.BuildingState.Inventory)
				{
					if (item.Location == null)
					{
						global::UnityEngine.Debug.LogWarning("[PopulateBuildingCommand] Building " + item.ID + " has null location, attempting patch...");
						TryPatchBuildingLocation(item);
					}
					global::UnityEngine.Debug.Log("[PopulateBuildingCommand] Placing building " + item.ID + " at position " + item.Location);
					placeBuildingSignal.Dispatch(item.ID, item.Location);
					placedCount++;
				}
			}
			global::UnityEngine.Debug.Log("[PopulateBuildingCommand] Placed " + placedCount + " buildings total");
			DebugUpdateGridSignal.Dispatch();
			global::UnityEngine.Debug.Log("[PopulateBuildingCommand] ========== EXECUTE COMPLETE ==========");
		}

		private void TryPatchBuildingLocation(global::Kampai.Game.Building building)
		{
			if (building == null)
			{
				return;
			}
			global::Kampai.Game.Definition definition = building.Definition;
			if (definition == null || building.Definition.Movable)
			{
				return;
			}
			string initialPlayer = definitionService.GetInitialPlayer();
			if (string.IsNullOrEmpty(initialPlayer))
			{
				return;
			}
			try
			{
				global::Kampai.Game.Player player = playerService.LoadPlayerData(initialPlayer);
				global::System.Collections.Generic.List<global::Kampai.Game.Instance> instancesByDefinitionID = player.GetInstancesByDefinitionID(definition.ID);
				if (instancesByDefinitionID.Count > 0 && instancesByDefinitionID[0] != null)
				{
					global::Kampai.Game.Locatable locatable = instancesByDefinitionID[0] as global::Kampai.Game.Locatable;
					if (locatable != null)
					{
						building.Location = locatable.Location;
					}
				}
			}
			catch (global::System.Exception)
			{
			}
		}
	}
}
