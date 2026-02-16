namespace Kampai.Game
{
	public class QuestStepRushCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Game.Quest quest { get; set; }

		[Inject]
		public int step { get; set; }

		[Inject]
		public global::Kampai.Game.IQuestService questService { get; set; }

		[Inject]
		public global::Kampai.UI.View.SetGrindCurrencySignal setGrindCurrencySignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.SetPremiumCurrencySignal setPremiumCurrencySignal { get; set; }

		public override void Execute()
		{
			setGrindCurrencySignal.Dispatch();
			setPremiumCurrencySignal.Dispatch();
			questService.RushQuestStep(quest.GetActiveDefinition().ID, step);
		}
	}
}
