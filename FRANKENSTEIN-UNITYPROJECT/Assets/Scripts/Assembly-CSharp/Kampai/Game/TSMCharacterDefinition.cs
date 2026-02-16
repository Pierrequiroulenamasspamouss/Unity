namespace Kampai.Game
{
	public class TSMCharacterDefinition : global::Kampai.Game.NamedCharacterDefinition
	{
		public global::System.Collections.Generic.IList<global::UnityEngine.Vector3> IntroPath { get; set; }

		public float IntroTime { get; set; }

		public int CooldownInSeconds { get; set; }

		public int CooldownMignetteDelayInSeconds { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "INTROPATH":
				reader.Read();
				IntroPath = global::Kampai.Util.ReaderUtil.PopulateList<global::UnityEngine.Vector3>(reader, converters, global::Kampai.Util.ReaderUtil.ReadVector3);
				break;
			case "INTROTIME":
				reader.Read();
				IntroTime = global::System.Convert.ToSingle(reader.Value);
				break;
			case "COOLDOWNINSECONDS":
				reader.Read();
				CooldownInSeconds = global::System.Convert.ToInt32(reader.Value);
				break;
			case "COOLDOWNMIGNETTEDELAYINSECONDS":
				reader.Read();
				CooldownMignetteDelayInSeconds = global::System.Convert.ToInt32(reader.Value);
				break;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			return true;
		}

		public override global::Kampai.Game.Instance Build()
		{
			return new global::Kampai.Game.TSMCharacter(this);
		}
	}
}
