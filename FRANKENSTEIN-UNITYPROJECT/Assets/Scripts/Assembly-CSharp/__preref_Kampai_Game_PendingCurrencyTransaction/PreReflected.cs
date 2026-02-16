namespace __preref_Kampai_Game_PendingCurrencyTransaction
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.PendingCurrencyTransaction((global::Kampai.Game.Transaction.TransactionDefinition)p[0], (bool)p[1], (int)p[2], (global::System.Collections.Generic.IList<global::Kampai.Util.QuantityItem>)p[3], (global::System.Collections.Generic.IList<global::Kampai.Game.Instance>)p[4], (global::System.Action<global::Kampai.Game.PendingCurrencyTransaction>)p[5], (global::Kampai.Game.TransactionTarget)(int)p[6]);
			ConstructorParameters = new global::System.Type[7]
			{
				typeof(global::Kampai.Game.Transaction.TransactionDefinition),
				typeof(bool),
				typeof(int),
				typeof(global::System.Collections.Generic.IList<global::Kampai.Util.QuantityItem>),
				typeof(global::System.Collections.Generic.IList<global::Kampai.Game.Instance>),
				typeof(global::System.Action<global::Kampai.Game.PendingCurrencyTransaction>),
				typeof(global::Kampai.Game.TransactionTarget)
			};
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = KampaiPreprocessedModule.EmptySettersArray;
			SetterNames = KampaiPreprocessedModule.EmptyObjectsArray;
		}
	}
}
