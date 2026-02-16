namespace Kampai.Game
{
    public class PremiumCurrencyItemDefinition : global::Kampai.Game.CurrencyItemDefinition, global::Kampai.Game.MTXItem
    {
        private string sku;
        private global::Kampai.Game.PlatformStoreSkuDefinition platformDef;

        public string SKU
        {
            get { return sku; }
            set { sku = value; }
        }

        public global::Kampai.Game.PlatformStoreSkuDefinition PlatformStoreSku
        {
            get
            {
                return platformDef;
            }
            set
            {
                platformDef = value;
                // LE FIX EST ICI : On vérifie si c'est null avant d'appeler GetPlatformSKU
                if (platformDef != null)
                {
                    sku = GetPlatformSKU();
                }
                else
                {
                    sku = null;
                }
            }
        }

        protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
        {
            // Note: Ton switch/default d'origine est très étrange (il essaie de lire un SKU pour n'importe quelle propriété inconnue ?),
            // mais je le garde tel quel pour ne pas casser la logique existante, sauf la lecture sécurisée.
            switch (propertyName)
            {
                case "SKU":
                    reader.Read();
                    SKU = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
                    break;

                // J'ajoute le cas explicite pour PLATFORMSTORESKU pour être propre
                case "PLATFORMSTORESKU":
                    reader.Read();
                    PlatformStoreSku = global::Kampai.Util.ReaderUtil.ReadPlatformStoreSkuDefinition(reader, converters);
                    break;

                default:
                    {
                        // On garde ta logique "barbare" du default case au cas où, 
                        // mais on l'encadre pour qu'elle ne mange pas tout.
                        // Si le nom ressemble à un SKU, on tente, sinon on laisse la base gérer.
                        if (propertyName.Contains("SKU") || propertyName.Contains("STORE"))
                        {
                            reader.Read();
                            PlatformStoreSku = global::Kampai.Util.ReaderUtil.ReadPlatformStoreSkuDefinition(reader, converters);
                            break;
                        }
                        return base.DeserializeProperty(propertyName, reader, converters);
                    }
            }
            return true;
        }

        private string GetPlatformSKU()
        {
            // Sécurité supplémentaire
            if (platformDef == null) return null;
            return platformDef.googlePlay;
        }
    }
}