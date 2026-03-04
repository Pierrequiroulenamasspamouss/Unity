using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Kampai.Game
{
    public class BuildingDefinitionFastConverter : global::Kampai.Util.FastJsonCreationConverter<global::Kampai.Game.BuildingDefinition>
    {
        private BuildingType.BuildingTypeIdentifier buildingType;

        public override global::Kampai.Game.BuildingDefinition ReadJson(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
        {
            if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
            {
                return null;
            }

            // FIX: Reset state so it never leaks from a previous object!
            buildingType = BuildingType.BuildingTypeIdentifier.UNKNOWN;

            // 1. On charge l'objet
            global::Newtonsoft.Json.Linq.JObject jObject = global::Newtonsoft.Json.Linq.JObject.Load(reader);

            // 2. LE FIX : On cherche "type" (minuscule) OU "TYPE" (majuscule)
            // Le script Python a mis "TYPE", donc le code d'origine échouait ici.
            global::Newtonsoft.Json.Linq.JProperty jProperty = jObject.Property("type");
            if (jProperty == null) jProperty = jObject.Property("TYPE");
            if (jProperty == null) jProperty = jObject.Property("BuildingType");
            if (jProperty == null) jProperty = jObject.Property("Type");

            if (jProperty != null)
            {
                string value = jProperty.Value.ToString();
                try
                {
                    // 3. LE FIX : On ajoute 'true' à la fin de Enum.Parse pour ignorer la casse
                    // Ça marchera que le JSON contienne "Crafting", "CRAFTING" ou "crafting"
                    buildingType = (BuildingType.BuildingTypeIdentifier)global::System.Enum.Parse(typeof(BuildingType.BuildingTypeIdentifier), value, true);
                }
                catch
                {
                    // Si le type est invalide, on ne fait rien, ça tombera dans le default du Create()
                    // ou on peut logger un warning ici.
                    Debug.LogWarning("BuildingDefinitionFastConverter: Impossible de parser le type: " + value);
                }
            }
            else
            {
                Debug.LogWarning("BuildingDefinitionFastConverter: Propriété 'type' introuvable dans le JSON.");
            }

            reader = jObject.CreateReader();
            return base.ReadJson(reader, converters);
        }

        public override global::Kampai.Game.BuildingDefinition Create()
        {
            switch (buildingType)
            {
                case BuildingType.BuildingTypeIdentifier.CRAFTING:
                    return new global::Kampai.Game.CraftingBuildingDefinition();
                case BuildingType.BuildingTypeIdentifier.DECORATION:
                    return new global::Kampai.Game.DecorationBuildingDefinition();
                case BuildingType.BuildingTypeIdentifier.RESOURCE:
                    return new global::Kampai.Game.ResourceBuildingDefinition();
                case BuildingType.BuildingTypeIdentifier.LEISURE:
                    return new global::Kampai.Game.LeisureBuildingDefintiion();
                case BuildingType.BuildingTypeIdentifier.SPECIAL:
                    return new global::Kampai.Game.SpecialBuildingDefinition();
                case BuildingType.BuildingTypeIdentifier.BLACKMARKETBOARD:
                    return new global::Kampai.Game.BlackMarketBoardDefinition();
                case BuildingType.BuildingTypeIdentifier.STORAGE:
                    return new global::Kampai.Game.StorageBuildingDefinition();
                case BuildingType.BuildingTypeIdentifier.LANDEXPANSION:
                    return new global::Kampai.Game.LandExpansionBuildingDefinition();
                case BuildingType.BuildingTypeIdentifier.DEBRIS:
                    return new global::Kampai.Game.DebrisBuildingDefinition();
                case BuildingType.BuildingTypeIdentifier.MIGNETTE:
                    return new global::Kampai.Game.MignetteBuildingDefinition();
                case BuildingType.BuildingTypeIdentifier.TIKIBAR:
                    return new global::Kampai.Game.TikiBarBuildingDefinition();
                case BuildingType.BuildingTypeIdentifier.CABANA:
                    return new global::Kampai.Game.CabanaBuildingDefinition();
                case BuildingType.BuildingTypeIdentifier.BRIDGE:
                    return new global::Kampai.Game.BridgeBuildingDefinition();
                case BuildingType.BuildingTypeIdentifier.COMPOSITE:
                    return new global::Kampai.Game.CompositeBuildingDefinition();
                case BuildingType.BuildingTypeIdentifier.STAGE:
                    return new global::Kampai.Game.StageBuildingDefinition();
                case BuildingType.BuildingTypeIdentifier.FOUNTAIN:
                    return new global::Kampai.Game.FountainBuildingDefinition();
                case BuildingType.BuildingTypeIdentifier.WELCOMEHUT:
                    return new global::Kampai.Game.WelcomeHutBuildingDefinition();
                case BuildingType.BuildingTypeIdentifier.MAILBOX:
                    return new global::Kampai.Game.MailboxBuildingDefinition();

                default:
                    // FIX DE SURVIE : Si le type est inconnu (ex: "UNKNOWN"), on renvoie un bâtiment générique
                    // au lieu de faire crasher tout le jeu.
                    Debug.LogWarning("BuildingDefinitionFastConverter: Type inconnu ou null (" + buildingType + "). Fallback sur Decoration.");
                    return new global::Kampai.Game.DecorationBuildingDefinition();
            }
        }
    }
}