public class GPGTurnBasedMatch
{
	private string data;

	public bool canRematch;

	public string matchDescription;

	public string matchId;

	public int matchNumber;

	public int matchVersion;

	public string pendingParticipantId;

	public string localParticipantId;

	public int statusInt;

	public int userMatchStatusInt;

	public int availableAutoMatchSlots;

	public global::System.Collections.Generic.List<GPGTurnBasedParticipant> players;

	public bool hasDataAvailable
	{
		get
		{
			return data != null;
		}
	}

	public byte[] matchData
	{
		get
		{
			return (data == null) ? null : global::System.Convert.FromBase64String(data);
		}
	}

	public GPGTurnBasedMatchStatus status
	{
		get
		{
			return (GPGTurnBasedMatchStatus)(int)global::System.Enum.ToObject(typeof(GPGTurnBasedMatchStatus), statusInt);
		}
	}

	public string statusString
	{
		get
		{
			return status.ToString();
		}
	}

	public GPGTurnBasedUserMatchStatus userMatchStatus
	{
		get
		{
			return (GPGTurnBasedUserMatchStatus)(int)global::System.Enum.ToObject(typeof(GPGTurnBasedUserMatchStatus), userMatchStatusInt);
		}
	}

	public string userMatchStatusString
	{
		get
		{
			return userMatchStatus.ToString();
		}
	}

	public bool isLocalPlayersTurn
	{
		get
		{
			return userMatchStatus == GPGTurnBasedUserMatchStatus.YourTurn;
		}
	}

	public override string ToString()
	{
		return global::Prime31.JsonFormatter.prettyPrint(global::Prime31.Json.encode(this));
	}
}
