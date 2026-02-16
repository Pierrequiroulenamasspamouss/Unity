namespace __preref_Kampai_Game_View_BobMediator
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.View.BobMediator();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[12]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.View.BobView), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BobMediator)target).view = (global::Kampai.Game.View.BobView)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.BobPointsAtSignSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BobMediator)target).pointAtSignSignal = (global::Kampai.Game.BobPointsAtSignSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.BobIdleInTownHallSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BobMediator)target).bobIdleInTownHallSignal = (global::Kampai.Game.BobIdleInTownHallSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CharacterArrivedAtDestinationSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BobMediator)target).arrivedAtDestinationSignal = (global::Kampai.Game.CharacterArrivedAtDestinationSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.BobCelebrateLandExpansionSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BobMediator)target).celebrateLandExpansionSignal = (global::Kampai.Game.BobCelebrateLandExpansionSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.BobCelebrateLandExpansionCompleteSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BobMediator)target).celebrateLandExpansionCompleteSignal = (global::Kampai.Game.BobCelebrateLandExpansionCompleteSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.BobReturnToTownSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BobMediator)target).bobReturnToTown = (global::Kampai.Game.BobReturnToTownSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.PointBobLandExpansionSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BobMediator)target).pointBobLandExpansionSignal = (global::Kampai.Game.PointBobLandExpansionSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IDefinitionService), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BobMediator)target).definitionService = (global::Kampai.Game.IDefinitionService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerService), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BobMediator)target).playerService = (global::Kampai.Game.IPlayerService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.BobFrolicsSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BobMediator)target).bobFrolicsSignal = (global::Kampai.Game.BobFrolicsSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Game.View.BobMediator)target).contextView = (global::UnityEngine.GameObject)val;
				})
			};
			SetterNames = new object[12]
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
				null,
				global::strange.extensions.context.api.ContextKeys.CONTEXT_VIEW
			};
		}
	}
}
