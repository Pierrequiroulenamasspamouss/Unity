namespace __preref_Kampai_Game_CameraMovementSettings
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.CameraMovementSettings((global::Kampai.Game.CameraMovementSettings.Settings)(int)p[0], (global::Kampai.Game.Building)p[1], (global::Kampai.Game.Quest)p[2]);
			ConstructorParameters = new global::System.Type[3]
			{
				typeof(global::Kampai.Game.CameraMovementSettings.Settings),
				typeof(global::Kampai.Game.Building),
				typeof(global::Kampai.Game.Quest)
			};
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = KampaiPreprocessedModule.EmptySettersArray;
			SetterNames = KampaiPreprocessedModule.EmptyObjectsArray;
		}
	}
}
