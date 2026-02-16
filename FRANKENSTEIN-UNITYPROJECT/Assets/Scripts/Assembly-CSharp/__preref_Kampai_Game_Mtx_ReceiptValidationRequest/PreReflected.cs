namespace __preref_Kampai_Game_Mtx_ReceiptValidationRequest
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.Mtx.ReceiptValidationRequest((string)p[0], (string)p[1], (global::Kampai.Game.Mtx.IMtxReceipt)p[2]);
			ConstructorParameters = new global::System.Type[3]
			{
				typeof(string),
				typeof(string),
				typeof(global::Kampai.Game.Mtx.IMtxReceipt)
			};
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = KampaiPreprocessedModule.EmptySettersArray;
			SetterNames = KampaiPreprocessedModule.EmptyObjectsArray;
		}
	}
}
