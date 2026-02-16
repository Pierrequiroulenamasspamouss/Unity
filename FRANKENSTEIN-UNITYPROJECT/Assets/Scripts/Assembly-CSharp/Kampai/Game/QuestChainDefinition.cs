namespace Kampai.Game
{
	public class QuestChainDefinition : global::Kampai.Game.Definition
	{
		public string Name { get; set; }

		public string Summary { get; set; }

		public int Giver { get; set; }

		public int Level { get; set; }

		public global::System.Collections.Generic.IList<global::Kampai.Game.QuestChainStepDefinition> Steps { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "NAME":
				reader.Read();
				Name = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "SUMMARY":
				reader.Read();
				Summary = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "GIVER":
				reader.Read();
				Giver = global::System.Convert.ToInt32(reader.Value);
				break;
			case "LEVEL":
				reader.Read();
				Level = global::System.Convert.ToInt32(reader.Value);
				break;
			case "STEPS":
				reader.Read();
				Steps = global::Kampai.Util.ReaderUtil.PopulateList<global::Kampai.Game.QuestChainStepDefinition>(reader, converters, global::Kampai.Util.ReaderUtil.ReadQuestChainStepDefinition);
				break;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			return true;
		}
	}
}
