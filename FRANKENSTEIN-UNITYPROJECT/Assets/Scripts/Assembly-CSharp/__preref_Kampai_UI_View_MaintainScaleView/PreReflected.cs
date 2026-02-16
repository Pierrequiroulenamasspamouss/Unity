namespace __preref_Kampai_UI_View_MaintainScaleView
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.UI.View.MaintainScaleView();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[2]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.ZoomPercentageSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MaintainScaleView)target).zoomSignal = (global::Kampai.Common.ZoomPercentageSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.RequestZoomPercentageSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.MaintainScaleView)target).requestZoomPercentage = (global::Kampai.Common.RequestZoomPercentageSignal)val;
				})
			};
			SetterNames = new object[2];
		}
	}
}
