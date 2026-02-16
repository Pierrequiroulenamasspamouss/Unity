namespace Kampai.Game
{
	public class QuestStepDefinition
	{
		public global::Kampai.Game.QuestStepType Type { get; set; }

		public int ItemAmount { get; set; }

		public int ItemDefinitionID { get; set; }

		public int CostumeDefinitionID { get; set; }

		public bool ShowWayfinder { get; set; }
	}
}
