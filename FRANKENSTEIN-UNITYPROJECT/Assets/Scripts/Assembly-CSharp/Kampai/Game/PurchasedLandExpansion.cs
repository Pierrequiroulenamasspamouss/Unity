namespace Kampai.Game
{
	public class PurchasedLandExpansion : global::Kampai.Game.Instance<global::Kampai.Game.PurchasedLandExpansionDefinition>, global::Kampai.Game.Instance, global::Kampai.Util.IFastJSONDeserializable, global::Kampai.Util.IFastJSONSerializable, global::Kampai.Util.Identifiable
	{
		global::Kampai.Game.Definition global::Kampai.Game.Instance.Definition
		{
			get
			{
				return Definition;
			}
		}

		public int ID { get; set; }

		public global::System.Collections.Generic.IList<int> PurchasedExpansions { get; set; }

		public global::System.Collections.Generic.IList<int> AdjacentExpansions { get; set; }

		public global::Kampai.Game.PurchasedLandExpansionDefinition Definition { get; protected set; }

		public PurchasedLandExpansion(global::Kampai.Game.PurchasedLandExpansionDefinition definition)
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
			case "PURCHASEDEXPANSIONS":
				reader.Read();
				PurchasedExpansions = global::Kampai.Util.ReaderUtil.PopulateListInt32(reader);
				break;
			case "ADJACENTEXPANSIONS":
				reader.Read();
				AdjacentExpansions = global::Kampai.Util.ReaderUtil.PopulateListInt32(reader);
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
			if (PurchasedExpansions != null)
			{
				writer.WritePropertyName("PurchasedExpansions");
				writer.WriteStartArray();
				global::System.Collections.Generic.IEnumerator<int> enumerator = PurchasedExpansions.GetEnumerator();
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
			if (AdjacentExpansions == null)
			{
				return;
			}
			writer.WritePropertyName("AdjacentExpansions");
			writer.WriteStartArray();
			global::System.Collections.Generic.IEnumerator<int> enumerator2 = AdjacentExpansions.GetEnumerator();
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

		public bool HasPurchased(int expansionId)
		{
			return PurchasedExpansions.Contains(expansionId);
		}

		public bool IsAdjacentExpansion(int expansionId)
		{
			return AdjacentExpansions.Contains(expansionId);
		}

		public bool IsUnpurchasedAdjacentExpansion(int expansionId)
		{
			return !HasPurchased(expansionId) && IsAdjacentExpansion(expansionId);
		}

		public int PurchasedExpansionsCount()
		{
			return PurchasedExpansions.Count;
		}
	}
}
