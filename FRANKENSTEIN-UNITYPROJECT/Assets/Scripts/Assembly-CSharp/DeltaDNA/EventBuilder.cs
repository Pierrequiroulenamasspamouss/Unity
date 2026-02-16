namespace DeltaDNA
{
	public class EventBuilder
	{
		private global::System.Collections.Generic.Dictionary<string, object> dict = new global::System.Collections.Generic.Dictionary<string, object>();

		public global::DeltaDNA.EventBuilder AddParam(string key, object value)
		{
			if (value == null)
			{
				return this;
			}
			if (value.GetType() == typeof(global::DeltaDNA.ProductBuilder))
			{
				global::DeltaDNA.ProductBuilder productBuilder = value as global::DeltaDNA.ProductBuilder;
				value = productBuilder.ToDictionary();
			}
			else if (value.GetType() == typeof(global::DeltaDNA.EventBuilder))
			{
				global::DeltaDNA.EventBuilder eventBuilder = value as global::DeltaDNA.EventBuilder;
				value = eventBuilder.ToDictionary();
			}
			dict.Add(key, value);
			return this;
		}

		public global::System.Collections.Generic.Dictionary<string, object> ToDictionary()
		{
			return dict;
		}

		public string ToPrettyString()
		{
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
			stringBuilder.Append("{");
			foreach (global::System.Collections.Generic.KeyValuePair<string, object> item in dict)
			{
				stringBuilder.Append(string.Format(" {0}={1} ", item.Key, item.Value));
			}
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}
	}
}
