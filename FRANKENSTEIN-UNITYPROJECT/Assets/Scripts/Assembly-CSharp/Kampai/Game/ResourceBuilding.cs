namespace Kampai.Game
{
	public class ResourceBuilding : global::Kampai.Game.TaskableBuilding<global::Kampai.Game.ResourceBuildingDefinition>
	{
		public int AvailableHarvest { get; set; }

		public int BuildingNumber { get; set; }

		public ResourceBuilding(global::Kampai.Game.ResourceBuildingDefinition def)
			: base(def)
		{
		}

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "BUILDINGNUMBER":
				reader.Read();
				BuildingNumber = global::System.Convert.ToInt32(reader.Value);
				break;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			case "AVAILABLEHARVEST":
				reader.Read();
				AvailableHarvest = global::System.Convert.ToInt32(reader.Value);
				break;
			}
			return true;
		}

		public override void Serialize(global::Newtonsoft.Json.JsonWriter writer)
		{
			writer.WriteStartObject();
			SerializeProperties(writer);
			writer.WriteEndObject();
		}

		protected override void SerializeProperties(global::Newtonsoft.Json.JsonWriter writer)
		{
			base.SerializeProperties(writer);
			writer.WritePropertyName("AvailableHarvest");
			writer.WriteValue(AvailableHarvest);
			writer.WritePropertyName("BuildingNumber");
			writer.WriteValue(BuildingNumber);
		}

		public int GetMaxSlotCount()
		{
			return Definition.SlotUnlocks[BuildingNumber - 1].SlotUnlockLevels.Count;
		}

		public int GetSlotCostByIndex(int i)
		{
			return Definition.SlotUnlocks[BuildingNumber - 1].SlotUnlockCosts[i];
		}

		public int GetSlotUnlockLevelByIndex(int i)
		{
			return Definition.SlotUnlocks[BuildingNumber - 1].SlotUnlockLevels[i];
		}

		public void IncrementMinionSlotsOwned()
		{
			base.MinionSlotsOwned++;
		}

		public override global::Kampai.Game.View.BuildingObject AddBuildingObject(global::UnityEngine.GameObject gameObject)
		{
			return gameObject.AddComponent<global::Kampai.Game.View.GaggableBuildingObject>();
		}

		public override int GetTransactionID(global::Kampai.Game.IDefinitionService definitionService)
		{
			int itemId = Definition.ItemId;
			global::Kampai.Game.IngredientsItemDefinition ingredientsItemDefinition = definitionService.Get<global::Kampai.Game.IngredientsItemDefinition>(itemId);
			return ingredientsItemDefinition.TransactionId;
		}

		public void PrepareForHarvest(int utcTime)
		{
			ReconcileMinionStoppedTasking(utcTime);
			AvailableHarvest++;
		}

		public override int GetAvailableHarvest()
		{
			return AvailableHarvest;
		}
	}
}
