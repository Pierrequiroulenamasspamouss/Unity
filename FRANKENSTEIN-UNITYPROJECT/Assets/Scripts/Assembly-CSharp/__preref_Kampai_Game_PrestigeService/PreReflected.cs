namespace __preref_Kampai_Game_PrestigeService
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.PrestigeService();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = new global::System.Action<object>[1]
			{
				delegate(object t)
				{
					((global::Kampai.Game.PrestigeService)t).PostConstruct();
				}
			};
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[14]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IDefinitionService), delegate(object target, object val)
				{
					((global::Kampai.Game.PrestigeService)target).definitionService = (global::Kampai.Game.IDefinitionService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerService), delegate(object target, object val)
				{
					((global::Kampai.Game.PrestigeService)target).playerService = (global::Kampai.Game.IPlayerService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IQuestService), delegate(object target, object val)
				{
					((global::Kampai.Game.PrestigeService)target).questService = (global::Kampai.Game.IQuestService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ITimeService), delegate(object target, object val)
				{
					((global::Kampai.Game.PrestigeService)target).timeService = (global::Kampai.Game.ITimeService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IOrderBoardService), delegate(object target, object val)
				{
					((global::Kampai.Game.PrestigeService)target).orderBoardService = (global::Kampai.Game.IOrderBoardService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.Game.PrestigeService)target).logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.context.api.ICrossContextCapable), delegate(object target, object val)
				{
					((global::Kampai.Game.PrestigeService)target).gameContext = (global::strange.extensions.context.api.ICrossContextCapable)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.context.api.ICrossContextCapable), delegate(object target, object val)
				{
					((global::Kampai.Game.PrestigeService)target).uiContext = (global::strange.extensions.context.api.ICrossContextCapable)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Game.PrestigeService)target).namedCharacterManager = (global::UnityEngine.GameObject)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Game.PrestigeService)target).minionManager = (global::UnityEngine.GameObject)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Game.PrestigeService)target).buildingManager = (global::UnityEngine.GameObject)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.PathFinder), delegate(object target, object val)
				{
					((global::Kampai.Game.PrestigeService)target).pathFinder = (global::Kampai.Util.PathFinder)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.UpdateNamedCharacterPrestigeSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.PrestigeService)target).updateNamedCharacterSignal = (global::Kampai.Game.UpdateNamedCharacterPrestigeSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.UpdateVillainPrestigeSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.PrestigeService)target).updateVillainSignal = (global::Kampai.Game.UpdateVillainPrestigeSignal)val;
				})
			};
			SetterNames = new object[14]
			{
				null,
				null,
				null,
				null,
				null,
				null,
				global::Kampai.Game.GameElement.CONTEXT,
				global::Kampai.UI.View.UIElement.CONTEXT,
				global::Kampai.Game.GameElement.NAMED_CHARACTER_MANAGER,
				global::Kampai.Game.GameElement.MINION_MANAGER,
				global::Kampai.Game.GameElement.BUILDING_MANAGER,
				null,
				null,
				null
			};
		}
	}
}
