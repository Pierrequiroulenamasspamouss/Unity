namespace UnityTest
{
	public static class Assertions
	{
		public static void CheckAssertions()
		{
			global::UnityTest.AssertionComponent[] assertions = global::UnityEngine.Object.FindObjectsOfType(typeof(global::UnityTest.AssertionComponent)) as global::UnityTest.AssertionComponent[];
			CheckAssertions(assertions);
		}

		public static void CheckAssertions(global::UnityTest.AssertionComponent assertion)
		{
			CheckAssertions(new global::UnityTest.AssertionComponent[1] { assertion });
		}

		public static void CheckAssertions(global::UnityEngine.GameObject gameObject)
		{
			CheckAssertions(gameObject.GetComponents<global::UnityTest.AssertionComponent>());
		}

		public static void CheckAssertions(global::UnityTest.AssertionComponent[] assertions)
		{
			if (!global::UnityEngine.Debug.isDebugBuild)
			{
				return;
			}
			foreach (global::UnityTest.AssertionComponent assertionComponent in assertions)
			{
				assertionComponent.checksPerformed++;
				if (!assertionComponent.Action.Compare())
				{
					assertionComponent.hasFailed = true;
					assertionComponent.Action.Fail(assertionComponent);
				}
			}
		}
	}
}
