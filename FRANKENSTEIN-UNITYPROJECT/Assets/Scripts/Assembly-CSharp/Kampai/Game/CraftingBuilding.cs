namespace Kampai.Game
{
	public class CraftingBuilding : global::Kampai.Game.Building<global::Kampai.Game.CraftingBuildingDefinition>
	{
		[global::Newtonsoft.Json.JsonIgnore]
		public int LastRushedSlot { get; set; }

		public int Slots { get; set; }

		public global::System.Collections.Generic.IList<int> RecipeInQueue { get; set; }

		public global::System.Collections.Generic.IList<int> CompletedCrafts { get; set; }

		public global::System.Collections.Generic.IList<int> DynamicCrafts { get; set; }

		public int CraftingStartTime { get; set; }

		public CraftingBuilding(global::Kampai.Game.CraftingBuildingDefinition def)
			: base(def)
		{
			LastRushedSlot = -1;
			Slots = def.InitialSlots;
			RecipeInQueue = new global::System.Collections.Generic.List<int>();
			CompletedCrafts = new global::System.Collections.Generic.List<int>();
			DynamicCrafts = new global::System.Collections.Generic.List<int>();
		}

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "SLOTS":
				reader.Read();
				Slots = global::System.Convert.ToInt32(reader.Value);
				break;
			case "RECIPEINQUEUE":
				reader.Read();
				RecipeInQueue = global::Kampai.Util.ReaderUtil.PopulateListInt32(reader);
				break;
			case "COMPLETEDCRAFTS":
				reader.Read();
				CompletedCrafts = global::Kampai.Util.ReaderUtil.PopulateListInt32(reader);
				break;
			case "DYNAMICCRAFTS":
				reader.Read();
				DynamicCrafts = global::Kampai.Util.ReaderUtil.PopulateListInt32(reader);
				break;
			case "CRAFTINGSTARTTIME":
				reader.Read();
				CraftingStartTime = global::System.Convert.ToInt32(reader.Value);
				break;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
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
			writer.WritePropertyName("Slots");
			writer.WriteValue(Slots);
			if (RecipeInQueue != null)
			{
				writer.WritePropertyName("RecipeInQueue");
				writer.WriteStartArray();
				global::System.Collections.Generic.IEnumerator<int> enumerator = RecipeInQueue.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						int current = enumerator.Current;
						writer.WriteValue(current);
					}
				}
				finally
				{
					enumerator.Dispose();
				}
				writer.WriteEndArray();
			}
			if (CompletedCrafts != null)
			{
				writer.WritePropertyName("CompletedCrafts");
				writer.WriteStartArray();
				global::System.Collections.Generic.IEnumerator<int> enumerator2 = CompletedCrafts.GetEnumerator();
				try
				{
					while (enumerator2.MoveNext())
					{
						int current2 = enumerator2.Current;
						writer.WriteValue(current2);
					}
				}
				finally
				{
					enumerator2.Dispose();
				}
				writer.WriteEndArray();
			}
			if (DynamicCrafts != null)
			{
				writer.WritePropertyName("DynamicCrafts");
				writer.WriteStartArray();
				global::System.Collections.Generic.IEnumerator<int> enumerator3 = DynamicCrafts.GetEnumerator();
				try
				{
					while (enumerator3.MoveNext())
					{
						int current3 = enumerator3.Current;
						writer.WriteValue(current3);
					}
				}
				finally
				{
					enumerator3.Dispose();
				}
				writer.WriteEndArray();
			}
			writer.WritePropertyName("CraftingStartTime");
			writer.WriteValue(CraftingStartTime);
		}

		public int getNextIncrementalCost()
		{
			return Definition.SlotCost + (Slots - Definition.InitialSlots) * Definition.SlotIncrementalCost;
		}

		public override global::Kampai.Game.View.BuildingObject AddBuildingObject(global::UnityEngine.GameObject gameObject)
		{
			return gameObject.AddComponent<global::Kampai.Game.View.CraftableBuildingObject>();
		}
	}
}
