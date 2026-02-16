namespace __preref_Kampai_Download_BackgroundDownloadDlcService
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Download.BackgroundDownloadDlcService();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[4]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.Download.BackgroundDownloadDlcService)target).logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Download.DLCModel), delegate(object target, object val)
				{
					((global::Kampai.Download.BackgroundDownloadDlcService)target).dlcModel = (global::Kampai.Download.DLCModel)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.IManifestService), delegate(object target, object val)
				{
					((global::Kampai.Download.BackgroundDownloadDlcService)target).manifestService = (global::Kampai.Common.IManifestService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IConfigurationsService), delegate(object target, object val)
				{
					((global::Kampai.Download.BackgroundDownloadDlcService)target).configurationsService = (global::Kampai.Game.IConfigurationsService)val;
				})
			};
			SetterNames = new object[4];
		}
	}
}
