namespace __preref_Kampai_Game_View_CompositeBuildingMediator
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.View.CompositeBuildingMediator();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[6]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.View.CompositeBuildingView), delegate(object target, object val)
				{
					((global::Kampai.Game.View.CompositeBuildingMediator)target).view = (global::Kampai.Game.View.CompositeBuildingView)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerService), delegate(object target, object val)
				{
					((global::Kampai.Game.View.CompositeBuildingMediator)target).playerService = (global::Kampai.Game.IPlayerService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.Game.View.CompositeBuildingMediator)target).logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CompositeBuildingPieceAddedSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.CompositeBuildingMediator)target).compositeBuildingPieceAddedSignal = (global::Kampai.Game.CompositeBuildingPieceAddedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.MinionReactInRadiusSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.CompositeBuildingMediator)target).minionReactInRadiusSignal = (global::Kampai.Common.MinionReactInRadiusSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Game.View.CompositeBuildingMediator)target).contextView = (global::UnityEngine.GameObject)val;
				})
			};
			SetterNames = new object[6]
			{
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
