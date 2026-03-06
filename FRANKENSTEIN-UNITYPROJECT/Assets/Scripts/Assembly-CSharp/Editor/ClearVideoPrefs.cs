using UnityEngine;
using UnityEditor;

public class ClearVideoPrefs
{
    [MenuItem("Kampai Tools/Clear Intro Video Pref")]
    public static void ClearIntroVideoPref()
    {
        PlayerPrefs.DeleteKey("intro_video_played");
        PlayerPrefs.DeleteKey("VideoCache");
        PlayerPrefs.Save();
        Debug.Log("Intro Video PlayerPrefs cleared. The video will download and play on next launch.");
    }
}
