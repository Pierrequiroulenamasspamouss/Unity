namespace Kampai.Game
{
	public class MignetteBuilding : global::Kampai.Game.TaskableBuilding<global::Kampai.Game.MignetteBuildingDefinition>, global::Kampai.Game.Building, global::Kampai.Game.IBuildingWithCooldown, global::Kampai.Game.Instance, global::Kampai.Game.Locatable, global::Kampai.Util.IFastJSONDeserializable, global::Kampai.Util.IFastJSONSerializable, global::Kampai.Util.Identifiable
	{
		public int MignetteData;

		public global::System.Collections.Generic.IList<int> StartedMainCollectionIDs { get; set; }

		public global::System.Collections.Generic.IList<int> StartedRepeatableCollectionIDs { get; set; }

		[global::Newtonsoft.Json.JsonIgnore]
		public global::Kampai.Game.MignetteBuildingDefinition MignetteBuildingDefinition
		{
			get
			{
				return Definition;
			}
		}

		public MignetteBuilding(global::Kampai.Game.MignetteBuildingDefinition def)
			: base(def)
		{
			StartedMainCollectionIDs = new global::System.Collections.Generic.List<int>();
			StartedRepeatableCollectionIDs = new global::System.Collections.Generic.List<int>();
		}

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "STARTEDMAINCOLLECTIONIDS":
				reader.Read();
				StartedMainCollectionIDs = global::Kampai.Util.ReaderUtil.PopulateListInt32(reader);
				break;
			case "STARTEDREPEATABLECOLLECTIONIDS":
				reader.Read();
				StartedRepeatableCollectionIDs = global::Kampai.Util.ReaderUtil.PopulateListInt32(reader);
				break;
			case "MIGNETTEDATA":
				reader.Read();
				MignetteData = global::System.Convert.ToInt32(reader.Value);
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
			if (StartedMainCollectionIDs != null)
			{
				writer.WritePropertyName("StartedMainCollectionIDs");
				writer.WriteStartArray();
				global::System.Collections.Generic.IEnumerator<int> enumerator = StartedMainCollectionIDs.GetEnumerator();
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
			if (StartedRepeatableCollectionIDs != null)
			{
				writer.WritePropertyName("StartedRepeatableCollectionIDs");
				writer.WriteStartArray();
				global::System.Collections.Generic.IEnumerator<int> enumerator2 = StartedRepeatableCollectionIDs.GetEnumerator();
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
			writer.WritePropertyName("MignetteData");
			writer.WriteValue(MignetteData);
		}

		public override global::Kampai.Game.View.BuildingObject AddBuildingObject(global::UnityEngine.GameObject gameObject)
		{
			return gameObject.AddComponent<global::Kampai.Game.View.MignetteBuildingObject>();
		}

		public int GetCooldown()
		{
			return MignetteBuildingDefinition.CooldownInSeconds;
		}

		public string SelectMenuToLoad()
		{
			if (AreAllMinionSlotsFilled())
			{
				return "MignettePlayConfirmMenu";
			}
			if (State == global::Kampai.Game.BuildingState.Cooldown)
			{
				return "MignetteCooldownMenu";
			}
			return "MignetteCallMinionsMenu";
		}

		public override int GetTransactionID(global::Kampai.Game.IDefinitionService definitionService)
		{
			return 5001;
		}
	}
}
