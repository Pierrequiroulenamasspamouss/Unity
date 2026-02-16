namespace Kampai.Common
{
	public interface ITelemetryService : global::Kampai.Common.IIapTelemetryService
	{
		void AddTelemetrySender(global::Kampai.Common.ITelemetrySender sender);

		void AddIapTelemetryService(global::Kampai.Common.IIapTelemetryService service);

		void SharingUsage(global::Kampai.Common.ITelemetrySender sender, bool enabled);

		void COPPACompliance();

		void SharingUsageCompliance();

		void SharingUsage(bool enabled);

		bool SharingUsageEnabled();

		void LogGameEvent(global::Kampai.Common.TelemetryEvent gameEvent);

		void Send_Telemetry_EVT_GAME_ERROR_GAMEPLAY(string nameOfError, string errorDetails);

		void Send_Telemetry_EVT_GAME_ERROR_CONNECTIVITY(string nameOfError, string errorDetails);

		void Send_Telemetry_EVT_IGE_FREE_CREDITS_EARNED(int grindEarned, string eventName, bool purchasedCurrencySpent);

		void Send_Telemetry_EVT_IGE_PAID_CREDITS_EARNED(int premiumEarned, string eventName, bool purchasedCurrencySpent);

		void Send_Telemetry_EVT_IGE_FREE_CREDITS_PURCHASE_REVENUE(int grindSpent, string itemPurchased, bool purchasedCurrencySpent, string highLevel, string specific, string type, string other);

		void Send_Telemetry_EVT_IGE_PAID_CREDITS_PURCHASE_REVENUE(int premiumSpent, string itemPurchased, bool purchasedCurrencySpent, string highLevel, string specific, string type, string other);

		void Send_Telemetry_EVT_IGE_RESOURCE_CRAFTABLE_EARNED(int amount, string sourceName, string itemName, string highLevel, string specific, string type, string other);

		void Send_Telemetry_EVT_IGE_RESOURCE_CRAFTABLE_SPENT(int amount, string sourceName, string itemName, string highLevel, string specific, string type, string other);

		void Send_Telemetry_EVT_IGE_STORE_VISIT(string trafficSource, string storeVisited);

		void Send_Telemetry_EVT_USER_TUTORIAL_FUNNEL_EAL(string tutorialName, string step);

		void Send_Telemetry_EVT_USER_GAME_LOAD_FUNNEL(string step, string swrveGroup);

		void Send_Telemetry_EVT_USER_GAME_DOWNLOAD_FUNNEL(string bundleName, int duration, long size);

		void Send_Telemetry_EVT_GP_LEVEL_PROMOTION_DAYS_TOTAL_EALL();

		void Send_Telemetry_EVT_GP_ACHIEVEMENTS_CHECKPOINTS_EAL(string achievementName, global::Kampai.Common.Service.Telemetry.AchievementType type, string questGiver = "");

		void Send_Telemetry_EVT_GP_ACHIEVEMENTS_CHECKPOINTS_EAL_ProceduralQuest(string achievementName, global::Kampai.Common.Service.Telemetry.ProceduralQuestEndState endState);

		void Send_Telemetry_EVT_GP_ACHIEVEMENTS_STARTED_EAL(string achievementName, global::Kampai.Common.Service.Telemetry.AchievementType type, string questGiver = "");

		void Send_Telemetry_EVT_EBISU_LOGIN_GAMECENTER(string loginLocation);

		void Send_Telemetry_EVT_EBISU_LOGIN_GOOGLEPLAY(string loginLocation);

		void Send_Telemetry_EVT_EBISU_LOGIN_FACEBOOK(string loginLocation, string loginSource);

		void Send_Telemetry_EVT_AGE_GATE_SET(int year, int month);

		void Send_TelemetryCharacterPrestiged(global::Kampai.Game.Prestige prestige);

		void Send_TelemetryOrderBoard(bool isFillingOrder, global::Kampai.Game.Transaction.TransactionDefinition transactionDef, int characterDefinitionId, string difficulty);

		void Send_Telemetry_EVT_IN_APP_MESSAGE_DISPLAYED(string inAppMessageName, string choice);

		void Send_Telemetry_EVT_USER_TRACKING_OPTOUT();

		void Send_Telemetry_EVT_MINI_GAME_PLAYED(string mignetteName, int score, float timePlayed);

		void Send_Telemetry_EVT_USER_DATA_AT_APP_START(int seconds, string language, int minions, string swrveGroup, string expansions);

		void Send_Telemetry_EVT_USER_DATA_AT_APP_CLOSE();

		void Send_Telemetry_EVT_STORAGE_LIMIT_HIT(int storageLimit);

		void Send_Telemetry_EVT_RATE_MY_APP(string promptType, bool? userAccepted);

		void Send_Telemetry_EVT_SOCIAL_EVENT_COMPLETION(int teamSize);

		void Send_Telemetry_EVT_SOCIAL_EVENT_CONTRIBUTION(string item, int quantity, int teamSize);

		void Send_Telemtry_EVT_MINI_TIER_REACHED(string mignetteName, int tier, int plays);

		void Send_Telemtry_EVT_MARKETPLACE_ITEM_LISTED(string itemName, int quantity, string highLevel, string specific, string type, string other);

		void Send_Telemtry_EVT_MARKETPLACE_VIEWED(string viewType);
	}
}
