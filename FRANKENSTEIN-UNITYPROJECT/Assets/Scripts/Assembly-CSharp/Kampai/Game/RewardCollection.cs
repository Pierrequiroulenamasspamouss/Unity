namespace Kampai.Game
{
	public class RewardCollection : global::Kampai.Game.Instance<global::Kampai.Game.RewardCollectionDefinition>, global::Kampai.Game.Instance, global::Kampai.Util.IFastJSONDeserializable, global::Kampai.Util.IFastJSONSerializable, global::Kampai.Util.Identifiable
	{
		global::Kampai.Game.Definition global::Kampai.Game.Instance.Definition
		{
			get
			{
				return Definition;
			}
		}

		public int ID { get; set; }

		public global::Kampai.Game.RewardCollectionDefinition Definition { get; protected set; }

		public int CollectionScoreProgress { get; set; }

		public int NumRewardsCollected { get; set; }

		public int NumTimesPlayed { get; set; }

		public RewardCollection(global::Kampai.Game.RewardCollectionDefinition definition)
		{
			Definition = definition;
		}

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
				ID = global::System.Convert.ToInt32(reader.Value);
				break;
			case "COLLECTIONSCOREPROGRESS":
				reader.Read();
				CollectionScoreProgress = global::System.Convert.ToInt32(reader.Value);
				break;
			case "NUMREWARDSCOLLECTED":
				reader.Read();
				NumRewardsCollected = global::System.Convert.ToInt32(reader.Value);
				break;
			case "NUMTIMESPLAYED":
				reader.Read();
				NumTimesPlayed = global::System.Convert.ToInt32(reader.Value);
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
			global::Kampai.Game.FastInstanceSerializationHelper.SerializeInstanceData(writer, this);
			writer.WritePropertyName("CollectionScoreProgress");
			writer.WriteValue(CollectionScoreProgress);
			writer.WritePropertyName("NumRewardsCollected");
			writer.WriteValue(NumRewardsCollected);
			writer.WritePropertyName("NumTimesPlayed");
			writer.WriteValue(NumTimesPlayed);
		}

		public void IncreaseScore(int scoreIncrease)
		{
			CollectionScoreProgress += scoreIncrease;
		}

		public int GetMaxScore()
		{
			int num = 0;
			for (int i = 0; i < Definition.Rewards.Count; i++)
			{
				num = global::UnityEngine.Mathf.Max(num, Definition.Rewards[i].RequiredPoints);
			}
			return num;
		}

		public int GetPointTotalForNextReward()
		{
			int num = GetMaxScore();
			for (int i = 0; i < Definition.Rewards.Count; i++)
			{
				if (CollectionScoreProgress < Definition.Rewards[i].RequiredPoints)
				{
					num = global::UnityEngine.Mathf.Min(num, Definition.Rewards[i].RequiredPoints);
				}
			}
			return num;
		}

		public bool HasRewardReadyForCollection()
		{
			return GetTransactionIDReadyForCollection() != -1;
		}

		public bool IsRewardReadyForCollection(global::Kampai.Game.CollectionReward reward)
		{
			int num = Definition.Rewards.IndexOf(reward);
			return reward.RequiredPoints <= CollectionScoreProgress && NumRewardsCollected <= num;
		}

		public int GetTransactionIDReadyForCollection()
		{
			for (int i = 0; i < Definition.Rewards.Count; i++)
			{
				global::Kampai.Game.CollectionReward collectionReward = Definition.Rewards[i];
				if (collectionReward.RequiredPoints <= CollectionScoreProgress && NumRewardsCollected <= i)
				{
					return collectionReward.TransactionID;
				}
			}
			return -1;
		}

		public bool IsCompleted()
		{
			return NumRewardsCollected >= Definition.Rewards.Count;
		}

		public void ResetProgress()
		{
			CollectionScoreProgress = 0;
			NumRewardsCollected = 0;
		}
	}
}
