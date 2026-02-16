namespace __preref_Kampai_Game_CreateNamedCharacterViewCommand
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.CreateNamedCharacterViewCommand();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[9]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.NamedCharacter), delegate(object target, object val)
				{
					((global::Kampai.Game.CreateNamedCharacterViewCommand)target).namedCharacter = (global::Kampai.Game.NamedCharacter)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.Game.CreateNamedCharacterViewCommand)target).logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.INamedCharacterBuilder), delegate(object target, object val)
				{
					((global::Kampai.Game.CreateNamedCharacterViewCommand)target).builder = (global::Kampai.Util.INamedCharacterBuilder)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.InitCharacterObjectSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.CreateNamedCharacterViewCommand)target).initSignal = (global::Kampai.Game.InitCharacterObjectSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Game.CreateNamedCharacterViewCommand)target).namedCharacterManager = (global::UnityEngine.GameObject)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.AddNamedCharacterSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.CreateNamedCharacterViewCommand)target).addCharacterSignal = (global::Kampai.Game.AddNamedCharacterSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.MapAnimationEventSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.CreateNamedCharacterViewCommand)target).mapAnimationEventSignal = (global::Kampai.Game.MapAnimationEventSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.command.api.ICommandBinder), delegate(object target, object val)
				{
					((global::Kampai.Game.CreateNamedCharacterViewCommand)target).commandBinder = (global::strange.extensions.command.api.ICommandBinder)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.injector.api.IInjectionBinder), delegate(object target, object val)
				{
					((global::Kampai.Game.CreateNamedCharacterViewCommand)target).injectionBinder = (global::strange.extensions.injector.api.IInjectionBinder)val;
				})
			};
			SetterNames = new object[9]
			{
				null,
				null,
				null,
				null,
				global::Kampai.Game.GameElement.NAMED_CHARACTER_MANAGER,
				null,
				null,
				null,
				null
			};
		}
	}
}
