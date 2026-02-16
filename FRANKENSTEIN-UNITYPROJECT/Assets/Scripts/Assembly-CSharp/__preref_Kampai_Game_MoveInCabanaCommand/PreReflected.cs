namespace __preref_Kampai_Game_MoveInCabanaCommand
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.MoveInCabanaCommand();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[17]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.Prestige), delegate(object target, object val)
				{
					((global::Kampai.Game.MoveInCabanaCommand)target).prestige = (global::Kampai.Game.Prestige)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.Game.MoveInCabanaCommand)target).logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerService), delegate(object target, object val)
				{
					((global::Kampai.Game.MoveInCabanaCommand)target).playerService = (global::Kampai.Game.IPlayerService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.INamedCharacterBuilder), delegate(object target, object val)
				{
					((global::Kampai.Game.MoveInCabanaCommand)target).builder = (global::Kampai.Util.INamedCharacterBuilder)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Game.MoveInCabanaCommand)target).manager = (global::UnityEngine.GameObject)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.PromptReceivedSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.MoveInCabanaCommand)target).receivedSignal = (global::Kampai.UI.View.PromptReceivedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CruiseShipService), delegate(object target, object val)
				{
					((global::Kampai.Game.MoveInCabanaCommand)target).shipService = (global::Kampai.Game.CruiseShipService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CameraAutoMoveToInstanceSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.MoveInCabanaCommand)target).cameraMoveSignal = (global::Kampai.Game.CameraAutoMoveToInstanceSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.KevinGreetVillainSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.MoveInCabanaCommand)target).kevinGreetSignal = (global::Kampai.Game.KevinGreetVillainSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.VillainPlayWelcomeSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.MoveInCabanaCommand)target).villainPlayWelcomeSignal = (global::Kampai.Game.VillainPlayWelcomeSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.VillainGotoCabanaSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.MoveInCabanaCommand)target).villainGotoCabanaSignal = (global::Kampai.Game.VillainGotoCabanaSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ShowDialogSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.MoveInCabanaCommand)target).showDialogSignal = (global::Kampai.Game.ShowDialogSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.BuildingChangeStateSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.MoveInCabanaCommand)target).buildingStateSignal = (global::Kampai.Game.BuildingChangeStateSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.RecreateBuildingSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.MoveInCabanaCommand)target).recreateSignal = (global::Kampai.Common.RecreateBuildingSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPrestigeService), delegate(object target, object val)
				{
					((global::Kampai.Game.MoveInCabanaCommand)target).prestigeService = (global::Kampai.Game.IPrestigeService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.command.api.ICommandBinder), delegate(object target, object val)
				{
					((global::Kampai.Game.MoveInCabanaCommand)target).commandBinder = (global::strange.extensions.command.api.ICommandBinder)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.injector.api.IInjectionBinder), delegate(object target, object val)
				{
					((global::Kampai.Game.MoveInCabanaCommand)target).injectionBinder = (global::strange.extensions.injector.api.IInjectionBinder)val;
				})
			};
			SetterNames = new object[17]
			{
				null,
				null,
				null,
				null,
				global::Kampai.Game.GameElement.VILLAIN_MANAGER,
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
				null
			};
		}
	}
}
