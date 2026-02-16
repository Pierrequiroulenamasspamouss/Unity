namespace Kampai.Game
{
	public abstract class Building<T> : global::Kampai.Game.Instance<T>, global::Kampai.Game.Building, global::Kampai.Game.Instance, global::Kampai.Game.Locatable, global::Kampai.Util.IFastJSONDeserializable, global::Kampai.Util.IFastJSONSerializable, global::Kampai.Util.Identifiable where T : global::Kampai.Game.BuildingDefinition
	{
		global::Kampai.Game.BuildingDefinition global::Kampai.Game.Building.Definition
		{
			get
			{
				return Definition;
			}
		}

		global::Kampai.Game.Definition global::Kampai.Game.Instance.Definition
		{
			get
			{
				return Definition;
			}
		}

		public int ID { get; set; }

		[global::Kampai.Util.FastDeserializerIgnore]
		public T Definition { get; protected set; }

		public global::Kampai.Game.BuildingState State { get; set; }

		public global::Kampai.Game.Location Location { get; set; }

		public bool IsFootprintable
		{
			get
			{
				return State != global::Kampai.Game.BuildingState.Disabled;
			}
		}

		public int StateStartTime { get; set; }

		public Building(T definition)
		{
			Definition = definition;
		}

		public virtual object Deserialize(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters = null)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None)
			{
				reader.Read();
			}
			global::Kampai.Util.ReaderUtil.EnsureToken(global::Newtonsoft.Json.JsonToken.StartObject, reader);
			while (reader.Read())
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.PropertyName:
				{
					string propertyName = ((string)reader.Value).ToUpper();
					if (!DeserializeProperty(propertyName, reader, converters))
					{
						reader.Skip();
					}
					break;
				}
				case global::Newtonsoft.Json.JsonToken.EndObject:
					return this;
				default:
					throw new global::Newtonsoft.Json.JsonSerializationException(string.Format("Unexpected token when deserializing object: {0}. {1}", reader.TokenType, global::Kampai.Util.ReaderUtil.GetPositionInSource(reader)));
				case global::Newtonsoft.Json.JsonToken.Comment:
					break;
				}
			}
			throw new global::Newtonsoft.Json.JsonSerializationException("Unexpected end when deserializing object.");
		}

		protected virtual bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "ID":
				reader.Read();
				ID = global::System.Convert.ToInt32(reader.Value);
				break;
			case "STATE":
				reader.Read();
				State = global::Kampai.Util.ReaderUtil.ReadEnum<global::Kampai.Game.BuildingState>(reader);
				break;
			case "LOCATION":
				reader.Read();
				Location = global::Kampai.Util.ReaderUtil.ReadLocation(reader, converters);
				break;
			case "STATESTARTTIME":
				reader.Read();
				StateStartTime = global::System.Convert.ToInt32(reader.Value);
				break;
			default:
				return false;
			}
			return true;
		}

		public virtual void Serialize(global::Newtonsoft.Json.JsonWriter writer)
		{
			writer.WriteStartObject();
			SerializeProperties(writer);
			writer.WriteEndObject();
		}

		protected virtual void SerializeProperties(global::Newtonsoft.Json.JsonWriter writer)
		{
			global::Kampai.Game.FastInstanceSerializationHelper.SerializeInstanceData(writer, this);
			writer.WritePropertyName("State");
			writer.WriteValue((int)State);
			if (Location != null)
			{
				writer.WritePropertyName("Location");
				writer.WriteStartObject();
				writer.WritePropertyName("x");
				writer.WriteValue(Location.x);
				writer.WritePropertyName("y");
				writer.WriteValue(Location.y);
				writer.WriteEndObject();
			}
			writer.WritePropertyName("StateStartTime");
			writer.WriteValue(StateStartTime);
		}

		public virtual bool HasDetailMenuToShow()
		{
			T definition = Definition;
			return !string.IsNullOrEmpty(definition.MenuPrefab);
		}

		public abstract global::Kampai.Game.View.BuildingObject AddBuildingObject(global::UnityEngine.GameObject gameObject);

		public void SetState(global::Kampai.Game.BuildingState buildingState)
		{
			State = buildingState;
		}

		public virtual string GetPrefab()
		{
			T definition = Definition;
			return definition.Prefab;
		}

		public virtual bool IsBuildingRepaired()
		{
			return true;
		}
	}
	public interface Building : global::Kampai.Game.Instance, global::Kampai.Game.Locatable, global::Kampai.Util.IFastJSONDeserializable, global::Kampai.Util.IFastJSONSerializable, global::Kampai.Util.Identifiable
	{
		global::Kampai.Game.BuildingState State { get; }

		new global::Kampai.Game.BuildingDefinition Definition { get; }

		bool IsFootprintable { get; }

		int StateStartTime { get; set; }

		bool HasDetailMenuToShow();

		global::Kampai.Game.View.BuildingObject AddBuildingObject(global::UnityEngine.GameObject gameObject);

		void SetState(global::Kampai.Game.BuildingState buildingState);

		string GetPrefab();

		bool IsBuildingRepaired();
	}
}
