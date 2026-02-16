namespace UnityTest.IntegrationTestRunner
{
	public class TestRunnerCallbackList : global::UnityTest.IntegrationTestRunner.ITestRunnerCallback
	{
		private global::System.Collections.Generic.List<global::UnityTest.IntegrationTestRunner.ITestRunnerCallback> callbackList = new global::System.Collections.Generic.List<global::UnityTest.IntegrationTestRunner.ITestRunnerCallback>();

		public void Add(global::UnityTest.IntegrationTestRunner.ITestRunnerCallback callback)
		{
			callbackList.Add(callback);
		}

		public void Remove(global::UnityTest.IntegrationTestRunner.ITestRunnerCallback callback)
		{
			callbackList.Remove(callback);
		}

		public void RunStarted(string platform, global::System.Collections.Generic.List<global::UnityTest.TestComponent> testsToRun)
		{
			foreach (global::UnityTest.IntegrationTestRunner.ITestRunnerCallback callback in callbackList)
			{
				callback.RunStarted(platform, testsToRun);
			}
		}

		public void RunFinished(global::System.Collections.Generic.List<global::UnityTest.TestResult> testResults)
		{
			foreach (global::UnityTest.IntegrationTestRunner.ITestRunnerCallback callback in callbackList)
			{
				callback.RunFinished(testResults);
			}
		}

		public void TestStarted(global::UnityTest.TestResult test)
		{
			foreach (global::UnityTest.IntegrationTestRunner.ITestRunnerCallback callback in callbackList)
			{
				callback.TestStarted(test);
			}
		}

		public void TestFinished(global::UnityTest.TestResult test)
		{
			foreach (global::UnityTest.IntegrationTestRunner.ITestRunnerCallback callback in callbackList)
			{
				callback.TestFinished(test);
			}
		}

		public void TestRunInterrupted(global::System.Collections.Generic.List<global::UnityTest.ITestComponent> testsNotRun)
		{
			foreach (global::UnityTest.IntegrationTestRunner.ITestRunnerCallback callback in callbackList)
			{
				callback.TestRunInterrupted(testsNotRun);
			}
		}
	}
}
