namespace UnityTest
{
	public class GeneralComparer : global::UnityTest.ComparerBase
	{
		public enum CompareType
		{
			AEqualsB = 0,
			ANotEqualsB = 1
		}

		public global::UnityTest.GeneralComparer.CompareType compareType;

		protected override bool Compare(object a, object b)
		{
			if (compareType == global::UnityTest.GeneralComparer.CompareType.AEqualsB)
			{
				return a.Equals(b);
			}
			if (compareType == global::UnityTest.GeneralComparer.CompareType.ANotEqualsB)
			{
				return !a.Equals(b);
			}
			throw new global::System.Exception();
		}
	}
}
