namespace UnityTest
{
	public class Vector3Comparer : global::UnityTest.VectorComparerBase<global::UnityEngine.Vector3>
	{
		public enum CompareType
		{
			MagnitudeEquals = 0,
			MagnitudeNotEquals = 1
		}

		public global::UnityTest.Vector3Comparer.CompareType compareType;

		public double floatingPointError = 9.999999747378752E-05;

		protected override bool Compare(global::UnityEngine.Vector3 a, global::UnityEngine.Vector3 b)
		{
			switch (compareType)
			{
			case global::UnityTest.Vector3Comparer.CompareType.MagnitudeEquals:
				return AreVectorMagnitudeEqual(a.magnitude, b.magnitude, floatingPointError);
			case global::UnityTest.Vector3Comparer.CompareType.MagnitudeNotEquals:
				return !AreVectorMagnitudeEqual(a.magnitude, b.magnitude, floatingPointError);
			default:
				throw new global::System.Exception();
			}
		}
	}
}
