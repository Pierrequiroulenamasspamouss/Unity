namespace Kampai.Game
{
	public class SocialSettingsDefinition : global::Kampai.Game.Definition
	{
		public bool ShowFacebookConnectPopup { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "SHOWFACEBOOKCONNECTPOPUP":
				reader.Read();
				ShowFacebookConnectPopup = global::System.Convert.ToBoolean(reader.Value);
				return true;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
		}
	}
}
