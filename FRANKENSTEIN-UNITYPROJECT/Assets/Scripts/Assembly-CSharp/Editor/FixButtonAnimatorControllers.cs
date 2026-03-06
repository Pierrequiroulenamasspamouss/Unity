#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Collections.Generic;

/// <summary>
/// Editor tool to fix Animator Controllers used by UI Buttons in Unity 4.7.
/// Adds missing standard UI button states: Normal, Highlighted, Pressed, Disabled.
/// Run via: Tools > Fix Button Animator Controllers
/// </summary>
public class FixButtonAnimatorControllers : MonoBehaviour
{
    private static readonly string[] RequiredStates = { "Normal", "Highlighted", "Pressed", "Disabled" };

    [MenuItem("Tools/Fix Button Animator Controllers")]
    static void Fix()
    {
        string[] guids = AssetDatabase.FindAssets("t:AnimatorController");
        int fixedCount = 0;
        int total = 0;

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            AnimatorController controller = AssetDatabase.LoadAssetAtPath(path, typeof(AnimatorController)) as AnimatorController;
            if (controller == null) continue;
            total++;

            bool dirty = false;
            for (int li = 0; li < controller.layerCount; li++)
            {
                AnimatorControllerLayer layer = controller.GetLayer(li);
                StateMachine sm = layer.stateMachine;

                // Collect existing state names
                List<string> existingNames = new List<string>();
                for (int si = 0; si < sm.stateCount; si++)
                    existingNames.Add(sm.GetState(si).name);

                // Add any missing required states
                foreach (string required in RequiredStates)
                {
                    if (!existingNames.Contains(required))
                    {
                        sm.AddState(required);
                        existingNames.Add(required);
                        dirty = true;
                        Debug.Log(string.Format("[FixButtonAnimators] Added state '{0}' to '{1}'", required, controller.name));
                    }
                }
            }

            if (dirty)
            {
                EditorUtility.SetDirty(controller);
                fixedCount++;
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        string msg = string.Format("Scanned: {0} controllers\nFixed: {1} controllers", total, fixedCount);
        EditorUtility.DisplayDialog("Fix Button Animator Controllers", "Done!\n" + msg, "OK");
        Debug.Log("[FixButtonAnimators] " + msg);
    }
}
#endif
