//public class NimbleBridge_Tracking
//{
//	private NimbleBridge_Tracking()
//	{
//	}

#if UNITY_ANDROID && !UNITY_EDITOR
//	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
//	private static extern void NimbleBridge_Tracking_logEvent(string type, global::System.IntPtr map);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
//	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
//	private static extern bool NimbleBridge_Tracking_isEnabled();
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
//	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
//	private static extern void NimbleBridge_Tracking_setEnabled(bool enable);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
//	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
//	private static extern void NimbleBridge_Tracking_addCustomSessionData(string keyString, string valueString);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
//	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
//	private static extern void NimbleBridge_Tracking_clearCustomSessionData();
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
//	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
//	private static extern bool NimbleBridge_Tracking_isNimbleStandardEvent(string type);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
//	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
//	private static extern bool NimbleBridge_Tracking_isEventTypeEqual(string _event, string otherEvent);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
//	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
//	private static extern bool NimbleBridge_Tracking_isEventTypeMemberOfSet(string _event, string[] eventTypeArray);
#endif

//	public static NimbleBridge_Tracking GetComponent()
//	{
//		return new NimbleBridge_Tracking();
//	}

//	public void LogEvent(string type, global::System.Collections.Generic.Dictionary<string, string> parameters)
//	{
//		global::System.IntPtr intPtr = global::System.IntPtr.Zero;
//		try
//		{
//			intPtr = MarshalUtility.ConvertDictionaryToPtr(parameters);
//			NimbleBridge_Tracking_logEvent(type, intPtr);
//		}
//		finally
//		{
//			if (intPtr != global::System.IntPtr.Zero)
//			{
//				MarshalUtility.DisposeMapPtr(intPtr);
//			}
//		}
//	}

//	public bool IsEnabled()
//	{
//		return NimbleBridge_Tracking_isEnabled();
//	}

//	public void SetEnabled(bool enable)
//	{
//		NimbleBridge_Tracking_setEnabled(enable);
//	}

//	public void AddCustomSessionValue(string key, string value)
//	{
//		NimbleBridge_Tracking_addCustomSessionData(key, value);
//	}

//	public void ClearCustomSessionData()
//	{
//		NimbleBridge_Tracking_clearCustomSessionData();
//	}

//	public static bool IsNimbleStandardEvent(string type)
//	{
//		return NimbleBridge_Tracking_isNimbleStandardEvent(type);
//	}

//	public static bool IsEventTypeEqual(string _event, string otherEvent)
//	{
//		return NimbleBridge_Tracking_isEventTypeEqual(_event, otherEvent);
//	}

//	public static bool IsEventTypeMemberOfSet(string _event, global::System.Collections.Generic.List<string> eventTypeSet)
//	{
//		string[] array = new string[eventTypeSet.Count + 1];
//		eventTypeSet.CopyTo(array);
//		array[eventTypeSet.Count] = null;
//		return NimbleBridge_Tracking_isEventTypeMemberOfSet(_event, array);
//	}
//}
using UnityEngine;
using System.Collections.Generic;

public class NimbleBridge_Tracking
{
    private NimbleBridge_Tracking()
    {
    }

    // --- NATIVE DECLARATIONS (KEPT BUT UNUSED TO PREVENT COMPILER ERRORS) ---
    // Even though we don't call them, we leave them here in case other scripts reference them via reflection,
    // but practically we just ignore them.

    public static NimbleBridge_Tracking GetComponent()
    {
        return new NimbleBridge_Tracking();
    }

    // --- SAFE IMPLEMENTATIONS BELOW ---

    public void LogEvent(string type, Dictionary<string, string> parameters)
    {
        // FIX: Removed native call and Marshal logic.
        // We simply do nothing.
        return;
    }

    public bool IsEnabled()
    {
        // FIX: Always return false.
        return false;
    }

    public void SetEnabled(bool enable)
    {
        // FIX: Do nothing.
        return;
    }

    public void AddCustomSessionValue(string key, string value)
    {
        // FIX: Do nothing.
        return;
    }

    public void ClearCustomSessionData()
    {
        // FIX: Removed native call.
        return;
    }

    public static bool IsNimbleStandardEvent(string type)
    {
        // FIX: Removed native call.
        // Returning false is safe; it just means "this isn't a special event".
        return false;
    }

    public static bool IsEventTypeEqual(string _event, string otherEvent)
    {
        // FIX: Replaced native call with standard C# string comparison.
        // This keeps logic valid without needing the DLL.
        return string.Equals(_event, otherEvent);
    }

    public static bool IsEventTypeMemberOfSet(string _event, List<string> eventTypeSet)
    {
        // FIX: Replaced native call with standard C# list check.
        if (eventTypeSet == null) return false;
        return eventTypeSet.Contains(_event);
    }
}