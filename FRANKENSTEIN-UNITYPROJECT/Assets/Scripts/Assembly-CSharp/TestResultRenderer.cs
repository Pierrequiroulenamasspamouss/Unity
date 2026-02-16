public class TestResultRenderer
{
	private static class Styles
	{
		public static global::UnityEngine.GUIStyle succeedLabelStyle;

		public static global::UnityEngine.GUIStyle failedLabelStyle;

		public static global::UnityEngine.GUIStyle failedMessagesStyle;

		static Styles()
		{
			succeedLabelStyle = new global::UnityEngine.GUIStyle("label");
			succeedLabelStyle.normal.textColor = global::UnityEngine.Color.green;
			succeedLabelStyle.fontSize = 48;
			failedLabelStyle = new global::UnityEngine.GUIStyle("label");
			failedLabelStyle.normal.textColor = global::UnityEngine.Color.red;
			failedLabelStyle.fontSize = 32;
			failedMessagesStyle = new global::UnityEngine.GUIStyle("label");
			failedMessagesStyle.wordWrap = false;
			failedMessagesStyle.richText = true;
		}
	}

	private global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.List<ITestResult>> testCollection = new global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.List<ITestResult>>();

	private bool showResults;

	private global::UnityEngine.Vector2 scrollPosition;

	public void ShowResults()
	{
		showResults = true;
		global::UnityEngine.Screen.showCursor = true;
	}

	public void AddResults(string sceneName, ITestResult result)
	{
		if (!testCollection.ContainsKey(sceneName))
		{
			testCollection.Add(sceneName, new global::System.Collections.Generic.List<ITestResult>());
		}
		testCollection[sceneName].Add(result);
	}

	public void Draw()
	{
		if (!showResults)
		{
			return;
		}
		if (testCollection.Count == 0)
		{
			global::UnityEngine.GUILayout.Label("All test succeeded", TestResultRenderer.Styles.succeedLabelStyle, global::UnityEngine.GUILayout.Width(600f));
		}
		else
		{
			int num = 0;
			foreach (global::System.Collections.Generic.KeyValuePair<string, global::System.Collections.Generic.List<ITestResult>> item in testCollection)
			{
				num += item.Value.Count;
			}
			global::UnityEngine.GUILayout.Label(num + " tests failed!", TestResultRenderer.Styles.failedLabelStyle);
			scrollPosition = global::UnityEngine.GUILayout.BeginScrollView(scrollPosition, global::UnityEngine.GUILayout.ExpandWidth(true));
			string text = string.Empty;
			foreach (global::System.Collections.Generic.KeyValuePair<string, global::System.Collections.Generic.List<ITestResult>> item2 in testCollection)
			{
				text = text + "<b><size=18>" + item2.Key + "</size></b>\n";
				text += string.Join("\n", global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Select(global::System.Linq.Enumerable.Where(item2.Value, (ITestResult result) => !result.IsSuccess), (ITestResult result) => string.Concat(result.Name, " ", result.ResultState, "\n", result.Message))));
			}
			global::UnityEngine.GUILayout.TextArea(text, TestResultRenderer.Styles.failedMessagesStyle);
			global::UnityEngine.GUILayout.EndScrollView();
		}
		if (global::UnityEngine.GUILayout.Button("Close"))
		{
			global::UnityEngine.Application.Quit();
		}
	}
}
