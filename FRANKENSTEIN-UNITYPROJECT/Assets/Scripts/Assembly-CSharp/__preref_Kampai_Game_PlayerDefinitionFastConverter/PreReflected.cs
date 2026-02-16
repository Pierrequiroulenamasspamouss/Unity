namespace __preref_Kampai_Game_PlayerDefinitionFastConverter
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.PlayerDefinitionFastConverter((global::Kampai.Game.DefinitionService)p[0]);
			ConstructorParameters = new global::System.Type[1] { typeof(global::Kampai.Game.DefinitionService) };
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = KampaiPreprocessedModule.EmptySettersArray;
			SetterNames = KampaiPreprocessedModule.EmptyObjectsArray;
		}
	}
}
