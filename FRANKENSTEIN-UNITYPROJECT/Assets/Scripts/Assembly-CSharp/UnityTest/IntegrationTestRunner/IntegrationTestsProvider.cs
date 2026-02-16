namespace UnityTest.IntegrationTestRunner
{
	internal class IntegrationTestsProvider
	{
		internal global::System.Collections.Generic.Dictionary<global::UnityTest.ITestComponent, global::System.Collections.Generic.HashSet<global::UnityTest.ITestComponent>> testCollection = new global::System.Collections.Generic.Dictionary<global::UnityTest.ITestComponent, global::System.Collections.Generic.HashSet<global::UnityTest.ITestComponent>>();

		internal global::UnityTest.ITestComponent currentTestGroup;

		internal global::System.Collections.Generic.IEnumerable<global::UnityTest.ITestComponent> testToRun;

		public IntegrationTestsProvider(global::System.Collections.Generic.IEnumerable<global::UnityTest.ITestComponent> tests)
		{
			testToRun = tests;
			foreach (global::UnityTest.ITestComponent item in global::System.Linq.Enumerable.OrderBy(tests, (global::UnityTest.ITestComponent component) => component))
			{
				if (item.IsTestGroup())
				{
					throw new global::System.Exception(item.Name + " is test a group");
				}
				AddTestToList(item);
			}
			if (currentTestGroup == null)
			{
				currentTestGroup = FindInnerTestGroup(global::UnityTest.TestComponent.NullTestComponent);
			}
		}

		private void AddTestToList(global::UnityTest.ITestComponent test)
		{
			global::UnityTest.ITestComponent testGroup = test.GetTestGroup();
			if (!testCollection.ContainsKey(testGroup))
			{
				testCollection.Add(testGroup, new global::System.Collections.Generic.HashSet<global::UnityTest.ITestComponent>());
			}
			testCollection[testGroup].Add(test);
			if (testGroup != global::UnityTest.TestComponent.NullTestComponent)
			{
				AddTestToList(testGroup);
			}
		}

		public global::UnityTest.ITestComponent GetNextTest()
		{
			global::UnityTest.ITestComponent testComponent = global::System.Linq.Enumerable.First(testCollection[currentTestGroup]);
			testCollection[currentTestGroup].Remove(testComponent);
			testComponent.EnableTest(true);
			return testComponent;
		}

		public void FinishTest(global::UnityTest.ITestComponent test)
		{
			try
			{
				test.EnableTest(false);
				currentTestGroup = FindNextTestGroup(currentTestGroup);
			}
			catch (global::UnityEngine.MissingReferenceException exception)
			{
				global::UnityEngine.Debug.LogException(exception);
			}
		}

		private global::UnityTest.ITestComponent FindNextTestGroup(global::UnityTest.ITestComponent testGroup)
		{
			if (testGroup != null)
			{
				if (global::System.Linq.Enumerable.Any(testCollection[testGroup]))
				{
					testGroup.EnableTest(true);
					return FindInnerTestGroup(testGroup);
				}
				testCollection.Remove(testGroup);
				testGroup.EnableTest(false);
				global::UnityTest.ITestComponent testGroup2 = testGroup.GetTestGroup();
				if (testGroup2 == null)
				{
					return null;
				}
				testCollection[testGroup2].Remove(testGroup);
				return FindNextTestGroup(testGroup2);
			}
			throw new global::System.Exception("No test left");
		}

		private global::UnityTest.ITestComponent FindInnerTestGroup(global::UnityTest.ITestComponent group)
		{
			global::System.Collections.Generic.HashSet<global::UnityTest.ITestComponent> hashSet = testCollection[group];
			foreach (global::UnityTest.ITestComponent item in hashSet)
			{
				if (!item.IsTestGroup())
				{
					continue;
				}
				item.EnableTest(true);
				return FindInnerTestGroup(item);
			}
			return group;
		}

		public bool AnyTestsLeft()
		{
			return testCollection.Count != 0;
		}

		public global::System.Collections.Generic.List<global::UnityTest.ITestComponent> GetRemainingTests()
		{
			global::System.Collections.Generic.List<global::UnityTest.ITestComponent> list = new global::System.Collections.Generic.List<global::UnityTest.ITestComponent>();
			foreach (global::System.Collections.Generic.KeyValuePair<global::UnityTest.ITestComponent, global::System.Collections.Generic.HashSet<global::UnityTest.ITestComponent>> item in testCollection)
			{
				list.AddRange(item.Value);
			}
			return list;
		}
	}
}
