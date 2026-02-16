namespace __preref_Kampai_Game_Sticker
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.Sticker((global::Kampai.Game.StickerDefinition)p[0]);
			ConstructorParameters = new global::System.Type[1] { typeof(global::Kampai.Game.StickerDefinition) };
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = KampaiPreprocessedModule.EmptySettersArray;
			SetterNames = KampaiPreprocessedModule.EmptyObjectsArray;
		}
	}
}
