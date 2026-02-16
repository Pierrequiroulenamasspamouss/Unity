namespace UnityTest
{
	public class ValueDoesNotChange : global::UnityTest.ActionBase
	{
		private object val;

		protected override bool Compare(object a)
		{
			if (val == null)
			{
				val = a;
			}
			if (!val.Equals(a))
			{
				return false;
			}
			return true;
		}
	}
}
