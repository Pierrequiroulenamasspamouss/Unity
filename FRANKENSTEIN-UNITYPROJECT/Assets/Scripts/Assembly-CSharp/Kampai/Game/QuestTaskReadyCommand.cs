namespace Kampai.Game
{
	public class QuestTaskReadyCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Game.UpdateQuestWorldIconsSignal updateWorldQuestIcon { get; set; }

		[Inject]
		public global::Kampai.Game.Quest quest { get; set; }

		public override void Execute()
		{
			updateWorldQuestIcon.Dispatch(quest);
		}
	}
}
