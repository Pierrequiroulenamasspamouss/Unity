namespace UnityTest
{
	public interface ITestComponent : global::System.IComparable<global::UnityTest.ITestComponent>
	{
		global::UnityEngine.GameObject gameObject { get; }

		string Name { get; }

		void EnableTest(bool enable);

		bool IsTestGroup();

		global::UnityTest.ITestComponent GetTestGroup();

		bool IsExceptionExpected(string exceptionType);

		bool ShouldSucceedOnException();

		double GetTimeout();

		bool IsIgnored();

		bool ShouldSucceedOnAssertions();

		bool IsExludedOnThisPlatform();
	}
}
