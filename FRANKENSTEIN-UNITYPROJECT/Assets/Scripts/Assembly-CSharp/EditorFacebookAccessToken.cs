public class EditorFacebookAccessToken : global::UnityEngine.MonoBehaviour
{
	private const float windowWidth = 592f;

	private float windowHeight = 200f;

	private string accessToken = string.Empty;

	private bool isLoggingIn;

	private static global::UnityEngine.GUISkin fbSkin;

	private global::UnityEngine.GUIStyle greyButton;

	private global::System.Collections.IEnumerator Start()
	{
		if (!(fbSkin != null))
		{
			string downloadUrl = global::Facebook.IntegratedPluginCanvasLocation.FbSkinUrl;
			global::UnityEngine.WWW www = new global::UnityEngine.WWW(downloadUrl);
			yield return www;
			if (www.error != null)
			{
				FbDebug.Error("Could not find the Facebook Skin: " + www.error);
				yield break;
			}
			fbSkin = www.assetBundle.mainAsset as global::UnityEngine.GUISkin;
			www.assetBundle.Unload(false);
		}
	}

	private void OnGUI()
	{
		float top = (float)(global::UnityEngine.Screen.height / 2) - windowHeight / 2f;
		float left = (float)(global::UnityEngine.Screen.width / 2) - 296f;
		if (fbSkin != null)
		{
			global::UnityEngine.GUI.skin = fbSkin;
			greyButton = fbSkin.GetStyle("greyButton");
		}
		else
		{
			greyButton = global::UnityEngine.GUI.skin.button;
		}
		global::UnityEngine.GUI.ModalWindow(GetHashCode(), new global::UnityEngine.Rect(left, top, 592f, windowHeight), OnGUIDialog, "Unity Editor Facebook Login");
	}

	private void OnGUIDialog(int windowId)
	{
		global::UnityEngine.GUI.enabled = !isLoggingIn;
		global::UnityEngine.GUILayout.Space(10f);
		global::UnityEngine.GUILayout.BeginHorizontal();
		global::UnityEngine.GUILayout.BeginVertical();
		global::UnityEngine.GUILayout.Space(10f);
		global::UnityEngine.GUILayout.Label("User Access Token:");
		global::UnityEngine.GUILayout.EndVertical();
		accessToken = global::UnityEngine.GUILayout.TextField(accessToken, global::UnityEngine.GUI.skin.textArea, global::UnityEngine.GUILayout.MinWidth(400f));
		global::UnityEngine.GUILayout.EndHorizontal();
		global::UnityEngine.GUILayout.Space(20f);
		global::UnityEngine.GUILayout.BeginHorizontal();
		if (global::UnityEngine.GUILayout.Button("Find Access Token"))
		{
			global::UnityEngine.Application.OpenURL(string.Format("https://developers.facebook.com/tools/accesstoken/?app_id={0}", FB.AppId));
		}
		global::UnityEngine.GUILayout.FlexibleSpace();
		global::UnityEngine.GUIContent content = new global::UnityEngine.GUIContent("Login");
		global::UnityEngine.Rect rect = global::UnityEngine.GUILayoutUtility.GetRect(content, global::UnityEngine.GUI.skin.button);
		if (global::UnityEngine.GUI.Button(rect, content))
		{
			global::Facebook.EditorFacebook component = global::Facebook.FBComponentFactory.GetComponent<global::Facebook.EditorFacebook>();
			component.AccessToken = accessToken;
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			dictionary["batch"] = "[{\"method\":\"GET\", \"relative_url\":\"me?fields=id\"},{\"method\":\"GET\", \"relative_url\":\"app?fields=id\"}]";
			dictionary["method"] = "POST";
			dictionary["access_token"] = accessToken;
			FB.API("/", global::Facebook.HttpMethod.GET, component.MockLoginCallback, dictionary);
			isLoggingIn = true;
		}
		global::UnityEngine.GUI.enabled = true;
		global::UnityEngine.GUIContent content2 = new global::UnityEngine.GUIContent("Cancel");
		global::UnityEngine.Rect rect2 = global::UnityEngine.GUILayoutUtility.GetRect(content2, greyButton);
		if (global::UnityEngine.GUI.Button(rect2, content2, greyButton))
		{
			global::Facebook.FBComponentFactory.GetComponent<global::Facebook.EditorFacebook>().MockCancelledLoginCallback();
			global::UnityEngine.Object.Destroy(this);
		}
		global::UnityEngine.GUILayout.EndHorizontal();
		if (global::UnityEngine.Event.current.type == global::UnityEngine.EventType.Repaint)
		{
			windowHeight = rect2.y + rect2.height + (float)global::UnityEngine.GUI.skin.window.padding.bottom;
		}
	}
}
