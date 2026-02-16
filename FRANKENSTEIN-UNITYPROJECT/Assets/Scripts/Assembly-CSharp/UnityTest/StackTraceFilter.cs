namespace UnityTest
{
	public class StackTraceFilter
	{
		public static string Filter(string stack)
		{
			if (stack == null)
			{
				return null;
			}
			global::System.IO.StringWriter stringWriter = new global::System.IO.StringWriter();
			global::System.IO.StringReader stringReader = new global::System.IO.StringReader(stack);
			try
			{
				string text;
				while ((text = stringReader.ReadLine()) != null)
				{
					if (!FilterLine(text))
					{
						stringWriter.WriteLine(text.Trim());
					}
				}
			}
			catch (global::System.Exception)
			{
				return stack;
			}
			return stringWriter.ToString();
		}

		private static bool FilterLine(string line)
		{
			string[] array = new string[8] { "NUnit.Core.TestCase", "NUnit.Core.ExpectedExceptionTestCase", "NUnit.Core.TemplateTestCase", "NUnit.Core.TestResult", "NUnit.Core.TestSuite", "NUnit.Framework.Assertion", "NUnit.Framework.Assert", "System.Reflection.MonoMethod" };
			for (int i = 0; i < array.Length; i++)
			{
				if (line.IndexOf(array[i]) > 0)
				{
					return true;
				}
			}
			return false;
		}
	}
}
