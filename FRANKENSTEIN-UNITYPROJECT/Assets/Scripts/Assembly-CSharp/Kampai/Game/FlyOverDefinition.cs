namespace Kampai.Game
{
	public class FlyOverDefinition : global::Kampai.Game.Definition
	{
		public float time { get; set; }

		public global::System.Collections.Generic.IList<global::Kampai.Game.FlyOverNode> path { get; set; }

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
					path = global::Kampai.Util.ReaderUtil.PopulateList<global::Kampai.Game.FlyOverNode>(reader, converters, global::Kampai.Util.ReaderUtil.ReadFlyOverNode);
					break;
				}
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			case "TIME":
				reader.Read();
				time = global::System.Convert.ToSingle(reader.Value);
				break;
			}
			return true;
		}
	}
}
