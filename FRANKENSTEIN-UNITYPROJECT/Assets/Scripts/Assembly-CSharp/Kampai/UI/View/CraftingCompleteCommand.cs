namespace Kampai.UI.View
{
	public class CraftingCompleteCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public int buildingID { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.UI.View.RefreshQueueSlotSignal refreshSignal { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeEventService timeEventService { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeService timeService { get; set; }

		[Inject]
		public global::Kampai.UI.View.CraftingCompleteSignal craftingComplete { get; set; }

		[Inject]
		public global::Kampai.Game.HarvestReadySignal harvestSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.RemoveCraftingQueueSignal removeCraftingQueueSignal { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject(global::Kampai.Game.GameElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable gameContext { get; set; }

		public override void Execute()
		{
			global::Kampai.Game.CraftingBuilding byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.CraftingBuilding>(buildingID);
			if (byInstanceId.RecipeInQueue.Count > 0)
			{
				removeCraftingQueueSignal.Dispatch(new global::Kampai.Util.Tuple<int, int>(buildingID, 0));
				if (byInstanceId.RecipeInQueue.Count > 0)
				{
					byInstanceId.CraftingStartTime = timeService.GameTimeSeconds();
					global::Kampai.Game.IngredientsItemDefinition ingredientsItemDefinition = definitionService.Get<global::Kampai.Game.IngredientsItemDefinition>(byInstanceId.RecipeInQueue[0]);
					timeEventService.AddEvent(byInstanceId.ID, global::System.Convert.ToInt32(timeService.GameTimeSeconds()), (int)ingredientsItemDefinition.TimeToHarvest, craftingComplete);
				}
			}
		}
	}
}
