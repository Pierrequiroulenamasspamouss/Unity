namespace __preref_Kampai_Game_View_LoadRushDialogCommand
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.View.LoadRushDialogCommand();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[8]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.PendingCurrencyTransaction), delegate(object target, object val)
				{
					((global::Kampai.Game.View.LoadRushDialogCommand)target).pendingCurrencyTransaction = (global::Kampai.Game.PendingCurrencyTransaction)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.RushDialogView.RushDialogType), delegate(object target, object val)
				{
					((global::Kampai.Game.View.LoadRushDialogCommand)target).type = (global::Kampai.UI.View.RushDialogView.RushDialogType)(int)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.IGUIService), delegate(object target, object val)
				{
					((global::Kampai.Game.View.LoadRushDialogCommand)target).guiService = (global::Kampai.UI.View.IGUIService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.IRoutineRunner), delegate(object target, object val)
				{
					((global::Kampai.Game.View.LoadRushDialogCommand)target).routineRunner = (global::Kampai.Util.IRoutineRunner)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.PlayGlobalSoundFXSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.LoadRushDialogCommand)target).playSFXSignal = (global::Kampai.Main.PlayGlobalSoundFXSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.Game.View.LoadRushDialogCommand)target).logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.command.api.ICommandBinder), delegate(object target, object val)
				{
					((global::Kampai.Game.View.LoadRushDialogCommand)target).commandBinder = (global::strange.extensions.command.api.ICommandBinder)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.injector.api.IInjectionBinder), delegate(object target, object val)
				{
					((global::Kampai.Game.View.LoadRushDialogCommand)target).injectionBinder = (global::strange.extensions.injector.api.IInjectionBinder)val;
				})
			};
			SetterNames = new object[8];
		}
	}
}
