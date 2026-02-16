namespace Facebook
{
	public class OGActionType
	{
		public static readonly global::Facebook.OGActionType Send = new global::Facebook.OGActionType
		{
			actionTypeValue = "send"
		};

		public static readonly global::Facebook.OGActionType AskFor = new global::Facebook.OGActionType
		{
			actionTypeValue = "askfor"
		};

		private string actionTypeValue;

		public override string ToString()
		{
			return actionTypeValue;
		}
	}
}
