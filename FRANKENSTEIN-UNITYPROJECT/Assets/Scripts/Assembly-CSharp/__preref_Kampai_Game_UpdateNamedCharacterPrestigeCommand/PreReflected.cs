namespace __preref_Kampai_Game_UpdateNamedCharacterPrestigeCommand
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.UpdateNamedCharacterPrestigeCommand();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[8]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.Prestige), delegate(object target, object val)
				{
					((global::Kampai.Game.UpdateNamedCharacterPrestigeCommand)target).prestige = (global::Kampai.Game.Prestige)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.Tuple<global::Kampai.Game.PrestigeState, global::Kampai.Game.PrestigeState>), delegate(object target, object val)
				{
					((global::Kampai.Game.UpdateNamedCharacterPrestigeCommand)target).states = (global::Kampai.Util.Tuple<global::Kampai.Game.PrestigeState, global::Kampai.Game.PrestigeState>)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerService), delegate(object target, object val)
				{
					((global::Kampai.Game.UpdateNamedCharacterPrestigeCommand)target).playerService = (global::Kampai.Game.IPlayerService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.MinionPrestigeCompleteSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.UpdateNamedCharacterPrestigeCommand)target).minionPrestigeCompleteSignal = (global::Kampai.Game.MinionPrestigeCompleteSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.RemoveMinionFromTikiBarSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.UpdateNamedCharacterPrestigeCommand)target).removeMinionFromTikiBarSignal = (global::Kampai.Game.RemoveMinionFromTikiBarSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.context.api.ICrossContextCapable), delegate(object target, object val)
				{
					((global::Kampai.Game.UpdateNamedCharacterPrestigeCommand)target).uiContext = (global::strange.extensions.context.api.ICrossContextCapable)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.command.api.ICommandBinder), delegate(object target, object val)
				{
					((global::Kampai.Game.UpdateNamedCharacterPrestigeCommand)target).commandBinder = (global::strange.extensions.command.api.ICommandBinder)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.injector.api.IInjectionBinder), delegate(object target, object val)
				{
					((global::Kampai.Game.UpdateNamedCharacterPrestigeCommand)target).injectionBinder = (global::strange.extensions.injector.api.IInjectionBinder)val;
				})
			};
			SetterNames = new object[8]
			{
				null,
				null,
				null,
				null,
				null,
				global::Kampai.UI.View.UIElement.CONTEXT,
				null,
				null
			};
		}
	}
}
