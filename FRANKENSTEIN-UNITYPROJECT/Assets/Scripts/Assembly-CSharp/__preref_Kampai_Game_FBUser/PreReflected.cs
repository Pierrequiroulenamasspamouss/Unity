namespace __preref_Kampai_Game_FBUser
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.FBUser((string)p[0], (string)p[1], (string)p[2]);
			ConstructorParameters = new global::System.Type[3]
			{
				typeof(string),
				typeof(string),
				typeof(string)
			};
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[1]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.Game.FBUser)target).logger = (global::Kampai.Util.ILogger)val;
				})
			};
			SetterNames = new object[1];
		}
	}
}
