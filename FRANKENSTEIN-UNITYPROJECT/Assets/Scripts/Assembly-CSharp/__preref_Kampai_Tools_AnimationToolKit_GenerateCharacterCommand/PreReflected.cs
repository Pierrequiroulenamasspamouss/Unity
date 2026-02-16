namespace __preref_Kampai_Tools_AnimationToolKit_GenerateCharacterCommand
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Tools.AnimationToolKit.GenerateCharacterCommand();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[9]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.GenerateCharacterCommand)target).characterGroup = (global::UnityEngine.GameObject)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.GenerateCharacterCommand)target).logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.INamedCharacterBuilder), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.GenerateCharacterCommand)target).builder = (global::Kampai.Util.INamedCharacterBuilder)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerService), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.GenerateCharacterCommand)target).playerService = (global::Kampai.Game.IPlayerService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IDefinitionService), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.GenerateCharacterCommand)target).definitionService = (global::Kampai.Game.IDefinitionService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Tools.AnimationToolKit.CharacterCreatedSignal), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.GenerateCharacterCommand)target).characterCreatedSignal = (global::Kampai.Tools.AnimationToolKit.CharacterCreatedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(int), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.GenerateCharacterCommand)target).namedCharacterId = (int)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.command.api.ICommandBinder), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.GenerateCharacterCommand)target).commandBinder = (global::strange.extensions.command.api.ICommandBinder)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.injector.api.IInjectionBinder), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.GenerateCharacterCommand)target).injectionBinder = (global::strange.extensions.injector.api.IInjectionBinder)val;
				})
			};
			SetterNames = new object[9]
			{
				global::Kampai.Tools.AnimationToolKit.AnimationToolKitElement.CHARACTERS,
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				null
			};
		}
	}
}
