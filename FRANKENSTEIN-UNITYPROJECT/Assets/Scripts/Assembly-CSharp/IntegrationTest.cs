public static class IntegrationTest
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Class, AllowMultiple = false)]
	public class ExcludePlatformAttribute : global::System.Attribute
	{
		public string[] platformsToExclude;

		public ExcludePlatformAttribute(params global::UnityEngine.RuntimePlatform[] platformsToExclude)
		{
			this.platformsToExclude = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Select(platformsToExclude, (global::UnityEngine.RuntimePlatform platform) => platform.ToString()));
		}
	}

	[global::System.AttributeUsage(global::System.AttributeTargets.Class, AllowMultiple = false)]
	public class ExpectExceptions : global::System.Attribute
	{
		public string[] exceptionTypeNames;

		public bool succeedOnException;

		public ExpectExceptions()
			: this(false)
		{
		}

		public ExpectExceptions(bool succeedOnException)
			: this(succeedOnException, new string[0])
		{
		}

		public ExpectExceptions(bool succeedOnException, params string[] exceptionTypeNames)
		{
			this.succeedOnException = succeedOnException;
			this.exceptionTypeNames = exceptionTypeNames;
		}

		public ExpectExceptions(bool succeedOnException, params global::System.Type[] exceptionTypes)
			: this(succeedOnException, global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Select(exceptionTypes, (global::System.Type type) => type.FullName)))
		{
		}

		public ExpectExceptions(params string[] exceptionTypeNames)
			: this(false, exceptionTypeNames)
		{
		}

		public ExpectExceptions(params global::System.Type[] exceptionTypes)
			: this(false, exceptionTypes)
		{
		}
	}

	[global::System.AttributeUsage(global::System.AttributeTargets.Class, AllowMultiple = false)]
	public class IgnoreAttribute : global::System.Attribute
	{
	}

	[global::System.AttributeUsage(global::System.AttributeTargets.Class, AllowMultiple = false)]
	public class DynamicTestAttribute : global::System.Attribute
	{
		private string sceneName;

		public DynamicTestAttribute(string sceneName)
		{
			if (sceneName.EndsWith(".unity"))
			{
				sceneName = sceneName.Substring(0, sceneName.Length - ".unity".Length);
			}
			this.sceneName = sceneName;
		}

		public bool IncludeOnScene(string sceneName)
		{
			string fileNameWithoutExtension = global::System.IO.Path.GetFileNameWithoutExtension(sceneName);
			return fileNameWithoutExtension == this.sceneName;
		}
	}

	[global::System.AttributeUsage(global::System.AttributeTargets.Class, AllowMultiple = false)]
	public class SucceedWithAssertions : global::System.Attribute
	{
	}

	[global::System.AttributeUsage(global::System.AttributeTargets.Class, AllowMultiple = false)]
	public class TimeoutAttribute : global::System.Attribute
	{
		public float timeout;

		public TimeoutAttribute(float seconds)
		{
			timeout = seconds;
		}
	}

	public const string passMessage = "IntegrationTest Pass";

	public const string failMessage = "IntegrationTest Fail";

	public static void Pass(global::UnityEngine.GameObject go)
	{
		go = FindTopGameObject(go);
		LogResult(go, "IntegrationTest Pass");
	}

	public static void Fail(global::UnityEngine.GameObject go, string reason)
	{
		Fail(go);
		if (!string.IsNullOrEmpty(reason))
		{
			global::UnityEngine.Debug.Log(reason);
		}
	}

	public static void Fail(global::UnityEngine.GameObject go)
	{
		go = FindTopGameObject(go);
		LogResult(go, "IntegrationTest Fail");
	}

	public static void Assert(global::UnityEngine.GameObject go, bool condition)
	{
		Assert(go, condition, string.Empty);
	}

	public static void Assert(global::UnityEngine.GameObject go, bool condition, string message)
	{
		if (condition)
		{
			Pass(go);
		}
		else
		{
			Fail(go, message);
		}
	}

	private static void LogResult(global::UnityEngine.GameObject go, string message)
	{
		global::UnityEngine.Debug.Log(message + " (" + FindTopGameObject(go).name + ")", go);
	}

	private static global::UnityEngine.GameObject FindTopGameObject(global::UnityEngine.GameObject go)
	{
		while (go.transform.parent != null)
		{
			go = go.transform.parent.gameObject;
		}
		return go;
	}
}
