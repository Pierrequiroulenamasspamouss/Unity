namespace __preref_Kampai_Game_RandomFlyOverCommand
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.RandomFlyOverCommand();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[13]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(int), delegate(object target, object val)
				{
					((global::Kampai.Game.RandomFlyOverCommand)target).index = (int)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.IRandomService), delegate(object target, object val)
				{
					((global::Kampai.Game.RandomFlyOverCommand)target).randomService = (global::Kampai.Common.IRandomService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IDefinitionService), delegate(object target, object val)
				{
					((global::Kampai.Game.RandomFlyOverCommand)target).definitionService = (global::Kampai.Game.IDefinitionService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerService), delegate(object target, object val)
				{
					((global::Kampai.Game.RandomFlyOverCommand)target).playerService = (global::Kampai.Game.IPlayerService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CameraModel), delegate(object target, object val)
				{
					((global::Kampai.Game.RandomFlyOverCommand)target).model = (global::Kampai.Game.CameraModel)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.PickControllerModel), delegate(object target, object val)
				{
					((global::Kampai.Game.RandomFlyOverCommand)target).pickControllerModel = (global::Kampai.Common.PickControllerModel)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.HideAllWayFindersSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.RandomFlyOverCommand)target).hideAllWayFinderSignal = (global::Kampai.UI.View.HideAllWayFindersSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.ShowAllWayFindersSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.RandomFlyOverCommand)target).showAllWayFinderSignal = (global::Kampai.UI.View.ShowAllWayFindersSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.ShowAllResourceIconsSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.RandomFlyOverCommand)target).showAllResourceIconsSignal = (global::Kampai.UI.View.ShowAllResourceIconsSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.HideAllResourceIconsSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.RandomFlyOverCommand)target).hideAllResourceIconsSignal = (global::Kampai.UI.View.HideAllResourceIconsSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.Camera), delegate(object target, object val)
				{
					((global::Kampai.Game.RandomFlyOverCommand)target).camera = (global::UnityEngine.Camera)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.command.api.ICommandBinder), delegate(object target, object val)
				{
					((global::Kampai.Game.RandomFlyOverCommand)target).commandBinder = (global::strange.extensions.command.api.ICommandBinder)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.injector.api.IInjectionBinder), delegate(object target, object val)
				{
					((global::Kampai.Game.RandomFlyOverCommand)target).injectionBinder = (global::strange.extensions.injector.api.IInjectionBinder)val;
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
				null,
				null,
				null,
				null,
				global::Kampai.Main.MainElement.CAMERA,
				null,
				null
			};
		}
	}
}
