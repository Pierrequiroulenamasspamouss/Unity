namespace Kampai.Util
{
	public static class FastJSONDeserializer
	{
		public static T Deserialize<T>(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters = null) where T : global::Kampai.Util.IFastJSONDeserializable, new()
		{
			T result = new T();
			result.Deserialize(reader, converters);
			return result;
		}

		public static T Deserialize<T>(string json, JsonConverters converters = null) where T : global::Kampai.Util.IFastJSONDeserializable, new()
		{
			// Use FastJsonParser to bypass Newtonsoft text parsing bug
			object parsed = global::Kampai.Util.FastJsonParser.Deserialize(json);
			if (parsed == null)
			{
				throw new global::Newtonsoft.Json.JsonSerializationException("FastJsonParser returned null");
			}
			
			// Convert to JToken structure
			global::Newtonsoft.Json.Linq.JToken rootToken = global::Kampai.Util.FastJsonParser.ConvertToJToken(parsed);
			
			// Read from JToken (bypassing JsonTextReader text parsing issues)
			global::Newtonsoft.Json.JsonReader reader = new global::Newtonsoft.Json.Linq.JTokenReader(rootToken);
			
			T result = new T();
			result.Deserialize(reader, converters);
			return result;
		}
	}
}
