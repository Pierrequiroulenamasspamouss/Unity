namespace Kampai.Game
{
	public abstract class NamedCharacter<T> : global::Kampai.Game.Character<T>, global::Kampai.Game.Character, global::Kampai.Game.NamedCharacter, global::Kampai.Game.Instance, global::Kampai.Util.IFastJSONDeserializable, global::Kampai.Util.IFastJSONSerializable, global::Kampai.Util.Identifiable, global::Kampai.Util.Prestigable where T : global::Kampai.Game.NamedCharacterDefinition
	{
		global::Kampai.Game.Definition global::Kampai.Game.Instance.Definition
		{
			get
			{
				return Definition;
			}
		}

		global::Kampai.Game.NamedCharacterDefinition global::Kampai.Game.NamedCharacter.Definition
		{
			get
			{
				return Definition;
			}
		}

		public int ID { get; set; }

		public string Name { get; set; }

		public T Definition { get; protected set; }

		public NamedCharacter(T def)
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
			case "NAME":
				reader.Read();
				Name = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			default:
				return false;
			case "ID":
				reader.Read();
				ID = global::System.Convert.ToInt32(reader.Value);
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
			if (Name != null)
			{
				writer.WritePropertyName("Name");
				writer.WriteValue(Name);
			}
		}

		public abstract global::Kampai.Game.View.NamedCharacterObject Setup(global::UnityEngine.GameObject go);
	}
	public interface NamedCharacter : global::Kampai.Game.Character, global::Kampai.Game.Instance, global::Kampai.Util.IFastJSONDeserializable, global::Kampai.Util.IFastJSONSerializable, global::Kampai.Util.Identifiable, global::Kampai.Util.Prestigable
	{
		new global::Kampai.Game.NamedCharacterDefinition Definition { get; }

		global::Kampai.Game.View.NamedCharacterObject Setup(global::UnityEngine.GameObject go);
	}
}
