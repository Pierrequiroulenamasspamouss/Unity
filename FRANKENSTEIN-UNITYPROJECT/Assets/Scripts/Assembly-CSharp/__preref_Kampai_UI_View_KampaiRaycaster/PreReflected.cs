namespace __preref_Kampai_UI_View_KampaiRaycaster
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.UI.View.KampaiRaycaster();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[1]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.DisablePickControllerSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.KampaiRaycaster)target).signal = (global::Kampai.UI.View.DisablePickControllerSignal)val;
				})
			};
			SetterNames = new object[1];
		}
	}
}
