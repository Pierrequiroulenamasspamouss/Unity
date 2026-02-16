namespace Kampai.Game
{
	public class VillainDefinition : global::Kampai.Game.NamedCharacterDefinition
	{
		public int LoopCountMin { get; set; }

		public int LoopCountMax { get; set; }

		public string AsmCabana { get; set; }

		public string AsmFarewell { get; set; }

		public string AsmBoat { get; set; }

		public string WelcomeDialogKey { get; set; }

		public string FarewellDialogKey { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "LOOPCOUNTMIN":
				reader.Read();
				LoopCountMin = global::System.Convert.ToInt32(reader.Value);
				break;
			case "LOOPCOUNTMAX":
				reader.Read();
				LoopCountMax = global::System.Convert.ToInt32(reader.Value);
				break;
			case "ASMCABANA":
				reader.Read();
				AsmCabana = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "ASMFAREWELL":
				reader.Read();
				AsmFarewell = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "ASMBOAT":
				reader.Read();
				AsmBoat = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "WELCOMEDIALOGKEY":
				reader.Read();
				WelcomeDialogKey = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "FAREWELLDIALOGKEY":
				reader.Read();
				FarewellDialogKey = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			return true;
		}

		public override global::Kampai.Game.Instance Build()
		{
			return new global::Kampai.Game.Villain(this);
		}
	}
}
