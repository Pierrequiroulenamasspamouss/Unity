namespace Kampai.Common
{
	public class TelemetryService : global::Kampai.Common.IIapTelemetryService, global::Kampai.Common.ITelemetryService
	{
		private const string EVT_KEYTYPE_ENUMERATION = "EVT_KEYTYPE_ENUMERATION";

		private const string EVT_KEYTYPE_SCORE = "EVT_KEYTYPE_SCORE";

		private const string EVT_KEYTYPE_DURATION = "EVT_KEYTYPE_DURATION";

		private const string EVT_KEYTYPE_NONE = "EVT_KEYTYPE_NONE";

		private global::Kampai.Game.IPlayerService playerService;

		private global::Kampai.Game.IPlayerDurationService playerDurationService;

		private global::Kampai.Game.IDefinitionService definitionService;

		private global::System.Collections.Generic.List<global::Kampai.Common.ITelemetrySender> telemetrySenders = new global::System.Collections.Generic.List<global::Kampai.Common.ITelemetrySender>();

		private global::System.Collections.Generic.List<global::Kampai.Common.IIapTelemetryService> iapTelemetryServices = new global::System.Collections.Generic.List<global::Kampai.Common.IIapTelemetryService>();

		private float lastFunnelEalTime;

		private float lastGameLoadFunnelTime;

		[Inject]
		public ILocalPersistanceService localPersistService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		public void AddTelemetrySender(global::Kampai.Common.ITelemetrySender sender)
		{
			foreach (global::Kampai.Common.ITelemetrySender telemetrySender in telemetrySenders)
			{
				if (object.ReferenceEquals(telemetrySender, sender))
				{
					return;
				}
			}
			telemetrySenders.Add(sender);
		}

		public void AddIapTelemetryService(global::Kampai.Common.IIapTelemetryService service)
		{
			foreach (global::Kampai.Common.IIapTelemetryService iapTelemetryService in iapTelemetryServices)
			{
				if (object.ReferenceEquals(iapTelemetryService, service))
				{
					return;
				}
			}
			iapTelemetryServices.Add(service);
		}

		public void SharingUsage(global::Kampai.Common.ITelemetrySender sender, bool enabled)
		{
			foreach (global::Kampai.Common.ITelemetrySender telemetrySender in telemetrySenders)
			{
				if (object.ReferenceEquals(telemetrySender.GetType(), sender.GetType()))
				{
					telemetrySender.SharingUsage(SharingUsageEnabled() && enabled);
					break;
				}
			}
		}

		public virtual void LogGameEvent(global::Kampai.Common.TelemetryEvent gameEvent)
		{
			foreach (global::Kampai.Common.ITelemetrySender telemetrySender in telemetrySenders)
			{
				telemetrySender.SendEvent(gameEvent);
			}
		}

		public virtual void COPPACompliance()
		{
			foreach (global::Kampai.Common.ITelemetrySender telemetrySender in telemetrySenders)
			{
				telemetrySender.COPPACompliance();
			}
		}

		public void SharingUsageCompliance()
		{
			bool enabled = SharingUsageEnabled();
			foreach (global::Kampai.Common.ITelemetrySender telemetrySender in telemetrySenders)
			{
				telemetrySender.SharingUsage(enabled);
			}
		}

		public void SharingUsage(bool enabled)
		{
			localPersistService.PutDataIntPlayer("SharingUsage", enabled ? 1 : 0);
			foreach (global::Kampai.Common.ITelemetrySender telemetrySender in telemetrySenders)
			{
				telemetrySender.SharingUsage(enabled);
			}
		}

		public bool SharingUsageEnabled()
		{
			if (localPersistService.HasKeyPlayer("SharingUsage"))
			{
				return localPersistService.GetDataIntPlayer("SharingUsage") != 0;
			}
			return true;
		}

		public global::System.Collections.Generic.IList<global::Kampai.Common.TelemetryParameter> GetLevelGrindPremium()
		{
			global::System.Collections.Generic.IList<global::Kampai.Common.TelemetryParameter> list = new global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter>();
			list.Add(getPlayerItem(global::Kampai.Game.StaticItem.LEVEL_ID, global::Kampai.Common.ParameterName.LEVEL));
			list.Add(getPlayerItem(global::Kampai.Game.StaticItem.GRIND_CURRENCY_ID, global::Kampai.Common.ParameterName.GRIND_CURRENCY_BALANCE));
			list.Add(getPlayerItem(global::Kampai.Game.StaticItem.PREMIUM_CURRENCY_ID, global::Kampai.Common.ParameterName.PREMIUM_CURRENCY_BALANCE));
			return list;
		}

		public global::Kampai.Common.TelemetryParameter getPlayerItem(global::Kampai.Game.StaticItem item, global::Kampai.Common.ParameterName name)
		{
			object value = getPlayerItemValue(item);
			return new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", value, name);
		}

		private uint getPlayerItemValue(global::Kampai.Game.StaticItem item)
		{
			return (playerService != null && playerService.IsPlayerInitialized()) ? playerService.GetQuantity(item) : 0u;
		}

		public void Send_Telemetry_EVT_GAME_ERROR_GAMEPLAY(string nameOfError, string errorDetails)
		{
			global::Kampai.Common.TelemetryEvent telemetryEvent = new global::Kampai.Common.TelemetryEvent(SynergyTrackingEventType.EVT_GAME_ERROR_GAMEPLAY);
			global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter> list = new global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter>();
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", nameOfError, global::Kampai.Common.ParameterName.ERROR_NAME));
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", errorDetails, global::Kampai.Common.ParameterName.ERROR_DETAILS));
			list.AddRange(GetLevelGrindPremium());
			telemetryEvent.Parameters = list;
			LogGameEvent(telemetryEvent);
		}

		public void Send_Telemetry_EVT_GAME_ERROR_CONNECTIVITY(string nameOfError, string errorDetails)
		{
			global::Kampai.Common.TelemetryEvent telemetryEvent = new global::Kampai.Common.TelemetryEvent(SynergyTrackingEventType.EVT_GAME_ERROR_CONNECTIVITY);
			global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter> list = new global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter>();
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", nameOfError, global::Kampai.Common.ParameterName.ERROR_NAME));
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", errorDetails, global::Kampai.Common.ParameterName.ERROR_DETAILS));
			list.AddRange(GetLevelGrindPremium());
			telemetryEvent.Parameters = list;
			LogGameEvent(telemetryEvent);
		}

		public void Send_Telemetry_EVT_IGE_FREE_CREDITS_EARNED(int grindEarned, string eventName, bool purchasedCurrencySpent)
		{
			if (grindEarned != 0)
			{
				global::Kampai.Common.TelemetryEvent telemetryEvent = new global::Kampai.Common.TelemetryEvent(SynergyTrackingEventType.EVT_IGE_FREE_CREDITS_EARNED);
				global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter> list = new global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter>();
				list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_SCORE", grindEarned, global::Kampai.Common.ParameterName.GRIND_CURRENCY_EARNED));
				list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", eventName, global::Kampai.Common.ParameterName.EVENT_NAME));
				list.AddRange(GetLevelGrindPremium());
				list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", PremiumPurchaseArgument(purchasedCurrencySpent), global::Kampai.Common.ParameterName.CURRENCY_EARN_SPEND_TYPE));
				telemetryEvent.Parameters = list;
				LogGameEvent(telemetryEvent);
			}
		}

		public void Send_Telemetry_EVT_IGE_PAID_CREDITS_EARNED(int premiumEarned, string eventName, bool purchasedCurrencySpent)
		{
			if (premiumEarned != 0)
			{
				global::Kampai.Common.TelemetryEvent telemetryEvent = new global::Kampai.Common.TelemetryEvent(SynergyTrackingEventType.EVT_IGE_PAID_CREDITS_EARNED);
				global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter> list = new global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter>();
				list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_SCORE", premiumEarned, global::Kampai.Common.ParameterName.PREMIUM_CURRENCY_EARNED));
				list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", eventName, global::Kampai.Common.ParameterName.EVENT_NAME));
				list.AddRange(GetLevelGrindPremium());
				list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", PremiumPurchaseArgument(purchasedCurrencySpent), global::Kampai.Common.ParameterName.CURRENCY_EARN_SPEND_TYPE));
				telemetryEvent.Parameters = list;
				LogGameEvent(telemetryEvent);
			}
		}

		public void Send_Telemetry_EVT_IGE_FREE_CREDITS_PURCHASE_REVENUE(int grindSpent, string itemPurchased, bool purchasedCurrencySpent, string highLevel, string specific, string type, string other)
		{
			if (grindSpent != 0)
			{
				global::Kampai.Common.TelemetryEvent telemetryEvent = new global::Kampai.Common.TelemetryEvent(SynergyTrackingEventType.EVT_IGE_FREE_CREDITS_PURCHASE_REVENUE);
				global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter> list = new global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter>();
				list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_SCORE", grindSpent, global::Kampai.Common.ParameterName.GRIND_CURRENCY_SPENT));
				list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", itemPurchased, global::Kampai.Common.ParameterName.ITEM_PURCHASED));
				list.AddRange(GetLevelGrindPremium());
				list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", PremiumPurchaseArgument(purchasedCurrencySpent), global::Kampai.Common.ParameterName.CURRENCY_EARN_SPEND_TYPE));
				list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", highLevel, global::Kampai.Common.ParameterName.TAXONOMY_HIGH_LEVEL));
				list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", specific, global::Kampai.Common.ParameterName.TAXONOMY_SPECIFIC));
				list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", type, global::Kampai.Common.ParameterName.TAXONOMY_TYPE));
				list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", other, global::Kampai.Common.ParameterName.TAXONOMY_OTHER));
				telemetryEvent.Parameters = list;
				LogGameEvent(telemetryEvent);
			}
		}

		public void Send_Telemetry_EVT_IGE_PAID_CREDITS_PURCHASE_REVENUE(int premiumSpent, string itemPurchased, bool purchasedCurrencySpent, string highLevel, string specific, string type, string other)
		{
			if (premiumSpent != 0)
			{
				global::Kampai.Common.TelemetryEvent telemetryEvent = new global::Kampai.Common.TelemetryEvent(SynergyTrackingEventType.EVT_IGE_PAID_CREDITS_PURCHASE_REVENUE);
				global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter> list = new global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter>();
				list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_SCORE", premiumSpent, global::Kampai.Common.ParameterName.PREMIUM_CURRENCY_SPENT));
				list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", itemPurchased, global::Kampai.Common.ParameterName.ITEM_PURCHASED));
				list.AddRange(GetLevelGrindPremium());
				list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", PremiumPurchaseArgument(purchasedCurrencySpent), global::Kampai.Common.ParameterName.CURRENCY_EARN_SPEND_TYPE));
				list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", highLevel, global::Kampai.Common.ParameterName.TAXONOMY_HIGH_LEVEL));
				list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", specific, global::Kampai.Common.ParameterName.TAXONOMY_SPECIFIC));
				list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", type, global::Kampai.Common.ParameterName.TAXONOMY_TYPE));
				list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", other, global::Kampai.Common.ParameterName.TAXONOMY_OTHER));
				telemetryEvent.Parameters = list;
				LogGameEvent(telemetryEvent);
			}
		}

		public void Send_Telemetry_EVT_IGE_RESOURCE_CRAFTABLE_EARNED(int amount, string sourceName, string itemName, string highLevel, string specific, string type, string other)
		{
			global::Kampai.Common.TelemetryEvent telemetryEvent = new global::Kampai.Common.TelemetryEvent(SynergyTrackingEventType.EVT_IGE_RESOURCE_CRAFTABLE_EARNED);
			global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter> list = new global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter>();
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_SCORE", amount, global::Kampai.Common.ParameterName.AMOUNT));
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", itemName, global::Kampai.Common.ParameterName.ITEM_NAME));
			list.AddRange(GetLevelGrindPremium());
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", sourceName, global::Kampai.Common.ParameterName.SOURCE_NAME));
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", highLevel, global::Kampai.Common.ParameterName.TAXONOMY_HIGH_LEVEL));
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", specific, global::Kampai.Common.ParameterName.TAXONOMY_SPECIFIC));
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", type, global::Kampai.Common.ParameterName.TAXONOMY_TYPE));
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", other, global::Kampai.Common.ParameterName.TAXONOMY_OTHER));
			telemetryEvent.Parameters = list;
			LogGameEvent(telemetryEvent);
		}

		public void Send_Telemetry_EVT_IGE_RESOURCE_CRAFTABLE_SPENT(int amount, string sourceName, string itemName, string highLevel, string specific, string type, string other)
		{
			global::Kampai.Common.TelemetryEvent telemetryEvent = new global::Kampai.Common.TelemetryEvent(SynergyTrackingEventType.EVT_IGE_RESOURCE_CRAFTABLE_SPENT);
			global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter> list = new global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter>();
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_SCORE", amount, global::Kampai.Common.ParameterName.AMOUNT));
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", itemName, global::Kampai.Common.ParameterName.ITEM_NAME));
			list.AddRange(GetLevelGrindPremium());
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", sourceName, global::Kampai.Common.ParameterName.SOURCE_NAME));
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", highLevel, global::Kampai.Common.ParameterName.TAXONOMY_HIGH_LEVEL));
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", specific, global::Kampai.Common.ParameterName.TAXONOMY_SPECIFIC));
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", type, global::Kampai.Common.ParameterName.TAXONOMY_TYPE));
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", other, global::Kampai.Common.ParameterName.TAXONOMY_OTHER));
			telemetryEvent.Parameters = list;
			LogGameEvent(telemetryEvent);
		}

		public void Send_Telemetry_EVT_IGE_STORE_VISIT(string trafficSource, string storeVisited)
		{
			global::Kampai.Common.TelemetryEvent telemetryEvent = new global::Kampai.Common.TelemetryEvent(SynergyTrackingEventType.EVT_IGE_STORE_VISIT);
			global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter> list = new global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter>();
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", trafficSource, global::Kampai.Common.ParameterName.TRAFFIC_SOURCE));
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", storeVisited, global::Kampai.Common.ParameterName.STORE_VISITED));
			list.AddRange(GetLevelGrindPremium());
			telemetryEvent.Parameters = list;
			LogGameEvent(telemetryEvent);
		}

		public void Send_Telemetry_EVT_USER_TUTORIAL_FUNNEL_EAL(string tutorialName, string step)
		{
			int num = global::UnityEngine.Mathf.RoundToInt(global::UnityEngine.Time.realtimeSinceStartup - lastFunnelEalTime);
			lastFunnelEalTime = global::UnityEngine.Time.realtimeSinceStartup;
			global::Kampai.Common.TelemetryEvent telemetryEvent = new global::Kampai.Common.TelemetryEvent(SynergyTrackingEventType.EVT_USER_TUTORIAL_FUNNEL_EAL);
			global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter> list = new global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter>();
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", tutorialName, global::Kampai.Common.ParameterName.TUTORIAL_NAME));
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", step, global::Kampai.Common.ParameterName.STEP));
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_DURATION", num, global::Kampai.Common.ParameterName.DURATION));
			list.AddRange(GetLevelGrindPremium());
			telemetryEvent.Parameters = list;
			LogGameEvent(telemetryEvent);
		}

		public void Send_Telemetry_EVT_USER_GAME_LOAD_FUNNEL(string step, string swrveGroup)
		{
			int num = global::UnityEngine.Mathf.RoundToInt(global::UnityEngine.Time.realtimeSinceStartup - lastGameLoadFunnelTime);
			lastGameLoadFunnelTime = global::UnityEngine.Time.realtimeSinceStartup;
			global::Kampai.Common.TelemetryEvent telemetryEvent = new global::Kampai.Common.TelemetryEvent(SynergyTrackingEventType.EVT_USER_GAME_LOAD_FUNNEL);
			global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter> list = new global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter>();
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_DURATION", num, global::Kampai.Common.ParameterName.DURATION));
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", step, global::Kampai.Common.ParameterName.STEP));
			list.AddRange(GetLevelGrindPremium());
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", swrveGroup, global::Kampai.Common.ParameterName.SWRVE_GROUP));
			telemetryEvent.Parameters = list;
			logger.Debug(string.Format("Send_Telemetry_EVT_USER_GAME_LOAD_FUNNEL: {0} - {1}", step, swrveGroup));
			LogGameEvent(telemetryEvent);
		}

		public void Send_Telemetry_EVT_USER_GAME_DOWNLOAD_FUNNEL(string bundleName, int duration, long size)
		{
			global::Kampai.Common.TelemetryEvent telemetryEvent = new global::Kampai.Common.TelemetryEvent(SynergyTrackingEventType.EVT_USER_GAME_DOWNLOAD_FUNNEL);
			global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter> list = new global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter>();
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", bundleName, global::Kampai.Common.ParameterName.DLC_NAME));
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_DURATION", duration, global::Kampai.Common.ParameterName.DURATION));
			list.AddRange(GetLevelGrindPremium());
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_SCORE", size, global::Kampai.Common.ParameterName.DLC_SIZE));
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", global::Kampai.Util.NetworkUtil.IsNetworkWiFi().ToString(), global::Kampai.Common.ParameterName.DLC_IS_WIFI));
			telemetryEvent.Parameters = list;
			logger.Debug(string.Format("Send_Telemetry_EVT_USER_GAME_DOWNLOAD_FUNNEL: {0} in {1}", bundleName, duration));
			LogGameEvent(telemetryEvent);
		}

		public void Send_Telemetry_EVT_GP_LEVEL_PROMOTION_DAYS_TOTAL_EALL()
		{
			int totalSecondsSinceLevelUp = playerDurationService.TotalSecondsSinceLevelUp;
			int gameplaySecondsSinceLevelUp = playerDurationService.GameplaySecondsSinceLevelUp;
			logger.Info(string.Format("LEVELUP TELEMETRY: TOTAL SECONDS: {0}", totalSecondsSinceLevelUp));
			logger.Info(string.Format("LEVELUP TELEMETRY: GAMEPLAY SECONDS: {0}", gameplaySecondsSinceLevelUp));
			global::Kampai.Common.TelemetryEvent telemetryEvent = new global::Kampai.Common.TelemetryEvent(SynergyTrackingEventType.EVT_GP_LEVEL_PROMOTION_DAYS_TOTAL_EAL);
			global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter> list = new global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter>();
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_DURATION", totalSecondsSinceLevelUp, global::Kampai.Common.ParameterName.DURATION_TOTAL));
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_DURATION", gameplaySecondsSinceLevelUp, global::Kampai.Common.ParameterName.DURATION_GAMEPLAY));
			list.AddRange(GetLevelGrindPremium());
			telemetryEvent.Parameters = list;
			LogGameEvent(telemetryEvent);
		}

		public void Send_Telemetry_EVT_GP_ACHIEVEMENTS_CHECKPOINTS_EAL(string achievementName, global::Kampai.Common.Service.Telemetry.AchievementType type, string questGiver = "")
		{
			global::Kampai.Common.TelemetryEvent telemetryEvent = new global::Kampai.Common.TelemetryEvent(SynergyTrackingEventType.EVT_GP_ACHIEVEMENTS_CHECKPOINTS_EAL);
			global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter> list = new global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter>();
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", achievementName, global::Kampai.Common.ParameterName.ACHIEVEMENT_NAME));
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", type.ToString(), global::Kampai.Common.ParameterName.ARCHIVEMENT_TYPE));
			list.AddRange(GetLevelGrindPremium());
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", string.Empty, global::Kampai.Common.ParameterName.PROCEDURAL_QUEST_END_STATE));
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", questGiver, global::Kampai.Common.ParameterName.QUEST_GIVER));
			telemetryEvent.Parameters = list;
			LogGameEvent(telemetryEvent);
		}

		public void Send_Telemetry_EVT_GP_ACHIEVEMENTS_CHECKPOINTS_EAL_ProceduralQuest(string achievementName, global::Kampai.Common.Service.Telemetry.ProceduralQuestEndState endState)
		{
			global::Kampai.Common.Service.Telemetry.AchievementType achievementType = global::Kampai.Common.Service.Telemetry.AchievementType.ProceduralQuest;
			global::Kampai.Common.TelemetryEvent telemetryEvent = new global::Kampai.Common.TelemetryEvent(SynergyTrackingEventType.EVT_GP_ACHIEVEMENTS_CHECKPOINTS_EAL);
			global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter> list = new global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter>();
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", achievementName, global::Kampai.Common.ParameterName.ACHIEVEMENT_NAME));
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", achievementType.ToString(), global::Kampai.Common.ParameterName.ARCHIVEMENT_TYPE));
			list.AddRange(GetLevelGrindPremium());
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", endState.ToString(), global::Kampai.Common.ParameterName.PROCEDURAL_QUEST_END_STATE));
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", string.Empty, global::Kampai.Common.ParameterName.QUEST_GIVER));
			telemetryEvent.Parameters = list;
			LogGameEvent(telemetryEvent);
		}

		public void Send_Telemetry_EVT_GP_ACHIEVEMENTS_STARTED_EAL(string achievementName, global::Kampai.Common.Service.Telemetry.AchievementType type, string questGiver = "")
		{
			global::Kampai.Common.TelemetryEvent telemetryEvent = new global::Kampai.Common.TelemetryEvent(SynergyTrackingEventType.EVT_GP_ACHIEVEMENTS_STARTED_EAL);
			global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter> list = new global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter>();
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", achievementName, global::Kampai.Common.ParameterName.ACHIEVEMENT_NAME));
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", type.ToString(), global::Kampai.Common.ParameterName.ARCHIVEMENT_TYPE));
			list.AddRange(GetLevelGrindPremium());
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", string.Empty, global::Kampai.Common.ParameterName.PROCEDURAL_QUEST_END_STATE));
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", questGiver, global::Kampai.Common.ParameterName.QUEST_GIVER));
			telemetryEvent.Parameters = list;
			LogGameEvent(telemetryEvent);
		}

		public void Send_Telemetry_EVT_EBISU_LOGIN_GAMECENTER(string loginLocation)
		{
			global::Kampai.Common.TelemetryEvent telemetryEvent = new global::Kampai.Common.TelemetryEvent(SynergyTrackingEventType.EVT_EBISU_LOGIN_GAMECENTER);
			global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter> list = new global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter>();
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", loginLocation, global::Kampai.Common.ParameterName.LOGIN_LOCATION));
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_NONE", string.Empty, global::Kampai.Common.ParameterName.NONE));
			list.AddRange(GetLevelGrindPremium());
			telemetryEvent.Parameters = list;
			LogGameEvent(telemetryEvent);
		}

		public void Send_Telemetry_EVT_EBISU_LOGIN_GOOGLEPLAY(string loginLocation)
		{
			global::Kampai.Common.TelemetryEvent telemetryEvent = new global::Kampai.Common.TelemetryEvent(SynergyTrackingEventType.EVT_EBISU_LOGIN_GOOGLEPLAY);
			global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter> list = new global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter>();
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", loginLocation, global::Kampai.Common.ParameterName.LOGIN_LOCATION));
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_NONE", string.Empty, global::Kampai.Common.ParameterName.NONE));
			list.AddRange(GetLevelGrindPremium());
			telemetryEvent.Parameters = list;
			LogGameEvent(telemetryEvent);
		}

		public void Send_Telemetry_EVT_EBISU_LOGIN_FACEBOOK(string loginLocation, string loginSource)
		{
			global::Kampai.Common.TelemetryEvent telemetryEvent = new global::Kampai.Common.TelemetryEvent(SynergyTrackingEventType.EVT_EBISU_LOGIN_FACEBOOK);
			global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter> list = new global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter>();
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", loginLocation, global::Kampai.Common.ParameterName.LOGIN_LOCATION));
			if (loginSource == null)
			{
				list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_NONE", string.Empty, global::Kampai.Common.ParameterName.NONE));
			}
			else
			{
				list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", loginSource, global::Kampai.Common.ParameterName.LOGIN_SOURCE));
			}
			list.AddRange(GetLevelGrindPremium());
			telemetryEvent.Parameters = list;
			LogGameEvent(telemetryEvent);
		}

		public void Send_Telemetry_EVT_AGE_GATE_SET(int year, int month)
		{
			global::Kampai.Common.TelemetryEvent telemetryEvent = new global::Kampai.Common.TelemetryEvent(SynergyTrackingEventType.EVT_COPPA_AGE_GATE);
			global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter> list = new global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter>();
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", year, global::Kampai.Common.ParameterName.YEAR));
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", month, global::Kampai.Common.ParameterName.MONTH));
			telemetryEvent.Parameters = list;
			LogGameEvent(telemetryEvent);
		}

		public void Send_TelemetryCharacterPrestiged(global::Kampai.Game.Prestige prestige)
		{
			global::Kampai.Common.TelemetryEvent telemetryEvent = new global::Kampai.Common.TelemetryEvent(SynergyTrackingEventType.EVT_CHARACTER_PRESTIGED);
			global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter> list = new global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter>();
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", prestige.Definition.LocalizedKey, global::Kampai.Common.ParameterName.CHARACTER_NAME));
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", prestige.CurrentOrdersCompleted, global::Kampai.Common.ParameterName.ORDERS_COMPLETED));
			list.AddRange(GetLevelGrindPremium());
			telemetryEvent.Parameters = list;
			LogGameEvent(telemetryEvent);
		}

		public void Send_TelemetryOrderBoard(bool isFillingOrder, global::Kampai.Game.Transaction.TransactionDefinition transactionDef, int characterDefinitionId, string difficulty)
		{
			SynergyTrackingEventType type = ((!isFillingOrder) ? SynergyTrackingEventType.EVT_ORDER_CANCEL_SUM : SynergyTrackingEventType.EVT_ORDER_COMPLETED_SUM);
			global::Kampai.Common.TelemetryEvent telemetryEvent = new global::Kampai.Common.TelemetryEvent(type);
			global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter> list = new global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter>();
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", difficulty, global::Kampai.Common.ParameterName.DIFFICULTY));
			string value = "none";
			if (characterDefinitionId != 0 && definitionService != null)
			{
				global::Kampai.Game.PrestigeDefinition definition = null;
				if (definitionService.TryGet<global::Kampai.Game.PrestigeDefinition>(characterDefinitionId, out definition))
				{
					value = string.Format("Prestiged with {0}", (definition == null) ? "uknown" : definition.LocalizedKey);
				}
			}
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", value, global::Kampai.Common.ParameterName.PRESTIGED_WITH));
			list.AddRange(GetLevelGrindPremium());
			uint num = 0u;
			uint num2 = 0u;
			foreach (global::Kampai.Util.QuantityItem output in transactionDef.Outputs)
			{
				if (output.ID == 0)
				{
					num = output.Quantity;
				}
				else if (output.ID == 2)
				{
					num2 = output.Quantity;
				}
			}
			int count = transactionDef.Inputs.Count;
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", num, global::Kampai.Common.ParameterName.GRIND_CURRENCY_EARNED));
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", num2, global::Kampai.Common.ParameterName.XP_EARNED));
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", count, global::Kampai.Common.ParameterName.INPUTS_COUNT));
			telemetryEvent.Parameters = list;
			LogGameEvent(telemetryEvent);
			type = ((!isFillingOrder) ? SynergyTrackingEventType.EVT_ORDER_CANCEL_DET : SynergyTrackingEventType.EVT_ORDER_COMPLETED_DET);
			telemetryEvent = new global::Kampai.Common.TelemetryEvent(type);
			list = new global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter>();
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", difficulty, global::Kampai.Common.ParameterName.DIFFICULTY));
			if (count > 0)
			{
				AddIngredient(transactionDef.Inputs[0], list);
			}
			else
			{
				list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", "none:0", global::Kampai.Common.ParameterName.INGREDIENT));
			}
			list.AddRange(GetLevelGrindPremium());
			for (int i = 1; i < count; i++)
			{
				AddIngredient(transactionDef.Inputs[i], list);
			}
			telemetryEvent.Parameters = list;
			LogGameEvent(telemetryEvent);
		}

		private void AddIngredient(global::Kampai.Util.QuantityItem item, global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter> parameters)
		{
			global::Kampai.Game.ItemDefinition definition;
			if (definitionService != null && definitionService.TryGet<global::Kampai.Game.ItemDefinition>(item.ID, out definition))
			{
				string value = string.Format("{0}:{1}", (definition != null) ? definition.LocalizedKey : "unknown", item.Quantity);
				parameters.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", value, global::Kampai.Common.ParameterName.INGREDIENT));
			}
		}

		public void Send_Telemetry_EVT_IN_APP_MESSAGE_DISPLAYED(string inAppMessageName, string choice)
		{
			global::Kampai.Common.TelemetryEvent telemetryEvent = new global::Kampai.Common.TelemetryEvent(SynergyTrackingEventType.EVT_IPAD_UPSELL_MESSAGE_DISPLAYED);
			global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter> list = new global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter>();
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", inAppMessageName, global::Kampai.Common.ParameterName.IN_APP_MESSAGE_NAME));
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", choice, global::Kampai.Common.ParameterName.USER_CHOICE));
			list.AddRange(GetLevelGrindPremium());
			telemetryEvent.Parameters = list;
			LogGameEvent(telemetryEvent);
		}

		public void Send_Telemetry_EVT_USER_TRACKING_OPTOUT()
		{
			global::Kampai.Common.TelemetryEvent telemetryEvent = new global::Kampai.Common.TelemetryEvent(SynergyTrackingEventType.EVT_USER_TRACKING_OPTOUT);
			global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter> list = new global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter>();
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_NONE", string.Empty, global::Kampai.Common.ParameterName.NONE));
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_NONE", string.Empty, global::Kampai.Common.ParameterName.NONE));
			list.AddRange(GetLevelGrindPremium());
			telemetryEvent.Parameters = list;
			LogGameEvent(telemetryEvent);
		}

		public void Send_Telemetry_EVT_MINI_GAME_PLAYED(string mignetteName, int score, float timePlayed)
		{
			global::Kampai.Common.TelemetryEvent telemetryEvent = new global::Kampai.Common.TelemetryEvent(SynergyTrackingEventType.EVT_MINI_GAME_PLAYED);
			global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter> list = new global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter>();
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", mignetteName, global::Kampai.Common.ParameterName.MIGNETTE_NAME));
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", score, global::Kampai.Common.ParameterName.SCORE));
			list.AddRange(GetLevelGrindPremium());
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", timePlayed, global::Kampai.Common.ParameterName.TIME_PLAYED));
			telemetryEvent.Parameters = list;
			LogGameEvent(telemetryEvent);
		}

		public void Send_Telemetry_EVT_USER_DATA_AT_APP_START(int seconds, string language, int minions, string swrveGroup, string expansions)
		{
			logger.Debug(string.Format("Send_Telemetry_EVT_USER_DATA_AT_APP_START: {0} {1} {2} {3} {4}", seconds, language, minions, swrveGroup, expansions));
			global::Kampai.Common.TelemetryEvent telemetryEvent = new global::Kampai.Common.TelemetryEvent(SynergyTrackingEventType.EVT_USER_DATA_AT_APP_START);
			global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter> list = new global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter>();
			long iD = playerService.ID;
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", seconds, global::Kampai.Common.ParameterName.TIME_SINCE_LAST_PLAYED));
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", language, global::Kampai.Common.ParameterName.LANGUAGE));
			list.AddRange(GetLevelGrindPremium());
			string dataPlayer = localPersistService.GetDataPlayer("IsSpender");
			if (dataPlayer.Equals("true"))
			{
				list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", "spender", global::Kampai.Common.ParameterName.SPENDER));
			}
			else
			{
				list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", "non-spender", global::Kampai.Common.ParameterName.SPENDER));
			}
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", minions, global::Kampai.Common.ParameterName.NUM_MINIONS));
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", swrveGroup, global::Kampai.Common.ParameterName.SWRVE_GROUP));
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", expansions, global::Kampai.Common.ParameterName.LAND_EXPANSIONS));
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", iD, global::Kampai.Common.ParameterName.PLAYER_ID));
			telemetryEvent.Parameters = list;
			LogGameEvent(telemetryEvent);
		}

		public void Send_Telemetry_EVT_USER_DATA_AT_APP_CLOSE()
		{
			global::Kampai.Common.TelemetryEvent telemetryEvent = new global::Kampai.Common.TelemetryEvent(SynergyTrackingEventType.EVT_USER_DATA_AT_APP_CLOSE);
			global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter> list = new global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter>();
			long iD = playerService.ID;
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_NONE", string.Empty, global::Kampai.Common.ParameterName.NONE));
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_NONE", string.Empty, global::Kampai.Common.ParameterName.NONE));
			list.AddRange(GetLevelGrindPremium());
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", iD, global::Kampai.Common.ParameterName.PLAYER_ID));
			telemetryEvent.Parameters = list;
			LogGameEvent(telemetryEvent);
		}

		public void Send_Telemetry_EVT_STORAGE_LIMIT_HIT(int storageLimit)
		{
			global::Kampai.Common.TelemetryEvent telemetryEvent = new global::Kampai.Common.TelemetryEvent(SynergyTrackingEventType.EVT_STORAGE_LIMIT_HIT);
			global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter> list = new global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter>();
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", storageLimit, global::Kampai.Common.ParameterName.STORAGE_LIMIT));
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_NONE", string.Empty, global::Kampai.Common.ParameterName.NONE));
			list.AddRange(GetLevelGrindPremium());
			telemetryEvent.Parameters = list;
			LogGameEvent(telemetryEvent);
		}

		public void Send_Telemetry_EVT_SOCIAL_EVENT_COMPLETION(int teamSize)
		{
			global::Kampai.Common.TelemetryEvent telemetryEvent = new global::Kampai.Common.TelemetryEvent(SynergyTrackingEventType.EVT_SOCIAL_EVENT_COMPLETION);
			global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter> list = new global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter>();
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_NONE", string.Empty, global::Kampai.Common.ParameterName.NONE));
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_NONE", string.Empty, global::Kampai.Common.ParameterName.NONE));
			list.AddRange(GetLevelGrindPremium());
			if (teamSize == 1)
			{
				list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", "solo", global::Kampai.Common.ParameterName.SOLO_TEAM));
			}
			else
			{
				list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", "team", global::Kampai.Common.ParameterName.SOLO_TEAM));
			}
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", teamSize, global::Kampai.Common.ParameterName.TEAM_SIZE));
			telemetryEvent.Parameters = list;
			LogGameEvent(telemetryEvent);
		}

		public void Send_Telemetry_EVT_SOCIAL_EVENT_CONTRIBUTION(string item, int quantity, int teamSize)
		{
			global::Kampai.Common.TelemetryEvent telemetryEvent = new global::Kampai.Common.TelemetryEvent(SynergyTrackingEventType.EVT_SOCIAL_EVENT_CONTRIBUTION);
			global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter> list = new global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter>();
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", item, global::Kampai.Common.ParameterName.ITEM_NAME));
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", quantity, global::Kampai.Common.ParameterName.AMOUNT));
			list.AddRange(GetLevelGrindPremium());
			if (teamSize == 1)
			{
				list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", "solo", global::Kampai.Common.ParameterName.SOLO_TEAM));
			}
			else
			{
				list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", "team", global::Kampai.Common.ParameterName.SOLO_TEAM));
			}
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", teamSize, global::Kampai.Common.ParameterName.TEAM_SIZE));
			telemetryEvent.Parameters = list;
			LogGameEvent(telemetryEvent);
		}

		public void Send_Telemtry_EVT_MINI_TIER_REACHED(string mignetteName, int tier, int plays)
		{
			global::Kampai.Common.TelemetryEvent telemetryEvent = new global::Kampai.Common.TelemetryEvent(SynergyTrackingEventType.EVT_MINI_TIER_REACHED);
			global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter> list = new global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter>();
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", mignetteName, global::Kampai.Common.ParameterName.MIGNETTE_NAME));
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", tier, global::Kampai.Common.ParameterName.REWARD_TIER));
			list.AddRange(GetLevelGrindPremium());
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", plays, global::Kampai.Common.ParameterName.TIMES_PLAYED));
			telemetryEvent.Parameters = list;
			LogGameEvent(telemetryEvent);
		}

		public void Send_Telemtry_EVT_MARKETPLACE_ITEM_LISTED(string itemName, int quantity, string highLevel, string specific, string type, string other)
		{
			global::Kampai.Common.TelemetryEvent telemetryEvent = new global::Kampai.Common.TelemetryEvent(SynergyTrackingEventType.EVT_MARKETPLACE_ITEM_LISTED);
			global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter> list = new global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter>();
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", itemName, global::Kampai.Common.ParameterName.ITEM_NAME));
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", quantity, global::Kampai.Common.ParameterName.AMOUNT));
			list.AddRange(GetLevelGrindPremium());
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", highLevel, global::Kampai.Common.ParameterName.TAXONOMY_HIGH_LEVEL));
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", specific, global::Kampai.Common.ParameterName.TAXONOMY_SPECIFIC));
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", type, global::Kampai.Common.ParameterName.TAXONOMY_TYPE));
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", other, global::Kampai.Common.ParameterName.TAXONOMY_OTHER));
			telemetryEvent.Parameters = list;
			LogGameEvent(telemetryEvent);
		}

		public void Send_Telemtry_EVT_MARKETPLACE_VIEWED(string viewType)
		{
			global::Kampai.Common.TelemetryEvent telemetryEvent = new global::Kampai.Common.TelemetryEvent(SynergyTrackingEventType.EVT_MARKETPLACE_VIEWED);
			global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter> list = new global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter>();
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", viewType, global::Kampai.Common.ParameterName.VIEW_TYPE));
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_NONE", string.Empty, global::Kampai.Common.ParameterName.NONE));
			list.AddRange(GetLevelGrindPremium());
			telemetryEvent.Parameters = list;
			LogGameEvent(telemetryEvent);
		}

		public void SetPlayerServiceReference(global::Kampai.Game.IPlayerService playerService)
		{
			this.playerService = playerService;
		}

		public void SetPlayerDurationServiceReference(global::Kampai.Game.IPlayerDurationService playerDurationService)
		{
			this.playerDurationService = playerDurationService;
		}

		public void SetDefinitionServiceReference(global::Kampai.Game.IDefinitionService definitionService)
		{
			this.definitionService = definitionService;
		}

		public void Send_Telemetry_EVT_RATE_MY_APP(string promptType, bool? userAccepted)
		{
			global::Kampai.Common.TelemetryEvent telemetryEvent = new global::Kampai.Common.TelemetryEvent(SynergyTrackingEventType.EVT_RATE_MY_APP);
			global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter> list = new global::System.Collections.Generic.List<global::Kampai.Common.TelemetryParameter>();
			string value = "Cancel";
			if (userAccepted.HasValue)
			{
				value = ((!userAccepted.Value) ? "No" : "Yes");
			}
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", promptType, global::Kampai.Common.ParameterName.PROMPT_TYPE));
			list.Add(new global::Kampai.Common.TelemetryParameter("EVT_KEYTYPE_ENUMERATION", value, global::Kampai.Common.ParameterName.USER_CHOICE));
			list.AddRange(GetLevelGrindPremium());
			telemetryEvent.Parameters = list;
			LogGameEvent(telemetryEvent);
		}

		public void SendInAppPurchaseEventOnPurchaseComplete(global::Kampai.Common.IapTelemetryEvent iapTelemetryEvent)
		{
			foreach (global::Kampai.Common.IIapTelemetryService iapTelemetryService in iapTelemetryServices)
			{
				iapTelemetryService.SendInAppPurchaseEventOnPurchaseComplete(iapTelemetryEvent);
			}
		}

		public void SendInAppPurchaseEventOnProductDelivery(string sku, global::Kampai.Game.Transaction.TransactionDefinition reward)
		{
			foreach (global::Kampai.Common.IIapTelemetryService iapTelemetryService in iapTelemetryServices)
			{
				iapTelemetryService.SendInAppPurchaseEventOnProductDelivery(sku, reward);
			}
		}

		private string PremiumPurchaseArgument(bool isPremium)
		{
			return (!isPremium) ? "FREE" : "TRUE";
		}
	}
}
