namespace __preref_Kampai_UI_View_HideHUDAndIconsCommand
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.UI.View.HideHUDAndIconsCommand();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[9]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(bool), delegate(object target, object val)
				{
					((global::Kampai.UI.View.HideHUDAndIconsCommand)target).isVisible = (bool)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.SetBuildMenuEnabledSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.HideHUDAndIconsCommand)target).setBuildMenuEnabledSignal = (global::Kampai.UI.View.SetBuildMenuEnabledSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.SetHUDButtonsVisibleSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.HideHUDAndIconsCommand)target).setHUDButtonsVisibleSignal = (global::Kampai.UI.View.SetHUDButtonsVisibleSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.ShowAllWayFindersSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.HideHUDAndIconsCommand)target).showAllWayFindersSignal = (global::Kampai.UI.View.ShowAllWayFindersSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.HideAllWayFindersSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.HideHUDAndIconsCommand)target).hideAllWayFindersSignal = (global::Kampai.UI.View.HideAllWayFindersSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.ShowAllResourceIconsSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.HideHUDAndIconsCommand)target).showAllResourceIconsSignal = (global::Kampai.UI.View.ShowAllResourceIconsSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.HideAllResourceIconsSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.HideHUDAndIconsCommand)target).hideAllResourceIconsSignal = (global::Kampai.UI.View.HideAllResourceIconsSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.command.api.ICommandBinder), delegate(object target, object val)
				{
					((global::Kampai.UI.View.HideHUDAndIconsCommand)target).commandBinder = (global::strange.extensions.command.api.ICommandBinder)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.injector.api.IInjectionBinder), delegate(object target, object val)
				{
					((global::Kampai.UI.View.HideHUDAndIconsCommand)target).injectionBinder = (global::strange.extensions.injector.api.IInjectionBinder)val;
				})
			};
			SetterNames = new object[9];
		}
	}
}
