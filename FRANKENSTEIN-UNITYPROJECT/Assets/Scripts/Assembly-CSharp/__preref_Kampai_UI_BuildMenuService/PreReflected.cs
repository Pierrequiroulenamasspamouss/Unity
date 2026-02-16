namespace __preref_Kampai_UI_BuildMenuService
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.UI.BuildMenuService();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = new global::System.Action<object>[1]
			{
				delegate(object t)
				{
					((global::Kampai.UI.BuildMenuService)t).PostConstruct();
				}
			};
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[8]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerService), delegate(object target, object val)
				{
					((global::Kampai.UI.BuildMenuService)target).playerService = (global::Kampai.Game.IPlayerService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IDefinitionService), delegate(object target, object val)
				{
					((global::Kampai.UI.BuildMenuService)target).definitionService = (global::Kampai.Game.IDefinitionService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(ILocalPersistanceService), delegate(object target, object val)
				{
					((global::Kampai.UI.BuildMenuService)target).localPersistanceService = (ILocalPersistanceService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.UI.BuildMenuService)target).logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.SetBadgeForStoreTabSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.BuildMenuService)target).setBadgeForTabSignal = (global::Kampai.UI.View.SetBadgeForStoreTabSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.SetNewUnlockForStoreTabSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.BuildMenuService)target).setNewUnlockForTabSignal = (global::Kampai.UI.View.SetNewUnlockForStoreTabSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.SetNewUnlockForBuildMenuSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.BuildMenuService)target).setNewUnlockForBuildMenuSignal = (global::Kampai.UI.View.SetNewUnlockForBuildMenuSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.SetInventoryCountForBuildMenuSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.BuildMenuService)target).setInventoryCountForBuildMenuSignal = (global::Kampai.UI.View.SetInventoryCountForBuildMenuSignal)val;
				})
			};
			SetterNames = new object[8];
		}
	}
}
