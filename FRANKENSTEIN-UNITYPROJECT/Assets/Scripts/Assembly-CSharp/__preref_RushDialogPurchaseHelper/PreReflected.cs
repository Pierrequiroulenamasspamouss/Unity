namespace __preref_RushDialogPurchaseHelper
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new RushDialogPurchaseHelper();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[6]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerService), delegate(object target, object val)
				{
					((RushDialogPurchaseHelper)target).playerService = (global::Kampai.Game.IPlayerService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IDefinitionService), delegate(object target, object val)
				{
					((RushDialogPurchaseHelper)target).definitionService = (global::Kampai.Game.IDefinitionService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.RushDialogConfirmationSignal), delegate(object target, object val)
				{
					((RushDialogPurchaseHelper)target).confirmedSignal = (global::Kampai.UI.View.RushDialogConfirmationSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.LoadRushDialogSignal), delegate(object target, object val)
				{
					((RushDialogPurchaseHelper)target).loadRushDialogSignal = (global::Kampai.Game.LoadRushDialogSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.SetPremiumCurrencySignal), delegate(object target, object val)
				{
					((RushDialogPurchaseHelper)target).setPremiumCurrencySignal = (global::Kampai.UI.View.SetPremiumCurrencySignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.SetGrindCurrencySignal), delegate(object target, object val)
				{
					((RushDialogPurchaseHelper)target).setGrindCurrencySignal = (global::Kampai.UI.View.SetGrindCurrencySignal)val;
				})
			};
			SetterNames = new object[6];
		}
	}
}
