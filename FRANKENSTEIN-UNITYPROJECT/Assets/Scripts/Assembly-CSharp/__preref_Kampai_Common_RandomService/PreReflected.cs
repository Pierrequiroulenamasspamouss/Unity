namespace __preref_Kampai_Common_RandomService
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Common.RandomService((long)p[0]);
			ConstructorParameters = new global::System.Type[1] { typeof(long) };
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[1]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.Common.RandomService)target).logger = (global::Kampai.Util.ILogger)val;
				})
			};
			SetterNames = new object[1];
		}
	}
}
