namespace __preref_Kampai_Game_PopulateEnvironmentCommand
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.PopulateEnvironmentCommand();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[10]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.Game.PopulateEnvironmentCommand)target).logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.Environment), delegate(object target, object val)
				{
					((global::Kampai.Game.PopulateEnvironmentCommand)target).environment = (global::Kampai.Game.Environment)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.EnvironmentBuilder), delegate(object target, object val)
				{
					((global::Kampai.Game.PopulateEnvironmentCommand)target).environmentBuilder = (global::Kampai.Game.EnvironmentBuilder)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.PlayGlobalMusicSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.PopulateEnvironmentCommand)target).musicSignal = (global::Kampai.Main.PlayGlobalMusicSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.DebugUpdateGridSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.PopulateEnvironmentCommand)target).DebugUpdateGridSignal = (global::Kampai.Game.DebugUpdateGridSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IDefinitionService), delegate(object target, object val)
				{
					((global::Kampai.Game.PopulateEnvironmentCommand)target).definitionService = (global::Kampai.Game.IDefinitionService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(bool), delegate(object target, object val)
				{
					((global::Kampai.Game.PopulateEnvironmentCommand)target).display = (bool)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.EnvironmentState), delegate(object target, object val)
				{
					((global::Kampai.Game.PopulateEnvironmentCommand)target).state = (global::Kampai.Game.EnvironmentState)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.command.api.ICommandBinder), delegate(object target, object val)
				{
					((global::Kampai.Game.PopulateEnvironmentCommand)target).commandBinder = (global::strange.extensions.command.api.ICommandBinder)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.injector.api.IInjectionBinder), delegate(object target, object val)
				{
					((global::Kampai.Game.PopulateEnvironmentCommand)target).injectionBinder = (global::strange.extensions.injector.api.IInjectionBinder)val;
				})
			};
			SetterNames = new object[10];
		}
	}
}
