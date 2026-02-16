namespace __preref_Kampai_Splash_LoadingBarMediator
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Splash.LoadingBarMediator();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[5]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Splash.LoadingBarView), delegate(object target, object val)
				{
					((global::Kampai.Splash.LoadingBarMediator)target).view = (global::Kampai.Splash.LoadingBarView)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Splash.SplashProgressUpdateSignal), delegate(object target, object val)
				{
					((global::Kampai.Splash.LoadingBarMediator)target).splashProgressUpdateSignal = (global::Kampai.Splash.SplashProgressUpdateSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Splash.SplashProgressResetSignal), delegate(object target, object val)
				{
					((global::Kampai.Splash.LoadingBarMediator)target).splashProgressResetSignal = (global::Kampai.Splash.SplashProgressResetSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.Splash.LoadingBarMediator)target).logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Splash.LoadingBarMediator)target).contextView = (global::UnityEngine.GameObject)val;
				})
			};
			SetterNames = new object[5]
			{
				null,
				null,
				null,
				null,
				global::strange.extensions.context.api.ContextKeys.CONTEXT_VIEW
			};
		}
	}
}
