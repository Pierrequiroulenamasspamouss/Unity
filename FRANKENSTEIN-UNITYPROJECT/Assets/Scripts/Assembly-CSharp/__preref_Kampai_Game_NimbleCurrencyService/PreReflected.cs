namespace __preref_Kampai_Game_NimbleCurrencyService
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.NimbleCurrencyService();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = new global::System.Action<object>[1]
			{
				delegate(object t)
				{
					((global::Kampai.Game.NimbleCurrencyService)t).PostConstruct();
				}
			};
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[12]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.Mtx.IMtxReceiptValidationService), delegate(object target, object val)
				{
					((global::Kampai.Game.NimbleCurrencyService)target).receiptValidationService = (global::Kampai.Game.Mtx.IMtxReceiptValidationService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.ILocalizationService), delegate(object target, object val)
				{
					((global::Kampai.Game.NimbleCurrencyService)target).localService = (global::Kampai.Main.ILocalizationService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.PremiumCurrencyCatalogUpdatedSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.NimbleCurrencyService)target).premiumCurrencyCatalogUpdatedSignal = (global::Kampai.UI.View.PremiumCurrencyCatalogUpdatedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.ITelemetryService), delegate(object target, object val)
				{
					((global::Kampai.Game.NimbleCurrencyService)target).telemetryService = (global::Kampai.Common.ITelemetryService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.ISwrveService), delegate(object target, object val)
				{
					((global::Kampai.Game.NimbleCurrencyService)target).swrveService = (global::Kampai.Common.ISwrveService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(ILocalPersistanceService), delegate(object target, object val)
				{
					((global::Kampai.Game.NimbleCurrencyService)target).localPersistanceService = (ILocalPersistanceService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ProcessNextPendingTransactionSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.NimbleCurrencyService)target).processNextPendingTransactionSignal = (global::Kampai.Game.ProcessNextPendingTransactionSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.Game.NimbleCurrencyService)target).logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerService), delegate(object target, object val)
				{
					((global::Kampai.Game.NimbleCurrencyService)target).playerService = (global::Kampai.Game.IPlayerService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.FinishPremiumPurchaseSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.NimbleCurrencyService)target).finishPremiumPurchaseSignal = (global::Kampai.Game.FinishPremiumPurchaseSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CancelPremiumPurchaseSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.NimbleCurrencyService)target).cancelPremiumPurchaseSignal = (global::Kampai.Game.CancelPremiumPurchaseSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.CurrencyDialogClosedSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.NimbleCurrencyService)target).currencyDialogClosedSignal = (global::Kampai.UI.View.CurrencyDialogClosedSignal)val;
				})
			};
			SetterNames = new object[12];
		}
	}
}
