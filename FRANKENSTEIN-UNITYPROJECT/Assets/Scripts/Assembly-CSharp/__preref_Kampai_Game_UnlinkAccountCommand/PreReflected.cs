namespace __preref_Kampai_Game_UnlinkAccountCommand
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.UnlinkAccountCommand();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[11]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.Game.UnlinkAccountCommand)target).logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.SocialLogoutSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.UnlinkAccountCommand)target).SocialLogoutSignal = (global::Kampai.Game.SocialLogoutSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(string), delegate(object target, object val)
				{
					((global::Kampai.Game.UnlinkAccountCommand)target).ServerUrl = (string)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Download.IDownloadService), delegate(object target, object val)
				{
					((global::Kampai.Game.UnlinkAccountCommand)target).DownloadService = (global::Kampai.Download.IDownloadService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IUserSessionService), delegate(object target, object val)
				{
					((global::Kampai.Game.UnlinkAccountCommand)target).userSessionService = (global::Kampai.Game.IUserSessionService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ISocialService), delegate(object target, object val)
				{
					((global::Kampai.Game.UnlinkAccountCommand)target).FacebookService = (global::Kampai.Game.ISocialService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ISocialService), delegate(object target, object val)
				{
					((global::Kampai.Game.UnlinkAccountCommand)target).GameCenterService = (global::Kampai.Game.ISocialService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ISocialService), delegate(object target, object val)
				{
					((global::Kampai.Game.UnlinkAccountCommand)target).GooglePlayService = (global::Kampai.Game.ISocialService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IdentityType), delegate(object target, object val)
				{
					((global::Kampai.Game.UnlinkAccountCommand)target).UnlinkType = (global::Kampai.Game.IdentityType)(int)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.command.api.ICommandBinder), delegate(object target, object val)
				{
					((global::Kampai.Game.UnlinkAccountCommand)target).commandBinder = (global::strange.extensions.command.api.ICommandBinder)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.injector.api.IInjectionBinder), delegate(object target, object val)
				{
					((global::Kampai.Game.UnlinkAccountCommand)target).injectionBinder = (global::strange.extensions.injector.api.IInjectionBinder)val;
				})
			};
			SetterNames = new object[11]
			{
				null,
				null,
				"game.server.host",
				null,
				null,
				global::Kampai.Game.SocialServices.FACEBOOK,
				global::Kampai.Game.SocialServices.GAMECENTER,
				global::Kampai.Game.SocialServices.GOOGLEPLAY,
				null,
				null,
				null
			};
		}
	}
}
