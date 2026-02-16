namespace Kampai.Game
{
	public class Prestige : global::Kampai.Game.Instance<global::Kampai.Game.PrestigeDefinition>, global::Kampai.Game.Instance, global::Kampai.Game.ItemAccumulator, global::Kampai.Util.IFastJSONDeserializable, global::Kampai.Util.IFastJSONSerializable, global::Kampai.Util.Identifiable
	{
		global::Kampai.Game.Definition global::Kampai.Game.Instance.Definition
		{
			get
			{
				return Definition;
			}
		}

		public int ID { get; set; }

		public int trackedInstanceId { get; set; }

		public global::Kampai.Game.PrestigeState state { get; set; }

		public int CurrentPrestigeLevel { get; set; }

		public int CurrentPrestigePoints { get; set; }

		public int CurrentOrdersCompleted { get; set; }

		public int UTCTimeUnlocked { get; set; }

		public global::Kampai.Game.PrestigeDefinition Definition { get; protected set; }

		[global::Newtonsoft.Json.JsonIgnore]
		public int NeededPrestigePoints
		{
			get
			{
				return (int)GetCurrentPrestigeLevelDefinition().PointsNeeded;
			}
		}

		[global::Newtonsoft.Json.JsonIgnore]
		public string CurrentWelcomeMessage
		{
			get
			{
				return GetCurrentPrestigeLevelDefinition().WelcomePanelMessageLocalizedKey;
			}
		}

		[global::Newtonsoft.Json.JsonIgnore]
		public string CurrentFarewellMessage
		{
			get
			{
				return GetCurrentPrestigeLevelDefinition().FarewellPanelMessageLocalizedKey;
			}
		}

		public Prestige(global::Kampai.Game.PrestigeDefinition def)
		{
			Definition = def;
			CurrentPrestigeLevel = -2;
			CurrentPrestigePoints = 0;
			trackedInstanceId = 0;
			state = global::Kampai.Game.PrestigeState.Locked;
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
			case "TRACKEDINSTANCEID":
				reader.Read();
				trackedInstanceId = global::System.Convert.ToInt32(reader.Value);
				break;
			case "STATE":
				reader.Read();
				state = global::Kampai.Util.ReaderUtil.ReadEnum<global::Kampai.Game.PrestigeState>(reader);
				break;
			case "CURRENTPRESTIGELEVEL":
				reader.Read();
				CurrentPrestigeLevel = global::System.Convert.ToInt32(reader.Value);
				break;
			case "CURRENTPRESTIGEPOINTS":
				reader.Read();
				CurrentPrestigePoints = global::System.Convert.ToInt32(reader.Value);
				break;
			case "CURRENTORDERSCOMPLETED":
				reader.Read();
				CurrentOrdersCompleted = global::System.Convert.ToInt32(reader.Value);
				break;
			case "UTCTIMEUNLOCKED":
				reader.Read();
				UTCTimeUnlocked = global::System.Convert.ToInt32(reader.Value);
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
			writer.WritePropertyName("trackedInstanceId");
			writer.WriteValue(trackedInstanceId);
			writer.WritePropertyName("state");
			writer.WriteValue((int)state);
			writer.WritePropertyName("CurrentPrestigeLevel");
			writer.WriteValue(CurrentPrestigeLevel);
			writer.WritePropertyName("CurrentPrestigePoints");
			writer.WriteValue(CurrentPrestigePoints);
			writer.WritePropertyName("CurrentOrdersCompleted");
			writer.WriteValue(CurrentOrdersCompleted);
			writer.WritePropertyName("UTCTimeUnlocked");
			writer.WriteValue(UTCTimeUnlocked);
		}

		public void AwardOutput(global::Kampai.Util.QuantityItem item)
		{
			if (item.ID == 2)
			{
				CurrentPrestigePoints += (int)item.Quantity;
			}
		}

		public override string ToString()
		{
			return string.Format("{0}(ID:{1}, State:{2}, Definition:{3})", typeof(global::Kampai.Game.Prestige).FullName, ID, state, Definition);
		}

		private global::Kampai.Game.CharacterPrestigeLevelDefinition GetCurrentPrestigeLevelDefinition()
		{
			return Definition.PrestigeLevelSettings[(CurrentPrestigeLevel >= 1) ? CurrentPrestigeLevel : 0];
		}
	}
}
