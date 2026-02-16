namespace UnityTest
{
	public class ResultSummarizer
	{
		private int errorCount;

		private int failureCount;

		private int ignoreCount;

		private int inconclusiveCount;

		private int notRunnable;

		private int resultCount;

		private int skipCount;

		private int successCount;

		private int testsRun;

		private global::System.TimeSpan duration = default(global::System.TimeSpan);

		public bool Success
		{
			get
			{
				return failureCount == 0;
			}
		}

		public int ResultCount
		{
			get
			{
				return resultCount;
			}
		}

		public int TestsRun
		{
			get
			{
				return testsRun;
			}
		}

		public int Passed
		{
			get
			{
				return successCount;
			}
		}

		public int Errors
		{
			get
			{
				return errorCount;
			}
		}

		public int Failures
		{
			get
			{
				return failureCount;
			}
		}

		public int Inconclusive
		{
			get
			{
				return inconclusiveCount;
			}
		}

		public int NotRunnable
		{
			get
			{
				return notRunnable;
			}
		}

		public int Skipped
		{
			get
			{
				return skipCount;
			}
		}

		public int Ignored
		{
			get
			{
				return ignoreCount;
			}
		}

		public double Duration
		{
			get
			{
				return duration.TotalSeconds;
			}
		}

		public int TestsNotRun
		{
			get
			{
				return skipCount + ignoreCount + notRunnable;
			}
		}

		public ResultSummarizer(ITestResult[] results)
		{
			foreach (ITestResult result in results)
			{
				Summarize(result);
			}
		}

		public void Summarize(ITestResult result)
		{
			duration += global::System.TimeSpan.FromSeconds(result.Duration);
			resultCount++;
			switch (result.ResultState)
			{
			case global::UnityTest.TestResultState.Success:
				successCount++;
				testsRun++;
				break;
			case global::UnityTest.TestResultState.Failure:
				failureCount++;
				testsRun++;
				break;
			case global::UnityTest.TestResultState.Error:
			case global::UnityTest.TestResultState.Cancelled:
				errorCount++;
				testsRun++;
				break;
			case global::UnityTest.TestResultState.Inconclusive:
				inconclusiveCount++;
				testsRun++;
				break;
			case global::UnityTest.TestResultState.NotRunnable:
				notRunnable++;
				break;
			case global::UnityTest.TestResultState.Ignored:
				ignoreCount++;
				break;
			default:
				skipCount++;
				break;
			}
		}
	}
}
