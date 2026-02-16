namespace __preref_Kampai_Game_DebugCurrencyService
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.DebugCurrencyService();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[6]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.ShowMockStoreDialogSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.DebugCurrencyService)target).showMockStoreDialogSignal = (global::Kampai.UI.View.ShowMockStoreDialogSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.Game.DebugCurrencyService)target).logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerService), delegate(object target, object val)
				{
					((global::Kampai.Game.DebugCurrencyService)target).playerService = (global::Kampai.Game.IPlayerService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.FinishPremiumPurchaseSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.DebugCurrencyService)target).finishPremiumPurchaseSignal = (global::Kampai.Game.FinishPremiumPurchaseSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CancelPremiumPurchaseSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.DebugCurrencyService)target).cancelPremiumPurchaseSignal = (global::Kampai.Game.CancelPremiumPurchaseSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.CurrencyDialogClosedSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.DebugCurrencyService)target).currencyDialogClosedSignal = (global::Kampai.UI.View.CurrencyDialogClosedSignal)val;
				})
			};
			SetterNames = new object[6];
		}
	}
}
