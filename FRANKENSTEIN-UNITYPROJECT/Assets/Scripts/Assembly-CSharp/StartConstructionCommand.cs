public class StartConstructionCommand : global::strange.extensions.command.impl.Command
{
	[Inject]
	public global::UnityEngine.GameObject buildingObj { get; set; }

	[Inject]
	public int buildingId { get; set; }

	[Inject]
	public bool restoreTimer { get; set; }

	[Inject]
	public global::Kampai.Game.ConstructionCompleteSignal constructionCompleteSignal { get; set; }

	[Inject]
	public global::Kampai.Game.BuildingChangeStateSignal buildingChangeStateSignal { get; set; }

	[Inject]
	public global::Kampai.UI.View.DisplayWorldProgressSignal worldProgressSignal { get; set; }

	[Inject]
	public global::Kampai.Game.ITimeEventService timeEventService { get; set; }

	[Inject]
	public global::Kampai.Game.IPlayerService playerService { get; set; }

	[Inject]
	public global::Kampai.Game.ITimeService timeService { get; set; }

	[Inject]
	public global::Kampai.Game.IQuestService questService { get; set; }

	public override void Execute()
	{
		global::Kampai.Game.Building byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.Building>(buildingId);
		if (byInstanceId.Definition.ConstructionTime <= 0)
		{
			constructionCompleteSignal.Dispatch(buildingId);
			return;
		}
		int startTime = ((!restoreTimer) ? timeService.GameTimeSeconds() : byInstanceId.StateStartTime);
		global::Kampai.UI.View.ProgressBarSettings progressBarSettings = new global::Kampai.UI.View.ProgressBarSettings(buildingId, constructionCompleteSignal, startTime, byInstanceId.Definition.ConstructionTime);
		worldProgressSignal.Dispatch(progressBarSettings);
		timeEventService.AddEvent(buildingId, progressBarSettings.StartTime, progressBarSettings.Duration, constructionCompleteSignal);
		if (byInstanceId.State != global::Kampai.Game.BuildingState.Construction)
		{
			buildingChangeStateSignal.Dispatch(buildingId, global::Kampai.Game.BuildingState.Construction);
		}
		questService.UpdateConstructionTask();
	}
}
