namespace UnityTest
{
	[global::System.Serializable]
	public abstract class ComparerBaseGeneric<T> : global::UnityTest.ComparerBaseGeneric<T, T>
	{
	}
	[global::System.Serializable]
	public abstract class ComparerBaseGeneric<T1, T2> : global::UnityTest.ComparerBase
	{
		public T2 constantValueGeneric = default(T2);

		public override object ConstValue
		{
			get
			{
				return constantValueGeneric;
			}
			set
			{
				constantValueGeneric = (T2)value;
			}
		}

		protected override bool UseCache
		{
			get
			{
				return true;
			}
		}

		public override object GetDefaultConstValue()
		{
			return default(T2);
		}

		private static bool IsValueType(global::System.Type type)
		{
			return type.IsValueType;
		}

		protected override bool Compare(object a, object b)
		{
			global::System.Type typeFromHandle = typeof(T2);
			if (b == null && IsValueType(typeFromHandle))
			{
				throw new global::System.ArgumentException("Null was passed to a value-type argument");
			}
			return Compare((T1)a, (T2)b);
		}

		protected abstract bool Compare(T1 a, T2 b);

		public override global::System.Type[] GetAccepatbleTypesForA()
		{
			return new global::System.Type[1] { typeof(T1) };
		}

		public override global::System.Type[] GetAccepatbleTypesForB()
		{
			return new global::System.Type[1] { typeof(T2) };
		}
	}
}
