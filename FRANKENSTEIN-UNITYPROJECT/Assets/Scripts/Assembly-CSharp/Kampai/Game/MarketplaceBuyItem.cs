namespace Kampai.Game
{
	public class MarketplaceBuyItem : global::Kampai.Game.Instance<global::Kampai.Game.MarketplaceItemDefinition>, global::Kampai.Game.Instance, global::Kampai.Util.IFastJSONDeserializable, global::Kampai.Util.IFastJSONSerializable, global::Kampai.Util.Identifiable
	{
		global::Kampai.Game.Definition global::Kampai.Game.Instance.Definition
		{
			get
			{
				return Definition;
			}
		}

		public int ID { get; set; }

		public global::Kampai.Game.MarketplaceItemDefinition Definition { get; protected set; }

		public int BuyQuantity { get; set; }

		public int BuyPrice { get; set; }

		public bool BoughtFlag { get; set; }

		public MarketplaceBuyItem(global::Kampai.Game.MarketplaceItemDefinition definition)
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
			case "BUYQUANTITY":
				reader.Read();
				BuyQuantity = global::System.Convert.ToInt32(reader.Value);
				break;
			case "BUYPRICE":
				reader.Read();
				BuyPrice = global::System.Convert.ToInt32(reader.Value);
				break;
			case "BOUGHTFLAG":
				reader.Read();
				BoughtFlag = global::System.Convert.ToBoolean(reader.Value);
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
			writer.WritePropertyName("BuyQuantity");
			writer.WriteValue(BuyQuantity);
			writer.WritePropertyName("BuyPrice");
			writer.WriteValue(BuyPrice);
			writer.WritePropertyName("BoughtFlag");
			writer.WriteValue(BoughtFlag);
		}
	}
}
