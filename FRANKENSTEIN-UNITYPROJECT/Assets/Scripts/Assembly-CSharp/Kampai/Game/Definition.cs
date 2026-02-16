namespace Kampai.Game
{
	public abstract class Definition : global::Kampai.Util.IFastJSONDeserializable, global::Kampai.Util.Identifiable
	{
		public string LocalizedKey { get; set; }

		public virtual int ID { get; set; }

		public int MinDLC { get; set; }

		public bool Disabled { get; set; }

        // DANS Kampa/Game/Definition.cs

        public virtual object Deserialize(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters = null)
        {
            // 1. Sécurité : Calage du Reader
            if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None) reader.Read();
            if (reader.TokenType == global::Newtonsoft.Json.JsonToken.StartObject) reader.Read();

            // 2. Boucle de lecture
            while (reader.TokenType != global::Newtonsoft.Json.JsonToken.EndObject)
            {
                if (reader.TokenType == global::Newtonsoft.Json.JsonToken.PropertyName)
                {
                    string propertyName = ((string)reader.Value).ToUpper();

                    // --- LE FIX MAGIQUE : INTERCEPTION DE L'ID ---
                    // On force la lecture de l'ID ici, sans faire confiance aux classes filles.
                    if (propertyName == "ID" || propertyName == "BUILDINGDEFINITIONID")
                    {
                        reader.Read(); // On passe à la valeur
                                       // On utilise SafeParseInt pour éviter tout crash si le format est bizarre
                        int val = global::Kampai.Util.ReaderUtil.SafeParseInt(reader.Value);

                        // On assigne si c'est l'ID principal
                        if (propertyName == "ID") this.ID = val;
                        // (Tu peux ajouter d'autres champs critiques ici si nécessaire)
                    }
                    // Sinon, on laisse le comportement normal
                    else if (!DeserializeProperty(propertyName, reader, converters))
                    {
                        // Si personne ne veut de cette propriété, on la saute pour ne pas décaler le lecteur
                        reader.Skip();
                    }
                }

                // 3. Avance au prochain token
                if (!reader.Read()) break;
            }
            return this;
        }

        protected virtual bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
        {
            switch (propertyName)
            {
                case "LOCALIZEDKEY":
                    reader.Read();
                    LocalizedKey = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
                    break;

                case "ID":
                    reader.Read(); // Passe de la clé "ID" à la valeur (ex: 46001)
                                   // UTILISATION DE SafeParseInt POUR EVITER LE CRASH OU LE 0 ACCIDENTEL
                    ID = global::Kampai.Util.ReaderUtil.SafeParseInt(reader.Value);
                    break;

                case "MINDLC":
                    reader.Read();
                    MinDLC = global::Kampai.Util.ReaderUtil.SafeParseInt(reader.Value);
                    break;

                case "DISABLED":
                    reader.Read();
                    Disabled = global::System.Convert.ToBoolean(reader.Value);
                    break;

                default:
                    return false;
            }
            return true;
        }

        public override string ToString()
		{
			return string.Format("Defintion TYPE={0} ID={1} KEY={2}", GetType().Name, ID, LocalizedKey);
		}
	}
}
