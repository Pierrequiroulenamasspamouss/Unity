namespace __preref_Kampai_Game_CameraAutoMoveToInstanceCommand
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.CameraAutoMoveToInstanceCommand();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[10]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.PanInstructions), delegate(object target, object val)
				{
					((global::Kampai.Game.CameraAutoMoveToInstanceCommand)target).panInstructions = (global::Kampai.Game.PanInstructions)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Game.CameraAutoMoveToInstanceCommand)target).villainManager = (global::UnityEngine.GameObject)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Game.CameraAutoMoveToInstanceCommand)target).minionManager = (global::UnityEngine.GameObject)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.Game.CameraAutoMoveToInstanceCommand)target).logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerService), delegate(object target, object val)
				{
					((global::Kampai.Game.CameraAutoMoveToInstanceCommand)target).playerService = (global::Kampai.Game.IPlayerService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CameraAutoMoveSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.CameraAutoMoveToInstanceCommand)target).autoMoveSignal = (global::Kampai.Game.CameraAutoMoveSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CameraInstanceFocusSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.CameraAutoMoveToInstanceCommand)target).buildingFocusSignal = (global::Kampai.Game.CameraInstanceFocusSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CameraAutoMoveToBuildingSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.CameraAutoMoveToInstanceCommand)target).buildingMoveSignal = (global::Kampai.Game.CameraAutoMoveToBuildingSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.command.api.ICommandBinder), delegate(object target, object val)
				{
					((global::Kampai.Game.CameraAutoMoveToInstanceCommand)target).commandBinder = (global::strange.extensions.command.api.ICommandBinder)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.injector.api.IInjectionBinder), delegate(object target, object val)
				{
					((global::Kampai.Game.CameraAutoMoveToInstanceCommand)target).injectionBinder = (global::strange.extensions.injector.api.IInjectionBinder)val;
				})
			};
			SetterNames = new object[10]
			{
				null,
				global::Kampai.Game.GameElement.VILLAIN_MANAGER,
				global::Kampai.Game.GameElement.MINION_MANAGER,
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
