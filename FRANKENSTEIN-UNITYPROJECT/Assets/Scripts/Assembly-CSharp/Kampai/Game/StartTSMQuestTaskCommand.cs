namespace Kampai.Game
{
	public class StartTSMQuestTaskCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Game.Quest quest { get; set; }

		[Inject]
		public global::Kampai.Game.IQuestService questService { get; set; }

		[Inject]
		public global::Kampai.Game.PanAndOpenModalSignal panAndOpenSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.OpenStoreHighlightItemSignal openStoreSignal { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject(global::Kampai.UI.View.UIElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable uiContext { get; set; }

		public override void Execute()
		{
			if (quest.state == global::Kampai.Game.QuestState.Notstarted)
			{
				questService.GoToNextQuestState(quest);
			}
			if (quest.Steps.Count > 0 && quest.Steps[0].state == global::Kampai.Game.QuestStepState.Notstarted)
			{
				questService.GoToNextTaskState(quest, 0);
			}
			global::System.Collections.Generic.IList<global::Kampai.Game.Instance> list = null;
			int num = 0;
			switch (quest.GetActiveDefinition().QuestSteps[0].Type)
			{
			default:
				return;
			case global::Kampai.Game.QuestStepType.OrderBoard:
				list = playerService.GetInstancesByDefinition<global::Kampai.Game.BlackMarketBoardDefinition>();
				num = 3022;
				break;
			case global::Kampai.Game.QuestStepType.MinionTask:
				num = quest.GetActiveDefinition().QuestSteps[0].ItemDefinitionID;
				list = playerService.GetInstancesByDefinitionID(num);
				break;
			case global::Kampai.Game.QuestStepType.BridgeRepair:
				return;
			}
			if (list.Count > 0)
			{
				global::Kampai.Game.Building building = FindPlacedBuilding(list);
				if (building != null)
				{
					panAndOpenSignal.Dispatch(list[0].ID);
				}
				else
				{
					uiContext.injectionBinder.GetInstance<global::Kampai.UI.View.CloseAllOtherMenuSignal>().Dispatch(null);
				}
			}
			else if (num != 0)
			{
				openStoreSignal.Dispatch(num, true);
			}
		}

		private global::Kampai.Game.Building FindPlacedBuilding(global::System.Collections.Generic.IList<global::Kampai.Game.Instance> instances)
		{
			foreach (global::Kampai.Game.Instance instance in instances)
			{
				global::Kampai.Game.Building building = instance as global::Kampai.Game.Building;
				if (building != null && building.State != global::Kampai.Game.BuildingState.Inventory)
				{
					return building;
				}
			}
			return null;
		}
	}
}
