namespace UnityTest
{
	public class Vector2Comparer : global::UnityTest.VectorComparerBase<global::UnityEngine.Vector2>
	{
		public enum CompareType
		{
			MagnitudeEquals = 0,
			MagnitudeNotEquals = 1
		}

		public global::UnityTest.Vector2Comparer.CompareType compareType;

		public float floatingPointError = 0.0001f;

		protected override bool Compare(global::UnityEngine.Vector2 a, global::UnityEngine.Vector2 b)
		{
			switch (compareType)
			{
			case global::UnityTest.Vector2Comparer.CompareType.MagnitudeEquals:
				return AreVectorMagnitudeEqual(a.magnitude, b.magnitude, floatingPointError);
			case global::UnityTest.Vector2Comparer.CompareType.MagnitudeNotEquals:
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
