namespace __preref_Kampai_Util_DebugButton
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Util.DebugButton();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[3]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.IGUIService), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugButton)target).guiService = (global::Kampai.UI.View.IGUIService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.HUDChangedSiblingIndexSignal), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugButton)target).hudChangedSiblingIndexSignal = (global::Kampai.UI.View.HUDChangedSiblingIndexSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.DebugKeyHitSignal), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugButton)target).openSignal = (global::Kampai.Game.DebugKeyHitSignal)val;
				})
			};
			SetterNames = new object[3];
		}
	}
}
