namespace __preref_AppTrackerView
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new AppTrackerView();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[5]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.AppPauseSignal), delegate(object target, object val)
				{
					((AppTrackerView)target).pauseSignal = (global::Kampai.Common.AppPauseSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.AppResumeSignal), delegate(object target, object val)
				{
					((AppTrackerView)target).resumeSignal = (global::Kampai.Common.AppResumeSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.AppQuitSignal), delegate(object target, object val)
				{
					((AppTrackerView)target).quitSignal = (global::Kampai.Common.AppQuitSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.AppFocusGainedSignal), delegate(object target, object val)
				{
					((AppTrackerView)target).focusGainedSignal = (global::Kampai.Common.AppFocusGainedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((AppTrackerView)target).logger = (global::Kampai.Util.ILogger)val;
				})
			};
			SetterNames = new object[5];
		}
	}
}
