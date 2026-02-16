namespace __preref_Kampai_Game_Mtx_MtxReceiptValidationService
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.Mtx.MtxReceiptValidationService();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = new global::System.Action<object>[1]
			{
				delegate(object t)
				{
					((global::Kampai.Game.Mtx.MtxReceiptValidationService)t).PostConstruct();
				}
			};
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[4]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.Game.Mtx.MtxReceiptValidationService)target).logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.StartMtxReceiptValidationSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mtx.MtxReceiptValidationService)target).startMtxReceiptValidationSignal = (global::Kampai.Game.StartMtxReceiptValidationSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.FinishMtxReceiptValidationSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mtx.MtxReceiptValidationService)target).finishMtxReceiptValidationSignal = (global::Kampai.Game.FinishMtxReceiptValidationSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(ILocalPersistanceService), delegate(object target, object val)
				{
					((global::Kampai.Game.Mtx.MtxReceiptValidationService)target).localPersistence = (ILocalPersistanceService)val;
				})
			};
			SetterNames = new object[4];
		}
	}
}
