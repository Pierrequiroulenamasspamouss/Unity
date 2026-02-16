namespace Kampai.Game.Transaction
{
	public class WeightedInstance : global::Kampai.Game.Instance<global::Kampai.Game.Transaction.WeightedDefinition>, global::Kampai.Game.Instance, global::Kampai.Util.IFastJSONDeserializable, global::Kampai.Util.IFastJSONSerializable, global::Kampai.Util.Identifiable
	{
		global::Kampai.Game.Definition global::Kampai.Game.Instance.Definition
		{
			get
			{
				return Definition;
			}
		}

		public int DeckIndex { get; set; }

		public int Seed { get; set; }

		public int ID { get; set; }

		public global::Kampai.Game.Transaction.WeightedDefinition Definition { get; protected set; }

		public WeightedInstance()
		{
		}

		public WeightedInstance(global::Kampai.Game.Transaction.WeightedDefinition def)
		{
			Definition = def;
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
			case "DECKINDEX":
				reader.Read();
				DeckIndex = global::System.Convert.ToInt32(reader.Value);
				break;
			case "SEED":
				reader.Read();
				Seed = global::System.Convert.ToInt32(reader.Value);
				break;
			case "ID":
				reader.Read();
				ID = global::System.Convert.ToInt32(reader.Value);
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
			writer.WritePropertyName("DeckIndex");
			writer.WriteValue(DeckIndex);
			writer.WritePropertyName("Seed");
			writer.WriteValue(Seed);
		}

		public virtual global::Kampai.Util.QuantityItem NextPick(global::Kampai.Common.IRandomService gameRandomService)
		{
			if (gameRandomService != null)
			{
				int num = Count();
				int num2 = DeckIndex;
				if (num2 < 1 || num2 > num)
				{
					Seed = gameRandomService.NextInt(int.MaxValue);
					int num3 = (DeckIndex = 1);
					num2 = num3;
				}
				global::Kampai.Common.IRandomService randomService = new global::Kampai.Common.RandomService(Seed);
				global::Kampai.Game.Transaction.WeightedQuantityItem[] array = Shuffle(randomService, num);
				DeckIndex = num2 + 1;
				return array[num2 - 1];
			}
			return null;
		}

		private global::Kampai.Game.Transaction.WeightedQuantityItem[] Shuffle(global::Kampai.Common.IRandomService randomService, int count)
		{
			DeckIndex = 1;
			global::Kampai.Game.Transaction.WeightedQuantityItem[] array = new global::Kampai.Game.Transaction.WeightedQuantityItem[count];
			int num = 0;
			foreach (global::Kampai.Game.Transaction.WeightedQuantityItem entity in Definition.Entities)
			{
				uint weight = entity.Weight;
				for (uint num2 = 0u; num2 < weight; num2++)
				{
					array[num++] = entity;
				}
			}
			for (int i = 0; i < array.Length; i++)
			{
				int num3 = randomService.NextInt(i, array.Length);
				global::Kampai.Game.Transaction.WeightedQuantityItem weightedQuantityItem = array[i];
				array[i] = array[num3];
				array[num3] = weightedQuantityItem;
			}
			return array;
		}

		protected int Count()
		{
			int num = 0;
			foreach (global::Kampai.Game.Transaction.WeightedQuantityItem entity in Definition.Entities)
			{
				num += (int)entity.Weight;
			}
			return num;
		}
	}
}
