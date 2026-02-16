namespace UnityTest
{
	public class IntComparer : global::UnityTest.ComparerBaseGeneric<int>
	{
		public enum CompareType
		{
			Equal = 0,
			NotEqual = 1,
			Greater = 2,
			GreaterOrEqual = 3,
			Less = 4,
			LessOrEqual = 5
		}

		public global::UnityTest.IntComparer.CompareType compareType;

		protected override bool Compare(int a, int b)
		{
			switch (compareType)
			{
			case global::UnityTest.IntComparer.CompareType.Equal:
				return a == b;
			case global::UnityTest.IntComparer.CompareType.NotEqual:
				return a != b;
			case global::UnityTest.IntComparer.CompareType.Greater:
				return a > b;
			case global::UnityTest.IntComparer.CompareType.GreaterOrEqual:
				return a >= b;
			case global::UnityTest.IntComparer.CompareType.Less:
				return a < b;
			case global::UnityTest.IntComparer.CompareType.LessOrEqual:
				return a <= b;
			default:
				throw new global::System.Exception();
			}
		}
	}
}
