namespace __preref_Kampai_Game_View_SetCullingModeAction
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.View.SetCullingModeAction((global::Kampai.Game.View.Animatable)p[0], (global::UnityEngine.AnimatorCullingMode)(int)p[1], (global::Kampai.Util.ILogger)p[2]);
			ConstructorParameters = new global::System.Type[3]
			{
				typeof(global::Kampai.Game.View.Animatable),
				typeof(global::UnityEngine.AnimatorCullingMode),
				typeof(global::Kampai.Util.ILogger)
			};
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = KampaiPreprocessedModule.EmptySettersArray;
			SetterNames = KampaiPreprocessedModule.EmptyObjectsArray;
		}
	}
}
