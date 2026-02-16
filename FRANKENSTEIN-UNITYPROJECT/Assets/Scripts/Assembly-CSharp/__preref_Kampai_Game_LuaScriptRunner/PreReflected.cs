namespace __preref_Kampai_Game_LuaScriptRunner
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.LuaScriptRunner((global::Kampai.Game.LuaKernel)p[0], (global::Kampai.Util.ILogger)p[1]);
			ConstructorParameters = new global::System.Type[2]
			{
				typeof(global::Kampai.Game.LuaKernel),
				typeof(global::Kampai.Util.ILogger)
			};
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[2]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.QuestScriptController), delegate(object target, object val)
				{
					((global::Kampai.Game.LuaScriptRunner)target).controller = (global::Kampai.Game.QuestScriptController)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.QuestScriptKernel), delegate(object target, object val)
				{
					((global::Kampai.Game.LuaScriptRunner)target).qsKernel = (global::Kampai.Game.QuestScriptKernel)val;
				})
			};
			SetterNames = new object[2];
		}
	}
}
