namespace __preref_Kampai_Game_View_StuartMediator
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.View.StuartMediator();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[11]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.View.StuartView), delegate(object target, object val)
				{
					((global::Kampai.Game.View.StuartMediator)target).view = (global::Kampai.Game.View.StuartView)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.StuartAddToStageSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.StuartMediator)target).addToStageSignal = (global::Kampai.Game.StuartAddToStageSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.StuartStartPerformingSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.StuartMediator)target).startPerformingSignal = (global::Kampai.Game.StuartStartPerformingSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.StuartGetOnStageSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.StuartMediator)target).getOnStageSignal = (global::Kampai.Game.StuartGetOnStageSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.StuartGetOnStageImmediateSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.StuartMediator)target).getOnStageImmediateSignal = (global::Kampai.Game.StuartGetOnStageImmediateSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.StuartCelebrateSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.StuartMediator)target).celebrateSignal = (global::Kampai.Game.StuartCelebrateSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.StuartGetAttentionSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.StuartMediator)target).getAttentionSignal = (global::Kampai.Game.StuartGetAttentionSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.AnimateStuartSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.StuartMediator)target).animateStuartSignal = (global::Kampai.Game.AnimateStuartSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.StuartShowCompleteSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.StuartMediator)target).stuartShowCompleteSignal = (global::Kampai.Game.StuartShowCompleteSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.GenerateTemporaryMinionsStageSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.StuartMediator)target).generateTemporaryMinionsStageSignal = (global::Kampai.Game.GenerateTemporaryMinionsStageSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Game.View.StuartMediator)target).contextView = (global::UnityEngine.GameObject)val;
				})
			};
			SetterNames = new object[11]
			{
				null,
				null,
				null,
				null,
				null,
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
