namespace __preref_Kampai_Main_SetupHockeyAppCommand
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Main.SetupHockeyAppCommand();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[6]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Main.SetupHockeyAppCommand)target).managers = (global::UnityEngine.GameObject)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(ILocalPersistanceService), delegate(object target, object val)
				{
					((global::Kampai.Main.SetupHockeyAppCommand)target).persistanceService = (ILocalPersistanceService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.Service.HealthMetrics.IClientHealthService), delegate(object target, object val)
				{
					((global::Kampai.Main.SetupHockeyAppCommand)target).clientHealthService = (global::Kampai.Common.Service.HealthMetrics.IClientHealthService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.Main.SetupHockeyAppCommand)target).logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.command.api.ICommandBinder), delegate(object target, object val)
				{
					((global::Kampai.Main.SetupHockeyAppCommand)target).commandBinder = (global::strange.extensions.command.api.ICommandBinder)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.injector.api.IInjectionBinder), delegate(object target, object val)
				{
					((global::Kampai.Main.SetupHockeyAppCommand)target).injectionBinder = (global::strange.extensions.injector.api.IInjectionBinder)val;
				})
			};
			SetterNames = new object[6]
			{
				global::Kampai.Main.MainElement.MANAGER_PARENT,
				null,
				null,
				null,
				null,
				null
			};
		}
	}
}
