namespace __preref_Kampai_Game_View_VillainManagerMediator
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.View.VillainManagerMediator();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = new global::System.Action<object>[1]
			{
				delegate(object t)
				{
					((global::Kampai.Game.View.VillainManagerMediator)t).Init();
				}
			};
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[13]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.View.VillainManagerView), delegate(object target, object val)
				{
					((global::Kampai.Game.View.VillainManagerMediator)target).view = (global::Kampai.Game.View.VillainManagerView)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.VillainPlayWelcomeSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.VillainManagerMediator)target).welcomeSignal = (global::Kampai.Game.VillainPlayWelcomeSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.VillainGotoCarpetSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.VillainManagerMediator)target).carpetSignal = (global::Kampai.Game.VillainGotoCarpetSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.VillainGotoCabanaSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.VillainManagerMediator)target).cabanaSignal = (global::Kampai.Game.VillainGotoCabanaSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.VillainGotoBoatSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.VillainManagerMediator)target).boatSignal = (global::Kampai.Game.VillainGotoBoatSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CreateVillainViewSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.VillainManagerMediator)target).createViewSignal = (global::Kampai.Game.CreateVillainViewSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Game.View.VillainManagerMediator)target).buildingManager = (global::UnityEngine.GameObject)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CruiseShipModel), delegate(object target, object val)
				{
					((global::Kampai.Game.View.VillainManagerMediator)target).shipModel = (global::Kampai.Game.CruiseShipModel)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerService), delegate(object target, object val)
				{
					((global::Kampai.Game.View.VillainManagerMediator)target).playerService = (global::Kampai.Game.IPlayerService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.INamedCharacterBuilder), delegate(object target, object val)
				{
					((global::Kampai.Game.View.VillainManagerMediator)target).builder = (global::Kampai.Util.INamedCharacterBuilder)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.VillainAttachToShipSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.VillainManagerMediator)target).attachToShipSignal = (global::Kampai.Game.VillainAttachToShipSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.dispatcher.eventdispatcher.api.IEventDispatcher), delegate(object target, object val)
				{
					((global::Kampai.Game.View.VillainManagerMediator)target).dispatcher = (global::strange.extensions.dispatcher.eventdispatcher.api.IEventDispatcher)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Game.View.VillainManagerMediator)target).contextView = (global::UnityEngine.GameObject)val;
				})
			};
			SetterNames = new object[13]
			{
				null,
				null,
				null,
				null,
				null,
				null,
				global::Kampai.Game.GameElement.BUILDING_MANAGER,
				null,
				null,
				null,
				null,
				global::strange.extensions.context.api.ContextKeys.CONTEXT_DISPATCHER,
				global::strange.extensions.context.api.ContextKeys.CONTEXT_VIEW
			};
		}
	}
}
