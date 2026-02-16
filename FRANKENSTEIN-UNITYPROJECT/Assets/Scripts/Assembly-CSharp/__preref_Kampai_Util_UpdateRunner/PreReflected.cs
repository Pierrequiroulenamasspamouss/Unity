namespace __preref_Kampai_Util_UpdateRunner
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Util.UpdateRunner();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = new global::System.Action<object>[1]
			{
				delegate(object t)
				{
					((global::Kampai.Util.UpdateRunner)t).PostConstruct();
				}
			};
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[1]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Util.UpdateRunner)target).contextView = (global::UnityEngine.GameObject)val;
				})
			};
			SetterNames = new object[1] { global::strange.extensions.context.api.ContextKeys.CONTEXT_VIEW };
		}
	}
}
