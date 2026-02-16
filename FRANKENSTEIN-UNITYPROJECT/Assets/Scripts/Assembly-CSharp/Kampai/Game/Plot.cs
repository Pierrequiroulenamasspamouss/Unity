namespace Kampai.Game
{
	public abstract class Plot<T> : global::Kampai.Game.Instance<T>, global::Kampai.Game.Instance, global::Kampai.Game.Locatable, global::Kampai.Game.Plot, global::Kampai.Util.IFastJSONDeserializable, global::Kampai.Util.IFastJSONSerializable, global::Kampai.Util.Identifiable where T : global::Kampai.Game.PlotDefinition
	{
		private readonly T definition;

		global::Kampai.Game.PlotDefinition global::Kampai.Game.Plot.Definition
		{
			get
			{
				return definition;
			}
		}

		global::Kampai.Game.Definition global::Kampai.Game.Instance.Definition
		{
			get
			{
				return definition;
			}
		}

		public T Definition
		{
			get
			{
				return definition;
			}
		}

		public global::Kampai.Game.Location Location { get; set; }

		public int ID { get; set; }

		public Plot(T definition)
		{
			this.definition = definition;
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
					ID = global::System.Convert.ToInt32(reader.Value);
					break;
				}
				return false;
			}
			case "LOCATION":
				reader.Read();
				Location = global::Kampai.Util.ReaderUtil.ReadLocation(reader, converters);
				break;
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
		}
	}
	public interface Plot : global::Kampai.Game.Instance, global::Kampai.Game.Locatable, global::Kampai.Util.IFastJSONDeserializable, global::Kampai.Util.IFastJSONSerializable, global::Kampai.Util.Identifiable
	{
		new global::Kampai.Game.PlotDefinition Definition { get; }
	}
}
