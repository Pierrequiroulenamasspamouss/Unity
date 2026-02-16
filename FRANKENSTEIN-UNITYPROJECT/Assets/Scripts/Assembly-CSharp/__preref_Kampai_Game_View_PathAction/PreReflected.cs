namespace __preref_Kampai_Game_View_PathAction
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.View.PathAction((global::Kampai.Game.View.ActionableObject)p[0], (global::System.Collections.Generic.IList<global::UnityEngine.Vector3>)p[1], (float)p[2], (global::Kampai.Util.ILogger)p[3]);
			ConstructorParameters = new global::System.Type[4]
			{
				typeof(global::Kampai.Game.View.ActionableObject),
				typeof(global::System.Collections.Generic.IList<global::UnityEngine.Vector3>),
				typeof(float),
				typeof(global::Kampai.Util.ILogger)
			};
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = KampaiPreprocessedModule.EmptySettersArray;
			SetterNames = KampaiPreprocessedModule.EmptyObjectsArray;
		}
	}
}
