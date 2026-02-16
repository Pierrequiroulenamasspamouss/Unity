namespace UnityTest
{
	public class ColliderComparer : global::UnityTest.ComparerBaseGeneric<global::UnityEngine.Bounds>
	{
		public enum CompareType
		{
			Intersects = 0,
			DoesNotIntersect = 1
		}

		public global::UnityTest.ColliderComparer.CompareType compareType;

		protected override bool Compare(global::UnityEngine.Bounds a, global::UnityEngine.Bounds b)
		{
			switch (compareType)
			{
			case global::UnityTest.ColliderComparer.CompareType.Intersects:
				return a.Intersects(b);
			case global::UnityTest.ColliderComparer.CompareType.DoesNotIntersect:
				return !a.Intersects(b);
			default:
				throw new global::System.Exception();
			}
		}
	}
}
