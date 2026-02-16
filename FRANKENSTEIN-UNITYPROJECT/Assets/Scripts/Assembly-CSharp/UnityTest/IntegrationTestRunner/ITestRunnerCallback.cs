namespace UnityTest.IntegrationTestRunner
{
	public interface ITestRunnerCallback
	{
		void RunStarted(string platform, global::System.Collections.Generic.List<global::UnityTest.TestComponent> testsToRun);

		void RunFinished(global::System.Collections.Generic.List<global::UnityTest.TestResult> testResults);

		void TestStarted(global::UnityTest.TestResult test);

		void TestFinished(global::UnityTest.TestResult test);

		void TestRunInterrupted(global::System.Collections.Generic.List<global::UnityTest.ITestComponent> testsNotRun);
	}
}
