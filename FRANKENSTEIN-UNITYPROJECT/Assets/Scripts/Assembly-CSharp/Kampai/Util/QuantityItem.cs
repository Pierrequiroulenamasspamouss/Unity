namespace Kampai.Util
{
	public class QuantityItem : global::Kampai.Util.IFastJSONDeserializable, global::Kampai.Util.Identifiable
	{
		public int ID { get; set; }

		public uint Quantity { get; set; }

		public QuantityItem()
		{
		}

		public QuantityItem(int id)
		{
			ID = id;
		}

		public QuantityItem(int id, uint quantity)
		{
			ID = id;
			Quantity = quantity;
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
			default:
					{
						int num;
                        num = 1; //added this line to remove use of unassigned variable
                        if (num == 1)
				{
					reader.Read();
					Quantity = global::System.Convert.ToUInt32(reader.Value);
					break;
				}
				return false;
			}
			case "ID":
				reader.Read();
				ID = global::System.Convert.ToInt32(reader.Value);
				break;
			}
			return true;
		}

		public static global::Kampai.Util.QuantityItem Build(global::System.Collections.Generic.IDictionary<string, object> src, global::Kampai.Util.QuantityItem qi = null)
		{
			if (src != null)
			{
				if (qi == null)
				{
					qi = new global::Kampai.Util.QuantityItem();
				}
				qi.ID = global::System.Convert.ToInt32(src["id"]);
				qi.Quantity = global::System.Convert.ToUInt32(src["quantity"]);
				return qi;
			}
			return null;
		}
	}
}
