namespace Kampai.Game
{
	public class UpdateTSMQuestTaskCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Game.Quest Quest { get; set; }

		[Inject]
		public bool Completed { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger Logger { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService PlayerService { get; set; }

		[Inject]
		public global::Kampai.Game.HideTSMCharacterSignal HideTSMCharacterSignal { get; set; }

		[Inject]
		public global::Kampai.Game.RemoveQuestWorldIconSignal RemoveQuestWorldIconSignal { get; set; }

		[Inject]
		public global::Kampai.Game.CreateTSMQuestTaskSignal CreateTSMQuestTaskSignal { get; set; }

		[Inject]
		public global::Kampai.Game.IQuestService QuestService { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeEventService TimeEventService { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeService TimeService { get; set; }

		[Inject]
		public global::Kampai.Common.ITelemetryService TelemetryService { get; set; }

		public override void Execute()
		{
			if (Quest == null)
			{
				Logger.Error("Quest does not exist on traveling sales minion.");
				return;
			}
			global::Kampai.Game.TSMCharacter firstInstanceByDefinitionId = PlayerService.GetFirstInstanceByDefinitionId<global::Kampai.Game.TSMCharacter>(70008);
			if (firstInstanceByDefinitionId == null)
			{
				Logger.Error("Failed to Cancel QuestId {0} because there isn't a traveling sales minion found", Quest.ID);
				return;
			}
			HideTSMCharacterSignal.Dispatch(Completed);
			RemoveQuestWorldIconSignal.Dispatch(Quest);
			firstInstanceByDefinitionId.PreviousTaskUTCTime = TimeService.GameTimeSeconds();
			QuestService.RemoveQuest(Quest);
			TimeEventService.AddEvent(firstInstanceByDefinitionId.ID, firstInstanceByDefinitionId.PreviousTaskUTCTime, firstInstanceByDefinitionId.Definition.CooldownInSeconds, CreateTSMQuestTaskSignal);
			if (Quest.dynamicDefinition == null)
			{
				Logger.Error("Quest dynamic definition does not exist on traveling sales minion.");
				return;
			}
			string achievementName = new global::System.Text.StringBuilder().Append("ProceduralQuest").Append(Quest.dynamicDefinition.ID).ToString();
			TelemetryService.Send_Telemetry_EVT_GP_ACHIEVEMENTS_CHECKPOINTS_EAL_ProceduralQuest(achievementName, (!Completed) ? global::Kampai.Common.Service.Telemetry.ProceduralQuestEndState.Dismissed : global::Kampai.Common.Service.Telemetry.ProceduralQuestEndState.Completed);
		}
	}
}
