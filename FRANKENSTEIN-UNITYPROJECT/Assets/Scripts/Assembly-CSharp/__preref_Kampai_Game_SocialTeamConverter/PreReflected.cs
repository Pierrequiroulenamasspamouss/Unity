namespace __preref_Kampai_Game_SocialTeamConverter
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.SocialTeamConverter((global::Kampai.Game.IDefinitionService)p[0]);
			ConstructorParameters = new global::System.Type[1] { typeof(global::Kampai.Game.IDefinitionService) };
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = KampaiPreprocessedModule.EmptySettersArray;
			SetterNames = KampaiPreprocessedModule.EmptyObjectsArray;
		}
	}
}
