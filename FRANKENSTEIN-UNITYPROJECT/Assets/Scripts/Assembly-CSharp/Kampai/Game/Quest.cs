namespace Kampai.Game
{
	public class Quest : global::Kampai.Game.Instance<global::Kampai.Game.QuestDefinition>, global::Kampai.Game.Instance, global::Kampai.Util.IFastJSONDeserializable, global::Kampai.Util.IFastJSONSerializable, global::Kampai.Util.Identifiable
	{
		public global::System.Collections.Generic.Dictionary<string, global::Kampai.Game.QuestScriptInstance> questScriptInstances = new global::System.Collections.Generic.Dictionary<string, global::Kampai.Game.QuestScriptInstance>();

		global::Kampai.Game.Definition global::Kampai.Game.Instance.Definition
		{
			get
			{
				return Definition;
			}
		}

		public global::System.Collections.Generic.IList<global::Kampai.Game.QuestStep> Steps { get; set; }

		public int UTCQuestStartTime { get; set; }

		public int QuestIconTrackedInstanceId { get; set; }

		public global::Kampai.Game.QuestState state { get; set; }

		[global::Newtonsoft.Json.JsonIgnore]
		public bool AutoGrantReward { get; set; }

		public int ID { get; set; }

		public global::Kampai.Game.QuestDefinition Definition { get; protected set; }

		public global::Kampai.Game.DynamicQuestDefinition dynamicDefinition { get; set; }

		public Quest(global::Kampai.Game.QuestDefinition def)
		{
			global::Kampai.Game.DynamicQuestDefinition dynamicQuestDefinition = def as global::Kampai.Game.DynamicQuestDefinition;
			if (dynamicQuestDefinition != null)
			{
				Definition = new global::Kampai.Game.QuestDefinition();
				Definition.ID = 77777;
				dynamicDefinition = dynamicQuestDefinition;
				dynamicDefinition.ID = Definition.ID;
			}
			else
			{
				Definition = def;
			}
			AutoGrantReward = false;
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
			case "STEPS":
				reader.Read();
                    //Steps = global::Kampai.Util.ReaderUtil.PopulateList(reader, converters, global::Kampai.Util.ReaderUtil.ReadQuestStep);
                    Steps = global::Kampai.Util.ReaderUtil.PopulateList<global::Kampai.Game.QuestStep>(reader, converters, global::Kampai.Util.ReaderUtil.ReadQuestStep);
                    break;
			case "UTCQUESTSTARTTIME":
				reader.Read();
				UTCQuestStartTime = global::System.Convert.ToInt32(reader.Value);
				break;
			case "QUESTICONTRACKEDINSTANCEID":
				reader.Read();
				QuestIconTrackedInstanceId = global::System.Convert.ToInt32(reader.Value);
				break;
			case "STATE":
				reader.Read();
				state = global::Kampai.Util.ReaderUtil.ReadEnum<global::Kampai.Game.QuestState>(reader);
				break;
			case "ID":
				reader.Read();
				ID = global::System.Convert.ToInt32(reader.Value);
				break;
			case "DYNAMICDEFINITION":
				reader.Read();
				dynamicDefinition = ((converters.questDefinitionConverter == null) ? global::Kampai.Util.FastJSONDeserializer.Deserialize<global::Kampai.Game.DynamicQuestDefinition>(reader, converters) : ((global::Kampai.Game.DynamicQuestDefinition)converters.questDefinitionConverter.ReadJson(reader, converters)));
				break;
			case "QUESTSCRIPTINSTANCES":
				reader.Read();
				questScriptInstances = global::Kampai.Util.ReaderUtil.ReadDictionary<global::Kampai.Game.QuestScriptInstance>(reader, converters);
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
			if (Steps != null)
			{
				writer.WritePropertyName("Steps");
				writer.WriteStartArray();
				global::System.Collections.Generic.IEnumerator<global::Kampai.Game.QuestStep> enumerator = Steps.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						global::Kampai.Game.QuestStep current = enumerator.Current;
						writer.WriteStartObject();
						writer.WritePropertyName("state");
						writer.WriteValue((int)current.state);
						writer.WritePropertyName("AmountCompleted");
						writer.WriteValue(current.AmountCompleted);
						writer.WritePropertyName("AmountReady");
						writer.WriteValue(current.AmountReady);
						writer.WritePropertyName("TrackedID");
						writer.WriteValue(current.TrackedID);
						writer.WriteEndObject();
					}
				}
				finally
				{
					enumerator.Dispose();
				}
				writer.WriteEndArray();
			}
			writer.WritePropertyName("UTCQuestStartTime");
			writer.WriteValue(UTCQuestStartTime);
			writer.WritePropertyName("QuestIconTrackedInstanceId");
			writer.WriteValue(QuestIconTrackedInstanceId);
			writer.WritePropertyName("state");
			writer.WriteValue((int)state);
			if (dynamicDefinition != null)
			{
				writer.WritePropertyName("dynamicDefinition");
				writer.WriteStartObject();
				if (dynamicDefinition.LocalizedKey != null)
				{
					writer.WritePropertyName("LocalizedKey");
					writer.WriteValue(dynamicDefinition.LocalizedKey);
				}
				writer.WritePropertyName("ID");
				writer.WriteValue(dynamicDefinition.ID);
				writer.WritePropertyName("MinDLC");
				writer.WriteValue(dynamicDefinition.MinDLC);
				writer.WritePropertyName("Disabled");
				writer.WriteValue(dynamicDefinition.Disabled);
				writer.WritePropertyName("QuestLineID");
				writer.WriteValue(dynamicDefinition.QuestLineID);
				writer.WritePropertyName("type");
				writer.WriteValue((int)dynamicDefinition.type);
				writer.WritePropertyName("NarrativeOrder");
				writer.WriteValue(dynamicDefinition.NarrativeOrder);
				writer.WritePropertyName("SurfaceType");
				writer.WriteValue((int)dynamicDefinition.SurfaceType);
				writer.WritePropertyName("SurfaceID");
				writer.WriteValue(dynamicDefinition.SurfaceID);
				writer.WritePropertyName("UnlockLevel");
				writer.WriteValue(dynamicDefinition.UnlockLevel);
				writer.WritePropertyName("UnlockQuestId");
				writer.WriteValue(dynamicDefinition.UnlockQuestId);
				if (dynamicDefinition.QuestBookIcon != null)
				{
					writer.WritePropertyName("QuestBookIcon");
					writer.WriteValue(dynamicDefinition.QuestBookIcon);
				}
				if (dynamicDefinition.QuestBookMask != null)
				{
					writer.WritePropertyName("QuestBookMask");
					writer.WriteValue(dynamicDefinition.QuestBookMask);
				}
				if (dynamicDefinition.QuestSteps != null)
				{
					writer.WritePropertyName("QuestSteps");
					writer.WriteStartArray();
					global::System.Collections.Generic.IEnumerator<global::Kampai.Game.QuestStepDefinition> enumerator2 = dynamicDefinition.QuestSteps.GetEnumerator();
					try
					{
						while (enumerator2.MoveNext())
						{
							global::Kampai.Game.QuestStepDefinition current2 = enumerator2.Current;
							writer.WriteStartObject();
							writer.WritePropertyName("Type");
							writer.WriteValue((int)current2.Type);
							writer.WritePropertyName("ItemAmount");
							writer.WriteValue(current2.ItemAmount);
							writer.WritePropertyName("ItemDefinitionID");
							writer.WriteValue(current2.ItemDefinitionID);
							writer.WritePropertyName("CostumeDefinitionID");
							writer.WriteValue(current2.CostumeDefinitionID);
							writer.WritePropertyName("ShowWayfinder");
							writer.WriteValue(current2.ShowWayfinder);
							writer.WriteEndObject();
						}
					}
					finally
					{
						enumerator2.Dispose();
					}
					writer.WriteEndArray();
				}
				writer.WritePropertyName("RewardTransaction");
				writer.WriteValue(dynamicDefinition.RewardTransaction);
				if (dynamicDefinition.WayFinderIcon != null)
				{
					writer.WritePropertyName("WayFinderIcon");
					writer.WriteValue(dynamicDefinition.WayFinderIcon);
				}
				if (dynamicDefinition.QuestIntro != null)
				{
					writer.WritePropertyName("QuestIntro");
					writer.WriteValue(dynamicDefinition.QuestIntro);
				}
				if (dynamicDefinition.QuestVoice != null)
				{
					writer.WritePropertyName("QuestVoice");
					writer.WriteValue(dynamicDefinition.QuestVoice);
				}
				if (dynamicDefinition.QuestOutro != null)
				{
					writer.WritePropertyName("QuestOutro");
					writer.WriteValue(dynamicDefinition.QuestOutro);
				}
				if (dynamicDefinition.QuestIntroMood != null)
				{
					writer.WritePropertyName("QuestIntroMood");
					writer.WriteValue(dynamicDefinition.QuestIntroMood);
				}
				if (dynamicDefinition.QuestVoiceMood != null)
				{
					writer.WritePropertyName("QuestVoiceMood");
					writer.WriteValue(dynamicDefinition.QuestVoiceMood);
				}
				if (dynamicDefinition.QuestOutroMood != null)
				{
					writer.WritePropertyName("QuestOutroMood");
					writer.WriteValue(dynamicDefinition.QuestOutroMood);
				}
				if (dynamicDefinition.RewardTransactionInstance != null)
				{
					writer.WritePropertyName("RewardTransactionInstance");
					writer.WriteStartObject();
					writer.WritePropertyName("ID");
					writer.WriteValue(dynamicDefinition.RewardTransactionInstance.ID);
					if (dynamicDefinition.RewardTransactionInstance.Inputs != null)
					{
						writer.WritePropertyName("Inputs");
						writer.WriteStartArray();
						global::System.Collections.Generic.IEnumerator<global::Kampai.Util.QuantityItem> enumerator3 = dynamicDefinition.RewardTransactionInstance.Inputs.GetEnumerator();
						try
						{
							while (enumerator3.MoveNext())
							{
								global::Kampai.Util.QuantityItem current3 = enumerator3.Current;
								writer.WriteStartObject();
								writer.WritePropertyName("ID");
								writer.WriteValue(current3.ID);
								writer.WritePropertyName("Quantity");
								writer.WriteValue(current3.Quantity);
								writer.WriteEndObject();
							}
						}
						finally
						{
							enumerator3.Dispose();
						}
						writer.WriteEndArray();
					}
					if (dynamicDefinition.RewardTransactionInstance.Outputs != null)
					{
						writer.WritePropertyName("Outputs");
						writer.WriteStartArray();
						global::System.Collections.Generic.IEnumerator<global::Kampai.Util.QuantityItem> enumerator4 = dynamicDefinition.RewardTransactionInstance.Outputs.GetEnumerator();
						try
						{
							while (enumerator4.MoveNext())
							{
								global::Kampai.Util.QuantityItem current4 = enumerator4.Current;
								writer.WriteStartObject();
								writer.WritePropertyName("ID");
								writer.WriteValue(current4.ID);
								writer.WritePropertyName("Quantity");
								writer.WriteValue(current4.Quantity);
								writer.WriteEndObject();
							}
						}
						finally
						{
							enumerator4.Dispose();
						}
						writer.WriteEndArray();
					}
					writer.WriteEndObject();
				}
				writer.WritePropertyName("DropStep");
				writer.WriteValue(dynamicDefinition.DropStep);
				writer.WriteEndObject();
			}
			if (questScriptInstances == null)
			{
				return;
			}
			writer.WritePropertyName("questScriptInstances");
			writer.WriteStartObject();
			global::System.Collections.Generic.Dictionary<string, global::Kampai.Game.QuestScriptInstance>.Enumerator enumerator5 = questScriptInstances.GetEnumerator();
			try
			{
				while (enumerator5.MoveNext())
				{
					global::System.Collections.Generic.KeyValuePair<string, global::Kampai.Game.QuestScriptInstance> current5 = enumerator5.Current;
					writer.WritePropertyName(global::System.Convert.ToString(current5.Key));
					current5.Value.Serialize(writer);
				}
			}
			finally
			{
				enumerator5.Dispose();
			}
			writer.WriteEndObject();
		}

		public void Initialize()
		{
			if (Steps != null)
			{
				return;
			}
			global::Kampai.Game.QuestDefinition activeDefinition = GetActiveDefinition();
			if (activeDefinition.QuestSteps != null)
			{
				Steps = new global::System.Collections.Generic.List<global::Kampai.Game.QuestStep>(activeDefinition.QuestSteps.Count);
				for (int i = 0; i < activeDefinition.QuestSteps.Count; i++)
				{
					global::Kampai.Game.QuestStep item = new global::Kampai.Game.QuestStep();
					Steps.Add(item);
				}
			}
			else
			{
				Steps = new global::System.Collections.Generic.List<global::Kampai.Game.QuestStep>();
			}
		}

		public void Clear()
		{
			state = global::Kampai.Game.QuestState.Notstarted;
			UTCQuestStartTime = 0;
			if (Steps != null)
			{
				Steps.Clear();
				global::Kampai.Game.QuestDefinition activeDefinition = GetActiveDefinition();
				if (activeDefinition.QuestSteps != null)
				{
					for (int i = 0; i < activeDefinition.QuestSteps.Count; i++)
					{
						global::Kampai.Game.QuestStep item = new global::Kampai.Game.QuestStep();
						Steps.Add(item);
					}
				}
			}
			if (questScriptInstances != null)
			{
				questScriptInstances.Clear();
			}
		}

		public global::Kampai.Game.QuestDefinition GetActiveDefinition()
		{
			if (dynamicDefinition != null)
			{
				if (dynamicDefinition.ID == 0)
				{
					dynamicDefinition.ID = Definition.ID;
				}
				return dynamicDefinition;
			}
			return Definition;
		}

		public bool IsDynamic()
		{
			return dynamicDefinition != null;
		}

		public bool IsProcedurallyGenerated()
		{
			return GetActiveDefinition().SurfaceType == global::Kampai.Game.QuestSurfaceType.ProcedurallyGenerated;
		}
	}
}
