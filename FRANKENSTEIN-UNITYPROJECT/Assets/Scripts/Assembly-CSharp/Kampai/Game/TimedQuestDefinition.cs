namespace Kampai.Game
{
	public class TimedQuestDefinition : global::Kampai.Game.QuestDefinition
	{
		public int Duration { get; set; }

		public int PushNoteWarningTime { get; set; }

		public bool Repeat { get; set; }

		public TimedQuestDefinition()
		{
			Duration = 720;
			PushNoteWarningTime = 540;
			Repeat = false;
		}

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "DURATION":
				reader.Read();
				Duration = global::System.Convert.ToInt32(reader.Value);
				break;
			case "PUSHNOTEWARNINGTIME":
				reader.Read();
				PushNoteWarningTime = global::System.Convert.ToInt32(reader.Value);
				break;
			case "REPEAT":
				reader.Read();
				Repeat = global::System.Convert.ToBoolean(reader.Value);
				break;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			return true;
		}
	}
}
