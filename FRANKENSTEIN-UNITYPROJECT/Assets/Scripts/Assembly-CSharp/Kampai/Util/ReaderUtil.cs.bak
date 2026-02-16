namespace Kampai.Util
{
	public static class ReaderUtil
	{
		public static global::Kampai.Game.UpsightPromoTrigger ReadUpsightPromoTrigger(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters = null)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None)
			{
				reader.Read();
			}
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
			{
				return null;
			}
			global::Kampai.Game.UpsightPromoTrigger upsightPromoTrigger = new global::Kampai.Game.UpsightPromoTrigger();
			EnsureToken(global::Newtonsoft.Json.JsonToken.StartObject, reader);
			while (reader.Read())
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.PropertyName:
					switch (((string)reader.Value).ToUpper())
					{
					case "PLACEMENT":
						reader.Read();
						upsightPromoTrigger.placement = ReadEnum<global::Kampai.Game.UpsightPromoTrigger.Placement>(reader);
						break;
					case "ENABLED":
						reader.Read();
						upsightPromoTrigger.enabled = global::System.Convert.ToBoolean(reader.Value);
						break;
					default:
						reader.Skip();
						break;
					}
					break;
				case global::Newtonsoft.Json.JsonToken.EndObject:
					return upsightPromoTrigger;
				default:
					throw new global::Newtonsoft.Json.JsonSerializationException(string.Format("Unexpected token when deserializing object: {0}. {1}", reader.TokenType, GetPositionInSource(reader)));
				case global::Newtonsoft.Json.JsonToken.Comment:
					break;
				}
			}
			throw new global::Newtonsoft.Json.JsonSerializationException("Unexpected end when deserializing object.");
		}

		public static global::Kampai.Util.Logging.Hosted.LogglyConfiguration ReadLogglyConfiguration(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters = null)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None)
			{
				reader.Read();
			}
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
			{
				return null;
			}
			global::Kampai.Util.Logging.Hosted.LogglyConfiguration logglyConfiguration = new global::Kampai.Util.Logging.Hosted.LogglyConfiguration();
			EnsureToken(global::Newtonsoft.Json.JsonToken.StartObject, reader);
			while (reader.Read())
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.PropertyName:
					switch (((string)reader.Value).ToUpper())
					{
					case "LOGLEVEL":
						reader.Read();
						logglyConfiguration.logLevel = global::System.Convert.ToInt32(reader.Value);
						break;
					case "SAMPLEPERCENTAGE":
						reader.Read();
						logglyConfiguration.samplePercentage = global::System.Convert.ToInt32(reader.Value);
						break;
					default:
						reader.Skip();
						break;
					}
					break;
				case global::Newtonsoft.Json.JsonToken.EndObject:
					return logglyConfiguration;
				default:
					throw new global::Newtonsoft.Json.JsonSerializationException(string.Format("Unexpected token when deserializing object: {0}. {1}", reader.TokenType, GetPositionInSource(reader)));
				case global::Newtonsoft.Json.JsonToken.Comment:
					break;
				}
			}
			throw new global::Newtonsoft.Json.JsonSerializationException("Unexpected end when deserializing object.");
		}

		public static global::Kampai.Game.CharacterPrestigeLevelDefinition ReadCharacterPrestigeLevelDefinition(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters = null)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None)
			{
				reader.Read();
			}
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
			{
				return null;
			}
			global::Kampai.Game.CharacterPrestigeLevelDefinition characterPrestigeLevelDefinition = new global::Kampai.Game.CharacterPrestigeLevelDefinition();
			EnsureToken(global::Newtonsoft.Json.JsonToken.StartObject, reader);
			while (reader.Read())
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.PropertyName:
					switch (((string)reader.Value).ToUpper())
					{
					case "UNLOCKLEVEL":
						reader.Read();
						characterPrestigeLevelDefinition.UnlockLevel = global::System.Convert.ToUInt32(reader.Value);
						break;
					case "UNLOCKQUESTID":
						reader.Read();
						characterPrestigeLevelDefinition.UnlockQuestID = global::System.Convert.ToInt32(reader.Value);
						break;
					case "POINTSNEEDED":
						reader.Read();
						characterPrestigeLevelDefinition.PointsNeeded = global::System.Convert.ToUInt32(reader.Value);
						break;
					case "ATTACHEDQUESTID":
						reader.Read();
						characterPrestigeLevelDefinition.AttachedQuestID = global::System.Convert.ToInt32(reader.Value);
						break;
					case "WELCOMEPANELMESSAGELOCALIZEDKEY":
						reader.Read();
						characterPrestigeLevelDefinition.WelcomePanelMessageLocalizedKey = ReadString(reader, converters);
						break;
					case "FAREWELLPANELMESSAGELOCALIZEDKEY":
						reader.Read();
						characterPrestigeLevelDefinition.FarewellPanelMessageLocalizedKey = ReadString(reader, converters);
						break;
					default:
						reader.Skip();
						break;
					}
					break;
				case global::Newtonsoft.Json.JsonToken.EndObject:
					return characterPrestigeLevelDefinition;
				default:
					throw new global::Newtonsoft.Json.JsonSerializationException(string.Format("Unexpected token when deserializing object: {0}. {1}", reader.TokenType, GetPositionInSource(reader)));
				case global::Newtonsoft.Json.JsonToken.Comment:
					break;
				}
			}
			throw new global::Newtonsoft.Json.JsonSerializationException("Unexpected end when deserializing object.");
		}

		public static global::Kampai.Game.CameraOffset ReadCameraOffset(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters = null)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None)
			{
				reader.Read();
			}
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
			{
				return null;
			}
			global::Kampai.Game.CameraOffset cameraOffset = new global::Kampai.Game.CameraOffset();
			EnsureToken(global::Newtonsoft.Json.JsonToken.StartObject, reader);
			while (reader.Read())
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.PropertyName:
					switch (((string)reader.Value).ToUpper())
					{
					case "X":
						reader.Read();
						cameraOffset.x = global::System.Convert.ToSingle(reader.Value);
						break;
					case "Z":
						reader.Read();
						cameraOffset.z = global::System.Convert.ToSingle(reader.Value);
						break;
					case "ZOOM":
						reader.Read();
						cameraOffset.zoom = global::System.Convert.ToSingle(reader.Value);
						break;
					default:
						reader.Skip();
						break;
					}
					break;
				case global::Newtonsoft.Json.JsonToken.EndObject:
					return cameraOffset;
				default:
					throw new global::Newtonsoft.Json.JsonSerializationException(string.Format("Unexpected token when deserializing object: {0}. {1}", reader.TokenType, GetPositionInSource(reader)));
				case global::Newtonsoft.Json.JsonToken.Comment:
					break;
				}
			}
			throw new global::Newtonsoft.Json.JsonSerializationException("Unexpected end when deserializing object.");
		}

		public static global::UnityEngine.Vector3 ReadVector3(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters = null)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None)
			{
				reader.Read();
			}
			global::UnityEngine.Vector3 result = default(global::UnityEngine.Vector3);
			EnsureToken(global::Newtonsoft.Json.JsonToken.StartObject, reader);
			while (reader.Read())
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.PropertyName:
					switch (((string)reader.Value).ToUpper())
					{
					case "X":
						reader.Read();
						result.x = global::System.Convert.ToSingle(reader.Value);
						break;
					case "Y":
						reader.Read();
						result.y = global::System.Convert.ToSingle(reader.Value);
						break;
					case "Z":
						reader.Read();
						result.z = global::System.Convert.ToSingle(reader.Value);
						break;
					default:
						reader.Skip();
						break;
					}
					break;
				case global::Newtonsoft.Json.JsonToken.EndObject:
					return result;
				default:
					throw new global::Newtonsoft.Json.JsonSerializationException(string.Format("Unexpected token when deserializing object: {0}. {1}", reader.TokenType, GetPositionInSource(reader)));
				case global::Newtonsoft.Json.JsonToken.Comment:
					break;
				}
			}
			throw new global::Newtonsoft.Json.JsonSerializationException("Unexpected end when deserializing object.");
		}

		public static global::Kampai.Game.SlotUnlock ReadSlotUnlock(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters = null)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None)
			{
				reader.Read();
			}
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
			{
				return null;
			}
			global::Kampai.Game.SlotUnlock slotUnlock = new global::Kampai.Game.SlotUnlock();
			EnsureToken(global::Newtonsoft.Json.JsonToken.StartObject, reader);
			while (reader.Read())
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.PropertyName:
					switch (((string)reader.Value).ToUpper())
					{
					case "SLOTUNLOCKLEVELS":
						reader.Read();
						slotUnlock.SlotUnlockLevels = PopulateListInt32(reader);
						break;
					case "SLOTUNLOCKCOSTS":
						reader.Read();
						slotUnlock.SlotUnlockCosts = PopulateListInt32(reader);
						break;
					default:
						reader.Skip();
						break;
					}
					break;
				case global::Newtonsoft.Json.JsonToken.EndObject:
					return slotUnlock;
				default:
					throw new global::Newtonsoft.Json.JsonSerializationException(string.Format("Unexpected token when deserializing object: {0}. {1}", reader.TokenType, GetPositionInSource(reader)));
				case global::Newtonsoft.Json.JsonToken.Comment:
					break;
				}
			}
			throw new global::Newtonsoft.Json.JsonSerializationException("Unexpected end when deserializing object.");
		}

		public static MignetteRuleDefinition ReadMignetteRuleDefinition(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters = null)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None)
			{
				reader.Read();
			}
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
			{
				return null;
			}
			MignetteRuleDefinition mignetteRuleDefinition = new MignetteRuleDefinition();
			EnsureToken(global::Newtonsoft.Json.JsonToken.StartObject, reader);
			while (reader.Read())
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.PropertyName:
					switch (((string)reader.Value).ToUpper())
					{
					case "CAUSEIMAGE":
						reader.Read();
						mignetteRuleDefinition.CauseImage = ReadString(reader, converters);
						break;
					case "CAUSEIMAGEMASK":
						reader.Read();
						mignetteRuleDefinition.CauseImageMask = ReadString(reader, converters);
						break;
					case "EFFECTIMAGE":
						reader.Read();
						mignetteRuleDefinition.EffectImage = ReadString(reader, converters);
						break;
					case "EFFECTIMAGEMASK":
						reader.Read();
						mignetteRuleDefinition.EffectImageMask = ReadString(reader, converters);
						break;
					case "EFFECTAMOUNT":
						reader.Read();
						mignetteRuleDefinition.EffectAmount = global::System.Convert.ToInt32(reader.Value);
						break;
					default:
						reader.Skip();
						break;
					}
					break;
				case global::Newtonsoft.Json.JsonToken.EndObject:
					return mignetteRuleDefinition;
				default:
					throw new global::Newtonsoft.Json.JsonSerializationException(string.Format("Unexpected token when deserializing object: {0}. {1}", reader.TokenType, GetPositionInSource(reader)));
				case global::Newtonsoft.Json.JsonToken.Comment:
					break;
				}
			}
			throw new global::Newtonsoft.Json.JsonSerializationException("Unexpected end when deserializing object.");
		}

		public static global::Kampai.Game.MignetteChildObjectDefinition ReadMignetteChildObjectDefinition(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters = null)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None)
			{
				reader.Read();
			}
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
			{
				return null;
			}
			global::Kampai.Game.MignetteChildObjectDefinition mignetteChildObjectDefinition = new global::Kampai.Game.MignetteChildObjectDefinition();
			EnsureToken(global::Newtonsoft.Json.JsonToken.StartObject, reader);
			while (reader.Read())
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.PropertyName:
					switch (((string)reader.Value).ToUpper())
					{
					case "PREFAB":
						reader.Read();
						mignetteChildObjectDefinition.Prefab = ReadString(reader, converters);
						break;
					case "POSITION":
						reader.Read();
						mignetteChildObjectDefinition.Position = ReadVector3(reader, converters);
						break;
					case "ISLOCAL":
						reader.Read();
						mignetteChildObjectDefinition.IsLocal = global::System.Convert.ToBoolean(reader.Value);
						break;
					case "ROTATION":
						reader.Read();
						mignetteChildObjectDefinition.Rotation = global::System.Convert.ToSingle(reader.Value);
						break;
					default:
						reader.Skip();
						break;
					}
					break;
				case global::Newtonsoft.Json.JsonToken.EndObject:
					return mignetteChildObjectDefinition;
				default:
					throw new global::Newtonsoft.Json.JsonSerializationException(string.Format("Unexpected token when deserializing object: {0}. {1}", reader.TokenType, GetPositionInSource(reader)));
				case global::Newtonsoft.Json.JsonToken.Comment:
					break;
				}
			}
			throw new global::Newtonsoft.Json.JsonSerializationException("Unexpected end when deserializing object.");
		}

		public static global::Kampai.Game.StorageUpgradeDefinition ReadStorageUpgradeDefinition(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters = null)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None)
			{
				reader.Read();
			}
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
			{
				return null;
			}
			global::Kampai.Game.StorageUpgradeDefinition storageUpgradeDefinition = new global::Kampai.Game.StorageUpgradeDefinition();
			EnsureToken(global::Newtonsoft.Json.JsonToken.StartObject, reader);
			while (reader.Read())
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.PropertyName:
					switch (((string)reader.Value).ToUpper())
					{
					case "LEVEL":
						reader.Read();
						storageUpgradeDefinition.Level = global::System.Convert.ToInt32(reader.Value);
						break;
					case "STORAGECAPACITY":
						reader.Read();
						storageUpgradeDefinition.StorageCapacity = global::System.Convert.ToUInt32(reader.Value);
						break;
					case "TRANSACTIONID":
						reader.Read();
						storageUpgradeDefinition.TransactionId = global::System.Convert.ToInt32(reader.Value);
						break;
					default:
						reader.Skip();
						break;
					}
					break;
				case global::Newtonsoft.Json.JsonToken.EndObject:
					return storageUpgradeDefinition;
				default:
					throw new global::Newtonsoft.Json.JsonSerializationException(string.Format("Unexpected token when deserializing object: {0}. {1}", reader.TokenType, GetPositionInSource(reader)));
				case global::Newtonsoft.Json.JsonToken.Comment:
					break;
				}
			}
			throw new global::Newtonsoft.Json.JsonSerializationException("Unexpected end when deserializing object.");
		}

		public static global::Kampai.Game.Location ReadLocation(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters = null)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None)
			{
				reader.Read();
			}
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
			{
				return null;
			}
			global::Kampai.Game.Location location = new global::Kampai.Game.Location();
			EnsureToken(global::Newtonsoft.Json.JsonToken.StartObject, reader);
			while (reader.Read())
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.PropertyName:
					switch (((string)reader.Value).ToUpper())
					{
					case "X":
						reader.Read();
						location.x = global::System.Convert.ToInt32(reader.Value);
						break;
					case "Y":
						reader.Read();
						location.y = global::System.Convert.ToInt32(reader.Value);
						break;
					default:
						reader.Skip();
						break;
					}
					break;
				case global::Newtonsoft.Json.JsonToken.EndObject:
					return location;
				default:
					throw new global::Newtonsoft.Json.JsonSerializationException(string.Format("Unexpected token when deserializing object: {0}. {1}", reader.TokenType, GetPositionInSource(reader)));
				case global::Newtonsoft.Json.JsonToken.Comment:
					break;
				}
			}
			throw new global::Newtonsoft.Json.JsonSerializationException("Unexpected end when deserializing object.");
		}

		public static global::Kampai.Game.CharacterUIAnimationDefinition ReadCharacterUIAnimationDefinition(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters = null)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None)
			{
				reader.Read();
			}
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
			{
				return null;
			}
			global::Kampai.Game.CharacterUIAnimationDefinition characterUIAnimationDefinition = new global::Kampai.Game.CharacterUIAnimationDefinition();
			EnsureToken(global::Newtonsoft.Json.JsonToken.StartObject, reader);
			while (reader.Read())
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.PropertyName:
					switch (((string)reader.Value).ToUpper())
					{
					case "STATEMACHINE":
						reader.Read();
						characterUIAnimationDefinition.StateMachine = ReadString(reader, converters);
						break;
					case "IDLECOUNT":
						reader.Read();
						characterUIAnimationDefinition.IdleCount = global::System.Convert.ToInt32(reader.Value);
						break;
					case "HAPPYCOUNT":
						reader.Read();
						characterUIAnimationDefinition.HappyCount = global::System.Convert.ToInt32(reader.Value);
						break;
					case "SELECTEDCOUNT":
						reader.Read();
						characterUIAnimationDefinition.SelectedCount = global::System.Convert.ToInt32(reader.Value);
						break;
					default:
						reader.Skip();
						break;
					}
					break;
				case global::Newtonsoft.Json.JsonToken.EndObject:
					return characterUIAnimationDefinition;
				default:
					throw new global::Newtonsoft.Json.JsonSerializationException(string.Format("Unexpected token when deserializing object: {0}. {1}", reader.TokenType, GetPositionInSource(reader)));
				case global::Newtonsoft.Json.JsonToken.Comment:
					break;
				}
			}
			throw new global::Newtonsoft.Json.JsonSerializationException("Unexpected end when deserializing object.");
		}

		public static global::Kampai.Game.FloatLocation ReadFloatLocation(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters = null)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None)
			{
				reader.Read();
			}
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
			{
				return null;
			}
			global::Kampai.Game.FloatLocation floatLocation = new global::Kampai.Game.FloatLocation();
			EnsureToken(global::Newtonsoft.Json.JsonToken.StartObject, reader);
			while (reader.Read())
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.PropertyName:
					switch (((string)reader.Value).ToUpper())
					{
					case "X":
						reader.Read();
						floatLocation.x = global::System.Convert.ToSingle(reader.Value);
						break;
					case "Y":
						reader.Read();
						floatLocation.y = global::System.Convert.ToSingle(reader.Value);
						break;
					default:
						reader.Skip();
						break;
					}
					break;
				case global::Newtonsoft.Json.JsonToken.EndObject:
					return floatLocation;
				default:
					throw new global::Newtonsoft.Json.JsonSerializationException(string.Format("Unexpected token when deserializing object: {0}. {1}", reader.TokenType, GetPositionInSource(reader)));
				case global::Newtonsoft.Json.JsonToken.Comment:
					break;
				}
			}
			throw new global::Newtonsoft.Json.JsonSerializationException("Unexpected end when deserializing object.");
		}

		public static global::Kampai.Game.Angle ReadAngle(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters = null)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None)
			{
				reader.Read();
			}
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
			{
				return null;
			}
			global::Kampai.Game.Angle angle = new global::Kampai.Game.Angle();
			EnsureToken(global::Newtonsoft.Json.JsonToken.StartObject, reader);
			while (reader.Read())
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.PropertyName:
					switch (((string)reader.Value).ToUpper())
					{
					case "DEGREES":
						reader.Read();
						angle.Degrees = global::System.Convert.ToSingle(reader.Value);
						break;
					default:
						reader.Skip();
						break;
					}
					break;
				case global::Newtonsoft.Json.JsonToken.EndObject:
					return angle;
				default:
					throw new global::Newtonsoft.Json.JsonSerializationException(string.Format("Unexpected token when deserializing object: {0}. {1}", reader.TokenType, GetPositionInSource(reader)));
				case global::Newtonsoft.Json.JsonToken.Comment:
					break;
				}
			}
			throw new global::Newtonsoft.Json.JsonSerializationException("Unexpected end when deserializing object.");
		}

		public static global::Kampai.Game.CollectionReward ReadCollectionReward(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters = null)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None)
			{
				reader.Read();
			}
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
			{
				return null;
			}
			global::Kampai.Game.CollectionReward collectionReward = new global::Kampai.Game.CollectionReward();
			EnsureToken(global::Newtonsoft.Json.JsonToken.StartObject, reader);
			while (reader.Read())
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.PropertyName:
					switch (((string)reader.Value).ToUpper())
					{
					case "REQUIREDPOINTS":
						reader.Read();
						collectionReward.RequiredPoints = global::System.Convert.ToInt32(reader.Value);
						break;
					case "TRANSACTIONID":
						reader.Read();
						collectionReward.TransactionID = global::System.Convert.ToInt32(reader.Value);
						break;
					default:
						reader.Skip();
						break;
					}
					break;
				case global::Newtonsoft.Json.JsonToken.EndObject:
					return collectionReward;
				default:
					throw new global::Newtonsoft.Json.JsonSerializationException(string.Format("Unexpected token when deserializing object: {0}. {1}", reader.TokenType, GetPositionInSource(reader)));
				case global::Newtonsoft.Json.JsonToken.Comment:
					break;
				}
			}
			throw new global::Newtonsoft.Json.JsonSerializationException("Unexpected end when deserializing object.");
		}

		public static global::Kampai.Game.PartyDefinition ReadPartyDefinition(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters = null)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None)
			{
				reader.Read();
			}
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
			{
				return null;
			}
			global::Kampai.Game.PartyDefinition partyDefinition = new global::Kampai.Game.PartyDefinition();
			EnsureToken(global::Newtonsoft.Json.JsonToken.StartObject, reader);
			while (reader.Read())
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.PropertyName:
					switch (((string)reader.Value).ToUpper())
					{
					case "PARTYAREA":
						reader.Read();
						partyDefinition.PartyArea = ReadArea(reader, converters);
						break;
					case "CENTER":
						reader.Read();
						partyDefinition.Center = ReadLocation(reader, converters);
						break;
					case "RADIUS":
						reader.Read();
						partyDefinition.Radius = global::System.Convert.ToSingle(reader.Value);
						break;
					case "DURATION":
						reader.Read();
						partyDefinition.Duration = global::System.Convert.ToInt32(reader.Value);
						break;
					case "PERCENT":
						reader.Read();
						partyDefinition.Percent = global::System.Convert.ToSingle(reader.Value);
						break;
					case "STARTANIMATIONS":
						reader.Read();
						partyDefinition.StartAnimations = global::System.Convert.ToInt32(reader.Value);
						break;
					default:
						reader.Skip();
						break;
					}
					break;
				case global::Newtonsoft.Json.JsonToken.EndObject:
					return partyDefinition;
				default:
					throw new global::Newtonsoft.Json.JsonSerializationException(string.Format("Unexpected token when deserializing object: {0}. {1}", reader.TokenType, GetPositionInSource(reader)));
				case global::Newtonsoft.Json.JsonToken.Comment:
					break;
				}
			}
			throw new global::Newtonsoft.Json.JsonSerializationException("Unexpected end when deserializing object.");
		}

		public static global::Kampai.Game.Area ReadArea(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters = null)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None)
			{
				reader.Read();
			}
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
			{
				return null;
			}
			global::Kampai.Game.Area area = new global::Kampai.Game.Area();
			EnsureToken(global::Newtonsoft.Json.JsonToken.StartObject, reader);
			while (reader.Read())
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.PropertyName:
					switch (((string)reader.Value).ToUpper())
					{
					case "A":
						reader.Read();
						area.a = ReadLocation(reader, converters);
						break;
					case "B":
						reader.Read();
						area.b = ReadLocation(reader, converters);
						break;
					default:
						reader.Skip();
						break;
					}
					break;
				case global::Newtonsoft.Json.JsonToken.EndObject:
					return area;
				default:
					throw new global::Newtonsoft.Json.JsonSerializationException(string.Format("Unexpected token when deserializing object: {0}. {1}", reader.TokenType, GetPositionInSource(reader)));
				case global::Newtonsoft.Json.JsonToken.Comment:
					break;
				}
			}
			throw new global::Newtonsoft.Json.JsonSerializationException("Unexpected end when deserializing object.");
		}

		public static global::Kampai.Game.FlyOverNode ReadFlyOverNode(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters = null)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None)
			{
				reader.Read();
			}
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
			{
				return null;
			}
			global::Kampai.Game.FlyOverNode flyOverNode = new global::Kampai.Game.FlyOverNode();
			EnsureToken(global::Newtonsoft.Json.JsonToken.StartObject, reader);
			while (reader.Read())
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.PropertyName:
					switch (((string)reader.Value).ToUpper())
					{
					case "X":
						reader.Read();
						flyOverNode.x = global::System.Convert.ToSingle(reader.Value);
						break;
					case "Y":
						reader.Read();
						flyOverNode.y = global::System.Convert.ToSingle(reader.Value);
						break;
					case "Z":
						reader.Read();
						flyOverNode.z = global::System.Convert.ToSingle(reader.Value);
						break;
					default:
						reader.Skip();
						break;
					}
					break;
				case global::Newtonsoft.Json.JsonToken.EndObject:
					return flyOverNode;
				default:
					throw new global::Newtonsoft.Json.JsonSerializationException(string.Format("Unexpected token when deserializing object: {0}. {1}", reader.TokenType, GetPositionInSource(reader)));
				case global::Newtonsoft.Json.JsonToken.Comment:
					break;
				}
			}
			throw new global::Newtonsoft.Json.JsonSerializationException("Unexpected end when deserializing object.");
		}

		public static global::Kampai.Game.BridgeCameraOffset ReadBridgeCameraOffset(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters = null)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None)
			{
				reader.Read();
			}
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
			{
				return null;
			}
			global::Kampai.Game.BridgeCameraOffset bridgeCameraOffset = new global::Kampai.Game.BridgeCameraOffset();
			EnsureToken(global::Newtonsoft.Json.JsonToken.StartObject, reader);
			while (reader.Read())
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.PropertyName:
					switch (((string)reader.Value).ToUpper())
					{
					case "X":
						reader.Read();
						bridgeCameraOffset.x = global::System.Convert.ToSingle(reader.Value);
						break;
					case "Y":
						reader.Read();
						bridgeCameraOffset.y = global::System.Convert.ToSingle(reader.Value);
						break;
					case "Z":
						reader.Read();
						bridgeCameraOffset.z = global::System.Convert.ToSingle(reader.Value);
						break;
					case "ZOOM":
						reader.Read();
						bridgeCameraOffset.zoom = global::System.Convert.ToSingle(reader.Value);
						break;
					default:
						reader.Skip();
						break;
					}
					break;
				case global::Newtonsoft.Json.JsonToken.EndObject:
					return bridgeCameraOffset;
				default:
					throw new global::Newtonsoft.Json.JsonSerializationException(string.Format("Unexpected token when deserializing object: {0}. {1}", reader.TokenType, GetPositionInSource(reader)));
				case global::Newtonsoft.Json.JsonToken.Comment:
					break;
				}
			}
			throw new global::Newtonsoft.Json.JsonSerializationException("Unexpected end when deserializing object.");
		}

		public static global::Kampai.Game.KnuckleheadednessInfo ReadKnuckleheadednessInfo(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters = null)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None)
			{
				reader.Read();
			}
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
			{
				return null;
			}
			global::Kampai.Game.KnuckleheadednessInfo knuckleheadednessInfo = new global::Kampai.Game.KnuckleheadednessInfo();
			EnsureToken(global::Newtonsoft.Json.JsonToken.StartObject, reader);
			while (reader.Read())
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.PropertyName:
					switch (((string)reader.Value).ToUpper())
					{
					case "KNUCKLEHEADDEDNESSMIN":
						reader.Read();
						knuckleheadednessInfo.KnuckleheaddednessMin = global::System.Convert.ToSingle(reader.Value);
						break;
					case "KNUCKLEHEADDEDNESSMAX":
						reader.Read();
						knuckleheadednessInfo.KnuckleheaddednessMax = global::System.Convert.ToSingle(reader.Value);
						break;
					case "KNUCKLEHEADDEDNESSSCALE":
						reader.Read();
						knuckleheadednessInfo.KnuckleheaddednessScale = global::System.Convert.ToSingle(reader.Value);
						break;
					default:
						reader.Skip();
						break;
					}
					break;
				case global::Newtonsoft.Json.JsonToken.EndObject:
					return knuckleheadednessInfo;
				default:
					throw new global::Newtonsoft.Json.JsonSerializationException(string.Format("Unexpected token when deserializing object: {0}. {1}", reader.TokenType, GetPositionInSource(reader)));
				case global::Newtonsoft.Json.JsonToken.Comment:
					break;
				}
			}
			throw new global::Newtonsoft.Json.JsonSerializationException("Unexpected end when deserializing object.");
		}

		public static global::Kampai.Game.AnimationAlternate ReadAnimationAlternate(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters = null)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None)
			{
				reader.Read();
			}
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
			{
				return null;
			}
			global::Kampai.Game.AnimationAlternate animationAlternate = new global::Kampai.Game.AnimationAlternate();
			EnsureToken(global::Newtonsoft.Json.JsonToken.StartObject, reader);
			while (reader.Read())
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.PropertyName:
					switch (((string)reader.Value).ToUpper())
					{
					case "GROUPID":
						reader.Read();
						animationAlternate.GroupID = global::System.Convert.ToInt32(reader.Value);
						break;
					case "PERCENTCHANCE":
						reader.Read();
						animationAlternate.PercentChance = global::System.Convert.ToSingle(reader.Value);
						break;
					default:
						reader.Skip();
						break;
					}
					break;
				case global::Newtonsoft.Json.JsonToken.EndObject:
					return animationAlternate;
				default:
					throw new global::Newtonsoft.Json.JsonSerializationException(string.Format("Unexpected token when deserializing object: {0}. {1}", reader.TokenType, GetPositionInSource(reader)));
				case global::Newtonsoft.Json.JsonToken.Comment:
					break;
				}
			}
			throw new global::Newtonsoft.Json.JsonSerializationException("Unexpected end when deserializing object.");
		}

		public static global::Kampai.Game.Transaction.TransactionInstance ReadTransactionInstance(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters = null)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None)
			{
				reader.Read();
			}
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
			{
				return null;
			}
			global::Kampai.Game.Transaction.TransactionInstance transactionInstance = new global::Kampai.Game.Transaction.TransactionInstance();
			EnsureToken(global::Newtonsoft.Json.JsonToken.StartObject, reader);
			while (reader.Read())
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.PropertyName:
					switch (((string)reader.Value).ToUpper())
					{
					case "ID":
						reader.Read();
						transactionInstance.ID = global::System.Convert.ToInt32(reader.Value);
						break;
					case "INPUTS":
						reader.Read();
						transactionInstance.Inputs = PopulateList<global::Kampai.Util.QuantityItem>(reader, converters);
						break;
					case "OUTPUTS":
						reader.Read();
						transactionInstance.Outputs = PopulateList<global::Kampai.Util.QuantityItem>(reader, converters);
						break;
					default:
						reader.Skip();
						break;
					}
					break;
				case global::Newtonsoft.Json.JsonToken.EndObject:
					return transactionInstance;
				default:
					throw new global::Newtonsoft.Json.JsonSerializationException(string.Format("Unexpected token when deserializing object: {0}. {1}", reader.TokenType, GetPositionInSource(reader)));
				case global::Newtonsoft.Json.JsonToken.Comment:
					break;
				}
			}
			throw new global::Newtonsoft.Json.JsonSerializationException("Unexpected end when deserializing object.");
		}

		public static global::Kampai.Game.QuestStepDefinition ReadQuestStepDefinition(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters = null)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None)
			{
				reader.Read();
			}
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
			{
				return null;
			}
			global::Kampai.Game.QuestStepDefinition questStepDefinition = new global::Kampai.Game.QuestStepDefinition();
			EnsureToken(global::Newtonsoft.Json.JsonToken.StartObject, reader);
			while (reader.Read())
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.PropertyName:
					switch (((string)reader.Value).ToUpper())
					{
					case "TYPE":
						reader.Read();
						questStepDefinition.Type = ReadEnum<global::Kampai.Game.QuestStepType>(reader);
						break;
					case "ITEMAMOUNT":
						reader.Read();
						questStepDefinition.ItemAmount = global::System.Convert.ToInt32(reader.Value);
						break;
					case "ITEMDEFINITIONID":
						reader.Read();
						questStepDefinition.ItemDefinitionID = global::System.Convert.ToInt32(reader.Value);
						break;
					case "COSTUMEDEFINITIONID":
						reader.Read();
						questStepDefinition.CostumeDefinitionID = global::System.Convert.ToInt32(reader.Value);
						break;
					case "SHOWWAYFINDER":
						reader.Read();
						questStepDefinition.ShowWayfinder = global::System.Convert.ToBoolean(reader.Value);
						break;
					default:
						reader.Skip();
						break;
					}
					break;
				case global::Newtonsoft.Json.JsonToken.EndObject:
					return questStepDefinition;
				default:
					throw new global::Newtonsoft.Json.JsonSerializationException(string.Format("Unexpected token when deserializing object: {0}. {1}", reader.TokenType, GetPositionInSource(reader)));
				case global::Newtonsoft.Json.JsonToken.Comment:
					break;
				}
			}
			throw new global::Newtonsoft.Json.JsonSerializationException("Unexpected end when deserializing object.");
		}

		public static global::Kampai.Game.QuestChainStepDefinition ReadQuestChainStepDefinition(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters = null)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None)
			{
				reader.Read();
			}
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
			{
				return null;
			}
			global::Kampai.Game.QuestChainStepDefinition questChainStepDefinition = new global::Kampai.Game.QuestChainStepDefinition();
			EnsureToken(global::Newtonsoft.Json.JsonToken.StartObject, reader);
			while (reader.Read())
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.PropertyName:
					switch (((string)reader.Value).ToUpper())
					{
					case "INTRO":
						reader.Read();
						questChainStepDefinition.Intro = ReadString(reader, converters);
						break;
					case "VOICE":
						reader.Read();
						questChainStepDefinition.Voice = ReadString(reader, converters);
						break;
					case "OUTRO":
						reader.Read();
						questChainStepDefinition.Outro = ReadString(reader, converters);
						break;
					case "XP":
						reader.Read();
						questChainStepDefinition.XP = global::System.Convert.ToInt32(reader.Value);
						break;
					case "GRIND":
						reader.Read();
						questChainStepDefinition.Grind = global::System.Convert.ToInt32(reader.Value);
						break;
					case "PREMIUM":
						reader.Read();
						questChainStepDefinition.Premium = global::System.Convert.ToInt32(reader.Value);
						break;
					case "TASKS":
						reader.Read();
						questChainStepDefinition.Tasks = PopulateList<global::Kampai.Game.QuestChainTask>(reader, converters, ReadQuestChainTask);
						break;
					default:
						reader.Skip();
						break;
					}
					break;
				case global::Newtonsoft.Json.JsonToken.EndObject:
					return questChainStepDefinition;
				default:
					throw new global::Newtonsoft.Json.JsonSerializationException(string.Format("Unexpected token when deserializing object: {0}. {1}", reader.TokenType, GetPositionInSource(reader)));
				case global::Newtonsoft.Json.JsonToken.Comment:
					break;
				}
			}
			throw new global::Newtonsoft.Json.JsonSerializationException("Unexpected end when deserializing object.");
		}

		public static global::Kampai.Game.QuestChainTask ReadQuestChainTask(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters = null)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None)
			{
				reader.Read();
			}
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
			{
				return null;
			}
			global::Kampai.Game.QuestChainTask questChainTask = new global::Kampai.Game.QuestChainTask();
			EnsureToken(global::Newtonsoft.Json.JsonToken.StartObject, reader);
			while (reader.Read())
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.PropertyName:
					switch (((string)reader.Value).ToUpper())
					{
					case "TYPE":
						reader.Read();
						questChainTask.Type = ReadEnum<global::Kampai.Game.QuestChainTaskType>(reader);
						break;
					case "ITEM":
						reader.Read();
						questChainTask.Item = global::System.Convert.ToInt32(reader.Value);
						break;
					case "COUNT":
						reader.Read();
						questChainTask.Count = global::System.Convert.ToInt32(reader.Value);
						break;
					default:
						reader.Skip();
						break;
					}
					break;
				case global::Newtonsoft.Json.JsonToken.EndObject:
					return questChainTask;
				default:
					throw new global::Newtonsoft.Json.JsonSerializationException(string.Format("Unexpected token when deserializing object: {0}. {1}", reader.TokenType, GetPositionInSource(reader)));
				case global::Newtonsoft.Json.JsonToken.Comment:
					break;
				}
			}
			throw new global::Newtonsoft.Json.JsonSerializationException("Unexpected end when deserializing object.");
		}

		public static global::Kampai.Game.PlatformStoreSkuDefinition ReadPlatformStoreSkuDefinition(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters = null)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None)
			{
				reader.Read();
			}
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
			{
				return null;
			}
			global::Kampai.Game.PlatformStoreSkuDefinition platformStoreSkuDefinition = new global::Kampai.Game.PlatformStoreSkuDefinition();
			EnsureToken(global::Newtonsoft.Json.JsonToken.StartObject, reader);
			while (reader.Read())
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.PropertyName:
					switch (((string)reader.Value).ToUpper())
					{
					case "APPLEAPPSTORE":
						reader.Read();
						platformStoreSkuDefinition.appleAppstore = ReadString(reader, converters);
						break;
					case "GOOGLEPLAY":
						reader.Read();
						platformStoreSkuDefinition.googlePlay = ReadString(reader, converters);
						break;
					case "DEFAULTSTORE":
						reader.Read();
						platformStoreSkuDefinition.defaultStore = ReadString(reader, converters);
						break;
					default:
						reader.Skip();
						break;
					}
					break;
				case global::Newtonsoft.Json.JsonToken.EndObject:
					return platformStoreSkuDefinition;
				default:
					throw new global::Newtonsoft.Json.JsonSerializationException(string.Format("Unexpected token when deserializing object: {0}. {1}", reader.TokenType, GetPositionInSource(reader)));
				case global::Newtonsoft.Json.JsonToken.Comment:
					break;
				}
			}
			throw new global::Newtonsoft.Json.JsonSerializationException("Unexpected end when deserializing object.");
		}

		public static global::Kampai.Game.SocialEventOrderDefinition ReadSocialEventOrderDefinition(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters = null)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None)
			{
				reader.Read();
			}
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
			{
				return null;
			}
			global::Kampai.Game.SocialEventOrderDefinition socialEventOrderDefinition = new global::Kampai.Game.SocialEventOrderDefinition();
			EnsureToken(global::Newtonsoft.Json.JsonToken.StartObject, reader);
			while (reader.Read())
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.PropertyName:
					switch (((string)reader.Value).ToUpper())
					{
					case "ORDERID":
						reader.Read();
						socialEventOrderDefinition.OrderID = global::System.Convert.ToInt32(reader.Value);
						break;
					case "TRANSACTION":
						reader.Read();
						socialEventOrderDefinition.Transaction = global::System.Convert.ToInt32(reader.Value);
						break;
					default:
						reader.Skip();
						break;
					}
					break;
				case global::Newtonsoft.Json.JsonToken.EndObject:
					return socialEventOrderDefinition;
				default:
					throw new global::Newtonsoft.Json.JsonSerializationException(string.Format("Unexpected token when deserializing object: {0}. {1}", reader.TokenType, GetPositionInSource(reader)));
				case global::Newtonsoft.Json.JsonToken.Comment:
					break;
				}
			}
			throw new global::Newtonsoft.Json.JsonSerializationException("Unexpected end when deserializing object.");
		}

		public static global::Kampai.Game.KampaiPendingTransaction ReadKampaiPendingTransaction(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters = null)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None)
			{
				reader.Read();
			}
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
			{
				return null;
			}
			global::Kampai.Game.KampaiPendingTransaction kampaiPendingTransaction = new global::Kampai.Game.KampaiPendingTransaction();
			EnsureToken(global::Newtonsoft.Json.JsonToken.StartObject, reader);
			while (reader.Read())
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.PropertyName:
					switch (((string)reader.Value).ToUpper())
					{
					case "EXTERNALIDENTIFIER":
						reader.Read();
						kampaiPendingTransaction.ExternalIdentifier = ReadString(reader, converters);
						break;
					case "TRANSACTION":
						reader.Read();
						kampaiPendingTransaction.Transaction = ((converters.transactionDefinitionConverter == null) ? global::Kampai.Util.FastJSONDeserializer.Deserialize<global::Kampai.Game.Transaction.TransactionDefinition>(reader, converters) : converters.transactionDefinitionConverter.ReadJson(reader, converters));
						break;
					case "STOREITEMDEFINITIONID":
						reader.Read();
						kampaiPendingTransaction.StoreItemDefinitionId = global::System.Convert.ToInt32(reader.Value);
						break;
					case "UTCTIMECREATED":
						reader.Read();
						kampaiPendingTransaction.UTCTimeCreated = global::System.Convert.ToInt32(reader.Value);
						break;
					default:
						reader.Skip();
						break;
					}
					break;
				case global::Newtonsoft.Json.JsonToken.EndObject:
					return kampaiPendingTransaction;
				default:
					throw new global::Newtonsoft.Json.JsonSerializationException(string.Format("Unexpected token when deserializing object: {0}. {1}", reader.TokenType, GetPositionInSource(reader)));
				case global::Newtonsoft.Json.JsonToken.Comment:
					break;
				}
			}
			throw new global::Newtonsoft.Json.JsonSerializationException("Unexpected end when deserializing object.");
		}

		public static global::Kampai.Game.UnlockedItem ReadUnlockedItem(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters = null)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None)
			{
				reader.Read();
			}
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
			{
				return null;
			}
			global::Kampai.Game.UnlockedItem unlockedItem = new global::Kampai.Game.UnlockedItem();
			EnsureToken(global::Newtonsoft.Json.JsonToken.StartObject, reader);
			while (reader.Read())
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.PropertyName:
					switch (((string)reader.Value).ToUpper())
					{
					case "DEFID":
						reader.Read();
						unlockedItem.defID = global::System.Convert.ToInt32(reader.Value);
						break;
					case "QUANTITY":
						reader.Read();
						unlockedItem.quantity = global::System.Convert.ToInt32(reader.Value);
						break;
					default:
						reader.Skip();
						break;
					}
					break;
				case global::Newtonsoft.Json.JsonToken.EndObject:
					return unlockedItem;
				default:
					throw new global::Newtonsoft.Json.JsonSerializationException(string.Format("Unexpected token when deserializing object: {0}. {1}", reader.TokenType, GetPositionInSource(reader)));
				case global::Newtonsoft.Json.JsonToken.Comment:
					break;
				}
			}
			throw new global::Newtonsoft.Json.JsonSerializationException("Unexpected end when deserializing object.");
		}

		public static global::Kampai.Game.SocialClaimRewardItem ReadSocialClaimRewardItem(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters = null)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None)
			{
				reader.Read();
			}
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
			{
				return null;
			}
			global::Kampai.Game.SocialClaimRewardItem socialClaimRewardItem = new global::Kampai.Game.SocialClaimRewardItem();
			EnsureToken(global::Newtonsoft.Json.JsonToken.StartObject, reader);
			while (reader.Read())
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.PropertyName:
					switch (((string)reader.Value).ToUpper())
					{
					case "EVENTID":
						reader.Read();
						socialClaimRewardItem.eventID = global::System.Convert.ToInt32(reader.Value);
						break;
					case "CLAIMSTATE":
						reader.Read();
						socialClaimRewardItem.claimState = ReadEnum<global::Kampai.Game.SocialClaimRewardItem.ClaimState>(reader);
						break;
					default:
						reader.Skip();
						break;
					}
					break;
				case global::Newtonsoft.Json.JsonToken.EndObject:
					return socialClaimRewardItem;
				default:
					throw new global::Newtonsoft.Json.JsonSerializationException(string.Format("Unexpected token when deserializing object: {0}. {1}", reader.TokenType, GetPositionInSource(reader)));
				case global::Newtonsoft.Json.JsonToken.Comment:
					break;
				}
			}
			throw new global::Newtonsoft.Json.JsonSerializationException("Unexpected end when deserializing object.");
		}

		public static global::Kampai.Game.QuestStep ReadQuestStep(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters = null)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None)
			{
				reader.Read();
			}
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
			{
				return null;
			}
			global::Kampai.Game.QuestStep questStep = new global::Kampai.Game.QuestStep();
			EnsureToken(global::Newtonsoft.Json.JsonToken.StartObject, reader);
			while (reader.Read())
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.PropertyName:
					switch (((string)reader.Value).ToUpper())
					{
					case "STATE":
						reader.Read();
						questStep.state = ReadEnum<global::Kampai.Game.QuestStepState>(reader);
						break;
					case "AMOUNTCOMPLETED":
						reader.Read();
						questStep.AmountCompleted = global::System.Convert.ToInt32(reader.Value);
						break;
					case "AMOUNTREADY":
						reader.Read();
						questStep.AmountReady = global::System.Convert.ToInt32(reader.Value);
						break;
					case "TRACKEDID":
						reader.Read();
						questStep.TrackedID = global::System.Convert.ToInt32(reader.Value);
						break;
					default:
						reader.Skip();
						break;
					}
					break;
				case global::Newtonsoft.Json.JsonToken.EndObject:
					return questStep;
				default:
					throw new global::Newtonsoft.Json.JsonSerializationException(string.Format("Unexpected token when deserializing object: {0}. {1}", reader.TokenType, GetPositionInSource(reader)));
				case global::Newtonsoft.Json.JsonToken.Comment:
					break;
				}
			}
			throw new global::Newtonsoft.Json.JsonSerializationException("Unexpected end when deserializing object.");
		}

		public static global::Kampai.Game.OrderBoardTicket ReadOrderBoardTicket(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters = null)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None)
			{
				reader.Read();
			}
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
			{
				return null;
			}
			global::Kampai.Game.OrderBoardTicket orderBoardTicket = new global::Kampai.Game.OrderBoardTicket();
			EnsureToken(global::Newtonsoft.Json.JsonToken.StartObject, reader);
			while (reader.Read())
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.PropertyName:
					switch (((string)reader.Value).ToUpper())
					{
					case "TRANSACTIONINST":
						reader.Read();
						orderBoardTicket.TransactionInst = ReadTransactionInstance(reader, converters);
						break;
					case "BOARDINDEX":
						reader.Read();
						orderBoardTicket.BoardIndex = global::System.Convert.ToInt32(reader.Value);
						break;
					case "ORDERNAMETABLEINDEX":
						reader.Read();
						orderBoardTicket.OrderNameTableIndex = global::System.Convert.ToInt32(reader.Value);
						break;
					case "STARTTIME":
						reader.Read();
						orderBoardTicket.StartTime = global::System.Convert.ToInt32(reader.Value);
						break;
					case "CHARACTERDEFINITIONID":
						reader.Read();
						orderBoardTicket.CharacterDefinitionId = global::System.Convert.ToInt32(reader.Value);
						break;
					case "DIFFICULTY":
						reader.Read();
						orderBoardTicket.Difficulty = ReadEnum<global::Kampai.Game.OrderBoardTicketDifficulty>(reader);
						break;
					default:
						reader.Skip();
						break;
					}
					break;
				case global::Newtonsoft.Json.JsonToken.EndObject:
					return orderBoardTicket;
				default:
					throw new global::Newtonsoft.Json.JsonSerializationException(string.Format("Unexpected token when deserializing object: {0}. {1}", reader.TokenType, GetPositionInSource(reader)));
				case global::Newtonsoft.Json.JsonToken.Comment:
					break;
				}
			}
			throw new global::Newtonsoft.Json.JsonSerializationException("Unexpected end when deserializing object.");
		}

		public static global::Kampai.Game.UserIdentity ReadUserIdentity(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters = null)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None)
			{
				reader.Read();
			}
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
			{
				return null;
			}
			global::Kampai.Game.UserIdentity userIdentity = new global::Kampai.Game.UserIdentity();
			EnsureToken(global::Newtonsoft.Json.JsonToken.StartObject, reader);
			while (reader.Read())
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.PropertyName:
					switch (((string)reader.Value).ToUpper())
					{
					case "id":
						reader.Read();
						userIdentity.ID = ReadString(reader, converters);
						break;
					case "externalId":
						reader.Read();
						userIdentity.ExternalID = ReadString(reader, converters);
						break;
					case "userId":
						reader.Read();
						userIdentity.UserID = ReadString(reader, converters);
						break;
					case "type":
						reader.Read();
						userIdentity.Type = ReadEnum<global::Kampai.Game.IdentityType>(reader);
						break;
					default:
						reader.Skip();
						break;
					}
					break;
				case global::Newtonsoft.Json.JsonToken.EndObject:
					return userIdentity;
				default:
					throw new global::Newtonsoft.Json.JsonSerializationException(string.Format("Unexpected token when deserializing object: {0}. {1}", reader.TokenType, GetPositionInSource(reader)));
				case global::Newtonsoft.Json.JsonToken.Comment:
					break;
				}
			}
			throw new global::Newtonsoft.Json.JsonSerializationException("Unexpected end when deserializing object.");
		}

		public static global::Kampai.Game.SocialOrderProgress ReadSocialOrderProgress(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters = null)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None)
			{
				reader.Read();
			}
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
			{
				return null;
			}
			global::Kampai.Game.SocialOrderProgress socialOrderProgress = new global::Kampai.Game.SocialOrderProgress();
			EnsureToken(global::Newtonsoft.Json.JsonToken.StartObject, reader);
			while (reader.Read())
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.PropertyName:
					switch (((string)reader.Value).ToUpper())
					{
					case "ORDERID":
						reader.Read();
						socialOrderProgress.OrderId = global::System.Convert.ToInt32(reader.Value);
						break;
					case "COMPLETEDBYUSERID":
						reader.Read();
						socialOrderProgress.CompletedByUserId = ReadString(reader, converters);
						break;
					default:
						reader.Skip();
						break;
					}
					break;
				case global::Newtonsoft.Json.JsonToken.EndObject:
					return socialOrderProgress;
				default:
					throw new global::Newtonsoft.Json.JsonSerializationException(string.Format("Unexpected token when deserializing object: {0}. {1}", reader.TokenType, GetPositionInSource(reader)));
				case global::Newtonsoft.Json.JsonToken.Comment:
					break;
				}
			}
			throw new global::Newtonsoft.Json.JsonSerializationException("Unexpected end when deserializing object.");
		}

		public static global::Kampai.Game.GachaConfig ReadGachaConfig(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters = null)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None)
			{
				reader.Read();
			}
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
			{
				return null;
			}
			global::Kampai.Game.GachaConfig gachaConfig = new global::Kampai.Game.GachaConfig();
			EnsureToken(global::Newtonsoft.Json.JsonToken.StartObject, reader);
			while (reader.Read())
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.PropertyName:
					switch (((string)reader.Value).ToUpper())
					{
					case "GATCHAANIMATIONDEFINITIONS":
						reader.Read();
						gachaConfig.GatchaAnimationDefinitions = PopulateList<global::Kampai.Game.GachaAnimationDefinition>(reader, converters);
						break;
					case "DISTRIBUTIONTABLES":
						reader.Read();
						gachaConfig.DistributionTables = PopulateList<global::Kampai.Game.GachaWeightedDefinition>(reader, converters);
						break;
					default:
						reader.Skip();
						break;
					}
					break;
				case global::Newtonsoft.Json.JsonToken.EndObject:
					return gachaConfig;
				default:
					throw new global::Newtonsoft.Json.JsonSerializationException(string.Format("Unexpected token when deserializing object: {0}. {1}", reader.TokenType, GetPositionInSource(reader)));
				case global::Newtonsoft.Json.JsonToken.Comment:
					break;
				}
			}
			throw new global::Newtonsoft.Json.JsonSerializationException("Unexpected end when deserializing object.");
		}

		public static global::Kampai.Game.TaskDefinition ReadTaskDefinition(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters = null)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None)
			{
				reader.Read();
			}
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
			{
				return null;
			}
			global::Kampai.Game.TaskDefinition taskDefinition = new global::Kampai.Game.TaskDefinition();
			EnsureToken(global::Newtonsoft.Json.JsonToken.StartObject, reader);
			while (reader.Read())
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.PropertyName:
					switch (((string)reader.Value).ToUpper())
					{
					case "LEVELBANDS":
						reader.Read();
						taskDefinition.levelBands = PopulateList<global::Kampai.Game.TaskLevelBandDefinition>(reader, converters);
						break;
					default:
						reader.Skip();
						break;
					}
					break;
				case global::Newtonsoft.Json.JsonToken.EndObject:
					return taskDefinition;
				default:
					throw new global::Newtonsoft.Json.JsonSerializationException(string.Format("Unexpected token when deserializing object: {0}. {1}", reader.TokenType, GetPositionInSource(reader)));
				case global::Newtonsoft.Json.JsonToken.Comment:
					break;
				}
			}
			throw new global::Newtonsoft.Json.JsonSerializationException("Unexpected end when deserializing object.");
		}

		public static global::Kampai.Splash.BucketAssignment ReadBucketAssignment(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters = null)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None)
			{
				reader.Read();
			}
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
			{
				return null;
			}
			global::Kampai.Splash.BucketAssignment bucketAssignment = new global::Kampai.Splash.BucketAssignment();
			EnsureToken(global::Newtonsoft.Json.JsonToken.StartObject, reader);
			while (reader.Read())
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.PropertyName:
					switch (((string)reader.Value).ToUpper())
					{
					case "BUCKETID":
						reader.Read();
						bucketAssignment.BucketId = global::System.Convert.ToInt32(reader.Value);
						break;
					case "TIME":
						reader.Read();
						bucketAssignment.Time = global::System.Convert.ToSingle(reader.Value);
						break;
					default:
						reader.Skip();
						break;
					}
					break;
				case global::Newtonsoft.Json.JsonToken.EndObject:
					return bucketAssignment;
				default:
					throw new global::Newtonsoft.Json.JsonSerializationException(string.Format("Unexpected token when deserializing object: {0}. {1}", reader.TokenType, GetPositionInSource(reader)));
				case global::Newtonsoft.Json.JsonToken.Comment:
					break;
				}
			}
			throw new global::Newtonsoft.Json.JsonSerializationException("Unexpected end when deserializing object.");
		}

		public static string ReadString(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
			{
				return null;
			}
			return global::System.Convert.ToString(reader.Value);
		}

		public static bool ReadBool(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			return global::System.Convert.ToBoolean(reader.Value);
		}

		public static global::System.Collections.Generic.Dictionary<string, string> ReadDictionaryString(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			return ReadDictionary<string>(reader, converters, ReadString);
		}

		public static T ReadEnum<T>(global::Newtonsoft.Json.JsonReader reader)
		{
			switch (reader.TokenType)
			{
			case global::Newtonsoft.Json.JsonToken.PropertyName:
			case global::Newtonsoft.Json.JsonToken.String:
				return (T)global::System.Enum.Parse(typeof(T), (string)reader.Value, true);
			case global::Newtonsoft.Json.JsonToken.Integer:
				return (T)global::System.Enum.ToObject(typeof(T), reader.Value);
			default:
				throw new global::Newtonsoft.Json.JsonSerializationException(string.Format("Unexpected can't read enum {0}. {1}", typeof(T), GetPositionInSource(reader)));
			}
		}

		public static global::System.Collections.Generic.Dictionary<string, object> ReadDictionary(global::Newtonsoft.Json.JsonReader reader)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None)
			{
				reader.Read();
			}
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
			{
				return null;
			}
			global::Newtonsoft.Json.Linq.JObject jObject = global::Newtonsoft.Json.Linq.JObject.Load(reader);
			global::Newtonsoft.Json.JsonSerializer jsonSerializer = new global::Newtonsoft.Json.JsonSerializer();
			return jsonSerializer.Deserialize<global::System.Collections.Generic.Dictionary<string, object>>(jObject.CreateReader());
		}

		public static global::System.Collections.Generic.Dictionary<string, T> ReadDictionary<T>(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters = null) where T : global::Kampai.Util.IFastJSONDeserializable, new()
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None)
			{
				reader.Read();
			}
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
			{
				return null;
			}
			global::System.Collections.Generic.Dictionary<string, T> dictionary = new global::System.Collections.Generic.Dictionary<string, T>();
			EnsureToken(global::Newtonsoft.Json.JsonToken.StartObject, reader);
			while (reader.Read())
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.PropertyName:
				{
					string key = (string)reader.Value;
					reader.Read();
					T value = global::Kampai.Util.FastJSONDeserializer.Deserialize<T>(reader, converters);
					dictionary.Add(key, value);
					break;
				}
				case global::Newtonsoft.Json.JsonToken.EndObject:
					return dictionary;
				default:
					throw new global::Newtonsoft.Json.JsonSerializationException(string.Format("Unexpected token when deserializing object: {0}. {1}", reader.TokenType, GetPositionInSource(reader)));
				case global::Newtonsoft.Json.JsonToken.Comment:
					break;
				}
			}
			throw new global::Newtonsoft.Json.JsonSerializationException("Unexpected end when deserializing object.");
		}

		public static global::System.Collections.Generic.Dictionary<string, T> ReadDictionary<T>(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters, global::System.Func<global::Newtonsoft.Json.JsonReader, JsonConverters, T> valueReader)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None)
			{
				reader.Read();
			}
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
			{
				return null;
			}
			global::System.Collections.Generic.Dictionary<string, T> dictionary = new global::System.Collections.Generic.Dictionary<string, T>();
			EnsureToken(global::Newtonsoft.Json.JsonToken.StartObject, reader);
			while (reader.Read())
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.PropertyName:
				{
					string key = (string)reader.Value;
					reader.Read();
					T value = valueReader(reader, converters);
					dictionary.Add(key, value);
					break;
				}
				case global::Newtonsoft.Json.JsonToken.EndObject:
					return dictionary;
				default:
					throw new global::Newtonsoft.Json.JsonSerializationException(string.Format("Unexpected token when deserializing object: {0}. {1}", reader.TokenType, GetPositionInSource(reader)));
				case global::Newtonsoft.Json.JsonToken.Comment:
					break;
				}
			}
			throw new global::Newtonsoft.Json.JsonSerializationException("Unexpected end when deserializing object.");
		}

		public static global::System.Collections.Generic.Dictionary<K, V> ReadDictionary<K, V>(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters, global::System.Func<global::Newtonsoft.Json.JsonReader, JsonConverters, K> keyReader, global::System.Func<global::Newtonsoft.Json.JsonReader, JsonConverters, V> valueReader)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.None)
			{
				reader.Read();
			}
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
			{
				return null;
			}
			global::System.Collections.Generic.Dictionary<K, V> dictionary = new global::System.Collections.Generic.Dictionary<K, V>();
			EnsureToken(global::Newtonsoft.Json.JsonToken.StartObject, reader);
			while (reader.Read())
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.PropertyName:
				{
					K key = keyReader(reader, converters);
					reader.Read();
					V value = valueReader(reader, converters);
					dictionary.Add(key, value);
					break;
				}
				case global::Newtonsoft.Json.JsonToken.EndObject:
					return dictionary;
				default:
					throw new global::Newtonsoft.Json.JsonSerializationException(string.Format("Unexpected token when deserializing object: {0}. {1}", reader.TokenType, GetPositionInSource(reader)));
				case global::Newtonsoft.Json.JsonToken.Comment:
					break;
				}
			}
			throw new global::Newtonsoft.Json.JsonSerializationException("Unexpected end when deserializing object.");
		}

		public static global::System.Collections.Generic.List<T> PopulateList<T>(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters, global::System.Func<global::Newtonsoft.Json.JsonReader, JsonConverters, T> elementReader)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
			{
				return null;
			}
			global::System.Collections.Generic.List<T> list = new global::System.Collections.Generic.List<T>();
			EnsureToken(global::Newtonsoft.Json.JsonToken.StartArray, reader);
			while (reader.Read())
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.EndArray:
					return list;
				case global::Newtonsoft.Json.JsonToken.Comment:
					continue;
				}
				T item = elementReader(reader, converters);
				list.Add(item);
			}
			throw new global::Newtonsoft.Json.JsonSerializationException(string.Format("Unexpected end when deserializing list. {0}", GetPositionInSource(reader)));
		}

		public static global::System.Collections.Generic.List<T> PopulateList<T>(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters, global::Kampai.Util.FastJsonConverter<T> converter) where T : class, global::Kampai.Util.IFastJSONDeserializable
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
			{
				return null;
			}
			global::System.Collections.Generic.List<T> list = new global::System.Collections.Generic.List<T>();
			EnsureToken(global::Newtonsoft.Json.JsonToken.StartArray, reader);
			while (reader.Read())
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.EndArray:
					return list;
				case global::Newtonsoft.Json.JsonToken.Comment:
					continue;
				}
				T item = converter.ReadJson(reader, converters);
				list.Add(item);
			}
			throw new global::Newtonsoft.Json.JsonSerializationException(string.Format("Unexpected end when deserializing list. {0}", GetPositionInSource(reader)));
		}

		public static global::System.Collections.Generic.List<T> PopulateList<T>(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters = null) where T : global::Kampai.Util.IFastJSONDeserializable, new()
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
			{
				return null;
			}
			global::System.Collections.Generic.List<T> list = new global::System.Collections.Generic.List<T>();
			EnsureToken(global::Newtonsoft.Json.JsonToken.StartArray, reader);
			while (reader.Read())
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.EndArray:
					return list;
				case global::Newtonsoft.Json.JsonToken.Comment:
					continue;
				}
				T item = new T();
				item.Deserialize(reader, converters);
				list.Add(item);
			}
			throw new global::Newtonsoft.Json.JsonSerializationException(string.Format("Unexpected end when deserializing list. {0}", GetPositionInSource(reader)));
		}

		public static global::System.Collections.Generic.List<string> PopulateListString(global::Newtonsoft.Json.JsonReader reader)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
			{
				return null;
			}
			global::System.Collections.Generic.List<string> list = new global::System.Collections.Generic.List<string>();
			EnsureToken(global::Newtonsoft.Json.JsonToken.StartArray, reader);
			while (reader.Read())
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.EndArray:
					return list;
				case global::Newtonsoft.Json.JsonToken.String:
				{
					string item = (string)reader.Value;
					list.Add(item);
					break;
				}
				default:
					throw new global::Newtonsoft.Json.JsonSerializationException(string.Format("Unexpected element type on list when deserializiong string list: {0}. {1}", reader.TokenType, GetPositionInSource(reader)));
				case global::Newtonsoft.Json.JsonToken.Comment:
					break;
				}
			}
			throw new global::Newtonsoft.Json.JsonSerializationException(string.Format("Unexpected end when deserializing string list. {0}", GetPositionInSource(reader)));
		}

		public static global::System.Collections.Generic.List<int> PopulateListInt32(global::Newtonsoft.Json.JsonReader reader)
		{
			if (reader.TokenType == global::Newtonsoft.Json.JsonToken.Null)
			{
				return null;
			}
			global::System.Collections.Generic.List<int> list = new global::System.Collections.Generic.List<int>();
			EnsureToken(global::Newtonsoft.Json.JsonToken.StartArray, reader);
			while (reader.Read())
			{
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.EndArray:
					return list;
				case global::Newtonsoft.Json.JsonToken.Integer:
				{
					int item = global::System.Convert.ToInt32(reader.Value);
					list.Add(item);
					break;
				}
				default:
					throw new global::Newtonsoft.Json.JsonSerializationException(string.Format("Unexpected element type on list when deserializiong int list: {0}. {1}", reader.TokenType, GetPositionInSource(reader)));
				case global::Newtonsoft.Json.JsonToken.Comment:
					break;
				}
			}
			throw new global::Newtonsoft.Json.JsonSerializationException(string.Format("Unexpected end when deserializing string list. {0}", GetPositionInSource(reader)));
		}

		public static void EnsureToken(global::Newtonsoft.Json.JsonToken token, global::Newtonsoft.Json.JsonReader reader)
		{
			if (reader.TokenType != token)
			{
				throw new global::Newtonsoft.Json.JsonSerializationException(string.Format("Expected token {0}. Encountered {1} instead. {2}", token, reader.TokenType, GetPositionInSource(reader)));
			}
		}

		public static T ReaderNotImplemented<T>(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters = null)
		{
			throw new global::Newtonsoft.Json.JsonSerializationException("Reading of this entity is not implemented.");
		}

		public static string GetPositionInSource(global::Newtonsoft.Json.JsonReader reader)
		{
			global::Newtonsoft.Json.JsonTextReader jsonTextReader = reader as global::Newtonsoft.Json.JsonTextReader;
			if (jsonTextReader != null)
			{
				return string.Format("Line number: {0}, Line position: {1}", jsonTextReader.LineNumber, jsonTextReader.LinePosition);
			}
			return "Line number: -, Line position: -";
		}

		public static global::System.Collections.Generic.Dictionary<global::Kampai.Game.ConfigurationDefinition.RateAppAfterEvent, bool> ReadRateAppTriggerConfig(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			return ReadDictionary<global::Kampai.Game.ConfigurationDefinition.RateAppAfterEvent, bool>(reader, converters, ReadRateAppAfterEvent, ReadBool);
		}

		public static global::Kampai.Game.ConfigurationDefinition.RateAppAfterEvent ReadRateAppAfterEvent(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			return ReadEnum<global::Kampai.Game.ConfigurationDefinition.RateAppAfterEvent>(reader);
		}

		public static global::Kampai.Game.KillSwitch ReadKillSwitch(global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			return ReadEnum<global::Kampai.Game.KillSwitch>(reader);
		}
	}
}
