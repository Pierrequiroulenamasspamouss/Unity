public class GPGTurnBasedManager : global::Prime31.AbstractManager
{
	public static event global::System.Action<GPGTurnBasedMatch> matchChangedEvent;

	public static event global::System.Action<string> matchFailedEvent;

	public static event global::System.Action<GPGTurnBasedMatch> matchEndedEvent;

	public static event global::System.Action playerSelectorCanceledEvent;

	public static event global::System.Action<bool, string, global::System.Collections.Generic.List<GPGTurnBasedMatch>> loadMatchesCompletedEvent;

	public static event global::System.Action<bool, string> takeTurnCompletedEvent;

	public static event global::System.Action<bool, string> finishMatchCompletedEvent;

	public static event global::System.Action<bool, string> dismissMatchCompletedEvent;

	public static event global::System.Action<bool, string> leaveDuringTurnCompletedEvent;

	public static event global::System.Action<bool, string> leaveOutOfTurnCompletedEvent;

	public static event global::System.Action<GPGTurnBasedInvitation> invitationReceivedEvent;

	static GPGTurnBasedManager()
	{
		global::Prime31.AbstractManager.initialize(typeof(GPGTurnBasedManager));
	}

	private void matchChanged(string json)
	{
		if (GPGTurnBasedManager.matchChangedEvent != null)
		{
			GPGTurnBasedManager.matchChangedEvent(global::Prime31.Json.decode<GPGTurnBasedMatch>(json));
		}
	}

	private void matchFailed(string error)
	{
		global::Prime31.ActionExtensions.fire(GPGTurnBasedManager.matchFailedEvent, error);
	}

	private void matchEnded(string json)
	{
		if (GPGTurnBasedManager.matchEndedEvent != null)
		{
			GPGTurnBasedManager.matchEndedEvent(global::Prime31.Json.decode<GPGTurnBasedMatch>(json));
		}
	}

	private void playerSelectorCanceled(string empty)
	{
		global::Prime31.ActionExtensions.fire(GPGTurnBasedManager.playerSelectorCanceledEvent);
	}

	private void loadMatchesFailed(string error)
	{
		GPGTurnBasedManager.loadMatchesCompletedEvent(false, error, null);
	}

	private void loadMatchesSucceeded(string json)
	{
		global::UnityEngine.Debug.Log("WOOOT " + json);
		if (GPGTurnBasedManager.loadMatchesCompletedEvent != null)
		{
			global::Prime31.ActionExtensions.fire(GPGTurnBasedManager.loadMatchesCompletedEvent, true, null, global::Prime31.Json.decode<global::System.Collections.Generic.List<GPGTurnBasedMatch>>(json));
		}
	}

	private void takeTurnFailed(string error)
	{
		global::Prime31.ActionExtensions.fire(GPGTurnBasedManager.takeTurnCompletedEvent, false, error);
	}

	private void takeTurnSucceeded(string empty)
	{
		global::Prime31.ActionExtensions.fire(GPGTurnBasedManager.takeTurnCompletedEvent, true, null);
	}

	private void finishMatchFailed(string error)
	{
		global::Prime31.ActionExtensions.fire(GPGTurnBasedManager.finishMatchCompletedEvent, false, error);
	}

	private void finishMatchSucceeded(string empty)
	{
		global::Prime31.ActionExtensions.fire(GPGTurnBasedManager.finishMatchCompletedEvent, true, null);
	}

	private void dismissMatchFailed(string error)
	{
		global::Prime31.ActionExtensions.fire(GPGTurnBasedManager.dismissMatchCompletedEvent, false, error);
	}

	private void dismissMatchSucceeded(string empty)
	{
		global::Prime31.ActionExtensions.fire(GPGTurnBasedManager.dismissMatchCompletedEvent, true, null);
	}

	private void leaveDuringTurnFailed(string error)
	{
		global::Prime31.ActionExtensions.fire(GPGTurnBasedManager.leaveDuringTurnCompletedEvent, false, error);
	}

	private void leaveDuringTurnSucceeded(string empty)
	{
		global::Prime31.ActionExtensions.fire(GPGTurnBasedManager.leaveDuringTurnCompletedEvent, true, null);
	}

	private void leaveOutOfTurnFailed(string error)
	{
		global::Prime31.ActionExtensions.fire(GPGTurnBasedManager.leaveOutOfTurnCompletedEvent, false, error);
	}

	private void leaveOutOfTurnSucceeded(string empty)
	{
		global::Prime31.ActionExtensions.fire(GPGTurnBasedManager.leaveOutOfTurnCompletedEvent, true, null);
	}

	private void invitationReceived(string json)
	{
		if (GPGTurnBasedManager.invitationReceivedEvent != null)
		{
			GPGTurnBasedManager.invitationReceivedEvent(global::Prime31.Json.decode<GPGTurnBasedInvitation>(json));
		}
	}
}
