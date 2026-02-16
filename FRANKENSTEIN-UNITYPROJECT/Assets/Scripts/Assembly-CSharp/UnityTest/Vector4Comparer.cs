namespace UnityTest
{
	public class Vector4Comparer : global::UnityTest.VectorComparerBase<global::UnityEngine.Vector4>
	{
		public enum CompareType
		{
			MagnitudeEquals = 0,
			MagnitudeNotEquals = 1
		}

		public global::UnityTest.Vector4Comparer.CompareType compareType;

		public double floatingPointError;

		protected override bool Compare(global::UnityEngine.Vector4 a, global::UnityEngine.Vector4 b)
		{
			switch (compareType)
			{
			case global::UnityTest.Vector4Comparer.CompareType.MagnitudeEquals:
				return AreVectorMagnitudeEqual(a.magnitude, b.magnitude, floatingPointError);
			case global::UnityTest.Vector4Comparer.CompareType.MagnitudeNotEquals:
				return !AreVectorMagnitudeEqual(a.magnitude, b.magnitude, floatingPointError);
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
