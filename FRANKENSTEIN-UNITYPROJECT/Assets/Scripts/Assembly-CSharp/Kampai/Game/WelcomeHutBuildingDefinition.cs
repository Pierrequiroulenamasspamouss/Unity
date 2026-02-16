namespace Kampai.Game
{
	public class WelcomeHutBuildingDefinition : global::Kampai.Game.RepairableBuildingDefinition
	{
		public override global::Kampai.Game.Building BuildBuilding()
		{
			return new global::Kampai.Game.WelcomeHutBuilding(this);
		}
	}
}
