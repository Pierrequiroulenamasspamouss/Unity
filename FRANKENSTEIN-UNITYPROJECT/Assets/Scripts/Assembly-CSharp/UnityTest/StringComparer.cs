namespace UnityTest
{
	public class StringComparer : global::UnityTest.ComparerBaseGeneric<string>
	{
		public enum CompareType
		{
			Equal = 0,
			NotEqual = 1,
			Shorter = 2,
			Longer = 3
		}

		public global::UnityTest.StringComparer.CompareType compareType;

		public global::System.StringComparison comparisonType = global::System.StringComparison.Ordinal;

		public bool ignoreCase;

		protected override bool Compare(string a, string b)
		{
			if (ignoreCase)
			{
				a = a.ToLower();
				b = b.ToLower();
			}
			switch (compareType)
			{
			case global::UnityTest.StringComparer.CompareType.Equal:
				return string.Compare(a, b, comparisonType) == 0;
			case global::UnityTest.StringComparer.CompareType.NotEqual:
				return string.Compare(a, b, comparisonType) != 0;
			case global::UnityTest.StringComparer.CompareType.Longer:
				return string.Compare(a, b, comparisonType) > 0;
			case global::UnityTest.StringComparer.CompareType.Shorter:
				return string.Compare(a, b, comparisonType) < 0;
			default:
				throw new global::System.Exception();
			}
		}
	}
}
