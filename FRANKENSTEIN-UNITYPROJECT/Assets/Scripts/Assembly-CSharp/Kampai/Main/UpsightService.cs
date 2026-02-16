namespace Kampai.Main
{
	public class UpsightService : global::Kampai.Common.IIapTelemetryService, global::Kampai.Common.ITelemetrySender, global::Kampai.Main.IUpsightService
	{
		private static class CUSTOM_DIMENSIONS
		{
			public const string USER_LEVEL = "user_level";

			public const string GRIND_CURRENCY = "grind_currency";

			public const string PREMIUM_CURRENCY = "premium_currency";

			public const string LOGGED_IN_FACEBOOK = "logged_in_facebook";

			public const string LOGGED_IN_GAMECENTER = "logged_in_gamecenter";

			public const string LOGGED_IN_GOOGLEPLAY = "logged_in_googleplay";
		}

		private const string TAG = "Ups: ";

		private string appToken = global::Kampai.Util.GameConstants.StaticConfig.UPSIGHT_TOKEN;

		private string appSecret = global::Kampai.Util.GameConstants.StaticConfig.UPSIGHT_SECRET;

		private global::System.Collections.Generic.Dictionary<string, bool> placementIdToContentReady;

		private bool initialized;

		[Inject]
		public global::Kampai.Common.ITelemetryService telemetryService { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal playSFXSignal { get; set; }

		[Inject]
		public global::Kampai.Common.ICoppaService coppaService { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject(global::Kampai.Game.SocialServices.FACEBOOK)]
		public global::Kampai.Game.ISocialService facebookService { get; set; }

		[Inject(global::Kampai.Game.SocialServices.GAMECENTER)]
		public global::Kampai.Game.ISocialService gcService { get; set; }

		[Inject(global::Kampai.Game.SocialServices.GOOGLEPLAY)]
		public global::Kampai.Game.ISocialService gpService { get; set; }

		[Inject]
		public global::Kampai.Main.UpsightContentPreloadedSignal upsightContentPreloadedSignal { get; set; }

		[Inject]
		public global::Kampai.Game.IConfigurationsService configurationsService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		public void Init(bool optOutStatus)
		{
			logger.Debug("{0}Init(): optOutStatus: {1}", "Ups: ", optOutStatus);
			placementIdToContentReady = new global::System.Collections.Generic.Dictionary<string, bool>();
			logger.Info("{0}InitUpsight(): Upsight app ID type: {1}", "Ups: ", global::Kampai.Util.GameConstants.StaticConfig.ENVIRONMENT);
			Upsight.init(appToken, appSecret);
			Upsight.setOptOutStatus(optOutStatus);
			Upsight.requestAppOpen();
			SubscribeOnUpsightEvents();
			initialized = true;
		}

		public void OnGameResumeCallback()
		{
			if (initialized)
			{
				Upsight.requestAppOpen();
			}
		}

		public void OnGameReloadCallback()
		{
			if (initialized)
			{
				UnsubscribeFromUpsightEvents();
			}
		}

		public void SendContentRequest(global::Kampai.Game.UpsightPromoTrigger.Placement placement)
		{
			if (initialized)
			{
				logger.Debug("{0}sendContentRequest: placementId = {1}", "Ups: ", global::Kampai.Game.UpsightPlacementUtils.GetPlacementId(placement));
				global::System.Collections.Generic.Dictionary<string, object> dimensionsData = GetDimensionsData();
				global::System.Collections.Generic.Dictionary<string, object> dimensions = dimensionsData;
				Upsight.sendContentRequest(global::Kampai.Game.UpsightPlacementUtils.GetPlacementId(placement), true, true, dimensions);
			}
		}

		public void PreloadContentRequest(global::Kampai.Game.UpsightPromoTrigger.Placement placement)
		{
			if (initialized)
			{
				logger.Debug("{0}preloadContentRequest: placementId = {1}", "Ups: ", global::Kampai.Game.UpsightPlacementUtils.GetPlacementId(placement));
				global::System.Collections.Generic.Dictionary<string, object> dimensionsData = GetDimensionsData();
				Upsight.preloadContentRequest(global::Kampai.Game.UpsightPlacementUtils.GetPlacementId(placement), dimensionsData);
			}
		}

		public bool CanLoadContent(global::Kampai.Game.UpsightPromoTrigger.Placement placement)
		{
			global::Kampai.Game.ConfigurationDefinition configurations = configurationsService.GetConfigurations();
			if (configurations.killSwitches != null && configurations.killSwitches.Contains(global::Kampai.Game.KillSwitch.UPSIGHT))
			{
				logger.Info("UpsightService, disabled by kill switch.");
				return false;
			}
			if (configurations.upsightPromoTriggers == null)
			{
				logger.Info("UpsightService, no upsightPromoTriggers in configuration.");
				return false;
			}
			global::Kampai.Game.UpsightPromoTrigger upsightPromoTrigger = global::System.Linq.Enumerable.FirstOrDefault(configurations.upsightPromoTriggers, (global::Kampai.Game.UpsightPromoTrigger t) => t.placement == placement);
			if (upsightPromoTrigger == null)
			{
				logger.Info("UpsightService, placement {0} is not found in configuration.", placement);
				return false;
			}
			if (!upsightPromoTrigger.enabled)
			{
				logger.Info("UpsightService, placement {0} is disabled in configuration.", placement);
				return false;
			}
			if (coppaService.Restricted())
			{
				logger.Info("UpsightService: Coppa restricted: skip {0} placement", placement);
				return false;
			}
			string placementId = global::Kampai.Game.UpsightPlacementUtils.GetPlacementId(placement);
			if (string.IsNullOrEmpty(placementId))
			{
				logger.Error("UpsightService, unknown trigger placement: {0}", placement);
				return false;
			}
			return true;
		}

		public bool HasPreloadedContent(global::Kampai.Game.UpsightPromoTrigger.Placement placement)
		{
			string placementId = global::Kampai.Game.UpsightPlacementUtils.GetPlacementId(placement);
			if (!placementIdToContentReady.ContainsKey(placementId))
			{
				return false;
			}
			return placementIdToContentReady[placementId];
		}

		public void SendInAppPurchaseEventOnPurchaseComplete(global::Kampai.Common.IapTelemetryEvent iapTelemetryEvent)
		{
			logger.Debug("{0}SendInAppPurchaseEventOnPurchaseComplete()...: iapTelemetryEvent = {1}", "Ups: ", iapTelemetryEvent);
			if (iapTelemetryEvent == null)
			{
				logger.Log(global::Kampai.Util.Logger.Level.Error, "{0}Skip sending IAP telemetry because pendingIapTelemetryEvent is null", "Ups: ");
				return;
			}
			UpsightAndroidPurchaseResolution resolution = GetResolution(iapTelemetryEvent.nimbleMtxErrorCode);
			string store = "Google";
			logger.Debug("{0}trackInAppPurchase: productId = {1}, resolutionType = {2}", "Ups: ", iapTelemetryEvent.productId, resolution);
			Upsight.trackInAppPurchase(iapTelemetryEvent.productId, 1, resolution, iapTelemetryEvent.productPrice, iapTelemetryEvent.googleOrderId, store);
			logger.Debug("{0}...SendInAppPurchaseEventOnPurchaseComplete()", "Ups: ");
		}

		public void SendInAppPurchaseEventOnProductDelivery(string sku, global::Kampai.Game.Transaction.TransactionDefinition reward)
		{
		}

		public void COPPACompliance()
		{
			if (coppaService.Restricted())
			{
				SharingUsage(false);
			}
		}

		public void SharingUsage(bool enabled)
		{
			bool flag = !enabled;
			if (Upsight.getOptOutStatus() != flag)
			{
				logger.Debug("{0}SharingUsage(): set optout status to {1}", "Ups: ", flag);
				Upsight.setOptOutStatus(flag);
			}
		}

		public void SendEvent(global::Kampai.Common.TelemetryEvent gameEvent)
		{
		}

		private void DebugLogTelemetryEvent(global::Kampai.Common.TelemetryEvent gameEvent)
		{
			global::Kampai.Util.Logger.Level level = global::Kampai.Util.Logger.Level.Debug;
			logger.Log(level, "{0}TelemetryEvent()... event {1}: params count: {2}", "Ups: ", gameEvent.Type.ToString(), gameEvent.Parameters.Count);
			foreach (global::Kampai.Common.TelemetryParameter parameter in gameEvent.Parameters)
			{
				logger.Log(level, "{0} p.keyType {1}, p.value {2}, p.name {3}", "Ups: ", parameter.keyType, parameter.value, parameter.name);
			}
			logger.Log(level, "{0}...TelemetryEvent()", "Ups: ");
		}

		private void DebugUpsightCustomEventProperties(global::System.Collections.Generic.Dictionary<string, object> customEvent)
		{
			global::Kampai.Util.Logger.Level level = global::Kampai.Util.Logger.Level.Debug;
			logger.Log(level, "{0}DebugUpsightCustomEventProperties(): event json: {1}", "Ups: ", global::MiniJSON.Json.Serialize(customEvent));
		}

		private global::System.Collections.Generic.Dictionary<string, object> GetUpsightEventProperties(global::Kampai.Common.TelemetryEvent gameEvent)
		{
			global::System.Collections.Generic.Dictionary<string, object> upsightPayload = GetUpsightPayload(gameEvent);
			string key = gameEvent.Type.ToString().ToLower();
			global::System.Collections.Generic.Dictionary<string, object> dictionary = new global::System.Collections.Generic.Dictionary<string, object>();
			dictionary.Add(key, upsightPayload);
			return dictionary;
		}

		private global::System.Collections.Generic.Dictionary<string, object> GetUpsightPayload(global::Kampai.Common.TelemetryEvent gameEvent)
		{
			global::System.Collections.Generic.Dictionary<string, object> dictionary = new global::System.Collections.Generic.Dictionary<string, object>();
			for (int i = 0; i < gameEvent.Parameters.Count; i++)
			{
				global::Kampai.Common.TelemetryParameter telemetryParameter = gameEvent.Parameters[i];
				string upsightParameterName = GetUpsightParameterName(telemetryParameter.name);
				if (!dictionary.ContainsKey(upsightParameterName))
				{
					dictionary.Add(upsightParameterName, telemetryParameter.value);
				}
			}
			return dictionary;
		}

		private string GetUpsightParameterName(global::Kampai.Common.ParameterName name)
		{
			return name.ToString().ToLower();
		}

		private global::System.Collections.Generic.Dictionary<string, object> GetDimensionsData()
		{
			global::System.Collections.Generic.Dictionary<string, object> dictionary = null;
			if (playerService.IsPlayerInitialized())
			{
				dictionary = new global::System.Collections.Generic.Dictionary<string, object>();
				dictionary.Add("user_level", playerService.GetQuantity(global::Kampai.Game.StaticItem.LEVEL_ID));
				dictionary.Add("grind_currency", playerService.GetQuantity(global::Kampai.Game.StaticItem.GRIND_CURRENCY_ID));
				dictionary.Add("premium_currency", playerService.GetQuantity(global::Kampai.Game.StaticItem.PREMIUM_CURRENCY_ID));
				dictionary.Add("logged_in_facebook", facebookService.isLoggedIn);
				dictionary.Add("logged_in_googleplay", gpService.isLoggedIn);
			}
			return dictionary;
		}

		private static UpsightAndroidPurchaseResolution GetResolution(uint nimbleMtxErrorCode)
		{
			switch (nimbleMtxErrorCode)
			{
			case 0u:
				return UpsightAndroidPurchaseResolution.Bought;
			case 20003u:
				return UpsightAndroidPurchaseResolution.Cancelled;
			case 20001u:
				return UpsightAndroidPurchaseResolution.Owned;
			case 20016u:
				return UpsightAndroidPurchaseResolution.Invalid;
			default:
				return UpsightAndroidPurchaseResolution.Error;
			}
		}

		private void SubscribeOnUpsightEvents()
		{
			UpsightManager.openRequestSucceededEvent += OpenRequestSucceededEvent;
			UpsightManager.openRequestFailedEvent += OpenRequestFailedEvent;
			UpsightManager.contentWillDisplayEvent += ContentWillDisplayEvent;
			UpsightManager.contentDidDisplayEvent += ContentDidDisplayEvent;
			UpsightManager.contentRequestLoadedEvent += ContentRequestLoadedEvent;
			UpsightManager.contentRequestFailedEvent += ContentRequestFailedEvent;
			UpsightManager.contentPreloadSucceededEvent += ContentPreloadSucceededEvent;
			UpsightManager.contentPreloadFailedEvent += ContentPreloadFailedEvent;
			UpsightManager.contentDismissedEvent += ContentDismissedEvent;
			UpsightManager.reportCustomEventSucceededEvent += ReportCustomEventSucceededEvent;
			UpsightManager.reportCustomEventFailedEvent += ReportCustomEventFailedEvent;
			UpsightManager.trackInAppPurchaseSucceededEvent += TrackInAppPurchaseSucceededEvent;
			UpsightManager.trackInAppPurchaseFailedEvent += TrackInAppPurchaseFailedEvent;
		}

		private void UnsubscribeFromUpsightEvents()
		{
			UpsightManager.openRequestSucceededEvent -= OpenRequestSucceededEvent;
			UpsightManager.openRequestFailedEvent -= OpenRequestFailedEvent;
			UpsightManager.contentWillDisplayEvent -= ContentWillDisplayEvent;
			UpsightManager.contentDidDisplayEvent -= ContentDidDisplayEvent;
			UpsightManager.contentRequestLoadedEvent -= ContentRequestLoadedEvent;
			UpsightManager.contentRequestFailedEvent -= ContentRequestFailedEvent;
			UpsightManager.contentPreloadSucceededEvent -= ContentPreloadSucceededEvent;
			UpsightManager.contentPreloadFailedEvent -= ContentPreloadFailedEvent;
			UpsightManager.contentDismissedEvent -= ContentDismissedEvent;
			UpsightManager.reportCustomEventSucceededEvent -= ReportCustomEventSucceededEvent;
			UpsightManager.reportCustomEventFailedEvent -= ReportCustomEventFailedEvent;
			UpsightManager.trackInAppPurchaseSucceededEvent -= TrackInAppPurchaseSucceededEvent;
			UpsightManager.trackInAppPurchaseFailedEvent -= TrackInAppPurchaseFailedEvent;
		}

		private void OpenRequestSucceededEvent(global::System.Collections.Generic.Dictionary<string, object> dict)
		{
			logger.Debug("{0}openRequestSucceededEvent: {1}", "Ups: ", global::MiniJSON.Json.Serialize(dict));
		}

		private void OpenRequestFailedEvent(string error)
		{
			logger.Debug("{0}openRequestFailedEvent: {1}", "Ups: ", error);
		}

		private void ContentWillDisplayEvent(string placementID)
		{
			playSFXSignal.Dispatch("Play_menu_popUp_01");
			logger.Debug("{0}contentWillDisplayEvent: {1}", "Ups: ", placementID);
		}

		private void ContentDidDisplayEvent(string placementID)
		{
			placementIdToContentReady[placementID] = false;
			logger.Debug("{0}contentDidDisplay: {1}", "Ups: ", placementID);
		}

		private void ContentRequestLoadedEvent(string placement)
		{
			logger.Debug("{0}contentRequestLoadedEvent: {1}", "Ups: ", placement);
		}

		private void ContentRequestFailedEvent(string placement, string error)
		{
			logger.Debug("{0}contentRequestFailedEvent. placement: {1}, error: {2}", "Ups: ", placement, error);
		}

		private void ContentPreloadSucceededEvent(string placement)
		{
			placementIdToContentReady[placement] = true;
			logger.Debug("{0}contentPreloadSucceededEvent: {1}", "Ups: ", placement);
			upsightContentPreloadedSignal.Dispatch(global::Kampai.Game.UpsightPlacementUtils.GetPlacement(placement));
		}

		private void ContentPreloadFailedEvent(string placement, string error)
		{
			placementIdToContentReady[placement] = false;
			logger.Debug("{0}contentPreloadFailedEvent. placement: {1}, error: {2}", "Ups: ", placement, error);
		}

		private string GetDismissedEventByType(string dismissType)
		{
			switch (dismissType)
			{
			case "Launch":
			case "ContentUnitTriggeredDismiss":
				return "Yes";
			default:
				return "Close";
			}
		}

		private void ContentDismissedEvent(string placement, string dismissType)
		{
			playSFXSignal.Dispatch("Play_button_click_01");
			string dismissedEventByType = GetDismissedEventByType(dismissType);
			logger.Debug("{0}ContentDismissedEvent. placement: {1}, dismissType: {2}, eventDismissType: {3}", "Ups: ", placement, dismissType, dismissedEventByType);
			telemetryService.Send_Telemetry_EVT_IN_APP_MESSAGE_DISPLAYED(placement, dismissedEventByType);
		}

		private void ReportCustomEventSucceededEvent()
		{
			logger.Debug("{0}ReportCustomEventSucceededEvent", "Ups: ");
		}

		private void ReportCustomEventFailedEvent(string error)
		{
			logger.Error("{0}ReportCustomEventFailedEvent: error = ", "Ups: ", error);
		}

		private void TrackInAppPurchaseSucceededEvent()
		{
			logger.Debug("{0}TrackInAppPurchaseSucceededEvent", "Ups: ");
		}

		private void TrackInAppPurchaseFailedEvent(string error)
		{
			logger.Error("{0}TrackInAppPurchaseFailedEvent: error = ", "Ups: ", error);
		}
	}
}
