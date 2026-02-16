namespace __preref_Kampai_Game_TransactionEngine
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.TransactionEngine((global::Kampai.Util.ILogger)p[0], (global::Kampai.Game.IDefinitionService)p[1], (global::Kampai.Common.IRandomService)p[2]);
			ConstructorParameters = new global::System.Type[3]
			{
				typeof(global::Kampai.Util.ILogger),
				typeof(global::Kampai.Game.IDefinitionService),
				typeof(global::Kampai.Common.IRandomService)
			};
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = KampaiPreprocessedModule.EmptySettersArray;
			SetterNames = KampaiPreprocessedModule.EmptyObjectsArray;
		}
	}
}
