namespace __preref_Kampai_Game_View_CharacterIntroCompleteAction
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.View.CharacterIntroCompleteAction((global::Kampai.Game.View.CharacterObject)p[0], (int)p[1], (global::UnityEngine.RuntimeAnimatorController)p[2], (global::Kampai.Game.CharacterIntroCompleteSignal)p[3], (global::Kampai.Util.ILogger)p[4]);
			ConstructorParameters = new global::System.Type[5]
			{
				typeof(global::Kampai.Game.View.CharacterObject),
				typeof(int),
				typeof(global::UnityEngine.RuntimeAnimatorController),
				typeof(global::Kampai.Game.CharacterIntroCompleteSignal),
				typeof(global::Kampai.Util.ILogger)
			};
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = KampaiPreprocessedModule.EmptySettersArray;
			SetterNames = KampaiPreprocessedModule.EmptyObjectsArray;
		}
	}
}
