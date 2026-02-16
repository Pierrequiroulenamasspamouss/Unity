using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Kampai.Util
{
    public static class ReaderUtil
    {
        // --- HELPERS DE SECURITE (Anti-Crash) ---

        public static int SafeParseInt(object value)
        {
            if (value == null) return 0;
            if (value is int || value is long || value is short) return Convert.ToInt32(value);
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

        private static object GetPropertyValue(JsonReader reader)
        {
            if (reader.TokenType == JsonToken.PropertyName) reader.Read();
            return reader.Value;
        }

        // --- IMPLEMENTATIONS DES METHODES ---

        public static global::Kampai.Game.UpsightPromoTrigger ReadUpsightPromoTrigger(JsonReader reader, JsonConverters converters = null)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            if (reader.TokenType != JsonToken.StartObject) { reader.Skip(); return null; }
            var res = new global::Kampai.Game.UpsightPromoTrigger();
            while (reader.Read() && reader.TokenType != JsonToken.EndObject)
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string prop = reader.Value.ToString().ToUpper();
                    object val = GetPropertyValue(reader);
                    if (prop == "PLACEMENT") res.placement = ReadEnum<global::Kampai.Game.UpsightPromoTrigger.Placement>(reader);
                    else if (prop == "ENABLED") res.enabled = Convert.ToBoolean(val);
                }
            }
            return res;
        }

        public static global::Kampai.Util.Logging.Hosted.LogglyConfiguration ReadLogglyConfiguration(JsonReader reader, JsonConverters converters = null)
        {
            if (reader.TokenType != JsonToken.StartObject) { reader.Skip(); return null; }
            var res = new global::Kampai.Util.Logging.Hosted.LogglyConfiguration();
            while (reader.Read() && reader.TokenType != JsonToken.EndObject)
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string prop = reader.Value.ToString().ToUpper();
                    object val = GetPropertyValue(reader);
                    if (prop == "LOGLEVEL") res.logLevel = SafeParseInt(val);
                    else if (prop == "SAMPLEPERCENTAGE") res.samplePercentage = SafeParseInt(val);
                }
            }
            return res;
        }

        public static global::Kampai.Game.CharacterPrestigeLevelDefinition ReadCharacterPrestigeLevelDefinition(JsonReader reader, JsonConverters converters = null)
        {
            if (reader.TokenType != JsonToken.StartObject) { reader.Skip(); return null; }
            var res = new global::Kampai.Game.CharacterPrestigeLevelDefinition();
            while (reader.Read() && reader.TokenType != JsonToken.EndObject)
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string prop = reader.Value.ToString().ToUpper();
                    object val = GetPropertyValue(reader);
                    switch (prop)
                    {
                        case "UNLOCKLEVEL": res.UnlockLevel = (uint)SafeParseInt(val); break;
                        case "UNLOCKQUESTID": res.UnlockQuestID = SafeParseInt(val); break;
                        case "POINTSNEEDED": res.PointsNeeded = (uint)SafeParseInt(val); break;
                        case "ATTACHEDQUESTID": res.AttachedQuestID = SafeParseInt(val); break;
                        case "WELCOMEPANELMESSAGELOCALIZEDKEY": res.WelcomePanelMessageLocalizedKey = Convert.ToString(val); break;
                        case "FAREWELLPANELMESSAGELOCALIZEDKEY": res.FarewellPanelMessageLocalizedKey = Convert.ToString(val); break;
                    }
                }
            }
            return res;
        }

        public static global::Kampai.Game.CameraOffset ReadCameraOffset(JsonReader reader, JsonConverters converters = null)
        {
            if (reader.TokenType != JsonToken.StartObject) { reader.Skip(); return new global::Kampai.Game.CameraOffset(); }
            var res = new global::Kampai.Game.CameraOffset();
            while (reader.Read() && reader.TokenType != JsonToken.EndObject)
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string prop = reader.Value.ToString().ToUpper();
                    object val = GetPropertyValue(reader);
                    if (prop == "X") res.x = SafeParseFloat(val);
                    else if (prop == "Z") res.z = SafeParseFloat(val);
                    else if (prop == "ZOOM") res.zoom = SafeParseFloat(val);
                }
            }
            return res;
        }

        public static Vector3 ReadVector3(JsonReader reader, JsonConverters converters = null)
        {
            if (reader.TokenType != JsonToken.StartObject) { reader.Skip(); return Vector3.zero; }
            var res = new Vector3();
            while (reader.Read() && reader.TokenType != JsonToken.EndObject)
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string prop = reader.Value.ToString().ToUpper();
                    object val = GetPropertyValue(reader);
                    if (prop == "X") res.x = SafeParseFloat(val);
                    else if (prop == "Y") res.y = SafeParseFloat(val);
                    else if (prop == "Z") res.z = SafeParseFloat(val);
                }
            }
            return res;
        }

        public static global::Kampai.Game.SlotUnlock ReadSlotUnlock(JsonReader reader, JsonConverters converters = null)
        {
            if (reader.TokenType != JsonToken.StartObject) { reader.Skip(); return null; }
            var res = new global::Kampai.Game.SlotUnlock();
            while (reader.Read() && reader.TokenType != JsonToken.EndObject)
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string prop = reader.Value.ToString().ToUpper();
                    if (prop == "SLOTUNLOCKLEVELS") { reader.Read(); res.SlotUnlockLevels = PopulateListInt32(reader); }
                    else if (prop == "SLOTUNLOCKCOSTS") { reader.Read(); res.SlotUnlockCosts = PopulateListInt32(reader); }
                }
            }
            return res;
        }

        public static MignetteRuleDefinition ReadMignetteRuleDefinition(JsonReader reader, JsonConverters converters = null)
        {
            if (reader.TokenType != JsonToken.StartObject) { reader.Skip(); return null; }
            var res = new MignetteRuleDefinition();
            while (reader.Read() && reader.TokenType != JsonToken.EndObject)
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string prop = reader.Value.ToString().ToUpper();
                    object val = GetPropertyValue(reader);
                    switch (prop)
                    {
                        case "CAUSEIMAGE": res.CauseImage = Convert.ToString(val); break;
                        case "CAUSEIMAGEMASK": res.CauseImageMask = Convert.ToString(val); break;
                        case "EFFECTIMAGE": res.EffectImage = Convert.ToString(val); break;
                        case "EFFECTIMAGEMASK": res.EffectImageMask = Convert.ToString(val); break;
                        case "EFFECTAMOUNT": res.EffectAmount = SafeParseInt(val); break;
                    }
                }
            }
            return res;
        }

        public static global::Kampai.Game.MignetteChildObjectDefinition ReadMignetteChildObjectDefinition(JsonReader reader, JsonConverters converters = null)
        {
            if (reader.TokenType != JsonToken.StartObject) { reader.Skip(); return null; }
            var res = new global::Kampai.Game.MignetteChildObjectDefinition();
            while (reader.Read() && reader.TokenType != JsonToken.EndObject)
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string prop = reader.Value.ToString().ToUpper();
                    if (prop == "PREFAB") res.Prefab = Convert.ToString(GetPropertyValue(reader));
                    else if (prop == "POSITION") { reader.Read(); res.Position = ReadVector3(reader, converters); }
                    else if (prop == "ISLOCAL") res.IsLocal = Convert.ToBoolean(GetPropertyValue(reader));
                    else if (prop == "ROTATION") res.Rotation = SafeParseFloat(GetPropertyValue(reader));
                }
            }
            return res;
        }

        public static global::Kampai.Game.StorageUpgradeDefinition ReadStorageUpgradeDefinition(JsonReader reader, JsonConverters converters = null)
        {
            if (reader.TokenType != JsonToken.StartObject) { reader.Skip(); return null; }
            var res = new global::Kampai.Game.StorageUpgradeDefinition();
            while (reader.Read() && reader.TokenType != JsonToken.EndObject)
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string prop = reader.Value.ToString().ToUpper();
                    object val = GetPropertyValue(reader);
                    if (prop == "LEVEL") res.Level = SafeParseInt(val);
                    else if (prop == "STORAGECAPACITY") res.StorageCapacity = (uint)SafeParseInt(val);
                    else if (prop == "TRANSACTIONID") res.TransactionId = SafeParseInt(val);
                }
            }
            return res;
        }

        public static global::Kampai.Game.Location ReadLocation(JsonReader reader, JsonConverters converters = null)
        {
            if (reader.TokenType != JsonToken.StartObject) return new global::Kampai.Game.Location();
            var res = new global::Kampai.Game.Location();
            while (reader.Read() && reader.TokenType != JsonToken.EndObject)
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string prop = reader.Value.ToString().ToUpper();
                    object val = GetPropertyValue(reader);
                    if (prop == "X") res.x = SafeParseInt(val);
                    else if (prop == "Y") res.y = SafeParseInt(val);
                }
            }
            return res;
        }

        public static global::Kampai.Game.CharacterUIAnimationDefinition ReadCharacterUIAnimationDefinition(JsonReader reader, JsonConverters converters = null)
        {
            if (reader.TokenType != JsonToken.StartObject) { reader.Skip(); return null; }
            var res = new global::Kampai.Game.CharacterUIAnimationDefinition();
            while (reader.Read() && reader.TokenType != JsonToken.EndObject)
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string prop = reader.Value.ToString().ToUpper();
                    object val = GetPropertyValue(reader);
                    switch (prop)
                    {
                        case "STATEMACHINE": res.StateMachine = Convert.ToString(val); break;
                        case "IDLECOUNT": res.IdleCount = SafeParseInt(val); break;
                        case "HAPPYCOUNT": res.HappyCount = SafeParseInt(val); break;
                        case "SELECTEDCOUNT": res.SelectedCount = SafeParseInt(val); break;
                    }
                }
            }
            return res;
        }

        public static global::Kampai.Game.FloatLocation ReadFloatLocation(JsonReader reader, JsonConverters converters = null)
        {
            if (reader.TokenType != JsonToken.StartObject) { reader.Skip(); return new global::Kampai.Game.FloatLocation(); }
            var res = new global::Kampai.Game.FloatLocation();
            while (reader.Read() && reader.TokenType != JsonToken.EndObject)
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string prop = reader.Value.ToString().ToUpper();
                    object val = GetPropertyValue(reader);
                    if (prop == "X") res.x = SafeParseFloat(val);
                    else if (prop == "Y") res.y = SafeParseFloat(val);
                }
            }
            return res;
        }

        public static global::Kampai.Game.Angle ReadAngle(JsonReader reader, JsonConverters converters = null)
        {
            var res = new global::Kampai.Game.Angle();
            if (reader.TokenType != JsonToken.StartObject) { reader.Skip(); return res; }
            while (reader.Read() && reader.TokenType != JsonToken.EndObject)
            {
                if (reader.TokenType == JsonToken.PropertyName && reader.Value.ToString().ToUpper() == "DEGREES")
                    res.Degrees = SafeParseFloat(GetPropertyValue(reader));
            }
            return res;
        }

        public static global::Kampai.Game.CollectionReward ReadCollectionReward(JsonReader reader, JsonConverters converters = null)
        {
            if (reader.TokenType != JsonToken.StartObject) { reader.Skip(); return null; }
            var res = new global::Kampai.Game.CollectionReward();
            while (reader.Read() && reader.TokenType != JsonToken.EndObject)
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string prop = reader.Value.ToString().ToUpper();
                    object val = GetPropertyValue(reader);
                    if (prop == "REQUIREDPOINTS") res.RequiredPoints = SafeParseInt(val);
                    else if (prop == "TRANSACTIONID") res.TransactionID = SafeParseInt(val);
                }
            }
            return res;
        }

        public static global::Kampai.Game.PartyDefinition ReadPartyDefinition(JsonReader reader, JsonConverters converters = null)
        {
            if (reader.TokenType != JsonToken.StartObject) { reader.Skip(); return null; }
            var res = new global::Kampai.Game.PartyDefinition();
            while (reader.Read() && reader.TokenType != JsonToken.EndObject)
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string prop = reader.Value.ToString().ToUpper();
                    switch (prop)
                    {
                        case "PARTYAREA": reader.Read(); res.PartyArea = ReadArea(reader, converters); break;
                        case "CENTER": reader.Read(); res.Center = ReadLocation(reader, converters); break;
                        case "RADIUS": res.Radius = SafeParseFloat(GetPropertyValue(reader)); break;
                        case "DURATION": res.Duration = SafeParseInt(GetPropertyValue(reader)); break;
                        case "PERCENT": res.Percent = SafeParseFloat(GetPropertyValue(reader)); break;
                        case "STARTANIMATIONS": res.StartAnimations = SafeParseInt(GetPropertyValue(reader)); break;
                    }
                }
            }
            return res;
        }

        public static global::Kampai.Game.Area ReadArea(JsonReader reader, JsonConverters converters = null)
        {
            if (reader.TokenType != JsonToken.StartObject) { reader.Skip(); return new global::Kampai.Game.Area(); }
            var res = new global::Kampai.Game.Area();
            while (reader.Read() && reader.TokenType != JsonToken.EndObject)
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string prop = reader.Value.ToString().ToUpper();
                    if (prop == "A") { reader.Read(); res.a = ReadLocation(reader, converters); }
                    else if (prop == "B") { reader.Read(); res.b = ReadLocation(reader, converters); }
                }
            }
            return res;
        }

        public static global::Kampai.Game.FlyOverNode ReadFlyOverNode(JsonReader reader, JsonConverters converters = null)
        {
            if (reader.TokenType != JsonToken.StartObject) { reader.Skip(); return null; }
            var res = new global::Kampai.Game.FlyOverNode();
            while (reader.Read() && reader.TokenType != JsonToken.EndObject)
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string prop = reader.Value.ToString().ToUpper();
                    object val = GetPropertyValue(reader);
                    if (prop == "X") res.x = SafeParseFloat(val);
                    else if (prop == "Y") res.y = SafeParseFloat(val);
                    else if (prop == "Z") res.z = SafeParseFloat(val);
                }
            }
            return res;
        }

        public static global::Kampai.Game.BridgeCameraOffset ReadBridgeCameraOffset(JsonReader reader, JsonConverters converters = null)
        {
            var res = new global::Kampai.Game.BridgeCameraOffset();
            if (reader.TokenType != JsonToken.StartObject) { reader.Skip(); return res; }
            while (reader.Read() && reader.TokenType != JsonToken.EndObject)
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string prop = reader.Value.ToString().ToUpper();
                    object val = GetPropertyValue(reader);
                    if (prop == "X") res.x = SafeParseFloat(val);
                    else if (prop == "Y") res.y = SafeParseFloat(val);
                    else if (prop == "Z") res.z = SafeParseFloat(val);
                    else if (prop == "ZOOM") res.zoom = SafeParseFloat(val);
                }
            }
            return res;
        }

        public static global::Kampai.Game.KnuckleheadednessInfo ReadKnuckleheadednessInfo(JsonReader reader, JsonConverters converters = null)
        {
            if (reader.TokenType != JsonToken.StartObject) { reader.Skip(); return null; }
            var res = new global::Kampai.Game.KnuckleheadednessInfo();
            while (reader.Read() && reader.TokenType != JsonToken.EndObject)
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string prop = reader.Value.ToString().ToUpper();
                    object val = GetPropertyValue(reader);
                    if (prop == "KNUCKLEHEADDEDNESSMIN") res.KnuckleheaddednessMin = SafeParseFloat(val);
                    else if (prop == "KNUCKLEHEADDEDNESSMAX") res.KnuckleheaddednessMax = SafeParseFloat(val);
                    else if (prop == "KNUCKLEHEADDEDNESSSCALE") res.KnuckleheaddednessScale = SafeParseFloat(val);
                }
            }
            return res;
        }

        public static global::Kampai.Game.AnimationAlternate ReadAnimationAlternate(JsonReader reader, JsonConverters converters = null)
        {
            if (reader.TokenType != JsonToken.StartObject) { reader.Skip(); return null; }
            var res = new global::Kampai.Game.AnimationAlternate();
            while (reader.Read() && reader.TokenType != JsonToken.EndObject)
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string prop = reader.Value.ToString().ToUpper();
                    object val = GetPropertyValue(reader);
                    if (prop == "GROUPID") res.GroupID = SafeParseInt(val);
                    else if (prop == "PERCENTCHANCE") res.PercentChance = SafeParseFloat(val);
                }
            }
            return res;
        }

        public static global::Kampai.Game.Transaction.TransactionInstance ReadTransactionInstance(JsonReader reader, JsonConverters converters = null)
        {
            if (reader.TokenType != JsonToken.StartObject) { reader.Skip(); return null; }
            var res = new global::Kampai.Game.Transaction.TransactionInstance();
            while (reader.Read() && reader.TokenType != JsonToken.EndObject)
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string prop = reader.Value.ToString().ToUpper();
                    if (prop == "ID") res.ID = SafeParseInt(GetPropertyValue(reader));
                    else if (prop == "INPUTS") { reader.Read(); res.Inputs = PopulateList<global::Kampai.Util.QuantityItem>(reader, converters); }
                    else if (prop == "OUTPUTS") { reader.Read(); res.Outputs = PopulateList<global::Kampai.Util.QuantityItem>(reader, converters); }
                }
            }
            return res;
        }

        public static global::Kampai.Game.QuestStepDefinition ReadQuestStepDefinition(JsonReader reader, JsonConverters converters = null)
        {
            if (reader.TokenType != JsonToken.StartObject) { reader.Skip(); return null; }
            var res = new global::Kampai.Game.QuestStepDefinition();
            while (reader.Read() && reader.TokenType != JsonToken.EndObject)
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string prop = reader.Value.ToString().ToUpper();
                    object val = GetPropertyValue(reader);
                    switch (prop)
                    {
                        case "TYPE": res.Type = ReadEnum<global::Kampai.Game.QuestStepType>(reader); break;
                        case "ITEMAMOUNT": res.ItemAmount = SafeParseInt(val); break;
                        case "ITEMDEFINITIONID": res.ItemDefinitionID = SafeParseInt(val); break;
                        case "COSTUMEDEFINITIONID": res.CostumeDefinitionID = SafeParseInt(val); break;
                        case "SHOWWAYFINDER": res.ShowWayfinder = Convert.ToBoolean(val); break;
                    }
                }
            }
            return res;
        }

        public static global::Kampai.Game.QuestChainStepDefinition ReadQuestChainStepDefinition(JsonReader reader, JsonConverters converters = null)
        {
            if (reader.TokenType != JsonToken.StartObject) { reader.Skip(); return null; }
            var res = new global::Kampai.Game.QuestChainStepDefinition();
            while (reader.Read() && reader.TokenType != JsonToken.EndObject)
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string prop = reader.Value.ToString().ToUpper();
                    switch (prop)
                    {
                        case "INTRO": res.Intro = Convert.ToString(GetPropertyValue(reader)); break;
                        case "VOICE": res.Voice = Convert.ToString(GetPropertyValue(reader)); break;
                        case "OUTRO": res.Outro = Convert.ToString(GetPropertyValue(reader)); break;
                        case "XP": res.XP = SafeParseInt(GetPropertyValue(reader)); break;
                        case "GRIND": res.Grind = SafeParseInt(GetPropertyValue(reader)); break;
                        case "PREMIUM": res.Premium = SafeParseInt(GetPropertyValue(reader)); break;
                        case "TASKS": reader.Read(); res.Tasks = PopulateList<global::Kampai.Game.QuestChainTask>(reader, converters, ReadQuestChainTask); break;
                    }
                }
            }
            return res;
        }

        public static global::Kampai.Game.QuestChainTask ReadQuestChainTask(JsonReader reader, JsonConverters converters = null)
        {
            if (reader.TokenType != JsonToken.StartObject) { reader.Skip(); return null; }
            var res = new global::Kampai.Game.QuestChainTask();
            while (reader.Read() && reader.TokenType != JsonToken.EndObject)
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string prop = reader.Value.ToString().ToUpper();
                    object val = GetPropertyValue(reader);
                    if (prop == "TYPE") res.Type = ReadEnum<global::Kampai.Game.QuestChainTaskType>(reader);
                    else if (prop == "ITEM") res.Item = SafeParseInt(val);
                    else if (prop == "COUNT") res.Count = SafeParseInt(val);
                }
            }
            return res;
        }

        public static global::Kampai.Game.PlatformStoreSkuDefinition ReadPlatformStoreSkuDefinition(JsonReader reader, JsonConverters converters = null)
        {
            if (reader.TokenType != JsonToken.StartObject) { reader.Skip(); return null; }
            var res = new global::Kampai.Game.PlatformStoreSkuDefinition();
            while (reader.Read() && reader.TokenType != JsonToken.EndObject)
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string prop = reader.Value.ToString().ToUpper();
                    object val = GetPropertyValue(reader);
                    if (prop == "APPLEAPPSTORE") res.appleAppstore = Convert.ToString(val);
                    else if (prop == "GOOGLEPLAY") res.googlePlay = Convert.ToString(val);
                    else if (prop == "DEFAULTSTORE") res.defaultStore = Convert.ToString(val);
                }
            }
            return res;
        }

        public static global::Kampai.Game.SocialEventOrderDefinition ReadSocialEventOrderDefinition(JsonReader reader, JsonConverters converters = null)
        {
            if (reader.TokenType != JsonToken.StartObject) { reader.Skip(); return null; }
            var res = new global::Kampai.Game.SocialEventOrderDefinition();
            while (reader.Read() && reader.TokenType != JsonToken.EndObject)
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string prop = reader.Value.ToString().ToUpper();
                    object val = GetPropertyValue(reader);
                    if (prop == "ORDERID") res.OrderID = SafeParseInt(val);
                    else if (prop == "TRANSACTION") res.Transaction = SafeParseInt(val);
                }
            }
            return res;
        }

        public static global::Kampai.Game.KampaiPendingTransaction ReadKampaiPendingTransaction(JsonReader reader, JsonConverters converters = null)
        {
            if (reader.TokenType != JsonToken.StartObject) { reader.Skip(); return null; }
            var res = new global::Kampai.Game.KampaiPendingTransaction();
            while (reader.Read() && reader.TokenType != JsonToken.EndObject)
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string prop = reader.Value.ToString().ToUpper();
                    if (prop == "EXTERNALIDENTIFIER") res.ExternalIdentifier = Convert.ToString(GetPropertyValue(reader));
                    else if (prop == "TRANSACTION") res.Transaction = ((converters.transactionDefinitionConverter == null) ? global::Kampai.Util.FastJSONDeserializer.Deserialize<global::Kampai.Game.Transaction.TransactionDefinition>(reader, converters) : converters.transactionDefinitionConverter.ReadJson(reader, converters));
                    else if (prop == "STOREITEMDEFINITIONID") res.StoreItemDefinitionId = SafeParseInt(GetPropertyValue(reader));
                    else if (prop == "UTCTIMECREATED") res.UTCTimeCreated = SafeParseInt(GetPropertyValue(reader));
                }
            }
            return res;
        }

        public static global::Kampai.Game.UnlockedItem ReadUnlockedItem(JsonReader reader, JsonConverters converters = null)
        {
            if (reader.TokenType != JsonToken.StartObject) { reader.Skip(); return null; }
            var res = new global::Kampai.Game.UnlockedItem();
            while (reader.Read() && reader.TokenType != JsonToken.EndObject)
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string prop = reader.Value.ToString().ToUpper();
                    object val = GetPropertyValue(reader);
                    if (prop == "DEFID") res.defID = SafeParseInt(val);
                    else if (prop == "QUANTITY") res.quantity = SafeParseInt(val);
                }
            }
            return res;
        }

        public static global::Kampai.Game.SocialClaimRewardItem ReadSocialClaimRewardItem(JsonReader reader, JsonConverters converters = null)
        {
            if (reader.TokenType != JsonToken.StartObject) { reader.Skip(); return null; }
            var res = new global::Kampai.Game.SocialClaimRewardItem();
            while (reader.Read() && reader.TokenType != JsonToken.EndObject)
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string prop = reader.Value.ToString().ToUpper();
                    object val = GetPropertyValue(reader);
                    if (prop == "EVENTID") res.eventID = SafeParseInt(val);
                    else if (prop == "CLAIMSTATE") res.claimState = ReadEnum<global::Kampai.Game.SocialClaimRewardItem.ClaimState>(reader);
                }
            }
            return res;
        }

        public static global::Kampai.Game.QuestStep ReadQuestStep(JsonReader reader, JsonConverters converters = null)
        {
            if (reader.TokenType != JsonToken.StartObject) { reader.Skip(); return null; }
            var res = new global::Kampai.Game.QuestStep();
            while (reader.Read() && reader.TokenType != JsonToken.EndObject)
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string prop = reader.Value.ToString().ToUpper();
                    object val = GetPropertyValue(reader);
                    switch (prop)
                    {
                        case "STATE": res.state = ReadEnum<global::Kampai.Game.QuestStepState>(reader); break;
                        case "AMOUNTCOMPLETED": res.AmountCompleted = SafeParseInt(val); break;
                        case "AMOUNTREADY": res.AmountReady = SafeParseInt(val); break;
                        case "TRACKEDID": res.TrackedID = SafeParseInt(val); break;
                    }
                }
            }
            return res;
        }

        public static global::Kampai.Game.OrderBoardTicket ReadOrderBoardTicket(JsonReader reader, JsonConverters converters = null)
        {
            if (reader.TokenType != JsonToken.StartObject) { reader.Skip(); return null; }
            var res = new global::Kampai.Game.OrderBoardTicket();
            while (reader.Read() && reader.TokenType != JsonToken.EndObject)
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string prop = reader.Value.ToString().ToUpper();
                    switch (prop)
                    {
                        case "TRANSACTIONINST": reader.Read(); res.TransactionInst = ReadTransactionInstance(reader, converters); break;
                        case "BOARDINDEX": res.BoardIndex = SafeParseInt(GetPropertyValue(reader)); break;
                        case "ORDERNAMETABLEINDEX": res.OrderNameTableIndex = SafeParseInt(GetPropertyValue(reader)); break;
                        case "STARTTIME": res.StartTime = SafeParseInt(GetPropertyValue(reader)); break;
                        case "CHARACTERDEFINITIONID": res.CharacterDefinitionId = SafeParseInt(GetPropertyValue(reader)); break;
                        case "DIFFICULTY": res.Difficulty = ReadEnum<global::Kampai.Game.OrderBoardTicketDifficulty>(reader); break;
                    }
                }
            }
            return res;
        }

        public static global::Kampai.Game.UserIdentity ReadUserIdentity(JsonReader reader, JsonConverters converters = null)
        {
            if (reader.TokenType != JsonToken.StartObject) { reader.Skip(); return null; }
            var res = new global::Kampai.Game.UserIdentity();
            while (reader.Read() && reader.TokenType != JsonToken.EndObject)
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string prop = reader.Value.ToString();
                    object val = GetPropertyValue(reader);
                    switch (prop)
                    {
                        case "id": res.ID = Convert.ToString(val); break;
                        case "externalId": res.ExternalID = Convert.ToString(val); break;
                        case "userId": res.UserID = Convert.ToString(val); break;
                        case "type": res.Type = ReadEnum<global::Kampai.Game.IdentityType>(reader); break;
                    }
                }
            }
            return res;
        }

        public static global::Kampai.Game.SocialOrderProgress ReadSocialOrderProgress(JsonReader reader, JsonConverters converters = null)
        {
            if (reader.TokenType != JsonToken.StartObject) { reader.Skip(); return null; }
            var res = new global::Kampai.Game.SocialOrderProgress();
            while (reader.Read() && reader.TokenType != JsonToken.EndObject)
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string prop = reader.Value.ToString().ToUpper();
                    object val = GetPropertyValue(reader);
                    if (prop == "ORDERID") res.OrderId = SafeParseInt(val);
                    else if (prop == "COMPLETEDBYUSERID") res.CompletedByUserId = Convert.ToString(val);
                }
            }
            return res;
        }

        public static global::Kampai.Game.GachaConfig ReadGachaConfig(JsonReader reader, JsonConverters converters = null)
        {
            if (reader.TokenType != JsonToken.StartObject) { reader.Skip(); return null; }
            var res = new global::Kampai.Game.GachaConfig();
            while (reader.Read() && reader.TokenType != JsonToken.EndObject)
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string prop = reader.Value.ToString().ToUpper();
                    if (prop == "GATCHAANIMATIONDEFINITIONS") { reader.Read(); res.GatchaAnimationDefinitions = PopulateList<global::Kampai.Game.GachaAnimationDefinition>(reader, converters); }
                    else if (prop == "DISTRIBUTIONTABLES") { reader.Read(); res.DistributionTables = PopulateList<global::Kampai.Game.GachaWeightedDefinition>(reader, converters); }
                }
            }
            return res;
        }

        public static global::Kampai.Game.TaskDefinition ReadTaskDefinition(JsonReader reader, JsonConverters converters = null)
        {
            if (reader.TokenType != JsonToken.StartObject) { reader.Skip(); return null; }
            var res = new global::Kampai.Game.TaskDefinition();
            while (reader.Read() && reader.TokenType != JsonToken.EndObject)
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string prop = reader.Value.ToString().ToUpper();
                    if (prop == "LEVELBANDS") { reader.Read(); res.levelBands = PopulateList<global::Kampai.Game.TaskLevelBandDefinition>(reader, converters); }
                }
            }
            return res;
        }

        public static global::Kampai.Splash.BucketAssignment ReadBucketAssignment(JsonReader reader, JsonConverters converters = null)
        {
            if (reader.TokenType != JsonToken.StartObject) { reader.Skip(); return null; }
            var res = new global::Kampai.Splash.BucketAssignment();
            while (reader.Read() && reader.TokenType != JsonToken.EndObject)
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string prop = reader.Value.ToString().ToUpper();
                    object val = GetPropertyValue(reader);
                    if (prop == "BUCKETID") res.BucketId = SafeParseInt(val);
                    else if (prop == "TIME") res.Time = SafeParseFloat(val);
                }
            }
            return res;
        }

        // --- POPULATE LIST ---

        public static global::System.Collections.Generic.List<T> PopulateList<T>(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters, global::Kampai.Util.FastJsonConverter<T> converter) where T : class, global::Kampai.Util.IFastJSONDeserializable
        {
            global::System.Collections.Generic.List<T> list = new global::System.Collections.Generic.List<T>();
            if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null || reader.TokenType == global::Newtonsoft.Json.JsonToken.String)
            {
                return list;
            }

            // Si on n'est pas sur le début du tableau, on tente d'avancer (cas du PropertyName)
            if (reader.TokenType != global::Newtonsoft.Json.JsonToken.StartArray) { reader.Read(); }
            if (reader.TokenType != global::Newtonsoft.Json.JsonToken.StartArray) return list;

            while (reader.Read())
            {
                if (reader.TokenType == global::Newtonsoft.Json.JsonToken.EndArray) return list;
                if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Comment) continue;

                T item = converter.ReadJson(reader, converters);
                list.Add(item);
            }
            return list;
        }

        public static List<T> PopulateList<T>(JsonReader reader, JsonConverters converters, global::Kampai.Util.FastJsonConverter<T> converter) where T : class, global::Kampai.Util.IFastJSONDeserializable
        {
            var list = new List<T>();
            if (reader.TokenType == JsonToken.Null || reader.TokenType == JsonToken.String) return list;
            if (reader.TokenType != JsonToken.StartArray) reader.Read();
            if (reader.TokenType != JsonToken.StartArray) return list;
            while (reader.Read() && reader.TokenType != JsonToken.EndArray)
            {
                if (reader.TokenType == JsonToken.Comment) continue;
                list.Add(converter.ReadJson(reader, converters));
            }
            return list;
        }

        public static List<T> PopulateList<T>(JsonReader reader, JsonConverters converters = null) where T : global::Kampai.Util.IFastJSONDeserializable, new()
        {
            var list = new List<T>();
            if (reader.TokenType == JsonToken.Null || reader.TokenType == JsonToken.String) return list;
            if (reader.TokenType != JsonToken.StartArray) reader.Read();
            if (reader.TokenType != JsonToken.StartArray) return list;
            while (reader.Read() && reader.TokenType != JsonToken.EndArray)
            {
                if (reader.TokenType == JsonToken.Comment) continue;
                T item = new T();
                item.Deserialize(reader, converters);
                list.Add(item);
            }
            return list;
        }

        public static List<string> PopulateListString(JsonReader reader)
        {
            var list = new List<string>();
            if (reader.TokenType == JsonToken.Null) return list;
            if (reader.TokenType != JsonToken.StartArray) reader.Read();
            if (reader.TokenType != JsonToken.StartArray) return list;
            while (reader.Read() && reader.TokenType != JsonToken.EndArray)
            {
                if (reader.TokenType == JsonToken.String) list.Add(Convert.ToString(reader.Value));
            }
            return list;
        }

        public static List<int> PopulateListInt32(JsonReader reader)
        {
            var list = new List<int>();
            if (reader.TokenType == JsonToken.Null || reader.TokenType == JsonToken.String) return list;
            if (reader.TokenType != JsonToken.StartArray) reader.Read();
            if (reader.TokenType != JsonToken.StartArray) return list;
            while (reader.Read() && reader.TokenType != JsonToken.EndArray)
            {
                list.Add(SafeParseInt(reader.Value));
            }
            return list;
        }

        // --- BASIQUES ---

        public static string ReadString(JsonReader reader, JsonConverters converters) { return Convert.ToString(reader.Value); }
        public static bool ReadBool(JsonReader reader, JsonConverters converters) { return Convert.ToBoolean(reader.Value); }
        public static Dictionary<string, string> ReadDictionaryString(JsonReader reader, JsonConverters converters) { return ReadDictionary<string>(reader, converters, ReadString); }

        public static T ReadEnum<T>(JsonReader reader)
        {
            try
            {
                if (reader.TokenType == JsonToken.Integer) return (T)Enum.ToObject(typeof(T), reader.Value);
                return (T)Enum.Parse(typeof(T), Convert.ToString(reader.Value), true);
            }
            catch { return default(T); }
        }

        public static Dictionary<string, object> ReadDictionary(JsonReader reader)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var jo = global::Newtonsoft.Json.Linq.JObject.Load(reader);
            return jo.ToObject<Dictionary<string, object>>();
        }

        public static Dictionary<string, T> ReadDictionary<T>(JsonReader reader, JsonConverters converters = null) where T : global::Kampai.Util.IFastJSONDeserializable, new()
        {
            var dict = new Dictionary<string, T>();
            if (reader.TokenType != JsonToken.StartObject) return dict;
            while (reader.Read() && reader.TokenType != JsonToken.EndObject)
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string key = reader.Value.ToString();
                    reader.Read();
                    T val = global::Kampai.Util.FastJSONDeserializer.Deserialize<T>(reader, converters);
                    dict.Add(key, val);
                }
            }
            return dict;
        }

        public static Dictionary<string, T> ReadDictionary<T>(JsonReader reader, JsonConverters converters, Func<JsonReader, JsonConverters, T> valueReader)
        {
            var dict = new Dictionary<string, T>();
            if (reader.TokenType != JsonToken.StartObject) return dict;
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
            var dict = new Dictionary<K, V>();
            if (reader.TokenType != JsonToken.StartObject) return dict;
            while (reader.Read() && reader.TokenType != JsonToken.EndObject)
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    K key = keyReader(reader, converters);
                    reader.Read();
                    V val = valueReader(reader, converters);
                    dict.Add(key, val);
                }
            }
            return dict;
        }

        public static void EnsureToken(global::Newtonsoft.Json.JsonToken token, global::Newtonsoft.Json.JsonReader reader)
        {
            // On log l'erreur au lieu de lancer une exception pour ne pas bloquer le chargement
            if (reader.TokenType != token)
            {
                UnityEngine.Debug.LogWarning("[ReaderUtil] Expected " + token + " but found " + reader.TokenType + " at " + GetPositionInSource(reader));
            }
        }

        public static T ReaderNotImplemented<T>(JsonReader reader, JsonConverters converters = null)
        {
            reader.Skip();
            return default(T);
        }

        public static string GetPositionInSource(JsonReader reader)
        {
            JsonTextReader tr = reader as JsonTextReader;
            if (tr != null) return "Line: " + tr.LineNumber + ", Pos: " + tr.LinePosition;
            return "N/A";
        }

        public static Dictionary<global::Kampai.Game.ConfigurationDefinition.RateAppAfterEvent, bool> ReadRateAppTriggerConfig(JsonReader reader, JsonConverters converters) { return ReadDictionary<global::Kampai.Game.ConfigurationDefinition.RateAppAfterEvent, bool>(reader, converters, ReadRateAppAfterEvent, ReadBool); }
        public static global::Kampai.Game.ConfigurationDefinition.RateAppAfterEvent ReadRateAppAfterEvent(JsonReader reader, JsonConverters converters) { return ReadEnum<global::Kampai.Game.ConfigurationDefinition.RateAppAfterEvent>(reader); }
        public static global::Kampai.Game.KillSwitch ReadKillSwitch(JsonReader reader, JsonConverters converters) { return ReadEnum<global::Kampai.Game.KillSwitch>(reader); }
    }
}