namespace UnityTest
{
	public class AssertionException : global::System.Exception
	{
		private global::UnityTest.AssertionComponent assertion;

		public override string StackTrace
		{
			get
			{
				return "Created in " + assertion.GetCreationLocation();
			}
		}

		public AssertionException(global::UnityTest.AssertionComponent assertion)
			: base(assertion.Action.GetFailureMessage())
		{
			this.assertion = assertion;
		}
	}
}
