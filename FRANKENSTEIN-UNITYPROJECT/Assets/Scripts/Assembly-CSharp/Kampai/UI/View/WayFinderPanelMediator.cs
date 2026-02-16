namespace Kampai.UI.View
{
	public class WayFinderPanelMediator : global::strange.extensions.mediation.impl.Mediator
	{
		[Inject]
		public global::Kampai.UI.View.WayFinderPanelView View { get; set; }

		[Inject]
		public global::Kampai.UI.View.CreateWayFinderSignal CreateWayFinderSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.RemoveWayFinderSignal RemoveWayFinderSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.GetWayFinderSignal GetWayFinderSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowAllWayFindersSignal ShowAllWayFindersSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.HideAllWayFindersSignal HideAllWayFindersSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.SetLimitTikiBarWayFindersSignal SetLimitTikiBarWayFindersSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.UpdateWayFinderPrioritySignal UpdateWayFinderPrioritySignal { get; set; }

		[Inject]
		public global::Kampai.Game.IPrestigeService PrestigeService { get; set; }

		[Inject]
		public global::Kampai.Game.ITikiBarService TikiBarService { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService PlayerService { get; set; }

		[Inject]
		public global::Kampai.UI.IPositionService PositionService { get; set; }

		[Inject]
		public global::Kampai.UI.View.AddQuestToExistingWayFinderSignal AddQuestToExistingWayFinderSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.RemoveQuestFromExistingWayFinderSignal RemoveQuestFromExistingWayFinderSignal { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger Logger { get; set; }

		public override void OnRegister()
		{
			View.Init(Logger, TikiBarService, PlayerService, PrestigeService, PositionService);
			CreateWayFinderSignal.AddListener(CreateWayFinder);
			RemoveWayFinderSignal.AddListener(RemoveWayFinder);
			GetWayFinderSignal.AddListener(GetWayFinder);
			ShowAllWayFindersSignal.AddListener(ShowAllWayFinders);
			HideAllWayFindersSignal.AddListener(HideAllWayFinders);
			SetLimitTikiBarWayFindersSignal.AddListener(SetLimitTikiBarWayFinders);
			UpdateWayFinderPrioritySignal.AddListener(UpdateWayFinderPriority);
			AddQuestToExistingWayFinderSignal.AddListener(AddQuestToExistingWayFinder);
			RemoveQuestFromExistingWayFinderSignal.AddListener(RemoveQuestFromExistingWayFinder);
		}

		public override void OnRemove()
		{
			View.Cleanup();
			CreateWayFinderSignal.RemoveListener(CreateWayFinder);
			RemoveWayFinderSignal.RemoveListener(RemoveWayFinder);
			GetWayFinderSignal.RemoveListener(GetWayFinder);
			ShowAllWayFindersSignal.RemoveListener(ShowAllWayFinders);
			HideAllWayFindersSignal.RemoveListener(HideAllWayFinders);
			SetLimitTikiBarWayFindersSignal.RemoveListener(SetLimitTikiBarWayFinders);
			UpdateWayFinderPrioritySignal.RemoveListener(UpdateWayFinderPriority);
			AddQuestToExistingWayFinderSignal.RemoveListener(AddQuestToExistingWayFinder);
			RemoveQuestFromExistingWayFinderSignal.RemoveListener(RemoveQuestFromExistingWayFinder);
		}

		private void CreateWayFinder(global::Kampai.UI.View.WayFinderSettings settings)
		{
			View.CreateWayFinder(settings);
		}

		private void RemoveWayFinder(int trackedId)
		{
			View.RemoveWayFinder(trackedId);
		}

		private void GetWayFinder(int trackedId, global::System.Action<int, global::Kampai.UI.View.IWayFinderView> callback)
		{
			callback(trackedId, View.GetWayFinder(trackedId));
		}

		private void ShowAllWayFinders()
		{
			View.ShowAllWayFinders();
		}

		private void HideAllWayFinders()
		{
			View.HideAllWayFinders();
		}

		private void SetLimitTikiBarWayFinders(bool limitWayFinders)
		{
			View.SetLimitTikiBarWayFinders(limitWayFinders);
		}

		private void UpdateWayFinderPriority()
		{
			View.UpdateWayFinderPriority();
		}

		private void AddQuestToExistingWayFinder(int questDefId, int trackedId)
		{
			View.AddQuestToExistingWayFinder(questDefId, trackedId);
		}

		private void RemoveQuestFromExistingWayFinder(int questDefId, int trackedId)
		{
			View.RemoveQuestFromExistingWayFinder(questDefId, trackedId);
		}
	}
}
