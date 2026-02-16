namespace UnityTest
{
	public class FloatComparer : global::UnityTest.ComparerBaseGeneric<float>
	{
		public enum CompareTypes
		{
			Equal = 0,
			NotEqual = 1,
			Greater = 2,
			Less = 3
		}

		public global::UnityTest.FloatComparer.CompareTypes compareTypes;

		public double floatingPointError = 9.999999747378752E-05;

		protected override bool Compare(float a, float b)
		{
			switch (compareTypes)
			{
			case global::UnityTest.FloatComparer.CompareTypes.Equal:
				return (double)global::System.Math.Abs(a - b) < floatingPointError;
			case global::UnityTest.FloatComparer.CompareTypes.NotEqual:
				return (double)global::System.Math.Abs(a - b) > floatingPointError;
			case global::UnityTest.FloatComparer.CompareTypes.Greater:
				return a > b;
			case global::UnityTest.FloatComparer.CompareTypes.Less:
				return a < b;
			default:
				throw new global::System.Exception();
			}
		}

		public override int GetDepthOfSearch()
		{
			return 3;
		}
	}
}
