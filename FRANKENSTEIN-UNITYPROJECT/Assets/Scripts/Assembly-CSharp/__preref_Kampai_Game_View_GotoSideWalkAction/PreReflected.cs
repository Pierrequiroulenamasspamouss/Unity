namespace __preref_Kampai_Game_View_GotoSideWalkAction
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.View.GotoSideWalkAction((global::Kampai.Game.View.CharacterObject)p[0], (global::Kampai.Game.Building)p[1], (global::Kampai.Util.ILogger)p[2], (global::Kampai.Game.IDefinitionService)p[3], (global::strange.extensions.signal.impl.Signal<global::Kampai.Game.View.CharacterObject>)p[4], (int)p[5]);
			ConstructorParameters = new global::System.Type[6]
			{
				typeof(global::Kampai.Game.View.CharacterObject),
				typeof(global::Kampai.Game.Building),
				typeof(global::Kampai.Util.ILogger),
				typeof(global::Kampai.Game.IDefinitionService),
				typeof(global::strange.extensions.signal.impl.Signal<global::Kampai.Game.View.CharacterObject>),
				typeof(int)
			};
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = KampaiPreprocessedModule.EmptySettersArray;
			SetterNames = KampaiPreprocessedModule.EmptyObjectsArray;
		}
	}
}
