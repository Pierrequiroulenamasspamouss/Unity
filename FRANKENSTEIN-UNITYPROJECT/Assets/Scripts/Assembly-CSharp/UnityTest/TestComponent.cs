namespace UnityTest
{
	public class TestComponent : global::UnityEngine.MonoBehaviour, global::UnityTest.ITestComponent, global::System.IComparable<global::UnityTest.ITestComponent>
	{
		[global::System.Flags]
		public enum IncludedPlatforms
		{
			WindowsEditor = 1,
			OSXEditor = 2,
			WindowsPlayer = 4,
			OSXPlayer = 8,
			LinuxPlayer = 0x10,
			MetroPlayerX86 = 0x20,
			MetroPlayerX64 = 0x40,
			MetroPlayerARM = 0x80,
			WindowsWebPlayer = 0x100,
			OSXWebPlayer = 0x200,
			Android = 0x400,
			IPhonePlayer = 0x800,
			TizenPlayer = 0x1000,
			WP8Player = 0x2000,
			BB10Player = 0x4000,
			NaCl = 0x8000,
			PS3 = 0x10000,
			XBOX360 = 0x20000,
			WiiPlayer = 0x40000,
			PSP2 = 0x80000,
			PS4 = 0x100000,
			PSMPlayer = 0x200000,
			XboxOne = 0x400000
		}

		private sealed class NullTestComponentImpl : global::UnityTest.ITestComponent, global::System.IComparable<global::UnityTest.ITestComponent>
		{
			public global::UnityEngine.GameObject gameObject { get; private set; }

			public string Name
			{
				get
				{
					return string.Empty;
				}
			}

			public int CompareTo(global::UnityTest.ITestComponent other)
			{
				if (other == this)
				{
					return 0;
				}
				return -1;
			}

			public void EnableTest(bool enable)
			{
			}

			public global::UnityTest.ITestComponent GetParentTestComponent()
			{
				throw new global::System.NotImplementedException();
			}

			public bool IsTestGroup()
			{
				throw new global::System.NotImplementedException();
			}

			public global::UnityTest.ITestComponent GetTestGroup()
			{
				return null;
			}

			public bool IsExceptionExpected(string exceptionType)
			{
				throw new global::System.NotImplementedException();
			}

			public bool ShouldSucceedOnException()
			{
				throw new global::System.NotImplementedException();
			}

			public double GetTimeout()
			{
				throw new global::System.NotImplementedException();
			}

			public bool IsIgnored()
			{
				throw new global::System.NotImplementedException();
			}

			public bool ShouldSucceedOnAssertions()
			{
				throw new global::System.NotImplementedException();
			}

			public bool IsExludedOnThisPlatform()
			{
				throw new global::System.NotImplementedException();
			}
		}

		public static global::UnityTest.ITestComponent NullTestComponent = new global::UnityTest.TestComponent.NullTestComponentImpl();

		public float timeout = 5f;

		public bool ignored;

		public bool succeedAfterAllAssertionsAreExecuted;

		public bool expectException;

		public string expectedExceptionList = string.Empty;

		public bool succeedWhenExceptionIsThrown;

		public global::UnityTest.TestComponent.IncludedPlatforms includedPlatforms = (global::UnityTest.TestComponent.IncludedPlatforms)(-1);

		public string[] platformsToIgnore;

		public bool dynamic;

		public string dynamicTypeName;

		public string Name
		{
			get
			{
				return (!(base.gameObject == null)) ? base.gameObject.name : string.Empty;
			}
		}

		global::UnityEngine.GameObject global::UnityTest.ITestComponent.gameObject // unsure this will work. changed  virtual global to simply global
		{
			get { return base.gameObject; }
		}

		public bool IsExludedOnThisPlatform()
		{
			return platformsToIgnore != null && global::System.Linq.Enumerable.Any(platformsToIgnore, (string platform) => platform == global::UnityEngine.Application.platform.ToString());
		}

		private static bool IsAssignableFrom(global::System.Type a, global::System.Type b)
		{
			return a.IsAssignableFrom(b);
		}

		public bool IsExceptionExpected(string exception)
		{
			if (!expectException)
			{
				return false;
			}
			exception = exception.Trim();
			foreach (string item in global::System.Linq.Enumerable.Select(expectedExceptionList.Split(','), (string e) => e.Trim()))
			{
				if (exception == item)
				{
					return true;
				}
				global::System.Type type = global::System.Type.GetType(exception) ?? GetTypeByName(exception);
				global::System.Type type2 = global::System.Type.GetType(item) ?? GetTypeByName(item);
				if (type != null && type2 != null && IsAssignableFrom(type2, type))
				{
					return true;
				}
			}
			return false;
		}

		public bool ShouldSucceedOnException()
		{
			return succeedWhenExceptionIsThrown;
		}

		public double GetTimeout()
		{
			return timeout;
		}

		public bool IsIgnored()
		{
			return ignored;
		}

		public bool ShouldSucceedOnAssertions()
		{
			return succeedAfterAllAssertionsAreExecuted;
		}

		private static global::System.Type GetTypeByName(string className)
		{
			return global::System.Linq.Enumerable.FirstOrDefault(global::System.Linq.Enumerable.SelectMany(global::System.AppDomain.CurrentDomain.GetAssemblies(), (global::System.Reflection.Assembly a) => a.GetTypes()), (global::System.Type type) => type.Name == className);
		}

		public void OnValidate()
		{
			if (timeout < 0.01f)
			{
				timeout = 0.01f;
			}
		}

		public void EnableTest(bool enable)
		{
			if (enable && dynamic)
			{
				global::System.Type type = global::System.Type.GetType(dynamicTypeName);
				global::UnityEngine.MonoBehaviour monoBehaviour = base.gameObject.GetComponent(type) as global::UnityEngine.MonoBehaviour;
				if (monoBehaviour != null)
				{
					global::UnityEngine.Object.DestroyImmediate(monoBehaviour);
				}
				base.gameObject.AddComponent(type);
			}
			if (base.gameObject.activeSelf != enable)
			{
				base.gameObject.SetActive(enable);
			}
		}

		public int CompareTo(global::UnityTest.ITestComponent obj)
		{
			if (obj == NullTestComponent)
			{
				return 1;
			}
			int num = base.gameObject.name.CompareTo(obj.gameObject.name);
			if (num == 0)
			{
				num = base.gameObject.GetInstanceID().CompareTo(obj.gameObject.GetInstanceID());
			}
			return num;
		}

		public bool IsTestGroup()
		{
			for (int i = 0; i < base.gameObject.transform.childCount; i++)
			{
				global::UnityEngine.Component component = base.gameObject.transform.GetChild(i).GetComponent(typeof(global::UnityTest.TestComponent));
				if (component != null)
				{
					return true;
				}
			}
			return false;
		}

		public global::UnityTest.ITestComponent GetTestGroup()
		{
			global::UnityEngine.Transform parent = base.gameObject.transform.parent;
			if (parent == null)
			{
				return NullTestComponent;
			}
			return parent.GetComponent<global::UnityTest.TestComponent>();
		}

		public override bool Equals(object o)
		{
			if (o is global::UnityTest.TestComponent)
			{
				return this == o as global::UnityTest.TestComponent;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public static global::UnityTest.TestComponent CreateDynamicTest(global::System.Type type)
		{
			global::UnityEngine.GameObject gameObject = CreateTest(type.Name);
			gameObject.hideFlags |= global::UnityEngine.HideFlags.DontSave;
			gameObject.SetActive(false);
			global::UnityTest.TestComponent component = gameObject.GetComponent<global::UnityTest.TestComponent>();
			component.dynamic = true;
			component.dynamicTypeName = type.AssemblyQualifiedName;
			object[] customAttributes = type.GetCustomAttributes(false);
			foreach (object obj in customAttributes)
			{
				if (obj is IntegrationTest.TimeoutAttribute)
				{
					component.timeout = (obj as IntegrationTest.TimeoutAttribute).timeout;
				}
				else if (obj is IntegrationTest.IgnoreAttribute)
				{
					component.ignored = true;
				}
				else if (obj is IntegrationTest.SucceedWithAssertions)
				{
					component.succeedAfterAllAssertionsAreExecuted = true;
				}
				else if (obj is IntegrationTest.ExcludePlatformAttribute)
				{
					component.platformsToIgnore = (obj as IntegrationTest.ExcludePlatformAttribute).platformsToExclude;
				}
				else if (obj is IntegrationTest.ExpectExceptions)
				{
					IntegrationTest.ExpectExceptions expectExceptions = obj as IntegrationTest.ExpectExceptions;
					component.expectException = true;
					component.expectedExceptionList = string.Join(",", expectExceptions.exceptionTypeNames);
					component.succeedWhenExceptionIsThrown = expectExceptions.succeedOnException;
				}
			}
			gameObject.AddComponent(type);
			return component;
		}

		public static global::UnityEngine.GameObject CreateTest()
		{
			return CreateTest("New Test");
		}

		private static global::UnityEngine.GameObject CreateTest(string name)
		{
			global::UnityEngine.GameObject gameObject = new global::UnityEngine.GameObject(name);
			gameObject.AddComponent<global::UnityTest.TestComponent>();
			gameObject.transform.hideFlags |= global::UnityEngine.HideFlags.HideInInspector;
			return gameObject;
		}

		public static global::System.Collections.Generic.List<global::UnityTest.TestComponent> FindAllTestsOnScene()
		{
			return global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.Cast<global::UnityTest.TestComponent>(global::UnityEngine.Resources.FindObjectsOfTypeAll(typeof(global::UnityTest.TestComponent))));
		}

		public static global::System.Collections.Generic.List<global::UnityTest.TestComponent> FindAllTopTestsOnScene()
		{
			return global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.Where(FindAllTestsOnScene(), (global::UnityTest.TestComponent component) => component.gameObject.transform.parent == null));
		}

		public static global::System.Collections.Generic.List<global::UnityTest.TestComponent> FindAllDynamicTestsOnScene()
		{
			return global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.Where(FindAllTestsOnScene(), (global::UnityTest.TestComponent t) => t.dynamic));
		}

		public static void DestroyAllDynamicTests()
		{
			foreach (global::UnityTest.TestComponent item in FindAllDynamicTestsOnScene())
			{
				global::UnityEngine.Object.DestroyImmediate(item.gameObject);
			}
		}

		public static void DisableAllTests()
		{
			foreach (global::UnityTest.TestComponent item in FindAllTestsOnScene())
			{
				item.EnableTest(false);
			}
		}

		public static bool AnyTestsOnScene()
		{
			return global::System.Linq.Enumerable.Any(FindAllTestsOnScene());
		}

		public static global::System.Collections.Generic.IEnumerable<global::System.Type> GetTypesWithHelpAttribute(string sceneName)
		{
			global::System.Reflection.Assembly[] assemblies = global::System.AppDomain.CurrentDomain.GetAssemblies();
			foreach (global::System.Reflection.Assembly assembly in assemblies)
			{
				global::System.Type[] types = assembly.GetTypes();
				foreach (global::System.Type type in types)
				{
					object[] attributes = type.GetCustomAttributes(typeof(IntegrationTest.DynamicTestAttribute), true);
					if (attributes.Length == 1)
					{
						IntegrationTest.DynamicTestAttribute a = global::System.Linq.Enumerable.Single(attributes) as IntegrationTest.DynamicTestAttribute;
						if (a.IncludeOnScene(sceneName))
						{
							yield return type;
						}
					}
				}
			}
		}

		public static bool operator ==(global::UnityTest.TestComponent a, global::UnityTest.TestComponent b)
		{
			if (object.ReferenceEquals(a, b))
			{
				return true;
			}
			if ((object)a == null || (object)b == null)
			{
				return false;
			}
			if (a.dynamic && b.dynamic)
			{
				return a.dynamicTypeName == b.dynamicTypeName;
			}
			if (a.dynamic || b.dynamic)
			{
				return false;
			}
			return a.gameObject == b.gameObject;
		}

		public static bool operator !=(global::UnityTest.TestComponent a, global::UnityTest.TestComponent b)
		{
			return !(a == b);
		}
	}
}
