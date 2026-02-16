namespace __preref_Kampai_Util_AnimatorStateInfo_AnimatorStateMediator
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Util.AnimatorStateInfo.AnimatorStateMediator();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[3]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::System.Collections.Generic.Dictionary<int, string>), delegate(object target, object val)
				{
					((global::Kampai.Util.AnimatorStateInfo.AnimatorStateMediator)target).animatorStateInfo = (global::System.Collections.Generic.Dictionary<int, string>)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.AnimatorStateInfo.AnimatorStateView), delegate(object target, object val)
				{
					((global::Kampai.Util.AnimatorStateInfo.AnimatorStateMediator)target).view = (global::Kampai.Util.AnimatorStateInfo.AnimatorStateView)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Util.AnimatorStateInfo.AnimatorStateMediator)target).contextView = (global::UnityEngine.GameObject)val;
				})
			};
			SetterNames = new object[3]
			{
				global::Kampai.Util.UtilElement.ANIMATOR_STATE_DEBUG_INFO,
				null,
				global::strange.extensions.context.api.ContextKeys.CONTEXT_VIEW
			};
		}
	}
}
