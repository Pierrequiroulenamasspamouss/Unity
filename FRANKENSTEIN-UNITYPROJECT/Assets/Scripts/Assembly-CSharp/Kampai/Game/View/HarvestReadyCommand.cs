namespace Kampai.Game.View
{
	public class HarvestReadyCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public int buildingID { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Game.IQuestService questService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject(global::Kampai.Main.MainElement.UI_WORLDCANVAS)]
		public global::UnityEngine.GameObject worldCanvas { get; set; }

		[Inject]
		public global::Kampai.UI.View.CreateResourceIconSignal createResourceIconSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.UpdateResourceIconCountSignal updateResourceIconCountSignal { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		public override void Execute()
		{
			global::Kampai.Game.Building byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.Building>(buildingID);
			global::Kampai.Game.TaskableBuilding taskableBuilding = byInstanceId as global::Kampai.Game.TaskableBuilding;
			global::Kampai.Game.CraftingBuilding craftingBuilding = byInstanceId as global::Kampai.Game.CraftingBuilding;
			if (taskableBuilding != null)
			{
				HarvestTaskableBuilding(taskableBuilding);
			}
			else if (craftingBuilding != null)
			{
				HarvestCraftingBuilding(craftingBuilding);
			}
			else
			{
				logger.Fatal(global::Kampai.Util.FatalCode.TE_NULL_ARG, "A non-producing building just finished its task...");
			}
		}

		private void HarvestTaskableBuilding(global::Kampai.Game.TaskableBuilding taskableBuilding)
		{
			global::Kampai.Game.TaskableBuildingDefinition definition = taskableBuilding.Definition;
			questService.HarvestReady(definition.ID);
			int transactionID = taskableBuilding.GetTransactionID(definitionService);
			int harvestItemDefinitionIdFromTransactionId = definitionService.GetHarvestItemDefinitionIdFromTransactionId(transactionID);
			questService.UpdateHarvestTask(harvestItemDefinitionIdFromTransactionId, global::Kampai.Game.QuestTaskTransition.Harvestable);
			int availableHarvest = taskableBuilding.GetAvailableHarvest();
			routineRunner.StartCoroutine(CreateResourceIcon(taskableBuilding.ID, harvestItemDefinitionIdFromTransactionId, availableHarvest));
		}

		private void HarvestCraftingBuilding(global::Kampai.Game.CraftingBuilding craftingBuilding)
		{
			int item = craftingBuilding.CompletedCrafts[craftingBuilding.CompletedCrafts.Count - 1];
			questService.UpdateHarvestTask(item, global::Kampai.Game.QuestTaskTransition.Harvestable);
			for (int i = 0; i < craftingBuilding.CompletedCrafts.Count; i++)
			{
				SetupCraftingIcon(craftingBuilding, i);
			}
		}

		private void SetupCraftingIcon(global::Kampai.Game.CraftingBuilding craftingBuilding, int itemIndex)
		{
			int num = craftingBuilding.CompletedCrafts[itemIndex];
			int num2 = 0;
			foreach (int completedCraft in craftingBuilding.CompletedCrafts)
			{
				if (completedCraft == num)
				{
					num2++;
				}
			}
			routineRunner.StartCoroutine(CreateResourceIcon(buildingID, num, num2));
		}

		private global::System.Collections.IEnumerator CreateResourceIcon(int buildingId, int itemDefId, int count)
		{
			yield return null;
			createResourceIconSignal.Dispatch(new global::Kampai.UI.View.ResourceIconSettings(buildingId, itemDefId, count));
		}
	}
}
