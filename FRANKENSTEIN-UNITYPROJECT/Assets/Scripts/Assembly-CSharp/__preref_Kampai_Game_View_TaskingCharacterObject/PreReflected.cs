namespace __preref_Kampai_Game_View_TaskingCharacterObject
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.View.TaskingCharacterObject((global::Kampai.Game.View.CharacterObject)p[0], (int)p[1]);
			ConstructorParameters = new global::System.Type[2]
			{
				typeof(global::Kampai.Game.View.CharacterObject),
				typeof(int)
			};
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = KampaiPreprocessedModule.EmptySettersArray;
			SetterNames = KampaiPreprocessedModule.EmptyObjectsArray;
		}
	}
}
