namespace __preref_Kampai_Game_Player
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.Player((global::Kampai.Game.IDefinitionService)p[0], (global::Kampai.Util.ILogger)p[1]);
			ConstructorParameters = new global::System.Type[2]
			{
				typeof(global::Kampai.Game.IDefinitionService),
				typeof(global::Kampai.Util.ILogger)
			};
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = KampaiPreprocessedModule.EmptySettersArray;
			SetterNames = KampaiPreprocessedModule.EmptyObjectsArray;
		}
	}
}
