namespace __preref_Kampai_Game_View_CharacterDrinkingCompleteAction
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.View.CharacterDrinkingCompleteAction((global::Kampai.Game.View.CharacterObject)p[0], (global::Kampai.Game.CharacterDrinkingCompleteSignal)p[1], (global::Kampai.Util.ILogger)p[2]);
			ConstructorParameters = new global::System.Type[3]
			{
				typeof(global::Kampai.Game.View.CharacterObject),
				typeof(global::Kampai.Game.CharacterDrinkingCompleteSignal),
				typeof(global::Kampai.Util.ILogger)
			};
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = KampaiPreprocessedModule.EmptySettersArray;
			SetterNames = KampaiPreprocessedModule.EmptyObjectsArray;
		}
	}
}
