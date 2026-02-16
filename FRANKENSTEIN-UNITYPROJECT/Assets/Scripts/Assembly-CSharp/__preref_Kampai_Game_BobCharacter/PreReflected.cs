namespace __preref_Kampai_Game_BobCharacter
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.BobCharacter((global::Kampai.Game.BobCharacterDefinition)p[0]);
			ConstructorParameters = new global::System.Type[1] { typeof(global::Kampai.Game.BobCharacterDefinition) };
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = KampaiPreprocessedModule.EmptySettersArray;
			SetterNames = KampaiPreprocessedModule.EmptyObjectsArray;
		}
	}
}
