namespace Kampai.Game
{
	public class SpecialBuildingDefinition : global::Kampai.Game.BuildingDefinition
	{
		public override global::Kampai.Game.Building BuildBuilding()
		{
			return new global::Kampai.Game.SpecialBuilding(this);
		}
	}
}
