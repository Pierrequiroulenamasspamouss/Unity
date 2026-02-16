namespace __preref_Kampai_Game_View_TSMCharacterMediator
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.View.TSMCharacterMediator();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[4]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.View.TSMCharacterView), delegate(object target, object val)
				{
					((global::Kampai.Game.View.TSMCharacterMediator)target).View = (global::Kampai.Game.View.TSMCharacterView)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.HideTSMCharacterSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.TSMCharacterMediator)target).HideTSMCharacterSignal = (global::Kampai.Game.HideTSMCharacterSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Game.View.TSMCharacterMediator)target).NamedCharacterManager = (global::UnityEngine.GameObject)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Game.View.TSMCharacterMediator)target).contextView = (global::UnityEngine.GameObject)val;
				})
			};
			SetterNames = new object[4]
			{
				null,
				null,
				global::Kampai.Game.GameElement.NAMED_CHARACTER_MANAGER,
				global::strange.extensions.context.api.ContextKeys.CONTEXT_VIEW
			};
		}
	}
}
