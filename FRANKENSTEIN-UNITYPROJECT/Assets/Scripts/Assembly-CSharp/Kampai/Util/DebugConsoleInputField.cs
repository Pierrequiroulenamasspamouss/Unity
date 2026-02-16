namespace Kampai.Util
{
	public class DebugConsoleInputField : global::UnityEngine.UI.InputField
	{
		public global::UnityEngine.TouchScreenKeyboard Keyboard
		{
			get
			{
				return global::UnityEngine.UI.InputField.m_Keyboard;
			}
		}
	}
}
