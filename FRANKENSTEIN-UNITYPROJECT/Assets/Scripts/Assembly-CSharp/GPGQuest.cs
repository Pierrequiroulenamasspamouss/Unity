public class GPGQuest
{
	public string questId;

	public string name;

	public string questDescription;

	public string iconUrl;

	public string bannerUrl;

	public int state;

	public global::System.DateTime startTimestamp;

	public global::System.DateTime expirationTimestamp;

	public global::System.DateTime acceptedTimestamp;

	public GPGQuestMilestone currentMilestone;

	public GPGQuestState stateEnum
	{
		get
		{
			return (GPGQuestState)state;
		}
	}
}
