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
            if (buildingType == BuildingType.BuildingTypeIdentifier.UNKNOWN)
            {
                bool hasType = false;
                string typeValue = null;
                if (jObject.Property("type") != null)
                {
                    typeValue = jObject.Property("type").Value.ToString();
                    hasType = true;
                }
                else if (jObject.Property("TYPE") != null)
                {
                    typeValue = jObject.Property("TYPE").Value.ToString();
                    hasType = true;
                }
                else if (jObject.Property("BuildingType") != null)
                {
                    typeValue = jObject.Property("BuildingType").Value.ToString();
                    hasType = true;
                }
                else if (jObject.Property("Type") != null)
                {
                    typeValue = jObject.Property("Type").Value.ToString();
                    hasType = true;
                }
                if (hasType)
                {
                    try
                    {
                        buildingType = (BuildingType.BuildingTypeIdentifier)global::System.Enum.Parse(typeof(BuildingType.BuildingTypeIdentifier), typeValue, true);
                    }
                    catch (global::System.Exception)
                    {
                        global::UnityEngine.Debug.LogWarning("BuildingDefinitionFastConverter: Type inconnu (" + typeValue + "). Fallback sur Decoration.");
                        buildingType = BuildingType.BuildingTypeIdentifier.DECORATION;
                    }
                }
                else if (jObject.Property("prefab") != null)
                {
                    global::UnityEngine.Debug.LogWarning("BuildingDefinitionFastConverter: Propriété 'type' introuvable dans le JSON pour l'objet avec prefab " + jObject.Property("prefab").Value.ToString() + ". Fallback sur Decoration.");
                    buildingType = BuildingType.BuildingTypeIdentifier.DECORATION;
                }
                else
                {
                    buildingType = BuildingType.BuildingTypeIdentifier.DECORATION;
                }
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
                    global::UnityEngine.Debug.Log("BuildingDefinitionFastConverter: Type inconnu ou null (" + buildingType + "). Fallback sur Decoration.");
                    return new global::Kampai.Game.DecorationBuildingDefinition();
            }
        }
    }
}