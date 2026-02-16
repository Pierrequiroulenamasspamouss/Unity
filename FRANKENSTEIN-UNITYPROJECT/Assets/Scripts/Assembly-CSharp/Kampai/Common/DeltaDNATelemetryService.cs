namespace Kampai.Common
{
	public class DeltaDNATelemetryService : global::Kampai.Common.IIapTelemetryService
	{
		private const string TAG = "DeltaDNA: ";

		private const string PREMIUM_CURRENCY_NAME = "PREMIUM";

		private const string GRIND_CURRENCY_NAME = "GRIND";

		private global::DeltaDNA.SDK DeltaDNASDK = global::DeltaDNA.Singleton<global::DeltaDNA.SDK>.Instance;

		private global::System.Collections.Generic.IList<global::Kampai.Common.DeltaDNATelemetryEvent> EventQueue = new global::System.Collections.Generic.List<global::Kampai.Common.DeltaDNATelemetryEvent>();

		private bool Initialized;

		private global::Kampai.Common.IapTelemetryEvent pendingIapTelemetryEvent;

		private string platform;

		[Inject]
		public ILocalPersistanceService persistanceService { get; set; }

		[Inject]
		public global::Kampai.Util.ClientVersion clientVersion { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		public string ClientIp { get; set; }

		public void Initialize()
		{
			if (!Initialized && persistanceService.HasKey("UserID"))
			{
				DeltaDNASDK.Settings.DebugMode = global::Kampai.Util.GameConstants.StaticConfig.ENVIRONMENT == "dev";
				DeltaDNASDK.ClientVersion = clientVersion.GetClientVersion();
				DeltaDNASDK.UseCollectTimestamp(true);
				DeltaDNASDK.StartSDK(global::Kampai.Util.GameConstants.StaticConfig.DELTADNA_ENVIRONMENT_KEY, global::Kampai.Util.GameConstants.StaticConfig.DELTADNA_COLLECT_URL, global::Kampai.Util.GameConstants.StaticConfig.DELTADNA_ENGAGE_URL, persistanceService.GetData("UserID"));
				logger.Info("DeltaDNA SDK initialized");
				Initialized = true;
			}
		}

		public void LogEvent(global::Kampai.Common.DeltaDNATelemetryEvent gameEvent)
		{
			EventQueue.Add(gameEvent);
			if (!Initialized)
			{
				return;
			}
			foreach (global::Kampai.Common.DeltaDNATelemetryEvent item in EventQueue)
			{
				LogDeltaDNAEvent(item);
			}
			EventQueue.Clear();
		}

		private void LogDeltaDNAEvent(global::Kampai.Common.DeltaDNATelemetryEvent gameEvent)
		{
			global::DeltaDNA.EventBuilder eventBuilder = new global::DeltaDNA.EventBuilder();
			foreach (global::System.Collections.Generic.KeyValuePair<string, object> parameter in gameEvent.Parameters)
			{
				eventBuilder.AddParam(parameter.Key, parameter.Value);
			}
			DeltaDNASDK.RecordEvent(gameEvent.EventName, eventBuilder);
		}

		public void PostEvents()
		{
			if (Initialized && !DeltaDNASDK.IsUploading)
			{
				DeltaDNASDK.Upload();
			}
		}

		public void SendInAppPurchaseEventOnPurchaseComplete(global::Kampai.Common.IapTelemetryEvent iapTelemetryEvent)
		{
			logger.Debug("{0}SendInAppPurchaseEventOnPurchaseComplete()...: iapTelemetryEvent = {1}", "DeltaDNA: ", (iapTelemetryEvent == null) ? "null" : iapTelemetryEvent.ToString());
			pendingIapTelemetryEvent = iapTelemetryEvent;
		}

		public string getPlatform()
		{
			if (platform == null)
			{
				platform = ((!global::Kampai.Util.DeviceCapabilities.IsTablet()) ? "ANDROID_MOBILE" : "ANDROID_TABLET");
			}
			return platform;
		}

		private global::DeltaDNA.EventBuilder prepareDeltaDNATransactionEventBuilder(global::Kampai.Game.Transaction.TransactionDefinition reward)
		{
			global::DeltaDNA.EventBuilder eventBuilder = new global::DeltaDNA.EventBuilder().AddParam("transactionName", reward.LocalizedKey).AddParam("platform", getPlatform()).AddParam("productID", pendingIapTelemetryEvent.productId)
				.AddParam("transactionType", "PURCHASE")
				.AddParam("productsSpent", new global::DeltaDNA.ProductBuilder().AddRealCurrency(pendingIapTelemetryEvent.currency, global::System.Convert.ToInt32(pendingIapTelemetryEvent.productPrice * 100.0)));
			eventBuilder.AddParam("transactorID", DeltaDNASDK.UserID);
			global::DeltaDNA.ProductBuilder productBuilder = new global::DeltaDNA.ProductBuilder();
			foreach (global::Kampai.Util.QuantityItem output in reward.Outputs)
			{
				if (output.ID == 0)
				{
					productBuilder.AddVirtualCurrency("GRIND", "GRIND", global::System.Convert.ToInt32(output.Quantity));
				}
				else if (output.ID == 1)
				{
					productBuilder.AddVirtualCurrency("PREMIUM", "PREMIUM", global::System.Convert.ToInt32(output.Quantity));
				}
			}
			eventBuilder.AddParam("productsReceived", productBuilder);
			eventBuilder.AddParam("transactionServer", "GOOGLE");
			eventBuilder.AddParam("transactionID", pendingIapTelemetryEvent.googleOrderId);
			return eventBuilder;
		}

		public void SendInAppPurchaseEventOnProductDelivery(string sku, global::Kampai.Game.Transaction.TransactionDefinition reward)
		{
			logger.Debug("{0}SendInAppPurchaseEventOnProductDelivery()...: sku = {1}", "DeltaDNA: ", sku);
			if (sku == null)
			{
				logger.Log(global::Kampai.Util.Logger.Level.Error, "{0}Skip sending IAP telemetry because of sku is null", "DeltaDNA: ");
				return;
			}
			if (pendingIapTelemetryEvent == null)
			{
				logger.Log(global::Kampai.Util.Logger.Level.Error, "{0}Skip sending IAP telemetry because pendingIapTelemetryEvent is null for sku: {1}", "DeltaDNA: ", sku);
				return;
			}
			if (!sku.Equals(pendingIapTelemetryEvent.productId))
			{
				logger.Log(global::Kampai.Util.Logger.Level.Error, "{0}Skip sending IAP telemetry because pendingIapTelemetryEvent's sku [{1}] does not match current sku: [{2}]", "DeltaDNA: ", pendingIapTelemetryEvent.productId ?? "null", sku);
			}
			global::DeltaDNA.EventBuilder eventBuilder = prepareDeltaDNATransactionEventBuilder(reward);
			logger.Info("DeltaDNA transaction : {0}#", eventBuilder.ToPrettyString());
			global::DeltaDNA.Singleton<global::DeltaDNA.SDK>.Instance.RecordEvent("transaction", eventBuilder);
			pendingIapTelemetryEvent = null;
			logger.Debug("{0}...SendInAppPurchaseEventOnProductDelivery()", "DeltaDNA: ");
		}
	}
}
