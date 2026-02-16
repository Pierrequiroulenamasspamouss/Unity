namespace __preref_Kampai_Game_View_TeleportAction
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.View.TeleportAction((global::Kampai.Game.View.ActionableObject)p[0], (global::UnityEngine.Vector3)p[1], (global::UnityEngine.Vector3)p[2], (global::Kampai.Util.ILogger)p[3]);
			ConstructorParameters = new global::System.Type[4]
			{
				typeof(global::Kampai.Game.View.ActionableObject),
				typeof(global::UnityEngine.Vector3),
				typeof(global::UnityEngine.Vector3),
				typeof(global::Kampai.Util.ILogger)
			};
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = KampaiPreprocessedModule.EmptySettersArray;
			SetterNames = KampaiPreprocessedModule.EmptyObjectsArray;
		}
	}
}
