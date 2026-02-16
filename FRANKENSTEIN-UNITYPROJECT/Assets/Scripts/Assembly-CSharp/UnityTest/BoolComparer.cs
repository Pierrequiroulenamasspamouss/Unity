namespace UnityTest
{
	public class BoolComparer : global::UnityTest.ComparerBaseGeneric<bool>
	{
		protected override bool Compare(bool a, bool b)
		{
			return a == b;
		}
	}
}
