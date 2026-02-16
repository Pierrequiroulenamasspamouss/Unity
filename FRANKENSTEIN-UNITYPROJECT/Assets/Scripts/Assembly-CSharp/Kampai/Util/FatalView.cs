namespace Kampai.Util
{
	[global::System.Serializable]
	public class FatalView : global::UnityEngine.MonoBehaviour
	{
		private static string code = "0000";

		private static string title = "FatalTitle";

		private static string message = "FatalMessage";

		[global::UnityEngine.SerializeField]
		public global::UnityEngine.GameObject ErrorCode;

		[global::UnityEngine.SerializeField]
		public global::UnityEngine.GameObject ActionMessage;

		[global::UnityEngine.SerializeField]
		public global::UnityEngine.GameObject TitleMessage;

		private void OnEnable()
		{
			ErrorCode.GetComponent<global::UnityEngine.UI.Text>().text = code;
			ActionMessage.GetComponent<global::UnityEngine.UI.Text>().text = message;
			TitleMessage.GetComponent<global::UnityEngine.UI.Text>().text = title;
		}

		public static void SetFatalText(string code, string message, string title)
		{
			global::Kampai.Util.FatalView.code = code;
			global::Kampai.Util.FatalView.message = message;
			global::Kampai.Util.FatalView.title = title;
		}
	}
}
