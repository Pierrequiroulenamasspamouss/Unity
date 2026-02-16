namespace __preref_Kampai_Game_MignetteBuilding
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.MignetteBuilding((global::Kampai.Game.MignetteBuildingDefinition)p[0]);
			ConstructorParameters = new global::System.Type[1] { typeof(global::Kampai.Game.MignetteBuildingDefinition) };
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = KampaiPreprocessedModule.EmptySettersArray;
			SetterNames = KampaiPreprocessedModule.EmptyObjectsArray;
		}
	}
}
