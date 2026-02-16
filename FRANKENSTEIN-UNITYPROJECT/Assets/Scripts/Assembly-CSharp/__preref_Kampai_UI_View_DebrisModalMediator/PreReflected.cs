namespace __preref_Kampai_UI_View_DebrisModalMediator
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.UI.View.DebrisModalMediator();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[21]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CallMinionSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.DebrisModalMediator)target).CallMinionSignal = (global::Kampai.Game.CallMinionSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.IPickService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.DebrisModalMediator)target).PickService = (global::Kampai.Common.IPickService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IDefinitionService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.DebrisModalMediator)target).DefinitionService = (global::Kampai.Game.IDefinitionService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.DebrisModalMediator)target).PlayerService = (global::Kampai.Game.IPlayerService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.SetGrindCurrencySignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.DebrisModalMediator)target).SetGrindCurrencySignal = (global::Kampai.UI.View.SetGrindCurrencySignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.SpawnDooberSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.DebrisModalMediator)target).TweenSignal = (global::Kampai.UI.View.SpawnDooberSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.HideSkrimSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.DebrisModalMediator)target).HideSignal = (global::Kampai.UI.View.HideSkrimSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.IGUIService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.DebrisModalMediator)target).GUIService = (global::Kampai.UI.View.IGUIService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.ILocalizationService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.DebrisModalMediator)target).LocalizationService = (global::Kampai.Main.ILocalizationService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.context.api.ICrossContextCapable), delegate(object target, object val)
				{
					((global::Kampai.UI.View.DebrisModalMediator)target).GameContext = (global::strange.extensions.context.api.ICrossContextCapable)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.OnDropItemOverDropAreaSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.DebrisModalMediator)target).OnDropItemOverDropAreaSignal = (global::Kampai.UI.View.OnDropItemOverDropAreaSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.OnDragItemOverDropAreaSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.DebrisModalMediator)target).OnDragItemOverDropAreaSignal = (global::Kampai.UI.View.OnDragItemOverDropAreaSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.RushDialogConfirmationSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.DebrisModalMediator)target).RushedSignal = (global::Kampai.UI.View.RushDialogConfirmationSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.ShowNeedXMinionsSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.DebrisModalMediator)target).ShowNeedXMinionsSignal = (global::Kampai.UI.View.ShowNeedXMinionsSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.UI.View.DebrisModalMediator)target).Logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.PlayGlobalSoundFXSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.DebrisModalMediator)target).soundFXSignal = (global::Kampai.Main.PlayGlobalSoundFXSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.DebrisModalView), delegate(object target, object val)
				{
					((global::Kampai.UI.View.DebrisModalMediator)target).view = (global::Kampai.UI.View.DebrisModalView)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.UIAddedSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.DebrisModalMediator)target).uiAddedSignal = (global::Kampai.UI.View.UIAddedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.UIRemovedSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.DebrisModalMediator)target).uiRemovedSignal = (global::Kampai.UI.View.UIRemovedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.CloseAllOtherMenuSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.DebrisModalMediator)target).closeAllOtherMenuSignal = (global::Kampai.UI.View.CloseAllOtherMenuSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.UI.View.DebrisModalMediator)target).contextView = (global::UnityEngine.GameObject)val;
				})
			};
			SetterNames = new object[21]
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
				global::Kampai.Game.GameElement.CONTEXT,
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
