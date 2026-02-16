namespace UnityTest
{
	[global::System.Serializable]
	public class TestRunner : global::UnityEngine.MonoBehaviour
	{
		private enum TestState
		{
			Running = 0,
			Success = 1,
			Failure = 2,
			Exception = 3,
			Timeout = 4,
			Ignored = 5
		}

		private const string startedMessage = "IntegrationTest started";

		private const string finishedMessage = "IntegrationTest finished";

		private const string timeoutMessage = "IntegrationTest timeout";

		private const string failedMessage = "IntegrationTest failed";

		private const string failedExceptionMessage = "IntegrationTest failed with exception";

		private const string ignoredMessage = "IntegrationTest ignored";

		private const string interruptedMessage = "IntegrationTest Run interrupted";

		public static string integrationTestsConfigFileName = "integrationtestsconfig.txt";

		public static string batchRunFileMarker = "batchrun.txt";

		public static string defaultResulFilePostfix = "TestResults.xml";

		private static TestResultRenderer resultRenderer = new TestResultRenderer();

		public global::UnityTest.TestComponent currentTest;

		private global::System.Collections.Generic.List<global::UnityTest.TestResult> resultList = new global::System.Collections.Generic.List<global::UnityTest.TestResult>();

		private global::System.Collections.Generic.List<global::UnityTest.TestComponent> testComponents;

		private double startTime;

		private bool readyToRun;

		private global::UnityTest.AssertionComponent[] assertionsToCheck;

		private string testMessages;

		private string stacktrace;

		private global::UnityTest.TestRunner.TestState testState;

		public global::UnityTest.IntegrationTestRunner.TestRunnerCallbackList TestRunnerCallback = new global::UnityTest.IntegrationTestRunner.TestRunnerCallbackList();

		private global::UnityTest.IntegrationTestRunner.IntegrationTestsProvider testsProvider;

		public bool isInitializedByRunner
		{
			get
			{
				return false;
			}
		}

		public void Awake()
		{
			global::UnityTest.TestComponent.DisableAllTests();
		}

		public void Start()
		{
			global::UnityTest.TestComponent.DestroyAllDynamicTests();
			global::System.Collections.Generic.IEnumerable<global::System.Type> typesWithHelpAttribute = global::UnityTest.TestComponent.GetTypesWithHelpAttribute(global::UnityEngine.Application.loadedLevelName);
			foreach (global::System.Type item in typesWithHelpAttribute)
			{
				global::UnityTest.TestComponent.CreateDynamicTest(item);
			}
			global::System.Collections.Generic.List<global::UnityTest.TestComponent> tests = global::UnityTest.TestComponent.FindAllTestsOnScene();
			InitRunner(tests, global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.Select(typesWithHelpAttribute, (global::System.Type type) => type.AssemblyQualifiedName)));
		}

		public void InitRunner(global::System.Collections.Generic.List<global::UnityTest.TestComponent> tests, global::System.Collections.Generic.List<string> dynamicTestsToRun)
		{
			global::UnityEngine.Application.RegisterLogCallback(LogHandler);
			foreach (string item in dynamicTestsToRun)
			{
				global::System.Type type = global::System.Type.GetType(item);
				if (type == null)
				{
					continue;
				}
				global::UnityEngine.MonoBehaviour[] array = global::UnityEngine.Resources.FindObjectsOfTypeAll(type) as global::UnityEngine.MonoBehaviour[];
				if (array.Length == 0)
				{
					global::UnityEngine.Debug.LogWarning(string.Concat(type, " not found. Skipping."));
					continue;
				}
				if (array.Length > 1)
				{
					global::UnityEngine.Debug.LogWarning("Multiple GameObjects refer to " + item);
				}
				tests.Add(global::System.Linq.Enumerable.First(array).GetComponent<global::UnityTest.TestComponent>());
			}
			testComponents = global::System.Linq.Enumerable.ToList(ParseListForGroups(tests));
			resultList = global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.Select(testComponents, (global::UnityTest.TestComponent component) => new global::UnityTest.TestResult(component)));
			testsProvider = new global::UnityTest.IntegrationTestRunner.IntegrationTestsProvider(global::System.Linq.Enumerable.Select((global::System.Collections.Generic.IEnumerable<global::UnityTest.TestResult>)resultList, (global::System.Func<global::UnityTest.TestResult, global::UnityTest.ITestComponent>)((global::UnityTest.TestResult result) => result.TestComponent)));
			readyToRun = true;
		}

		private static global::System.Collections.Generic.IEnumerable<global::UnityTest.TestComponent> ParseListForGroups(global::System.Collections.Generic.IEnumerable<global::UnityTest.TestComponent> tests)
		{
			global::System.Collections.Generic.HashSet<global::UnityTest.TestComponent> hashSet = new global::System.Collections.Generic.HashSet<global::UnityTest.TestComponent>();
			global::UnityTest.TestComponent testResult;
			foreach (global::UnityTest.TestComponent test in tests)
			{
				testResult = test;
				if (testResult.IsTestGroup())
				{
					global::UnityTest.TestComponent[] array = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Cast<global::UnityTest.TestComponent>(global::System.Linq.Enumerable.Where(testResult.gameObject.GetComponentsInChildren(typeof(global::UnityTest.TestComponent), true), (global::UnityEngine.Component t) => t != testResult)));
					global::UnityTest.TestComponent[] array2 = array;
					foreach (global::UnityTest.TestComponent testComponent in array2)
					{
						if (!testComponent.IsTestGroup())
						{
							hashSet.Add(testComponent);
						}
					}
				}
				else
				{
					hashSet.Add(testResult);
				}
			}
			return hashSet;
		}

		public void Update()
		{
			if (readyToRun && global::UnityEngine.Time.frameCount > 1)
			{
				readyToRun = false;
				StartCoroutine("StateMachine");
			}
		}

		public void OnDestroy()
		{
			if (currentTest != null)
			{
				global::UnityTest.TestResult testResult = global::System.Linq.Enumerable.Single(resultList, (global::UnityTest.TestResult result) => result.TestComponent == currentTest);
				testResult.messages += "Test run interrupted (crash?)";
				LogMessage("IntegrationTest Run interrupted");
				FinishTest(global::UnityTest.TestResult.ResultType.Failed);
			}
			if (currentTest != null || (testsProvider != null && testsProvider.AnyTestsLeft()))
			{
				global::System.Collections.Generic.List<global::UnityTest.ITestComponent> remainingTests = testsProvider.GetRemainingTests();
				TestRunnerCallback.TestRunInterrupted(global::System.Linq.Enumerable.ToList(remainingTests));
			}
			global::UnityEngine.Application.RegisterLogCallback(null);
		}

		private void LogHandler(string condition, string stacktrace, global::UnityEngine.LogType type)
		{
			testMessages = testMessages + condition + "\n";
			switch (type)
			{
			case global::UnityEngine.LogType.Exception:
			{
				string text = condition.Substring(0, condition.IndexOf(':'));
				if (currentTest.IsExceptionExpected(text))
				{
					testMessages = testMessages + text + " was expected\n";
					if (currentTest.ShouldSucceedOnException())
					{
						testState = global::UnityTest.TestRunner.TestState.Success;
					}
				}
				else
				{
					testState = global::UnityTest.TestRunner.TestState.Exception;
					this.stacktrace = stacktrace;
				}
				break;
			}
			case global::UnityEngine.LogType.Log:
				if (testState == global::UnityTest.TestRunner.TestState.Running && condition.StartsWith("IntegrationTest Pass"))
				{
					testState = global::UnityTest.TestRunner.TestState.Success;
				}
				if (condition.StartsWith("IntegrationTest Fail"))
				{
					testState = global::UnityTest.TestRunner.TestState.Failure;
				}
				break;
			}
		}

		public global::System.Collections.IEnumerator StateMachine()
		{
			TestRunnerCallback.RunStarted(global::UnityEngine.Application.platform.ToString(), testComponents);
			while (testsProvider.AnyTestsLeft() || !(currentTest == null))
			{
				if (currentTest == null)
				{
					StartNewTest();
				}
				if (currentTest != null)
				{
					if (testState == global::UnityTest.TestRunner.TestState.Running)
					{
						if (assertionsToCheck != null && global::System.Linq.Enumerable.All(assertionsToCheck, (global::UnityTest.AssertionComponent a) => a.checksPerformed > 0))
						{
							IntegrationTest.Pass(currentTest.gameObject);
							testState = global::UnityTest.TestRunner.TestState.Success;
						}
						if (currentTest != null && (double)global::UnityEngine.Time.time > startTime + currentTest.GetTimeout())
						{
							testState = global::UnityTest.TestRunner.TestState.Timeout;
						}
					}
					switch (testState)
					{
					case global::UnityTest.TestRunner.TestState.Success:
						LogMessage("IntegrationTest finished");
						FinishTest(global::UnityTest.TestResult.ResultType.Success);
						break;
					case global::UnityTest.TestRunner.TestState.Failure:
						LogMessage("IntegrationTest failed");
						FinishTest(global::UnityTest.TestResult.ResultType.Failed);
						break;
					case global::UnityTest.TestRunner.TestState.Exception:
						LogMessage("IntegrationTest failed with exception");
						FinishTest(global::UnityTest.TestResult.ResultType.FailedException);
						break;
					case global::UnityTest.TestRunner.TestState.Timeout:
						LogMessage("IntegrationTest timeout");
						FinishTest(global::UnityTest.TestResult.ResultType.Timeout);
						break;
					case global::UnityTest.TestRunner.TestState.Ignored:
						LogMessage("IntegrationTest ignored");
						FinishTest(global::UnityTest.TestResult.ResultType.Ignored);
						break;
					}
				}
				yield return null;
			}
			FinishTestRun();
		}

		private void LogMessage(string message)
		{
			if (currentTest != null)
			{
				global::UnityEngine.Debug.Log(message + " (" + currentTest.Name + ")", currentTest.gameObject);
			}
			else
			{
				global::UnityEngine.Debug.Log(message);
			}
		}

		private void FinishTestRun()
		{
			if (IsBatchRun())
			{
				SaveResults();
			}
			PrintResultToLog();
			TestRunnerCallback.RunFinished(resultList);
			LoadNextLevelOrQuit();
		}

		private void PrintResultToLog()
		{
			string empty = string.Empty;
			empty = empty + "Passed: " + global::System.Linq.Enumerable.Count(resultList, (global::UnityTest.TestResult t) => t.IsSuccess);
			if (global::System.Linq.Enumerable.Any(resultList, (global::UnityTest.TestResult result) => result.IsFailure))
			{
				empty = empty + " Failed: " + global::System.Linq.Enumerable.Count(resultList, (global::UnityTest.TestResult t) => t.IsFailure);
				global::UnityEngine.Debug.Log("Failed tests: " + string.Join(", ", global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Select(global::System.Linq.Enumerable.Where(resultList, (global::UnityTest.TestResult t) => t.IsFailure), (global::UnityTest.TestResult result) => result.Name))));
			}
			if (global::System.Linq.Enumerable.Any(resultList, (global::UnityTest.TestResult result) => result.IsIgnored))
			{
				empty = empty + " Ignored: " + global::System.Linq.Enumerable.Count(resultList, (global::UnityTest.TestResult t) => t.IsIgnored);
				global::UnityEngine.Debug.Log("Ignored tests: " + string.Join(", ", global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Select(global::System.Linq.Enumerable.Where(resultList, (global::UnityTest.TestResult t) => t.IsIgnored), (global::UnityTest.TestResult result) => result.Name))));
			}
			global::UnityEngine.Debug.Log(empty);
		}

		private void LoadNextLevelOrQuit()
		{
			if (isInitializedByRunner)
			{
				return;
			}
			if (global::UnityEngine.Application.loadedLevel < global::UnityEngine.Application.levelCount - 1)
			{
				global::UnityEngine.Application.LoadLevel(global::UnityEngine.Application.loadedLevel + 1);
				return;
			}
			resultRenderer.ShowResults();
			if (IsBatchRun())
			{
				global::UnityEngine.Application.Quit();
			}
		}

		public void OnGUI()
		{
			resultRenderer.Draw();
		}

		private void SaveResults()
		{
			if (IsFileSavingSupported())
			{
				string resultDestiantion = GetResultDestiantion();
				string text = global::UnityEngine.Application.loadedLevelName;
				if (text != string.Empty)
				{
					text += "-";
				}
				text += defaultResulFilePostfix;
				global::UnityTest.XmlResultWriter xmlResultWriter = new global::UnityTest.XmlResultWriter(global::UnityEngine.Application.loadedLevelName, resultList.ToArray());
				global::System.Uri result;
				if (global::System.Uri.TryCreate(resultDestiantion, global::System.UriKind.Absolute, out result) && result.Scheme == global::System.Uri.UriSchemeFile)
				{
					xmlResultWriter.WriteToFile(resultDestiantion, text);
				}
				else
				{
					global::UnityEngine.Debug.LogError("Provided path is invalid");
				}
			}
		}

		private bool IsFileSavingSupported()
		{
			return false;
		}

		private string GetResultDestiantion()
		{
			string path = integrationTestsConfigFileName.Substring(0, integrationTestsConfigFileName.LastIndexOf('.'));
			global::UnityEngine.TextAsset textAsset = global::UnityEngine.Resources.Load(path) as global::UnityEngine.TextAsset;
			string result = global::UnityEngine.Application.dataPath;
			if (textAsset != null)
			{
				result = textAsset.text;
			}
			return result;
		}

		private bool IsBatchRun()
		{
			string path = batchRunFileMarker.Substring(0, batchRunFileMarker.LastIndexOf('.'));
			global::UnityEngine.TextAsset textAsset = global::UnityEngine.Resources.Load(path) as global::UnityEngine.TextAsset;
			return textAsset != null;
		}

		private void StartNewTest()
		{
			testMessages = string.Empty;
			stacktrace = string.Empty;
			testState = global::UnityTest.TestRunner.TestState.Running;
			assertionsToCheck = null;
			startTime = global::UnityEngine.Time.time;
			currentTest = testsProvider.GetNextTest() as global::UnityTest.TestComponent;
			global::UnityTest.TestResult test = global::System.Linq.Enumerable.Single(resultList, (global::UnityTest.TestResult result) => result.TestComponent == currentTest);
			if (currentTest.ShouldSucceedOnAssertions())
			{
				global::System.Collections.Generic.IEnumerable<global::UnityTest.AssertionComponent> source = global::System.Linq.Enumerable.Where(currentTest.gameObject.GetComponentsInChildren<global::UnityTest.AssertionComponent>(), (global::UnityTest.AssertionComponent a) => a.enabled);
				if (global::System.Linq.Enumerable.Any(source))
				{
					assertionsToCheck = global::System.Linq.Enumerable.ToArray(source);
				}
			}
			if (currentTest.IsExludedOnThisPlatform())
			{
				testState = global::UnityTest.TestRunner.TestState.Ignored;
				global::UnityEngine.Debug.Log(currentTest.gameObject.name + " is excluded on this platform");
			}
			if (currentTest.IsIgnored() && (!isInitializedByRunner || resultList.Count != 1))
			{
				testState = global::UnityTest.TestRunner.TestState.Ignored;
			}
			LogMessage("IntegrationTest started");
			TestRunnerCallback.TestStarted(test);
		}

		private void FinishTest(global::UnityTest.TestResult.ResultType result)
		{
			testsProvider.FinishTest(currentTest);
			global::UnityTest.TestResult testResult = global::System.Linq.Enumerable.Single(resultList, (global::UnityTest.TestResult t) => t.GameObject == currentTest.gameObject);
			testResult.resultType = result;
			testResult.duration = (double)global::UnityEngine.Time.time - startTime;
			testResult.messages = testMessages;
			testResult.stacktrace = stacktrace;
			TestRunnerCallback.TestFinished(testResult);
			currentTest = null;
			if (!testResult.IsSuccess && testResult.Executed && !testResult.IsIgnored)
			{
				resultRenderer.AddResults(global::UnityEngine.Application.loadedLevelName, testResult);
			}
		}

		public static global::UnityTest.TestRunner GetTestRunner()
		{
			global::UnityTest.TestRunner result = null;
			global::UnityEngine.Object[] array = global::UnityEngine.Resources.FindObjectsOfTypeAll(typeof(global::UnityTest.TestRunner));
			if (global::System.Linq.Enumerable.Count(array) <= 1)
			{
				result = (global::System.Linq.Enumerable.Any(array) ? (global::System.Linq.Enumerable.Single(array) as global::UnityTest.TestRunner) : Create().GetComponent<global::UnityTest.TestRunner>());
			}
			else
			{
				global::UnityEngine.Object[] array2 = array;
				foreach (global::UnityEngine.Object obj in array2)
				{
					global::UnityEngine.Object.DestroyImmediate((obj as global::UnityTest.TestRunner).gameObject);
				}
			}
			return result;
		}

		private static global::UnityEngine.GameObject Create()
		{
			global::UnityEngine.GameObject gameObject = new global::UnityEngine.GameObject("TestRunner");
			global::UnityTest.TestRunner testRunner = gameObject.AddComponent<global::UnityTest.TestRunner>();
			testRunner.hideFlags = global::UnityEngine.HideFlags.NotEditable;
			global::UnityEngine.Debug.Log("Created Test Runner");
			return gameObject;
		}
	}
}
