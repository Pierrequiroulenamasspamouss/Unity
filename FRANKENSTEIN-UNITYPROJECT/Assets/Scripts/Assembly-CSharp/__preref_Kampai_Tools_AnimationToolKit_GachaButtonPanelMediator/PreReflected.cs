namespace __preref_Kampai_Tools_AnimationToolKit_GachaButtonPanelMediator
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Tools.AnimationToolKit.GachaButtonPanelMediator();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[6]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Tools.AnimationToolKit.GachaButtonPanelView), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.GachaButtonPanelMediator)target).view = (global::Kampai.Tools.AnimationToolKit.GachaButtonPanelView)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Tools.AnimationToolKit.AnimationToolkitModel), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.GachaButtonPanelMediator)target).model = (global::Kampai.Tools.AnimationToolKit.AnimationToolkitModel)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IDefinitionService), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.GachaButtonPanelMediator)target).definitionService = (global::Kampai.Game.IDefinitionService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Tools.AnimationToolKit.PlayMinionAnimationSignal), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.GachaButtonPanelMediator)target).playGachaSignal = (global::Kampai.Tools.AnimationToolKit.PlayMinionAnimationSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Tools.AnimationToolKit.EnableInterfaceSignal), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.GachaButtonPanelMediator)target).enableUISignal = (global::Kampai.Tools.AnimationToolKit.EnableInterfaceSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.GachaButtonPanelMediator)target).contextView = (global::UnityEngine.GameObject)val;
				})
			};
			SetterNames = new object[6]
			{
				null,
				null,
				null,
				null,
				null,
				global::strange.extensions.context.api.ContextKeys.CONTEXT_VIEW
			};
		}
	}
}
