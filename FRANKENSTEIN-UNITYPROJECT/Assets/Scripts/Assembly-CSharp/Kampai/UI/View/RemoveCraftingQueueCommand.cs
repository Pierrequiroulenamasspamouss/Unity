namespace Kampai.UI.View
{
	public class RemoveCraftingQueueCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Util.Tuple<int, int> buildingIDItemIndex { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.UI.View.RefreshQueueSlotSignal refreshSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.CraftingUpdateReagentsSignal craftingUpdateReagentsSignal { get; set; }

		[Inject]
		public global::Kampai.Game.HarvestReadySignal harvestSignal { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject(global::Kampai.Game.GameElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable gameContext { get; set; }

		public override void Execute()
		{
			global::Kampai.Game.CraftingBuilding byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.CraftingBuilding>(buildingIDItemIndex.Item1);
			if (byInstanceId.RecipeInQueue.Count > 0)
			{
				int id = byInstanceId.RecipeInQueue[buildingIDItemIndex.Item2];
				global::Kampai.Game.IngredientsItemDefinition ingredientsItemDefinition = definitionService.Get<global::Kampai.Game.IngredientsItemDefinition>(id);
				byInstanceId.RecipeInQueue.RemoveAt(buildingIDItemIndex.Item2);
				refreshSignal.Dispatch(false);
				if (byInstanceId.LastRushedSlot == buildingIDItemIndex.Item2)
				{
					byInstanceId.LastRushedSlot = -1;
					global::Kampai.Game.Transaction.TransactionDefinition transactionDefinition = definitionService.Get<global::Kampai.Game.Transaction.TransactionDefinition>(ingredientsItemDefinition.TransactionId);
					foreach (global::Kampai.Util.QuantityItem output in transactionDefinition.Outputs)
					{
						playerService.CreateAndRunCustomTransaction(output.ID, (int)output.Quantity, global::Kampai.Game.TransactionTarget.NO_VISUAL);
					}
					craftingUpdateReagentsSignal.Dispatch();
				}
				else
				{
					byInstanceId.CompletedCrafts.Add(ingredientsItemDefinition.ID);
					harvestSignal.Dispatch(buildingIDItemIndex.Item1);
				}
			}
			HandleStateChange(byInstanceId);
		}

		private void HandleStateChange(global::Kampai.Game.CraftingBuilding building)
		{
			global::Kampai.Game.BuildingChangeStateSignal instance = gameContext.injectionBinder.GetInstance<global::Kampai.Game.BuildingChangeStateSignal>();
			if (building.CompletedCrafts.Count == 0)
			{
				if (building.RecipeInQueue.Count > 0)
				{
					instance.Dispatch(building.ID, global::Kampai.Game.BuildingState.Working);
				}
				else
				{
					instance.Dispatch(building.ID, global::Kampai.Game.BuildingState.Idle);
				}
			}
			else if (building.RecipeInQueue.Count > 0)
			{
				instance.Dispatch(building.ID, global::Kampai.Game.BuildingState.HarvestableAndWorking);
			}
			else
			{
				instance.Dispatch(building.ID, global::Kampai.Game.BuildingState.Harvestable);
			}
		}
	}
}
