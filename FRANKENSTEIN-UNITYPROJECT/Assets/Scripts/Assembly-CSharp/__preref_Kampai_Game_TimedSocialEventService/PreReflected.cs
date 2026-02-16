namespace __preref_Kampai_Game_TimedSocialEventService
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.TimedSocialEventService();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[7]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.Game.TimedSocialEventService)target).logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IDefinitionService), delegate(object target, object val)
				{
					((global::Kampai.Game.TimedSocialEventService)target).definitionService = (global::Kampai.Game.IDefinitionService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ITimeService), delegate(object target, object val)
				{
					((global::Kampai.Game.TimedSocialEventService)target).timeService = (global::Kampai.Game.ITimeService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IUserSessionService), delegate(object target, object val)
				{
					((global::Kampai.Game.TimedSocialEventService)target).userSessionService = (global::Kampai.Game.IUserSessionService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerService), delegate(object target, object val)
				{
					((global::Kampai.Game.TimedSocialEventService)target).playerService = (global::Kampai.Game.IPlayerService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(string), delegate(object target, object val)
				{
					((global::Kampai.Game.TimedSocialEventService)target).ServerUrl = (string)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Download.IDownloadService), delegate(object target, object val)
				{
					((global::Kampai.Game.TimedSocialEventService)target).downloadService = (global::Kampai.Download.IDownloadService)val;
				})
			};
			SetterNames = new object[7] { null, null, null, null, null, "game.server.host", null };
		}
	}
}
