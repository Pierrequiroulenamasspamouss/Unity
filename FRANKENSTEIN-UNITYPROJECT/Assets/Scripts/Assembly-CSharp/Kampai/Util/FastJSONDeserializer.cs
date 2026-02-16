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
			global::Newtonsoft.Json.JsonTextReader reader = new global::Newtonsoft.Json.JsonTextReader(new global::System.IO.StringReader(json));
			T result = new T();
			result.Deserialize(reader, converters);
			return result;
		}
	}
}
