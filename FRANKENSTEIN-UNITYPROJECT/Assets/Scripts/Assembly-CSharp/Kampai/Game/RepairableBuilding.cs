namespace Kampai.Game
{
	public interface RepairableBuilding : global::Kampai.Game.Building, global::Kampai.Game.Instance, global::Kampai.Game.Locatable, global::Kampai.Util.IFastJSONDeserializable, global::Kampai.Util.IFastJSONSerializable, global::Kampai.Util.Identifiable
	{
	}
	public abstract class RepairableBuilding<T> : global::Kampai.Game.Building<T>, global::Kampai.Game.Building, global::Kampai.Game.RepairableBuilding, global::Kampai.Game.Instance, global::Kampai.Game.Locatable, global::Kampai.Util.IFastJSONDeserializable, global::Kampai.Util.IFastJSONSerializable, global::Kampai.Util.Identifiable where T : global::Kampai.Game.RepairableBuildingDefinition
	{
		public RepairableBuilding(T definition)
			: base(definition)
		{
		}

		public override string GetPrefab()
		{
			if (State == global::Kampai.Game.BuildingState.Broken || State == global::Kampai.Game.BuildingState.Inaccessible)
			{
				T definition = Definition;
				if (definition.brokenPrefab != null)
				{
					T definition2 = Definition;
					return definition2.brokenPrefab;
				}
			}
			return base.GetPrefab();
		}

		public override bool IsBuildingRepaired()
		{
			if (State == global::Kampai.Game.BuildingState.Broken || State == global::Kampai.Game.BuildingState.Inaccessible)
			{
				T definition = Definition;
				if (definition.brokenPrefab != null)
				{
					return false;
				}
			}
			return true;
		}
	}
}
