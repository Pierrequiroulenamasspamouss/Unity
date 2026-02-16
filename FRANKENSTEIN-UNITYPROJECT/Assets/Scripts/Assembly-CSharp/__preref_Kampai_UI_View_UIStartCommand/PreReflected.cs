namespace __preref_Kampai_UI_View_UIStartCommand
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.UI.View.UIStartCommand();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[11]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.UI.View.UIStartCommand)target).contextView = (global::UnityEngine.GameObject)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.SetupCanvasSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.UIStartCommand)target).canvasSignal = (global::Kampai.UI.View.SetupCanvasSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.Camera), delegate(object target, object val)
				{
					((global::Kampai.UI.View.UIStartCommand)target).mainCamera = (global::UnityEngine.Camera)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.Camera), delegate(object target, object val)
				{
					((global::Kampai.UI.View.UIStartCommand)target).uiCamera = (global::UnityEngine.Camera)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.LoadGUISignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.UIStartCommand)target).loadGUISignal = (global::Kampai.UI.View.LoadGUISignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.LoadUICompleteSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.UIStartCommand)target).loadUICompleteSignal = (global::Kampai.UI.View.LoadUICompleteSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.SetupDeepLinkSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.UIStartCommand)target).setupDeepLinkSignal = (global::Kampai.Main.SetupDeepLinkSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.UI.View.UIStartCommand)target).logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.AddFPSCounterSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.UIStartCommand)target).addFPSCounterSignal = (global::Kampai.UI.View.AddFPSCounterSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.command.api.ICommandBinder), delegate(object target, object val)
				{
					((global::Kampai.UI.View.UIStartCommand)target).commandBinder = (global::strange.extensions.command.api.ICommandBinder)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.injector.api.IInjectionBinder), delegate(object target, object val)
				{
					((global::Kampai.UI.View.UIStartCommand)target).injectionBinder = (global::strange.extensions.injector.api.IInjectionBinder)val;
				})
			};
			SetterNames = new object[11]
			{
				global::strange.extensions.context.api.ContextKeys.CONTEXT_VIEW,
				null,
				global::Kampai.Main.MainElement.CAMERA,
				global::Kampai.UI.View.UIElement.CAMERA,
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
