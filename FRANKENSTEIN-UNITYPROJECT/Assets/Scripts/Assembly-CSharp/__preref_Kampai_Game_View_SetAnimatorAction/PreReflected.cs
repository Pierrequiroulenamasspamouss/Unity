namespace __preref_Kampai_Game_View_SetAnimatorAction
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.View.SetAnimatorAction((global::Kampai.Game.View.ActionableObject)p[0], (global::UnityEngine.RuntimeAnimatorController)p[1], (global::Kampai.Util.ILogger)p[2], (global::System.Collections.Generic.Dictionary<string, object>)p[3]);
			ConstructorParameters = new global::System.Type[4]
			{
				typeof(global::Kampai.Game.View.ActionableObject),
				typeof(global::UnityEngine.RuntimeAnimatorController),
				typeof(global::Kampai.Util.ILogger),
				typeof(global::System.Collections.Generic.Dictionary<string, object>)
			};
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = KampaiPreprocessedModule.EmptySettersArray;
			SetterNames = KampaiPreprocessedModule.EmptyObjectsArray;
		}
	}
}
