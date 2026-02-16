namespace UnityTest
{
	public class TransformComparer : global::UnityTest.ComparerBaseGeneric<global::UnityEngine.Transform>
	{
		public enum CompareType
		{
			Equals = 0,
			NotEquals = 1
		}

		public global::UnityTest.TransformComparer.CompareType compareType;

		protected override bool Compare(global::UnityEngine.Transform a, global::UnityEngine.Transform b)
		{
			if (compareType == global::UnityTest.TransformComparer.CompareType.Equals)
			{
				return a.position == b.position;
			}
			if (compareType == global::UnityTest.TransformComparer.CompareType.NotEquals)
			{
				return a.position != b.position;
			}
			throw new global::System.Exception();
		}
	}
}
