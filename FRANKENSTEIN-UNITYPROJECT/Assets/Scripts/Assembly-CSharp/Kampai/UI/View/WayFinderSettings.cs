namespace Kampai.UI.View
{
	public class WayFinderSettings : global::Kampai.UI.View.WorldToGlassUISettings
	{
		public int QuestDefId { get; private set; }

		public WayFinderSettings(int trackedId)
			: base(trackedId)
		{
		}

		public WayFinderSettings(int questDefId, int trackedId)
			: this(trackedId)
		{
			QuestDefId = questDefId;
		}
	}
}
