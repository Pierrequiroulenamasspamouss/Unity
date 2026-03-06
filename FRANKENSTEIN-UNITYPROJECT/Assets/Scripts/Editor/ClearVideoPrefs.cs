using UnityEngine;
using UnityEditor;

public class KampaiClearPrefsTool
{
    [MenuItem("Kampai Tools/Clear All PlayerPrefs")]
    public static void ClearAllPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("<b>[Kampai Tools]</b> All PlayerPrefs have been successfully cleared! The intro video will now play again on the next launch.");
    }
}
