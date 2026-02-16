public class GPGMultiplayerManager : global::Prime31.AbstractManager
{
	private class GPGRoomUpdateInfo
	{
		public GPGRoom room { get; set; }

		public int statusCode { get; set; }

		public GPGRoomUpdateStatus status
		{
			get
			{
				return (GPGRoomUpdateStatus)statusCode;
			}
		}
	}

	public static event global::System.Action<string> onInvitationReceivedEvent;

	public static event global::System.Action<string> onInvitationRemovedEvent;

	public static event global::System.Action<bool> onWaitingRoomCompletedEvent;

	public static event global::System.Action<bool> onInvitationInboxCompletedEvent;

	public static event global::System.Action<bool> onInvitePlayersCompletedEvent;

	public static event global::System.Action<GPGRoom, GPGRoomUpdateStatus> onJoinedRoomEvent;

	public static event global::System.Action onLeftRoomEvent;

	public static event global::System.Action<GPGRoom, GPGRoomUpdateStatus> onRoomConnectedEvent;

	public static event global::System.Action<GPGRoom, GPGRoomUpdateStatus> onRoomCreatedEvent;

	public static event global::System.Action<string, byte[]> onRealTimeMessageReceivedEvent;

	public static event global::System.Action onConnectedToRoomEvent;

	public static event global::System.Action onDisconnectedFromRoomEvent;

	public static event global::System.Action<string> onP2PConnectedEvent;

	public static event global::System.Action<string> onP2PDisconnectedEvent;

	public static event global::System.Action<string> onPeerDeclinedEvent;

	public static event global::System.Action<string> onPeerInvitedToRoomEvent;

	public static event global::System.Action<string> onPeerJoinedEvent;

	public static event global::System.Action<string> onPeerLeftEvent;

	public static event global::System.Action<string> onPeerConnectedEvent;

	public static event global::System.Action<string> onPeerDisconnectedEvent;

	public static event global::System.Action<GPGRoom> onRoomAutoMatchingEvent;

	public static event global::System.Action<GPGRoom> onRoomConnectingEvent;

	static GPGMultiplayerManager()
	{
		global::Prime31.AbstractManager.initialize(typeof(GPGMultiplayerManager));
	}

	private void onInvitationReceived(string invitationId)
	{
		global::Prime31.ActionExtensions.fire(GPGMultiplayerManager.onInvitationReceivedEvent, invitationId);
	}

	private void onInvitationRemoved(string invitationId)
	{
		global::Prime31.ActionExtensions.fire(GPGMultiplayerManager.onInvitationRemovedEvent, invitationId);
	}

	private void onWaitingRoomCompleted(string success)
	{
		global::Prime31.ActionExtensions.fire(GPGMultiplayerManager.onWaitingRoomCompletedEvent, success == "1");
	}

	private void onInvitationInboxCompleted(string success)
	{
		global::Prime31.ActionExtensions.fire(GPGMultiplayerManager.onInvitationInboxCompletedEvent, success == "1");
	}

	private void onInvitePlayersCompleted(string success)
	{
		global::Prime31.ActionExtensions.fire(GPGMultiplayerManager.onInvitePlayersCompletedEvent, success == "1");
	}

	private void onJoinedRoom(string json)
	{
		GPGMultiplayerManager.GPGRoomUpdateInfo gPGRoomUpdateInfo = global::Prime31.Json.decode<GPGMultiplayerManager.GPGRoomUpdateInfo>(json);
		global::Prime31.ActionExtensions.fire(GPGMultiplayerManager.onJoinedRoomEvent, gPGRoomUpdateInfo.room, gPGRoomUpdateInfo.status);
	}

	private void onLeftRoom(string empty)
	{
		global::Prime31.ActionExtensions.fire(GPGMultiplayerManager.onLeftRoomEvent);
	}

	private void onRoomConnected(string json)
	{
		GPGMultiplayerManager.GPGRoomUpdateInfo gPGRoomUpdateInfo = global::Prime31.Json.decode<GPGMultiplayerManager.GPGRoomUpdateInfo>(json);
		global::Prime31.ActionExtensions.fire(GPGMultiplayerManager.onRoomConnectedEvent, gPGRoomUpdateInfo.room, gPGRoomUpdateInfo.status);
	}

	private void onRoomCreated(string json)
	{
		GPGMultiplayerManager.GPGRoomUpdateInfo gPGRoomUpdateInfo = global::Prime31.Json.decode<GPGMultiplayerManager.GPGRoomUpdateInfo>(json);
		global::Prime31.ActionExtensions.fire(GPGMultiplayerManager.onRoomCreatedEvent, gPGRoomUpdateInfo.room, gPGRoomUpdateInfo.status);
	}

	public static void onRealTimeMessageReceived(string senderParticipantId, byte[] message)
	{
		if (GPGMultiplayerManager.onRealTimeMessageReceivedEvent != null)
		{
			GPGMultiplayerManager.onRealTimeMessageReceivedEvent(senderParticipantId, message);
		}
	}

	private void onConnectedToRoom(string empty)
	{
		global::Prime31.ActionExtensions.fire(GPGMultiplayerManager.onConnectedToRoomEvent);
	}

	private void onDisconnectedFromRoom(string empty)
	{
		global::Prime31.ActionExtensions.fire(GPGMultiplayerManager.onDisconnectedFromRoomEvent);
	}

	private void onP2PConnected(string participantId)
	{
		global::Prime31.ActionExtensions.fire(GPGMultiplayerManager.onP2PConnectedEvent, participantId);
	}

	private void onP2PDisconnected(string participantId)
	{
		global::Prime31.ActionExtensions.fire(GPGMultiplayerManager.onP2PDisconnectedEvent, participantId);
	}

	private void onPeerDeclined(string id)
	{
		global::Prime31.ActionExtensions.fire(GPGMultiplayerManager.onPeerDeclinedEvent, id);
	}

	private void onPeerInvitedToRoom(string id)
	{
		global::Prime31.ActionExtensions.fire(GPGMultiplayerManager.onPeerInvitedToRoomEvent, id);
	}

	private void onPeerJoined(string id)
	{
		global::Prime31.ActionExtensions.fire(GPGMultiplayerManager.onPeerJoinedEvent, id);
	}

	private void onPeerLeft(string id)
	{
		global::Prime31.ActionExtensions.fire(GPGMultiplayerManager.onPeerLeftEvent, id);
	}

	private void onPeerConnected(string id)
	{
		global::Prime31.ActionExtensions.fire(GPGMultiplayerManager.onPeerConnectedEvent, id);
	}

	private void onPeerDisconnected(string id)
	{
		global::Prime31.ActionExtensions.fire(GPGMultiplayerManager.onPeerDisconnectedEvent, id);
	}

	private void onRoomAutoMatching(string json)
	{
		global::Prime31.ActionExtensions.fire(GPGMultiplayerManager.onRoomAutoMatchingEvent, global::Prime31.Json.decode<GPGRoom>(json));
	}

	private void onRoomConnecting(string json)
	{
		global::Prime31.ActionExtensions.fire(GPGMultiplayerManager.onRoomConnectingEvent, global::Prime31.Json.decode<GPGRoom>(json));
	}
}
