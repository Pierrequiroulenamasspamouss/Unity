namespace Kampai.Game
{
	public class Minion : global::Kampai.Game.Character<global::Kampai.Game.MinionDefinition>, global::Kampai.Game.Character, global::Kampai.Game.Instance, global::Kampai.Util.IFastJSONDeserializable, global::Kampai.Util.IFastJSONSerializable, global::Kampai.Util.Identifiable, global::Kampai.Util.Prestigable, global::Kampai.Util.Taskable
	{
		global::Kampai.Game.Definition global::Kampai.Game.Instance.Definition
		{
			get
			{
				return Definition;
			}
		}

		public int ID { get; set; }

		public int BuildingID { get; set; }

		public int CostumeID { get; set; }

		public string Name { get; set; }

		public global::Kampai.Game.MinionState State { get; set; }

		public int TaskDuration { get; set; }

		public int UTCTaskStartTime { get; set; }

		public bool AlreadyRushed { get; set; }

		public global::Kampai.Game.Prestige PrestigeCharacter { get; set; }

		public global::Kampai.Game.MinionDefinition Definition { get; protected set; }

		[global::Newtonsoft.Json.JsonIgnore]
		public bool Partying { get; set; }

		[global::Newtonsoft.Json.JsonIgnore]
		public int LastLeisureTime { get; set; }

		[global::Newtonsoft.Json.JsonIgnore]
		public bool IsInIncidental { get; set; }

		public Minion(global::Kampai.Game.MinionDefinition def)
		{
			Definition = def;
			Partying = true;
			CostumeID = 99;
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
			case "BUILDINGID":
				reader.Read();
				BuildingID = global::System.Convert.ToInt32(reader.Value);
				break;
			case "COSTUMEID":
				reader.Read();
				CostumeID = global::System.Convert.ToInt32(reader.Value);
				break;
			case "NAME":
				reader.Read();
				Name = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "STATE":
				reader.Read();
				State = global::Kampai.Util.ReaderUtil.ReadEnum<global::Kampai.Game.MinionState>(reader);
				break;
			case "TASKDURATION":
				reader.Read();
				TaskDuration = global::System.Convert.ToInt32(reader.Value);
				break;
			case "UTCTASKSTARTTIME":
				reader.Read();
				UTCTaskStartTime = global::System.Convert.ToInt32(reader.Value);
				break;
			case "ALREADYRUSHED":
				reader.Read();
				AlreadyRushed = global::System.Convert.ToBoolean(reader.Value);
				break;
			case "PRESTIGECHARACTER":
				reader.Read();
				PrestigeCharacter = (global::Kampai.Game.Prestige)converters.instanceConverter.ReadJson(reader, converters);
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
			writer.WritePropertyName("BuildingID");
			writer.WriteValue(BuildingID);
			writer.WritePropertyName("CostumeID");
			writer.WriteValue(CostumeID);
			if (Name != null)
			{
				writer.WritePropertyName("Name");
				writer.WriteValue(Name);
			}
			writer.WritePropertyName("State");
			writer.WriteValue((int)State);
			writer.WritePropertyName("TaskDuration");
			writer.WriteValue(TaskDuration);
			writer.WritePropertyName("UTCTaskStartTime");
			writer.WriteValue(UTCTaskStartTime);
			writer.WritePropertyName("AlreadyRushed");
			writer.WriteValue(AlreadyRushed);
			if (PrestigeCharacter != null)
			{
				writer.WritePropertyName("PrestigeCharacter");
				PrestigeCharacter.Serialize(writer);
			}
		}
	}
}
