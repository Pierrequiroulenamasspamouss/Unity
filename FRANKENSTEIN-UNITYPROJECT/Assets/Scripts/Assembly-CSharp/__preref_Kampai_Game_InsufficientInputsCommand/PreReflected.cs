namespace __preref_Kampai_Game_InsufficientInputsCommand
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.InsufficientInputsCommand();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[8]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.PendingCurrencyTransaction), delegate(object target, object val)
				{
					((global::Kampai.Game.InsufficientInputsCommand)target).model = (global::Kampai.Game.PendingCurrencyTransaction)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.LoadRushDialogSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.InsufficientInputsCommand)target).loadRushDialogSignal = (global::Kampai.Game.LoadRushDialogSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(bool), delegate(object target, object val)
				{
					((global::Kampai.Game.InsufficientInputsCommand)target).GrindFromPremium = (bool)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ICurrencyService), delegate(object target, object val)
				{
					((global::Kampai.Game.InsufficientInputsCommand)target).currencyService = (global::Kampai.Game.ICurrencyService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.LoadCurrencyWarningSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.InsufficientInputsCommand)target).loadCurrencyWarningSignal = (global::Kampai.Game.LoadCurrencyWarningSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerService), delegate(object target, object val)
				{
					((global::Kampai.Game.InsufficientInputsCommand)target).playerService = (global::Kampai.Game.IPlayerService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.command.api.ICommandBinder), delegate(object target, object val)
				{
					((global::Kampai.Game.InsufficientInputsCommand)target).commandBinder = (global::strange.extensions.command.api.ICommandBinder)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.injector.api.IInjectionBinder), delegate(object target, object val)
				{
					((global::Kampai.Game.InsufficientInputsCommand)target).injectionBinder = (global::strange.extensions.injector.api.IInjectionBinder)val;
				})
			};
			SetterNames = new object[8];
		}
	}
}
