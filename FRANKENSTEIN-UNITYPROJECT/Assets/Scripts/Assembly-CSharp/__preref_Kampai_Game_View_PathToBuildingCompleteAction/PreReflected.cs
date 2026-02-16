namespace __preref_Kampai_Game_View_PathToBuildingCompleteAction
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.View.PathToBuildingCompleteAction((global::Kampai.Game.View.CharacterObject)p[0], (int)p[1], (global::strange.extensions.signal.impl.Signal<global::Kampai.Game.View.CharacterObject, int>)p[2], (global::Kampai.Util.ILogger)p[3]);
			ConstructorParameters = new global::System.Type[4]
			{
				typeof(global::Kampai.Game.View.CharacterObject),
				typeof(int),
				typeof(global::strange.extensions.signal.impl.Signal<global::Kampai.Game.View.CharacterObject, int>),
				typeof(global::Kampai.Util.ILogger)
			};
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = KampaiPreprocessedModule.EmptySettersArray;
			SetterNames = KampaiPreprocessedModule.EmptyObjectsArray;
		}
	}
}
