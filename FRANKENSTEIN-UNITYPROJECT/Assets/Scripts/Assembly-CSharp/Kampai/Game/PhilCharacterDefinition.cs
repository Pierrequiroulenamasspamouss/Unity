namespace Kampai.Game
{
	public class PhilCharacterDefinition : global::Kampai.Game.NamedCharacterDefinition
	{
		public string TikiBarStateMachine { get; set; }

		public global::System.Collections.Generic.IList<global::UnityEngine.Vector3> IntroPath { get; set; }

		public global::UnityEngine.Vector3 IntroEulers { get; set; }

		public float IntroTime { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "TIKIBARSTATEMACHINE":
				reader.Read();
				TikiBarStateMachine = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "INTROPATH":
				reader.Read();
				IntroPath = global::Kampai.Util.ReaderUtil.PopulateList<global::UnityEngine.Vector3>(reader, converters, global::Kampai.Util.ReaderUtil.ReadVector3);
				break;
			case "INTROEULERS":
				reader.Read();
				IntroEulers = global::Kampai.Util.ReaderUtil.ReadVector3(reader, converters);
				break;
			case "INTROTIME":
				reader.Read();
				IntroTime = global::System.Convert.ToSingle(reader.Value);
				break;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			return true;
		}

		public override global::Kampai.Game.Instance Build()
		{
			return new global::Kampai.Game.PhilCharacter(this);
		}
	}
}
