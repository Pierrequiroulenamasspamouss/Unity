namespace Kampai.Splash
{
	public class LoadinTipBucketDefinition : global::Kampai.Game.Definition
	{
		public int Min { get; set; }

		public int Max { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
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
					Max = global::System.Convert.ToInt32(reader.Value);
					break;
				}
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			case "MIN":
				reader.Read();
				Min = global::System.Convert.ToInt32(reader.Value);
				break;
			}
			return true;
		}
	}
}
