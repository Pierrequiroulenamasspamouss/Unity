namespace Kampai.Game
{
	public class MarketplaceSaleItem : global::Kampai.Game.Instance<global::Kampai.Game.MarketplaceItemDefinition>, global::System.IComparable<global::Kampai.Game.MarketplaceSaleItem>, global::Kampai.Game.Instance, global::Kampai.Util.IFastJSONDeserializable, global::Kampai.Util.IFastJSONSerializable, global::Kampai.Util.Identifiable
	{
		public enum State
		{
			PENDING = 0,
			SOLD = 1
		}

		global::Kampai.Game.Definition global::Kampai.Game.Instance.Definition
		{
			get
			{
				return Definition;
			}
		}

		public int ID { get; set; }

		public global::Kampai.Game.MarketplaceItemDefinition Definition { get; protected set; }

		public global::Kampai.Game.MarketplaceSaleItem.State state { get; set; }

		public int QuantitySold { get; set; }

		public int SalePrice { get; set; }

		public int SaleStartTime { get; set; }

		public int LengthOfSale { get; set; }

		public MarketplaceSaleItem(global::Kampai.Game.MarketplaceItemDefinition definition)
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
				state = global::Kampai.Util.ReaderUtil.ReadEnum<global::Kampai.Game.MarketplaceSaleItem.State>(reader);
				break;
			case "QUANTITYSOLD":
				reader.Read();
				QuantitySold = global::System.Convert.ToInt32(reader.Value);
				break;
			case "SALEPRICE":
				reader.Read();
				SalePrice = global::System.Convert.ToInt32(reader.Value);
				break;
			case "SALESTARTTIME":
				reader.Read();
				SaleStartTime = global::System.Convert.ToInt32(reader.Value);
				break;
			case "LENGTHOFSALE":
				reader.Read();
				LengthOfSale = global::System.Convert.ToInt32(reader.Value);
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
			writer.WritePropertyName("state");
			writer.WriteValue((int)state);
			writer.WritePropertyName("QuantitySold");
			writer.WriteValue(QuantitySold);
			writer.WritePropertyName("SalePrice");
			writer.WriteValue(SalePrice);
			writer.WritePropertyName("SaleStartTime");
			writer.WriteValue(SaleStartTime);
			writer.WritePropertyName("LengthOfSale");
			writer.WriteValue(LengthOfSale);
		}

		public int CompareTo(global::Kampai.Game.MarketplaceSaleItem other)
		{
			if (other == null)
			{
				return -1;
			}
			return other.state - state;
		}
	}
}
