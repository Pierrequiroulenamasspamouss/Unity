namespace UnityTest
{
	[global::System.Serializable]
	public class TestResult : ITestResult, global::System.IComparable<global::UnityTest.TestResult>
	{
		public enum ResultType
		{
			Success = 0,
			Failed = 1,
			Timeout = 2,
			NotRun = 3,
			FailedException = 4,
			Ignored = 5
		}

		private global::UnityEngine.GameObject go;

		private global::UnityTest.TestComponent testComponent;

		private string name;

		public global::UnityTest.TestResult.ResultType resultType = global::UnityTest.TestResult.ResultType.NotRun;

		public double duration;

		public string messages;

		public string stacktrace;

		public string id;

		public bool dynamicTest;

		public global::UnityTest.TestComponent TestComponent;

		public global::UnityEngine.GameObject GameObject
		{
			get
			{
				return go;
			}
		}

		public global::UnityTest.TestResultState ResultState
		{
			get
			{
				switch (resultType)
				{
				case global::UnityTest.TestResult.ResultType.Success:
					return global::UnityTest.TestResultState.Success;
				case global::UnityTest.TestResult.ResultType.Failed:
					return global::UnityTest.TestResultState.Failure;
				case global::UnityTest.TestResult.ResultType.FailedException:
					return global::UnityTest.TestResultState.Error;
				case global::UnityTest.TestResult.ResultType.Ignored:
					return global::UnityTest.TestResultState.Ignored;
				case global::UnityTest.TestResult.ResultType.NotRun:
					return global::UnityTest.TestResultState.Skipped;
				case global::UnityTest.TestResult.ResultType.Timeout:
					return global::UnityTest.TestResultState.Cancelled;
				default:
					throw new global::System.Exception();
				}
			}
		}

		public string Message
		{
			get
			{
				return messages;
			}
		}

		public bool Executed
		{
			get
			{
				return resultType != global::UnityTest.TestResult.ResultType.NotRun;
			}
		}

		public string Name
		{
			get
			{
				if (go != null)
				{
					name = go.name;
				}
				return name;
			}
		}

		public string Id
		{
			get
			{
				return id;
			}
		}

		public bool IsSuccess
		{
			get
			{
				return resultType == global::UnityTest.TestResult.ResultType.Success;
			}
		}

		public bool IsTimeout
		{
			get
			{
				return resultType == global::UnityTest.TestResult.ResultType.Timeout;
			}
		}

		public double Duration
		{
			get
			{
				return duration;
			}
		}

		public string StackTrace
		{
			get
			{
				return stacktrace;
			}
		}

		public string FullName
		{
			get
			{
				string text = Name;
				if (go != null)
				{
					global::UnityEngine.Transform parent = go.transform.parent;
					while (parent != null)
					{
						text = parent.name + "." + text;
						parent = parent.transform.parent;
					}
				}
				return text;
			}
		}

		public bool IsIgnored
		{
			get
			{
				return resultType == global::UnityTest.TestResult.ResultType.Ignored;
			}
		}

		public bool IsFailure
		{
			get
			{
				return resultType == global::UnityTest.TestResult.ResultType.Failed || resultType == global::UnityTest.TestResult.ResultType.FailedException || resultType == global::UnityTest.TestResult.ResultType.Timeout;
			}
		}

		public TestResult(global::UnityTest.TestComponent testComponent)
		{
			TestComponent = testComponent;
			go = testComponent.gameObject;
			id = testComponent.gameObject.GetInstanceID().ToString();
			dynamicTest = testComponent.dynamic;
			if (go != null)
			{
				name = go.name;
			}
			if (dynamicTest)
			{
				id = testComponent.dynamicTypeName;
			}
		}

		public void Update(global::UnityTest.TestResult oldResult)
		{
			resultType = oldResult.resultType;
			duration = oldResult.duration;
			messages = oldResult.messages;
			stacktrace = oldResult.stacktrace;
		}

		public void Reset()
		{
			resultType = global::UnityTest.TestResult.ResultType.NotRun;
			duration = 0.0;
			messages = string.Empty;
			stacktrace = string.Empty;
		}

		public override int GetHashCode()
		{
			return id.GetHashCode();
		}

		public int CompareTo(global::UnityTest.TestResult other)
		{
			int num = Name.CompareTo(other.Name);
			if (num == 0)
			{
				num = go.GetInstanceID().CompareTo(other.go.GetInstanceID());
			}
			return num;
		}

		public override bool Equals(object obj)
		{
			if (obj is global::UnityTest.TestResult)
			{
				return GetHashCode() == obj.GetHashCode();
			}
			return base.Equals(obj);
		}
	}
}
