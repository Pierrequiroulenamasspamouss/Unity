namespace Kampai.Game.View
{
	public class RestoreCraftingBuildingsCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Game.CraftingBuilding building { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeService timeService { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		[Inject]
		public global::Kampai.Game.HarvestReadySignal harvestSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.CraftingCompleteSignal craftingCompleteSignal { get; set; }

		[Inject]
		public global::Kampai.Game.BuildingChangeStateSignal buildingChangeStateSignal { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeEventService timeEventService { get; set; }

		public override void Execute()
		{
			RestoreCraftingBuilding(building);
		}

		private void RestoreCraftingBuilding(global::Kampai.Game.CraftingBuilding craftingBuilding)
		{
			if (craftingBuilding.State != global::Kampai.Game.BuildingState.Construction && craftingBuilding.State != global::Kampai.Game.BuildingState.Inactive)
			{
				timeEventService.RemoveEvent(craftingBuilding.ID);
			}
			global::System.Collections.Generic.IList<int> list = new global::System.Collections.Generic.List<int>();
			int num = craftingBuilding.CraftingStartTime;
			global::System.Collections.Generic.IList<int> recipeInQueue = craftingBuilding.RecipeInQueue;
			foreach (int item in recipeInQueue)
			{
				global::Kampai.Game.IngredientsItemDefinition ingredientsItemDefinition = definitionService.Get<global::Kampai.Game.IngredientsItemDefinition>(item);
				if (num + ingredientsItemDefinition.TimeToHarvest <= timeService.GameTimeSeconds())
				{
					list.Add(ingredientsItemDefinition.ID);
					global::Kampai.Game.DynamicIngredientsDefinition definition;
					if (definitionService.TryGet<global::Kampai.Game.DynamicIngredientsDefinition>(item, out definition))
					{
						craftingBuilding.DynamicCrafts.Add(definition.ID);
					}
					craftingBuilding.CompletedCrafts.Add(ingredientsItemDefinition.ID);
					num += global::System.Convert.ToInt32(ingredientsItemDefinition.TimeToHarvest);
					continue;
				}
				timeEventService.AddEvent(craftingBuilding.ID, num, (int)ingredientsItemDefinition.TimeToHarvest, craftingCompleteSignal);
				break;
			}
			craftingBuilding.CraftingStartTime = num;
			foreach (int item2 in list)
			{
				craftingBuilding.RecipeInQueue.Remove(item2);
			}
			SetState(craftingBuilding);
		}

		private void SetState(global::Kampai.Game.CraftingBuilding craftingBuilding)
		{
			global::Kampai.Game.BuildingState newState = global::Kampai.Game.BuildingState.Inactive;
			if (craftingBuilding.CompletedCrafts.Count > 0)
			{
				harvestSignal.Dispatch(craftingBuilding.ID);
				if (craftingBuilding.RecipeInQueue.Count > 0)
				{
					newState = global::Kampai.Game.BuildingState.HarvestableAndWorking;
				}
				else
				{
					newState = global::Kampai.Game.BuildingState.Harvestable;
				}
			}
			else if (craftingBuilding.RecipeInQueue.Count > 0)
			{
				newState = global::Kampai.Game.BuildingState.Working;
			}
			if (newState != global::Kampai.Game.BuildingState.Inactive)
			{
				routineRunner.StartCoroutine(WaitAFrame(delegate
				{
					buildingChangeStateSignal.Dispatch(craftingBuilding.ID, newState);
				}));
			}
		}

		private global::System.Collections.IEnumerator WaitAFrame(global::System.Action a)
		{
			yield return null;
			a();
		}
	}
}
