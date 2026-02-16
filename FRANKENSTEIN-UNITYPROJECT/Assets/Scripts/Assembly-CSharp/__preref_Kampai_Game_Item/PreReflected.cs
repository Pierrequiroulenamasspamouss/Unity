namespace __preref_Kampai_Game_Item
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.Item((global::Kampai.Game.ItemDefinition)p[0]);
			ConstructorParameters = new global::System.Type[1] { typeof(global::Kampai.Game.ItemDefinition) };
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = KampaiPreprocessedModule.EmptySettersArray;
			SetterNames = KampaiPreprocessedModule.EmptyObjectsArray;
		}
	}
}
