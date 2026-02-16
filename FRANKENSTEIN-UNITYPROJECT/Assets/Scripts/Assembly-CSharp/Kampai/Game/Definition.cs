namespace Kampai.Game
{
	public abstract class Definition : global::Kampai.Util.IFastJSONDeserializable, global::Kampai.Util.Identifiable
	{
		public string LocalizedKey { get; set; }

		public virtual int ID { get; set; }

		public int MinDLC { get; set; }

		public bool Disabled { get; set; }

		public virtual object Deserialize(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters = null)
		{
			try
			{
				if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None)
				{
					reader.Read();
				}
				global::Kampai.Util.ReaderUtil.EnsureToken(global::Newtonsoft.Json.JsonToken.StartObject, reader);
				while (reader.Read())
				{
                    // Debug logging
                    global::UnityEngine.Debug.LogWarning(string.Format("Parsing {0}: Token={1}, Value={2}", this.GetType().Name, reader.TokenType, reader.Value));
					switch (reader.TokenType)
					{
					case global::Newtonsoft.Json.JsonToken.PropertyName:
					{
						string propertyName = ((string)reader.Value).ToUpper();
						if (!DeserializeProperty(propertyName, reader, converters))
						{
                            // global::UnityEngine.Debug.LogFormat("Skipping property: {0}", propertyName);
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
			catch (global::System.Exception ex)
			{
                global::UnityEngine.Debug.LogError(
                    string.Format("Definition Deserialization Error in {0}", this.GetType().Name)
                );

                var jsonTextReader = reader as global::Newtonsoft.Json.JsonTextReader;

                if (jsonTextReader != null)
                {
                    global::UnityEngine.Debug.LogError(
                        string.Format(
                            "Reader State - Line: {0}, Position: {1}, Token: {2}, Value: {3}",
                            jsonTextReader.LineNumber,
                            jsonTextReader.LinePosition,
                            reader.TokenType,
                            reader.Value
                        )
                    );
                }
                else
                {
                    global::UnityEngine.Debug.LogError(
                        string.Format(
                            "Reader State - Token: {0}, Value: {1}",
                            reader.TokenType,
                            reader.Value
                        )
                    );
                }

                throw;

                throw;
			}
		}

		protected virtual bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "LOCALIZEDKEY":
				reader.Read();
				LocalizedKey = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "ID":
				reader.Read();
				ID = global::System.Convert.ToInt32(reader.Value);
				break;
			case "MINDLC":
				reader.Read();
				MinDLC = global::System.Convert.ToInt32(reader.Value);
				break;
			case "DISABLED":
				reader.Read();
				Disabled = global::System.Convert.ToBoolean(reader.Value);
				break;
			default:
				return false;
			}
			return true;
		}

		public override string ToString()
		{
			return string.Format("Defintion TYPE={0} ID={1} KEY={2}", GetType().Name, ID, LocalizedKey);
		}
	}
}
