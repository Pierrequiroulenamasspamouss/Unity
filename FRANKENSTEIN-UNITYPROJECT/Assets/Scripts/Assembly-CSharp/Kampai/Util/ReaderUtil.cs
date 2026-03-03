using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Kampai.Util
{
    public static class ReaderUtil
    {
        // --- HELPERS DE SECURITE ---

        public static int SafeParseInt(object value)
        {
            if (value == null) return 0;
            if (value is int || value is long || value is short || value is uint) return Convert.ToInt32(value);
            float f;
            if (float.TryParse(value.ToString(), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out f))
                return (int)f;
            return 0;
        }

        public static float SafeParseFloat(object value)
        {
            if (value == null) return 0f;
            if (value is float || value is double) return Convert.ToSingle(value);
            float f;
            if (float.TryParse(value.ToString(), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out f))
                return f;
            return 0f;
        }

        // --- BASIQUES ---

        public static string ReadString(JsonReader reader, JsonConverters converters)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            return Convert.ToString(reader.Value);
        }

        public static bool ReadBool(JsonReader reader, JsonConverters converters)
        {
            return Convert.ToBoolean(reader.Value);
        }

        public static T ReadEnum<T>(JsonReader reader)
        {
            if (reader.TokenType == JsonToken.PropertyName || reader.TokenType == JsonToken.String)
                return (T)Enum.Parse(typeof(T), Convert.ToString(reader.Value), true);
            if (reader.TokenType == JsonToken.Integer)
                return (T)Enum.ToObject(typeof(T), reader.Value);
            return default(T);
        }

        // --- DESERIALISATION GENERIQUE (Newtonsoft) ---

        private static T InternalRead<T>(JsonReader reader)
        {
            if (reader.TokenType == JsonToken.Null) return default(T);
            JsonSerializer serializer = new JsonSerializer();
            return serializer.Deserialize<T>(reader);
        }

        // --- IMPLEMENTATIONS DES SIGNATURES ---

        public static global::Kampai.Game.UpsightPromoTrigger ReadUpsightPromoTrigger(JsonReader reader, JsonConverters converters = null) { return InternalRead<global::Kampai.Game.UpsightPromoTrigger>(reader); }
        public static global::Kampai.Util.Logging.Hosted.LogglyConfiguration ReadLogglyConfiguration(JsonReader reader, JsonConverters converters = null) { return InternalRead<global::Kampai.Util.Logging.Hosted.LogglyConfiguration>(reader); }
        public static global::Kampai.Game.CharacterPrestigeLevelDefinition ReadCharacterPrestigeLevelDefinition(JsonReader reader, JsonConverters converters = null) { return InternalRead<global::Kampai.Game.CharacterPrestigeLevelDefinition>(reader); }

        public static global::Kampai.Game.CameraOffset ReadCameraOffset(JsonReader reader, JsonConverters converters = null)
        {
            global::Kampai.Game.CameraOffset res = new global::Kampai.Game.CameraOffset();
            while (reader.Read() && reader.TokenType != JsonToken.EndObject)
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string prop = reader.Value.ToString().ToUpper();
                    reader.Read();
                    if (prop == "X") res.x = SafeParseFloat(reader.Value);
                    else if (prop == "Z") res.z = SafeParseFloat(reader.Value);
                    else if (prop == "ZOOM") res.zoom = SafeParseFloat(reader.Value);
                }
            }
            return res;
        }

        public static Vector3 ReadVector3(JsonReader reader, JsonConverters converters = null)
        {
            Vector3 res = new Vector3();
            if (reader.TokenType != JsonToken.StartObject) return res;
            while (reader.Read() && reader.TokenType != JsonToken.EndObject)
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string prop = reader.Value.ToString().ToUpper();
                    reader.Read();
                    if (prop == "X") res.x = SafeParseFloat(reader.Value);
                    else if (prop == "Y") res.y = SafeParseFloat(reader.Value);
                    else if (prop == "Z") res.z = SafeParseFloat(reader.Value);
                }
            }
            return res;
        }
        public static global::Kampai.Game.SlotUnlock ReadSlotUnlock(JsonReader reader, JsonConverters converters = null) { return InternalRead<global::Kampai.Game.SlotUnlock>(reader); }
        public static MignetteRuleDefinition ReadMignetteRuleDefinition(JsonReader reader, JsonConverters converters = null) { return InternalRead<MignetteRuleDefinition>(reader); }
        public static global::Kampai.Game.MignetteChildObjectDefinition ReadMignetteChildObjectDefinition(JsonReader reader, JsonConverters converters = null) { return InternalRead<global::Kampai.Game.MignetteChildObjectDefinition>(reader); }
        public static global::Kampai.Game.StorageUpgradeDefinition ReadStorageUpgradeDefinition(JsonReader reader, JsonConverters converters = null)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            global::Kampai.Game.StorageUpgradeDefinition res = new global::Kampai.Game.StorageUpgradeDefinition();
            // On ne fait pas de reader.Read() ici car PopulateList l'a déjà fait.
            while (reader.Read() && reader.TokenType != JsonToken.EndObject)
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string prop = reader.Value.ToString().ToUpper();
                    reader.Read(); // Aller sur la valeur
                    if (prop == "LEVEL") res.Level = SafeParseInt(reader.Value);
                    else if (prop == "STORAGECAPACITY") res.StorageCapacity = (uint)SafeParseInt(reader.Value);
                    else if (prop == "TRANSACTIONID") res.TransactionId = SafeParseInt(reader.Value);
                }
            }
            return res;
        }
        public static global::Kampai.Game.Location ReadLocation(JsonReader reader, JsonConverters converters = null)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            global::Kampai.Game.Location res = new global::Kampai.Game.Location();

            // Si on n'est pas sur un objet (ex: si on est sur l'ID directement), on skip.
            if (reader.TokenType != JsonToken.StartObject)
            {
                reader.Skip();
                return res;
            }

            while (reader.Read() && reader.TokenType != JsonToken.EndObject)
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string prop = reader.Value.ToString().ToUpper();
                    reader.Read(); // On va sur la valeur

                    if (prop == "X") res.x = SafeParseInt(reader.Value);
                    else if (prop == "Y") res.y = SafeParseInt(reader.Value);
                    else
                    {
                        // IMPORTANT : Si c'est "ID" ou n'importe quoi d'autre, on skip la valeur
                        reader.Skip();
                    }
                }
            }
            return res;
        }
        public static global::Kampai.Game.CharacterUIAnimationDefinition ReadCharacterUIAnimationDefinition(JsonReader reader, JsonConverters converters = null)
        {
            if (reader.TokenType == JsonToken.Null) return null;

            // SI LE JSON ENVOIE UNE STRING OU UN NOMBRE AU LIEU D'UN OBJET :
            // On le consomme (Skip) et on rend un objet vide pour ne pas crasher.
            if (reader.TokenType != JsonToken.StartObject)
            {
                reader.Skip();
                return new global::Kampai.Game.CharacterUIAnimationDefinition();
            }

            global::Kampai.Game.CharacterUIAnimationDefinition res = new global::Kampai.Game.CharacterUIAnimationDefinition();
            while (reader.Read() && reader.TokenType != JsonToken.EndObject)
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string prop = reader.Value.ToString().ToUpper();
                    reader.Read();
                    if (prop == "STATEMACHINE") res.StateMachine = Convert.ToString(reader.Value);
                    else if (prop == "IDLECOUNT") res.IdleCount = SafeParseInt(reader.Value);
                    else if (prop == "HAPPYCOUNT") res.HappyCount = SafeParseInt(reader.Value);
                    else if (prop == "SELECTEDCOUNT") res.SelectedCount = SafeParseInt(reader.Value);
                    else reader.Skip(); // Sécurité : ignore les champs inconnus (comme un "id" oublié)
                }
            }
            return res;
        }
        public static global::Kampai.Game.FloatLocation ReadFloatLocation(JsonReader reader, JsonConverters converters = null) { return InternalRead<global::Kampai.Game.FloatLocation>(reader); }
        public static global::Kampai.Game.Angle ReadAngle(JsonReader reader, JsonConverters converters = null) { return InternalRead<global::Kampai.Game.Angle>(reader); }
        public static global::Kampai.Game.CollectionReward ReadCollectionReward(JsonReader reader, JsonConverters converters = null) { return InternalRead<global::Kampai.Game.CollectionReward>(reader); }
        public static global::Kampai.Game.PartyDefinition ReadPartyDefinition(JsonReader reader, JsonConverters converters = null) { return InternalRead<global::Kampai.Game.PartyDefinition>(reader); }
        public static global::Kampai.Game.Area ReadArea(JsonReader reader, JsonConverters converters = null) { return InternalRead<global::Kampai.Game.Area>(reader); }
        public static global::Kampai.Game.FlyOverNode ReadFlyOverNode(JsonReader reader, JsonConverters converters = null) { return InternalRead<global::Kampai.Game.FlyOverNode>(reader); }
        public static global::Kampai.Game.BridgeCameraOffset ReadBridgeCameraOffset(JsonReader reader, JsonConverters converters = null) { return InternalRead<global::Kampai.Game.BridgeCameraOffset>(reader); }
        public static global::Kampai.Game.KnuckleheadednessInfo ReadKnuckleheadednessInfo(JsonReader reader, JsonConverters converters = null) { return InternalRead<global::Kampai.Game.KnuckleheadednessInfo>(reader); }
        public static global::Kampai.Game.AnimationAlternate ReadAnimationAlternate(JsonReader reader, JsonConverters converters = null) { return InternalRead<global::Kampai.Game.AnimationAlternate>(reader); }
        public static global::Kampai.Game.Transaction.TransactionInstance ReadTransactionInstance(JsonReader reader, JsonConverters converters = null) { return InternalRead<global::Kampai.Game.Transaction.TransactionInstance>(reader); }
        public static global::Kampai.Game.QuestStepDefinition ReadQuestStepDefinition(JsonReader reader, JsonConverters converters = null) { return InternalRead<global::Kampai.Game.QuestStepDefinition>(reader); }
        public static global::Kampai.Game.QuestChainStepDefinition ReadQuestChainStepDefinition(JsonReader reader, JsonConverters converters = null) { return InternalRead<global::Kampai.Game.QuestChainStepDefinition>(reader); }
        public static global::Kampai.Game.QuestChainTask ReadQuestChainTask(JsonReader reader, JsonConverters converters = null) { return InternalRead<global::Kampai.Game.QuestChainTask>(reader); }
        public static global::Kampai.Game.PlatformStoreSkuDefinition ReadPlatformStoreSkuDefinition(JsonReader reader, JsonConverters converters = null)
        {
            // Si la valeur est null, on renvoie null
            if (reader.TokenType == JsonToken.Null) return null;

            // LE FIX : Si c'est une String (ex: ""), on renvoie null ou un objet vide
            if (reader.TokenType == JsonToken.String)
            {
                // On ignore la valeur chaîne et on renvoie null pour éviter le crash
                return null;
            }

            // Sinon, on laisse Newtonsoft faire son travail habituel pour les Objets
            return InternalRead<global::Kampai.Game.PlatformStoreSkuDefinition>(reader);
        }
        public static global::Kampai.Game.SocialEventOrderDefinition ReadSocialEventOrderDefinition(JsonReader reader, JsonConverters converters = null) { return InternalRead<global::Kampai.Game.SocialEventOrderDefinition>(reader); }
        public static global::Kampai.Game.KampaiPendingTransaction ReadKampaiPendingTransaction(JsonReader reader, JsonConverters converters = null) { return InternalRead<global::Kampai.Game.KampaiPendingTransaction>(reader); }
        public static global::Kampai.Game.UnlockedItem ReadUnlockedItem(JsonReader reader, JsonConverters converters = null) { return InternalRead<global::Kampai.Game.UnlockedItem>(reader); }
        public static global::Kampai.Game.SocialClaimRewardItem ReadSocialClaimRewardItem(JsonReader reader, JsonConverters converters = null) { return InternalRead<global::Kampai.Game.SocialClaimRewardItem>(reader); }
        public static global::Kampai.Game.QuestStep ReadQuestStep(JsonReader reader, JsonConverters converters = null) { return InternalRead<global::Kampai.Game.QuestStep>(reader); }
        public static global::Kampai.Game.OrderBoardTicket ReadOrderBoardTicket(JsonReader reader, JsonConverters converters = null) { return InternalRead<global::Kampai.Game.OrderBoardTicket>(reader); }
        public static global::Kampai.Game.UserIdentity ReadUserIdentity(JsonReader reader, JsonConverters converters = null) { return InternalRead<global::Kampai.Game.UserIdentity>(reader); }
        public static global::Kampai.Game.SocialOrderProgress ReadSocialOrderProgress(JsonReader reader, JsonConverters converters = null) { return InternalRead<global::Kampai.Game.SocialOrderProgress>(reader); }
        public static global::Kampai.Game.GachaConfig ReadGachaConfig(JsonReader reader, JsonConverters converters = null) { return InternalRead<global::Kampai.Game.GachaConfig>(reader); }
        public static global::Kampai.Game.TaskDefinition ReadTaskDefinition(JsonReader reader, JsonConverters converters = null) { return InternalRead<global::Kampai.Game.TaskDefinition>(reader); }
        public static global::Kampai.Splash.BucketAssignment ReadBucketAssignment(JsonReader reader, JsonConverters converters = null) { return InternalRead<global::Kampai.Splash.BucketAssignment>(reader); }

        // --- DANS ReaderUtil.cs ---

        public static List<T> PopulateList<T>(JsonReader reader, JsonConverters converters, Func<JsonReader, JsonConverters, T> elementReader)
        {
            List<T> list = new List<T>();
            if (reader.TokenType == JsonToken.Null) return list;

            // On s'assure d'être au début du tableau
            if (reader.TokenType != JsonToken.StartArray)
            {
                if (!reader.Read()) return list;
                if (reader.TokenType != JsonToken.StartArray) return list;
            }

            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.EndArray) break;

                // LE FIX : On ne déclenche la lecture de l'élément QUE si on est sur un début d'objet.
                // Tout le reste (commentaires, virgules mal interprétées, tokens orphelins) est ignoré.
                if (reader.TokenType == JsonToken.StartObject || reader.TokenType == JsonToken.String || reader.TokenType == JsonToken.Integer || reader.TokenType == JsonToken.Float || reader.TokenType == JsonToken.Boolean)
                {
                    T item = elementReader(reader, converters);
                    if (item != null) list.Add(item);
                }
            }
            return list;
        }

        public static List<T> PopulateList<T>(JsonReader reader, JsonConverters converters, FastJsonConverter<T> converter) where T : class, IFastJSONDeserializable
        {
            List<T> list = new List<T>();
            if (reader.TokenType == JsonToken.Null) return list;

            if (reader.TokenType != JsonToken.StartArray)
            {
                if (!reader.Read()) return list;
                if (reader.TokenType != JsonToken.StartArray) return list;
            }

            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.EndArray) break;

                if (reader.TokenType == JsonToken.StartObject)
                {
                    T item = converter.ReadJson(reader, converters);
                    if (item != null) list.Add(item);
                }
            }
            return list;
        }

        // Version générique simple
        public static List<T> PopulateList<T>(JsonReader reader, JsonConverters converters = null) where T : IFastJSONDeserializable, new()
        {
            List<T> list = new List<T>();
            if (reader.TokenType == JsonToken.Null) return list;

            if (reader.TokenType != JsonToken.StartArray)
            {
                if (!reader.Read()) return list;
                if (reader.TokenType != JsonToken.StartArray) return list;
            }

            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.EndArray) break;

                if (reader.TokenType == JsonToken.StartObject)
                {
                    T item = new T();
                    item.Deserialize(reader, converters);
                    list.Add(item);
                }
            }
            return list;
        }
        public static List<string> PopulateListString(JsonReader reader)
        {
            List<string> list = new List<string>();
            if (reader.TokenType != JsonToken.StartArray) reader.Read();
            while (reader.Read() && reader.TokenType != JsonToken.EndArray)
            {
                if (reader.TokenType == JsonToken.String) list.Add(Convert.ToString(reader.Value));
            }
            return list;
        }
        public static List<int> PopulateListInt32(JsonReader reader)
        {
            List<int> list = new List<int>();
            if (reader.TokenType != JsonToken.StartArray) reader.Read();
            while (reader.Read() && reader.TokenType != JsonToken.EndArray)
            {
                list.Add(SafeParseInt(reader.Value));
            }
            return list;
        }
        // --- DICTIONNAIRES ---

        public static Dictionary<string, object> ReadDictionary(JsonReader reader)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            return JObject.Load(reader).ToObject<Dictionary<string, object>>();
        }
        public static Dictionary<string, string> ReadDictionaryString(JsonReader reader, JsonConverters converters)
        {
            return ReadDictionary<string>(reader, converters, ReadString);
        }
        public static Dictionary<string, T> ReadDictionary<T>(JsonReader reader, JsonConverters converters = null) { return InternalRead<Dictionary<string, T>>(reader); }

        public static Dictionary<string, T> ReadDictionary<T>(JsonReader reader, JsonConverters converters, Func<JsonReader, JsonConverters, T> valueReader)
        {
            Dictionary<string, T> dict = new Dictionary<string, T>();
            while (reader.Read() && reader.TokenType != JsonToken.EndObject)
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string key = reader.Value.ToString();
                    reader.Read();
                    dict.Add(key, valueReader(reader, converters));
                }
            }
            return dict;
        }

        public static Dictionary<K, V> ReadDictionary<K, V>(JsonReader reader, JsonConverters converters, Func<JsonReader, JsonConverters, K> keyReader, Func<JsonReader, JsonConverters, V> valueReader)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            Dictionary<K, V> dict = new Dictionary<K, V>();
            while (reader.Read() && reader.TokenType != JsonToken.EndObject)
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    K key = keyReader(reader, converters);
                    reader.Read();
                    dict.Add(key, valueReader(reader, converters));
                }
            }
            return dict;
        }

        // --- UTILS ---

        public static void EnsureToken(JsonToken token, JsonReader reader)
        {
            if (reader.TokenType != token)
                UnityEngine.Debug.LogWarning("[ReaderUtil] Expected " + token + " but found " + reader.TokenType);
        }

        public static string GetPositionInSource(JsonReader reader)
        {
            JsonTextReader tr = reader as JsonTextReader;
            return (tr != null) ? string.Format("Line: {0}, Pos: {1}", tr.LineNumber, tr.LinePosition) : "N/A";
        }

        public static Dictionary<global::Kampai.Game.ConfigurationDefinition.RateAppAfterEvent, bool> ReadRateAppTriggerConfig(JsonReader reader, JsonConverters converters)
        {
            return ReadDictionary<global::Kampai.Game.ConfigurationDefinition.RateAppAfterEvent, bool>(reader, converters, ReadRateAppAfterEvent, ReadBool);
        }

        public static global::Kampai.Game.ConfigurationDefinition.RateAppAfterEvent ReadRateAppAfterEvent(JsonReader reader, JsonConverters converters) { return ReadEnum<global::Kampai.Game.ConfigurationDefinition.RateAppAfterEvent>(reader); }
        public static global::Kampai.Game.KillSwitch ReadKillSwitch(JsonReader reader, JsonConverters converters) { return ReadEnum<global::Kampai.Game.KillSwitch>(reader); }

        public static T ReaderNotImplemented<T>(JsonReader reader, JsonConverters converters = null)
        {
            reader.Skip();
            return default(T);
        }
    }
}