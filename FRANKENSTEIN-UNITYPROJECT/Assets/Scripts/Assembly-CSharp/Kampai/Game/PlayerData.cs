namespace Kampai.Game
{
	public class PlayerData : global::Kampai.Util.IFastJSONDeserializable, global::Kampai.Util.IFastJSONSerializable
	{
		public long ID;

		public int version;

		public int nextId;

		public global::System.Collections.Generic.IList<int> villainQueue;

		[global::Kampai.Util.Serializer("PlayerData.SerializeInventory")]
		public global::System.Collections.Generic.IList<global::Kampai.Game.Instance> inventory;

		public global::System.Collections.Generic.IList<global::Kampai.Game.KampaiPendingTransaction> pendingTransactions;

		public global::System.Collections.Generic.IList<global::Kampai.Game.UnlockedItem> unlocks;

		public int lastLevelUpTime;

		public int lastGameStartTime;

		public int totalGameplayDurationSinceLastLevelUp;

		public int targetExpansionID;

		public int freezeTime;

		public int highestFtueLevel;

		public global::System.Collections.Generic.IList<global::Kampai.Game.SocialClaimRewardItem> socialRewards;

		public global::System.Collections.Generic.IList<string> PlatformStoreTransactionIDs;

		public virtual object Deserialize(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters = null)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None)
			{
				reader.Read();
			}
			global::Kampai.Util.ReaderUtil.EnsureToken(global::Newtonsoft.Json.JsonToken.StartObject, reader);
			while (reader.Read())
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.PropertyName:
				{
					string propertyName = ((string)reader.Value).ToUpper();
					if (!DeserializeProperty(propertyName, reader, converters))
					{
						reader.Skip();
					}
					break;
				}
				case global::Newtonsoft.Json.JsonToken.EndObject:
					return this;
				default:
					throw new global::Newtonsoft.Json.JsonSerializationException(string.Format("Unexpected token when deserializing object: {0}. {1}", reader.TokenType, global::Kampai.Util.ReaderUtil.GetPositionInSource(reader)));
				case global::Newtonsoft.Json.JsonToken.Comment:
					break;
				}
			}
			throw new global::Newtonsoft.Json.JsonSerializationException("Unexpected end when deserializing object.");
		}

		protected virtual bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "ID":
				reader.Read();
				ID = global::System.Convert.ToInt64(reader.Value);
				break;
			case "VERSION":
				reader.Read();
				version = global::System.Convert.ToInt32(reader.Value);
				break;
			case "NEXTID":
				reader.Read();
				nextId = global::System.Convert.ToInt32(reader.Value);
				break;
			case "VILLAINQUEUE":
				reader.Read();
				villainQueue = global::Kampai.Util.ReaderUtil.PopulateListInt32(reader);
				break;
			case "INVENTORY":
				reader.Read();
				inventory = global::Kampai.Util.ReaderUtil.PopulateList<global::Kampai.Game.Instance>(reader, converters, converters.instanceConverter);
				break;
			case "PENDINGTRANSACTIONS":
				reader.Read();
				pendingTransactions = global::Kampai.Util.ReaderUtil.PopulateList<global::Kampai.Game.KampaiPendingTransaction>(reader, converters, global::Kampai.Util.ReaderUtil.ReadKampaiPendingTransaction);
				break;
			case "UNLOCKS":
				reader.Read();
				unlocks = global::Kampai.Util.ReaderUtil.PopulateList<global::Kampai.Game.UnlockedItem>(reader, converters, global::Kampai.Util.ReaderUtil.ReadUnlockedItem);
				break;
			case "LASTLEVELUPTIME":
				reader.Read();
				lastLevelUpTime = global::System.Convert.ToInt32(reader.Value);
				break;
			case "LASTGAMESTARTTIME":
				reader.Read();
				lastGameStartTime = global::System.Convert.ToInt32(reader.Value);
				break;
			case "TOTALGAMEPLAYDURATIONSINCELASTLEVELUP":
				reader.Read();
				totalGameplayDurationSinceLastLevelUp = global::System.Convert.ToInt32(reader.Value);
				break;
			case "TARGETEXPANSIONID":
				reader.Read();
				targetExpansionID = global::System.Convert.ToInt32(reader.Value);
				break;
			case "FREEZETIME":
				reader.Read();
				freezeTime = global::System.Convert.ToInt32(reader.Value);
				break;
			case "HIGHESTFTUELEVEL":
				reader.Read();
				highestFtueLevel = global::System.Convert.ToInt32(reader.Value);
				break;
			case "SOCIALREWARDS":
				reader.Read();
				socialRewards = global::Kampai.Util.ReaderUtil.PopulateList<global::Kampai.Game.SocialClaimRewardItem>(reader, converters, global::Kampai.Util.ReaderUtil.ReadSocialClaimRewardItem);
				break;
			case "PLATFORMSTORETRANSACTIONIDS":
				reader.Read();
				PlatformStoreTransactionIDs = global::Kampai.Util.ReaderUtil.PopulateList<string>(reader, converters, global::Kampai.Util.ReaderUtil.ReadString);
				break;
			default:
				return false;
			}
			return true;
		}

		public virtual void Serialize(global::Newtonsoft.Json.JsonWriter writer)
		{
			writer.WriteStartObject();
			SerializeProperties(writer);
			writer.WriteEndObject();
		}

		protected virtual void SerializeProperties(global::Newtonsoft.Json.JsonWriter writer)
		{
			writer.WritePropertyName("ID");
			writer.WriteValue(ID);
			writer.WritePropertyName("version");
			writer.WriteValue(version);
			writer.WritePropertyName("nextId");
			writer.WriteValue(nextId);
			if (villainQueue != null)
			{
				writer.WritePropertyName("villainQueue");
				writer.WriteStartArray();
				global::System.Collections.Generic.IEnumerator<int> enumerator = villainQueue.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						int current = enumerator.Current;
						writer.WriteValue(current);
					}
				}
				finally
				{
					enumerator.Dispose();
				}
				writer.WriteEndArray();
			}
			if (inventory != null)
			{
				writer.WritePropertyName("inventory");
				SerializeInventory(writer, inventory);
			}
			if (pendingTransactions != null)
			{
				writer.WritePropertyName("pendingTransactions");
				writer.WriteStartArray();
				global::System.Collections.Generic.IEnumerator<global::Kampai.Game.KampaiPendingTransaction> enumerator2 = pendingTransactions.GetEnumerator();
				try
				{
					while (enumerator2.MoveNext())
					{
						global::Kampai.Game.KampaiPendingTransaction current2 = enumerator2.Current;
						writer.WriteStartObject();
						if (current2.ExternalIdentifier != null)
						{
							writer.WritePropertyName("ExternalIdentifier");
							writer.WriteValue(current2.ExternalIdentifier);
						}
						if (current2.Transaction != null)
						{
							writer.WritePropertyName("Transaction");
							global::Kampai.Game.KampaiPendingTransaction.SerializeDefinition(writer, current2.Transaction);
						}
						writer.WritePropertyName("StoreItemDefinitionId");
						writer.WriteValue(current2.StoreItemDefinitionId);
						writer.WritePropertyName("UTCTimeCreated");
						writer.WriteValue(current2.UTCTimeCreated);
						writer.WriteEndObject();
					}
				}
				finally
				{
					enumerator2.Dispose();
				}
				writer.WriteEndArray();
			}
			if (unlocks != null)
			{
				writer.WritePropertyName("unlocks");
				writer.WriteStartArray();
				global::System.Collections.Generic.IEnumerator<global::Kampai.Game.UnlockedItem> enumerator3 = unlocks.GetEnumerator();
				try
				{
					while (enumerator3.MoveNext())
					{
						global::Kampai.Game.UnlockedItem current3 = enumerator3.Current;
						writer.WriteStartObject();
						writer.WritePropertyName("defID");
						writer.WriteValue(current3.defID);
						writer.WritePropertyName("quantity");
						writer.WriteValue(current3.quantity);
						writer.WriteEndObject();
					}
				}
				finally
				{
					enumerator3.Dispose();
				}
				writer.WriteEndArray();
			}
			writer.WritePropertyName("lastLevelUpTime");
			writer.WriteValue(lastLevelUpTime);
			writer.WritePropertyName("lastGameStartTime");
			writer.WriteValue(lastGameStartTime);
			writer.WritePropertyName("totalGameplayDurationSinceLastLevelUp");
			writer.WriteValue(totalGameplayDurationSinceLastLevelUp);
			writer.WritePropertyName("targetExpansionID");
			writer.WriteValue(targetExpansionID);
			writer.WritePropertyName("freezeTime");
			writer.WriteValue(freezeTime);
			writer.WritePropertyName("highestFtueLevel");
			writer.WriteValue(highestFtueLevel);
			if (socialRewards != null)
			{
				writer.WritePropertyName("socialRewards");
				writer.WriteStartArray();
				global::System.Collections.Generic.IEnumerator<global::Kampai.Game.SocialClaimRewardItem> enumerator4 = socialRewards.GetEnumerator();
				try
				{
					while (enumerator4.MoveNext())
					{
						global::Kampai.Game.SocialClaimRewardItem current4 = enumerator4.Current;
						writer.WriteStartObject();
						writer.WritePropertyName("eventID");
						writer.WriteValue(current4.eventID);
						writer.WritePropertyName("claimState");
						writer.WriteValue((int)current4.claimState);
						writer.WriteEndObject();
					}
				}
				finally
				{
					enumerator4.Dispose();
				}
				writer.WriteEndArray();
			}
			if (PlatformStoreTransactionIDs == null)
			{
				return;
			}
			writer.WritePropertyName("PlatformStoreTransactionIDs");
			writer.WriteStartArray();
			global::System.Collections.Generic.IEnumerator<string> enumerator5 = PlatformStoreTransactionIDs.GetEnumerator();
			try
			{
				while (enumerator5.MoveNext())
				{
					string current5 = enumerator5.Current;
					writer.WriteValue(current5);
				}
			}
			finally
			{
				enumerator5.Dispose();
			}
			writer.WriteEndArray();
		}

		public static void SerializeInventory(global::Newtonsoft.Json.JsonWriter writer, global::System.Collections.Generic.IList<global::Kampai.Game.Instance> items)
		{
			writer.WriteStartArray();
			for (int i = 0; i < items.Count; i++)
			{
				items[i].Serialize(writer);
			}
			writer.WriteEndArray();
		}
	}
}
