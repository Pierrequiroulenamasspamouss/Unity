namespace UnityTest
{
	public abstract class ActionBaseGeneric<T> : global::UnityTest.ActionBase
	{
		protected override bool UseCache
		{
			get
			{
				return true;
			}
		}

		protected override bool Compare(object objVal)
		{
			return Compare((T)objVal);
		}

		protected abstract bool Compare(T objVal);

		public override global::System.Type[] GetAccepatbleTypesForA()
		{
			return new global::System.Type[1] { typeof(T) };
		}

		public override global::System.Type GetParameterType()
		{
			return typeof(T);
		}
	}
}
