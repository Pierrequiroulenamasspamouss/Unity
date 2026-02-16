namespace __preref_Kampai_Game_CruiseShipService
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.CruiseShipService();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[4]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CruiseShipModel), delegate(object target, object val)
				{
					((global::Kampai.Game.CruiseShipService)target).model = (global::Kampai.Game.CruiseShipModel)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.Game.CruiseShipService)target).logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.VillainGotoBoatSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.CruiseShipService)target).gotoBoatSignal = (global::Kampai.Game.VillainGotoBoatSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.VillainGotoCarpetSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.CruiseShipService)target).gotoCarpetSignal = (global::Kampai.Game.VillainGotoCarpetSignal)val;
				})
			};
			SetterNames = new object[4];
		}
	}
}
