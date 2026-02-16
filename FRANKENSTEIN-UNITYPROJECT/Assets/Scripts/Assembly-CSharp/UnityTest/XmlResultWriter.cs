namespace UnityTest
{
	public class XmlResultWriter
	{
		private const string nUnitVersion = "2.6.2-Unity";

		private global::System.Text.StringBuilder resultWriter = new global::System.Text.StringBuilder();

		private int indend;

		private string suiteName;

		private ITestResult[] results;

		public XmlResultWriter(string suiteName, ITestResult[] results)
		{
			this.suiteName = suiteName;
			this.results = results;
		}

		public string GetTestResult()
		{
			InitializeXmlFile(suiteName, new global::UnityTest.ResultSummarizer(results));
			ITestResult[] array = results;
			foreach (ITestResult result in array)
			{
				WriteResultElement(result);
			}
			TerminateXmlFile();
			return resultWriter.ToString();
		}

		private void InitializeXmlFile(string resultsName, global::UnityTest.ResultSummarizer summaryResults)
		{
			WriteHeader();
			global::System.DateTime now = global::System.DateTime.Now;
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			dictionary.Add("name", "Unity Tests");
			dictionary.Add("total", summaryResults.TestsRun.ToString());
			dictionary.Add("errors", summaryResults.Errors.ToString());
			dictionary.Add("failures", summaryResults.Failures.ToString());
			dictionary.Add("not-run", summaryResults.TestsNotRun.ToString());
			dictionary.Add("inconclusive", summaryResults.Inconclusive.ToString());
			dictionary.Add("ignored", summaryResults.Ignored.ToString());
			dictionary.Add("skipped", summaryResults.Skipped.ToString());
			dictionary.Add("invalid", summaryResults.NotRunnable.ToString());
			dictionary.Add("date", now.ToString("yyyy-MM-dd"));
			dictionary.Add("time", now.ToString("HH:mm:ss"));
			global::System.Collections.Generic.Dictionary<string, string> attributes = dictionary;
			WriteOpeningElement("test-results", attributes);
			WriteEnvironment();
			WriteCultureInfo();
			WriteTestSuite(resultsName, summaryResults);
			WriteOpeningElement("results");
		}

		private void WriteOpeningElement(string elementName)
		{
			WriteOpeningElement(elementName, new global::System.Collections.Generic.Dictionary<string, string>());
		}

		private void WriteOpeningElement(string elementName, global::System.Collections.Generic.Dictionary<string, string> attributes)
		{
			WriteOpeningElement(elementName, attributes, false);
		}

		private void WriteOpeningElement(string elementName, global::System.Collections.Generic.Dictionary<string, string> attributes, bool closeImmediatelly)
		{
			WriteIndend();
			indend++;
			resultWriter.Append("<");
			resultWriter.Append(elementName);
			foreach (global::System.Collections.Generic.KeyValuePair<string, string> attribute in attributes)
			{
				resultWriter.AppendFormat(" {0}=\"{1}\"", attribute.Key, global::System.Security.SecurityElement.Escape(attribute.Value));
			}
			if (closeImmediatelly)
			{
				resultWriter.Append(" /");
				indend--;
			}
			resultWriter.AppendLine(">");
		}

		private void WriteIndend()
		{
			for (int i = 0; i < indend; i++)
			{
				resultWriter.Append("  ");
			}
		}

		private void WriteClosingElement(string elementName)
		{
			indend--;
			WriteIndend();
			resultWriter.AppendLine("</" + elementName + ">");
		}

		private void WriteHeader()
		{
			resultWriter.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
			resultWriter.AppendLine("<!--This file represents the results of running a test suite-->");
		}

		private static string GetEnvironmentUserName()
		{
			return global::System.Environment.UserName;
		}

		private static string GetEnvironmentMachineName()
		{
			return global::System.Environment.MachineName;
		}

		private static string GetEnvironmentUserDomainName()
		{
			return global::System.Environment.UserDomainName;
		}

		private static string GetEnvironmentVersion()
		{
			return global::System.Environment.Version.ToString();
		}

		private static string GetEnvironmentOSVersion()
		{
			return global::System.Environment.OSVersion.ToString();
		}

		private static string GetEnvironmentOSVersionPlatform()
		{
			return global::System.Environment.OSVersion.Platform.ToString();
		}

		private static string EnvironmentGetCurrentDirectory()
		{
			return global::System.Environment.CurrentDirectory;
		}

		private void WriteEnvironment()
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			dictionary.Add("nunit-version", "2.6.2-Unity");
			dictionary.Add("clr-version", GetEnvironmentVersion());
			dictionary.Add("os-version", GetEnvironmentOSVersion());
			dictionary.Add("platform", GetEnvironmentOSVersionPlatform());
			dictionary.Add("cwd", EnvironmentGetCurrentDirectory());
			dictionary.Add("machine-name", GetEnvironmentMachineName());
			dictionary.Add("user", GetEnvironmentUserName());
			dictionary.Add("user-domain", GetEnvironmentUserDomainName());
			global::System.Collections.Generic.Dictionary<string, string> attributes = dictionary;
			WriteOpeningElement("environment", attributes, true);
		}

		private void WriteCultureInfo()
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			dictionary.Add("current-culture", global::System.Globalization.CultureInfo.CurrentCulture.ToString());
			dictionary.Add("current-uiculture", global::System.Globalization.CultureInfo.CurrentUICulture.ToString());
			global::System.Collections.Generic.Dictionary<string, string> attributes = dictionary;
			WriteOpeningElement("culture-info", attributes, true);
		}

		private void WriteTestSuite(string resultsName, global::UnityTest.ResultSummarizer summaryResults)
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			dictionary.Add("name", resultsName);
			dictionary.Add("type", "Assembly");
			dictionary.Add("executed", "True");
			dictionary.Add("result", (!summaryResults.Success) ? "Failure" : "Success");
			dictionary.Add("success", (!summaryResults.Success) ? "False" : "True");
			dictionary.Add("time", summaryResults.Duration.ToString("#####0.000", global::System.Globalization.NumberFormatInfo.InvariantInfo));
			global::System.Collections.Generic.Dictionary<string, string> attributes = dictionary;
			WriteOpeningElement("test-suite", attributes);
		}

		private void WriteResultElement(ITestResult result)
		{
			StartTestElement(result);
			switch (result.ResultState)
			{
			case global::UnityTest.TestResultState.NotRunnable:
			case global::UnityTest.TestResultState.Skipped:
			case global::UnityTest.TestResultState.Ignored:
				WriteReasonElement(result);
				break;
			case global::UnityTest.TestResultState.Failure:
			case global::UnityTest.TestResultState.Error:
			case global::UnityTest.TestResultState.Cancelled:
				WriteFailureElement(result);
				break;
			case global::UnityTest.TestResultState.Inconclusive:
			case global::UnityTest.TestResultState.Success:
				if (result.Message != null)
				{
					WriteReasonElement(result);
				}
				break;
			}
			WriteClosingElement("test-case");
		}

		private void TerminateXmlFile()
		{
			WriteClosingElement("results");
			WriteClosingElement("test-suite");
			WriteClosingElement("test-results");
		}

		private void StartTestElement(ITestResult result)
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			dictionary.Add("name", result.FullName);
			dictionary.Add("executed", result.Executed.ToString());
			global::System.Collections.Generic.Dictionary<string, string> dictionary2 = dictionary;
			string empty = string.Empty;
			global::UnityTest.TestResultState resultState = result.ResultState;
			empty = ((resultState != global::UnityTest.TestResultState.Cancelled) ? result.ResultState.ToString() : global::UnityTest.TestResultState.Failure.ToString());
			dictionary2.Add("result", empty);
			if (result.Executed)
			{
				dictionary2.Add("success", result.IsSuccess.ToString());
				dictionary2.Add("time", result.Duration.ToString("#####0.000", global::System.Globalization.NumberFormatInfo.InvariantInfo));
			}
			WriteOpeningElement("test-case", dictionary2);
		}

		private void WriteReasonElement(ITestResult result)
		{
			WriteOpeningElement("reason");
			WriteOpeningElement("message");
			WriteCData(result.Message);
			WriteClosingElement("message");
			WriteClosingElement("reason");
		}

		private void WriteFailureElement(ITestResult result)
		{
			WriteOpeningElement("failure");
			WriteOpeningElement("message");
			WriteCData(result.Message);
			WriteClosingElement("message");
			WriteOpeningElement("stack-trace");
			if (result.StackTrace != null)
			{
				WriteCData(global::UnityTest.StackTraceFilter.Filter(result.StackTrace));
			}
			WriteClosingElement("stack-trace");
			WriteClosingElement("failure");
		}

		private void WriteCData(string text)
		{
			if (text.Length != 0)
			{
				resultWriter.AppendFormat("<![CDATA[{0}]]>", text);
				resultWriter.AppendLine();
			}
		}

		public void WriteToFile(string resultDestiantion, string resultFileName)
		{
			try
			{
				string text = global::System.IO.Path.Combine(resultDestiantion, resultFileName);
				global::UnityEngine.Debug.Log("Saving results in " + text);
				using (global::System.IO.FileStream stream = global::System.IO.File.OpenWrite(text))
				{
					using (global::System.IO.StreamWriter streamWriter = new global::System.IO.StreamWriter(stream, global::System.Text.Encoding.UTF8))
					{
						streamWriter.Write(GetTestResult());
					}
				}
			}
			catch (global::System.Exception exception)
			{
				global::UnityEngine.Debug.LogError("Error while opening file");
				global::UnityEngine.Debug.LogException(exception);
			}
		}
	}
}
