using System;
using System.Collections.Generic;
//using Newtonsoft.Json; // NOT USING NEWTONSOFT FOR THE MAIN DESERIALIZATION STEP TO AVOID TEXT PARSING BUGS
using Newtonsoft.Json.Linq;
using Kampai.Util;

namespace Kampai.Game
{
    internal abstract class DefaultPlayerSerializer : IPlayerSerializer
    {
        public abstract int Version { get; }

        private PlayerData CreateEmptyPlayerData()
        {
            return new PlayerData
            {
                ID = 1001,
                version = 3,
                nextId = 1000,
                inventory = new List<Instance>(),
                unlocks = new List<UnlockedItem>(),
                villainQueue = new List<int>(),
                pendingTransactions = new List<KampaiPendingTransaction>(),
                socialRewards = new List<SocialClaimRewardItem>(),
                PlatformStoreTransactionIDs = new List<string>()
            };
        }

        /// <summary>
        /// Converts an object produced by FastJsonParser (Dictionary/List/primitive)
        /// into a Newtonsoft JToken so that the existing InventoryFastConverter and
        /// other Newtonsoft-dependent code can work unchanged.
        /// </summary>
        private static JToken DictToJToken(object obj)
        {
            if (obj == null)
                return JValue.CreateString(null);

            Dictionary<string, object> dict = obj as Dictionary<string, object>;
            if (dict != null)
            {
                JObject jo = new JObject();
                foreach (KeyValuePair<string, object> kvp in dict)
                    jo[kvp.Key] = DictToJToken(kvp.Value);
                return jo;
            }

            List<object> list = obj as List<object>;
            if (list != null)
            {
                JArray ja = new JArray();
                foreach (object item in list)
                    ja.Add(DictToJToken(item));
                return ja;
            }

            // primitives (long, double, bool, string)
            return new JValue(obj);
        }

        protected virtual PlayerData GeneratePlayerData(
            string serialized,
            IDefinitionService definitionService,
            ILogger logger)
        {
            // Always return a valid PlayerData
            PlayerData playerData = CreateEmptyPlayerData();

            if (string.IsNullOrEmpty(serialized))
                return playerData;

            serialized = serialized.Trim();

            try
            {
                logger.Info(
                    serialized.Length > 200
                        ? "[DefaultPlayerSerializer] Input JSON (len=" + serialized.Length + "): " +
                          serialized.Substring(0, 100) + " ... " +
                          serialized.Substring(serialized.Length - 20)
                        : "[DefaultPlayerSerializer] Input JSON: " + serialized
                );

                // Step 1: Use FastJsonParser for lenient parsing (survives missing commas, etc.)
                object parsedObj = FastJsonParser.Deserialize(serialized);
                if (parsedObj == null)
                    return playerData;

                var rawDict = parsedObj as Dictionary<string, object>;
                if (rawDict == null)
                    return playerData;

                // Step 2: Convert the Dictionary tree to a JObject so the existing
                //         converters (InventoryFastConverter etc.) work unchanged.
                JObject root = DictToJToken(rawDict) as JObject;
                if (root == null)
                    return playerData;

                // Validate Root Integrity: Must have inventory
                if (root["inventory"] == null)
                {
                    logger.Error("[DefaultPlayerSerializer] Invalid Root Object: Missing 'inventory' key. Returning empty player.");
                    return CreateEmptyPlayerData();
                }

                // ---- primitives ----
                playerData.ID = SafeGetLong(root, "ID");
                playerData.version = SafeGetInt(root, "version");
                playerData.nextId = SafeGetInt(root, "nextId");
                playerData.lastLevelUpTime = SafeGetInt(root, "lastLevelUpTime");
                playerData.lastGameStartTime = SafeGetInt(root, "lastGameStartTime");
                playerData.totalGameplayDurationSinceLastLevelUp =
                    SafeGetInt(root, "totalGameplayDurationSinceLastLevelUp");
                playerData.targetExpansionID = SafeGetInt(root, "targetExpansionID");
                playerData.freezeTime = SafeGetInt(root, "freezeTime");
                playerData.highestFtueLevel = SafeGetInt(root, "highestFtueLevel");

                // ---- inventory (uses InventoryFastConverter which needs JObject/JsonReader) ----
                playerData.inventory = new List<Instance>();
                JArray inventory = root["inventory"] as JArray;
                if (inventory != null)
                {
                    JsonConverters converters = new JsonConverters();
                    converters.instanceConverter =
                        new InventoryFastConverter(definitionService, logger);

                    for (int i = 0; i < inventory.Count; i++)
                    {
                        try
                        {
                            Instance inst = converters.instanceConverter.ReadJson(
                                inventory[i].CreateReader(), converters);
                            if (inst != null)
                                playerData.inventory.Add(inst);
                        }
                        catch (Exception invEx)
                        {
                            logger.Error(
                                "[DefaultPlayerSerializer] Failed to parse inventory item {0}: {1}",
                                i.ToString(), invEx.Message);
                        }
                    }
                }

                // ---- unlocks ----
                playerData.unlocks = new List<UnlockedItem>();
                JArray unlocks = root["unlocks"] as JArray;
                if (unlocks != null)
                {
                    for (int i = 0; i < unlocks.Count; i++)
                    {
                        try
                        {
                            playerData.unlocks.Add(
                                new UnlockedItem(
                                    (int)unlocks[i]["defID"],
                                    (int)unlocks[i]["quantity"]
                                )
                            );
                        }
                        catch (Exception unlockEx)
                        {
                            logger.Error(
                                "[DefaultPlayerSerializer] Failed to parse unlock item {0}: {1}",
                                i.ToString(), unlockEx.Message);
                        }
                    }
                }

                // ---- villain queue ----
                playerData.villainQueue = new List<int>();
                JArray queue = root["villainQueue"] as JArray;
                if (queue != null)
                {
                    for (int i = 0; i < queue.Count; i++)
                    {
                        int val = 0;
                        if (int.TryParse(queue[i].ToString(), out val))
                            playerData.villainQueue.Add(val);
                    }
                }

                // ---- pending transactions ----
                playerData.pendingTransactions = new List<KampaiPendingTransaction>();
                JArray ptArray = root["pendingTransactions"] as JArray;
                if (ptArray != null)
                {
                    for (int i = 0; i < ptArray.Count; i++)
                    {
                        try
                        {
                            KampaiPendingTransaction pt =
                                ptArray[i].ToObject<KampaiPendingTransaction>();
                            if (pt != null)
                                playerData.pendingTransactions.Add(pt);
                        }
                        catch (Exception ptEx)
                        {
                            logger.Error(
                                "[DefaultPlayerSerializer] Failed to parse pendingTransaction {0}: {1}",
                                i.ToString(), ptEx.Message);
                        }
                    }
                }

                // ---- social rewards ----
                playerData.socialRewards = new List<SocialClaimRewardItem>();
                JArray srArray = root["socialRewards"] as JArray;
                if (srArray != null)
                {
                    for (int i = 0; i < srArray.Count; i++)
                    {
                        try
                        {
                            int eventID = (int)srArray[i]["eventID"];
                            int claimState = (int)srArray[i]["claimState"];
                            playerData.socialRewards.Add(
                                new SocialClaimRewardItem(eventID, (SocialClaimRewardItem.ClaimState)claimState));
                        }
                        catch (Exception srEx)
                        {
                            logger.Error(
                                "[DefaultPlayerSerializer] Failed to parse socialReward {0}: {1}",
                                i.ToString(), srEx.Message);
                        }
                    }
                }

                // ---- platform store transaction IDs ----
                playerData.PlatformStoreTransactionIDs = new List<string>();
                JArray pstArray = root["PlatformStoreTransactionIDs"] as JArray;
                if (pstArray != null)
                {
                    for (int i = 0; i < pstArray.Count; i++)
                    {
                        string tid = pstArray[i] != null ? pstArray[i].ToString() : null;
                        if (tid != null)
                            playerData.PlatformStoreTransactionIDs.Add(tid);
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error(
                    "JSON PARSE FAILED. Raw:\n{0}\nException: {1}",
                    serialized,
                    e.ToString()
                );
                return CreateEmptyPlayerData();
            }

            return playerData;
        }


        public virtual Player Deserialize(string json, IDefinitionService definitionService, ILogger logger)
        {
            PlayerData playerData = GeneratePlayerData(json, definitionService, logger);
            Player player = new Player(definitionService, logger);

            player.ID = playerData.ID;
            player.Version = playerData.version;
            player.nextId = playerData.nextId;
            player.lastLevelUpTime = playerData.lastLevelUpTime;
            player.lastGameStartTime = playerData.lastGameStartTime;
            player.totalGameplayDurationSinceLastLevelUp = playerData.totalGameplayDurationSinceLastLevelUp;
            player.targetExpansionID = playerData.targetExpansionID;
            player.freezeTime = playerData.freezeTime;
            player.HighestFtueLevel = playerData.highestFtueLevel;

            if (playerData.inventory == null)
            {
                throw new FatalException(FatalCode.PS_JSON_PARSE_ERR, 1, "Player inventory is null");
            }

            for (int i = 0; i < playerData.inventory.Count; i++)
            {
                player.Add(playerData.inventory[i]);
            }

            if (playerData.pendingTransactions != null)
            {
                player.AddPendingTransactions(playerData.pendingTransactions);
            }

            if (playerData.unlocks != null)
            {
                for (int i = 0; i < playerData.unlocks.Count; i++)
                {
                    UnlockedItem unlock = playerData.unlocks[i];
                    player.AddUnlock(unlock.defID, unlock.quantity);
                }
            }

            if (playerData.socialRewards != null)
            {
                for (int i = 0; i < playerData.socialRewards.Count; i++)
                {
                    SocialClaimRewardItem reward = playerData.socialRewards[i];
                    player.AddSocialClaimRewards(reward.eventID, reward.claimState);
                }
            }

            if (playerData.villainQueue != null)
            {
                for (int i = 0; i < playerData.villainQueue.Count; i++)
                {
                    player.AddVillainQueue(playerData.villainQueue[i]);
                }
            }

            if (playerData.PlatformStoreTransactionIDs != null)
            {
                for (int i = 0; i < playerData.PlatformStoreTransactionIDs.Count; i++)
                {
                    player.AddPlatformStoreTransactionID(playerData.PlatformStoreTransactionIDs[i]);
                }
            }

            return player;
        }

        public virtual byte[] Serialize(Player player, IDefinitionService definitionService, ILogger logger)
        {
            PlayerData playerData = new PlayerData();
            playerData.ID = player.ID;
            playerData.version = player.Version;
            playerData.nextId = player.NextId;
            playerData.inventory = new List<Instance>();
            playerData.unlocks = new List<UnlockedItem>();
            playerData.villainQueue = new List<int>();
            playerData.lastLevelUpTime = player.lastLevelUpTime;
            playerData.lastGameStartTime = player.lastGameStartTime;
            playerData.totalGameplayDurationSinceLastLevelUp = player.totalGameplayDurationSinceLastLevelUp;
            playerData.targetExpansionID = player.targetExpansionID;
            playerData.freezeTime = player.freezeTime;
            playerData.highestFtueLevel = player.HighestFtueLevel;
            playerData.socialRewards = new List<SocialClaimRewardItem>();
            playerData.PlatformStoreTransactionIDs = new List<string>();

            SerializeCollections(playerData, player);
            return FastJSONSerializer.SerializeUTF8(playerData);
        }

        private void SerializeCollections(PlayerData playerData, Player player)
        {
            IList<Instance> instances = player.GetInstancesByDefinition();
            if (instances != null)
            {
                for (int i = 0; i < instances.Count; i++)
                {
                    playerData.inventory.Add(instances[i]);
                }
            }

            IList<KampaiPendingTransaction> pendingTransactions = player.GetPendingTransactions();
            if (pendingTransactions != null)
            {
                for (int i = 0; i < pendingTransactions.Count; i++)
                {
                    playerData.pendingTransactions.Add(pendingTransactions[i]);
                }
            }

            IDictionary<int, int> unlockedItems = player.GetUnlockedItems();
            if (unlockedItems != null)
            {
                foreach (KeyValuePair<int, int> kvp in unlockedItems)
                {
                    playerData.unlocks.Add(new UnlockedItem(kvp.Key, kvp.Value));
                }
            }

            IList<int> villainQueue = player.GetVillainQueue();
            if (villainQueue != null)
            {
                for (int i = 0; i < villainQueue.Count; i++)
                {
                    playerData.villainQueue.Add(villainQueue[i]);
                }
            }

            IList<string> platformIDs = player.GetPlatformStoreTransactionIDs();
            if (platformIDs != null)
            {
                for (int i = 0; i < platformIDs.Count; i++)
                {
                    playerData.PlatformStoreTransactionIDs.Add(platformIDs[i]);
                }
            }

            IDictionary<int, SocialClaimRewardItem.ClaimState> socialRewards = player.GetSocialClaimRewards();
            if (socialRewards != null)
            {
                foreach (KeyValuePair<int, SocialClaimRewardItem.ClaimState> kvp in socialRewards)
                {
                    playerData.socialRewards.Add(new SocialClaimRewardItem(kvp.Key, kvp.Value));
                }
            }
        }

        private long SafeGetLong(JObject root, string key)
        {
            if (root[key] != null)
            {
                try
                {
                    return (long)root[key];
                }
                catch
                {
                    long val = 0;
                    if (long.TryParse(root[key].ToString(), out val))
                        return val;
                }
            }
            return 0;
        }

        private int SafeGetInt(JObject root, string key)
        {
            if (root[key] != null)
            {
                try
                {
                    return (int)root[key];
                }
                catch
                {
                    int val = 0;
                    if (int.TryParse(root[key].ToString(), out val))
                        return val;
                }
            }
            return 0;
        }
    }
}
