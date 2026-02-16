namespace __preref_Kampai_UI_View_SocialPartyFillOrderMediator
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.UI.View.SocialPartyFillOrderMediator();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[26]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.IGUIService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SocialPartyFillOrderMediator)target).guiService = (global::Kampai.UI.View.IGUIService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.ShowSocialPartyFillOrderButtonSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SocialPartyFillOrderMediator)target).showPartyFillOrderButton = (global::Kampai.UI.View.ShowSocialPartyFillOrderButtonSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.ShowSocialPartyFillOrderProfileButtonSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SocialPartyFillOrderMediator)target).showPartyFillOrderProfile = (global::Kampai.UI.View.ShowSocialPartyFillOrderProfileButtonSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IDefinitionService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SocialPartyFillOrderMediator)target).definitionService = (global::Kampai.Game.IDefinitionService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ISocialService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SocialPartyFillOrderMediator)target).facebookService = (global::Kampai.Game.ISocialService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ITimedSocialEventService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SocialPartyFillOrderMediator)target).timedSocialEventService = (global::Kampai.Game.ITimedSocialEventService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.ShowSocialPartyStartSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SocialPartyFillOrderMediator)target).showSocialPartyStartSignal = (global::Kampai.UI.View.ShowSocialPartyStartSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.ShowSocialPartyInviteAlertSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SocialPartyFillOrderMediator)target).showSocialPartyInviteAlertSignal = (global::Kampai.UI.View.ShowSocialPartyInviteAlertSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.ILocalizationService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SocialPartyFillOrderMediator)target).localService = (global::Kampai.Main.ILocalizationService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.context.api.ICrossContextCapable), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SocialPartyFillOrderMediator)target).gameContext = (global::strange.extensions.context.api.ICrossContextCapable)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.ShowSocialPartyEventEndSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SocialPartyFillOrderMediator)target).showSocialPartyEventEndSignal = (global::Kampai.UI.View.ShowSocialPartyEventEndSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.PlayGlobalSoundFXSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SocialPartyFillOrderMediator)target).globalSFX = (global::Kampai.Main.PlayGlobalSoundFXSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.ICoppaService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SocialPartyFillOrderMediator)target).coppaService = (global::Kampai.Common.ICoppaService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.HideSkrimSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SocialPartyFillOrderMediator)target).hideSignal = (global::Kampai.UI.View.HideSkrimSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.SocialPartyFillOrderSetupUISignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SocialPartyFillOrderMediator)target).socialPartyFillOrderSetupUISignal = (global::Kampai.UI.View.SocialPartyFillOrderSetupUISignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.SocialPartyFillOrderButtonMediatorUpdateSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SocialPartyFillOrderMediator)target).socialPartyFillOrderButtonMediatorUpdateSignal = (global::Kampai.UI.View.SocialPartyFillOrderButtonMediatorUpdateSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.SocialPartyFillOrderProfileButtonMediatorUpdateSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SocialPartyFillOrderMediator)target).socialPartyFillOrderProfileButtonMediatorUpdateSignal = (global::Kampai.UI.View.SocialPartyFillOrderProfileButtonMediatorUpdateSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.NetworkConnectionLostSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SocialPartyFillOrderMediator)target).networkConnectionLostSignal = (global::Kampai.Common.NetworkConnectionLostSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ITimeService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SocialPartyFillOrderMediator)target).timeService = (global::Kampai.Game.ITimeService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.HideHUDAndIconsSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SocialPartyFillOrderMediator)target).hideHUDSignal = (global::Kampai.UI.View.HideHUDAndIconsSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.SocialLoginSuccessSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SocialPartyFillOrderMediator)target).loginSuccess = (global::Kampai.Game.SocialLoginSuccessSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.SocialPartyFillOrderView), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SocialPartyFillOrderMediator)target).view = (global::Kampai.UI.View.SocialPartyFillOrderView)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.UIAddedSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SocialPartyFillOrderMediator)target).uiAddedSignal = (global::Kampai.UI.View.UIAddedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.UIRemovedSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SocialPartyFillOrderMediator)target).uiRemovedSignal = (global::Kampai.UI.View.UIRemovedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.CloseAllOtherMenuSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SocialPartyFillOrderMediator)target).closeAllOtherMenuSignal = (global::Kampai.UI.View.CloseAllOtherMenuSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.UI.View.SocialPartyFillOrderMediator)target).contextView = (global::UnityEngine.GameObject)val;
				})
			};
			SetterNames = new object[26]
			{
				null,
				null,
				null,
				null,
				global::Kampai.Game.SocialServices.FACEBOOK,
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
