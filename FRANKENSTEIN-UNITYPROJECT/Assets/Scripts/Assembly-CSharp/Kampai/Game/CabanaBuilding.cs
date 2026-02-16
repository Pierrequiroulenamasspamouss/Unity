namespace Kampai.Game
{
	public class CabanaBuilding : global::Kampai.Game.Building<global::Kampai.Game.CabanaBuildingDefinition>
	{
		private bool occupied;

		[global::Newtonsoft.Json.JsonIgnore]
		public bool Occupied
		{
			get
			{
				return occupied;
			}
			set
			{
				occupied = value;
			}
		}

		public global::Kampai.Game.Quest Quest { get; set; }

		public CabanaBuilding(global::Kampai.Game.CabanaBuildingDefinition def)
			: base(def)
		{
		}

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "QUEST":
				reader.Read();
				Quest = (global::Kampai.Game.Quest)converters.instanceConverter.ReadJson(reader, converters);
				return true;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
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
			if (Quest != null)
			{
				writer.WritePropertyName("Quest");
				Quest.Serialize(writer);
			}
		}

		public override global::Kampai.Game.View.BuildingObject AddBuildingObject(global::UnityEngine.GameObject gameObject)
		{
			global::Kampai.Game.View.CabanaBuildingObject cabanaBuildingObject = gameObject.AddComponent<global::Kampai.Game.View.CabanaBuildingObject>();
			if (State == global::Kampai.Game.BuildingState.Broken || State == global::Kampai.Game.BuildingState.Inaccessible)
			{
				gameObject.SetLayerRecursively(9);
				cabanaBuildingObject.SetExternalScene(Definition.DisheveledScene);
			}
			return cabanaBuildingObject;
		}

		public override string GetPrefab()
		{
			switch (State)
			{
			case global::Kampai.Game.BuildingState.Broken:
			case global::Kampai.Game.BuildingState.Inaccessible:
				return string.Empty;
			case global::Kampai.Game.BuildingState.Working:
				return Definition.Prefab;
			case global::Kampai.Game.BuildingState.Idle:
				return Definition.InactivePrefab;
			default:
				return Definition.Prefab;
			}
		}

		public override bool IsBuildingRepaired()
		{
			return State != global::Kampai.Game.BuildingState.Broken && State != global::Kampai.Game.BuildingState.Inaccessible;
		}
	}
}
