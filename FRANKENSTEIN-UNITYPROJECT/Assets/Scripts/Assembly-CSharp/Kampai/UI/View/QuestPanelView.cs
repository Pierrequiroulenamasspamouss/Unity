namespace Kampai.UI.View
{
	public class QuestPanelView : global::Kampai.UI.View.PopupMenuView
	{
		public global::UnityEngine.RectTransform taskScrollViewTransform;

		public global::UnityEngine.RectTransform questTabScrollViewTransform;

		public global::UnityEngine.UI.Text questName;

		public global::UnityEngine.UI.Text cashReward;

		public global::UnityEngine.UI.Text expReward;

		public global::Kampai.UI.View.QuestPanelProgressBarView questPanelProgressBar;

		public global::Kampai.UI.View.QuestLineProgressBarView questLineProgressBar;

		public global::Kampai.UI.View.CurrentQuestView currentQuestView;

		internal global::Kampai.Main.ILocalizationService localizationService;

		internal global::Kampai.Game.ITimeService timeService;

		internal global::Kampai.UI.View.ModalSettings modalSettings;

		private float taskPanelWidth;

		private float normalHeight;

		private float questBookPadding;

		private global::UnityEngine.GameObject taskPanelPrefab;

		private global::System.Collections.Generic.IList<global::UnityEngine.GameObject> questStepViews = new global::System.Collections.Generic.List<global::UnityEngine.GameObject>();

		private global::System.Collections.Generic.IList<global::Kampai.UI.View.QuestView> questTabs = new global::System.Collections.Generic.List<global::Kampai.UI.View.QuestView>();

		internal void CreateQuestSteps(global::Kampai.Game.Quest quest, global::Kampai.Game.Transaction.TransactionDefinition rewardTransaction)
		{
			foreach (global::UnityEngine.GameObject questStepView in questStepViews)
			{
				global::UnityEngine.Object.Destroy(questStepView);
			}
			questStepViews.Clear();
			SetupQuestPanelInfo(quest.GetActiveDefinition(), rewardTransaction);
			if (quest.GetActiveDefinition().SurfaceType == global::Kampai.Game.QuestSurfaceType.LimitedEvent || quest.GetActiveDefinition().SurfaceType == global::Kampai.Game.QuestSurfaceType.TimedEvent)
			{
				questPanelProgressBar.gameObject.SetActive(true);
				SetupQuestProgressBar(quest);
			}
			else
			{
				questPanelProgressBar.gameObject.SetActive(false);
			}
			if (quest.Steps == null || quest.Steps.Count == 0)
			{
				return;
			}
			int count = quest.GetActiveDefinition().QuestSteps.Count;
			for (int i = 0; i < count; i++)
			{
				global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(taskPanelPrefab) as global::UnityEngine.GameObject;
				global::UnityEngine.Transform transform = gameObject.transform;
				transform.SetParent(taskScrollViewTransform, false);
				global::UnityEngine.RectTransform rectTransform = transform as global::UnityEngine.RectTransform;
				rectTransform.localPosition = global::UnityEngine.Vector3.zero;
				rectTransform.localScale = global::UnityEngine.Vector3.one;
				rectTransform.offsetMin = new global::UnityEngine.Vector2(taskPanelWidth * (float)i, 0f);
				rectTransform.offsetMax = new global::UnityEngine.Vector2(taskPanelWidth * (float)(i + 1), 0f);
				global::Kampai.UI.View.QuestStepView component = gameObject.GetComponent<global::Kampai.UI.View.QuestStepView>();
				component.questInstanceID = quest.ID;
				component.stepNumber = i;
				component.stepDefinition = quest.GetActiveDefinition().QuestSteps[i];
				component.step = quest.Steps[i];
				if (modalSettings.enableGotoThrob)
				{
					component.HighlightGoTo(true);
				}
				if (modalSettings.enableDeliverThrob)
				{
					component.HighlightDeliver(true);
				}
				if (!modalSettings.enablePurchaseButtons)
				{
					component.SetTaskButtonState(true);
				}
				questStepViews.Add(gameObject);
			}
			taskScrollViewTransform.offsetMin = new global::UnityEngine.Vector2(0f, 0f);
			taskScrollViewTransform.offsetMax = new global::UnityEngine.Vector2((int)((float)quest.GetActiveDefinition().QuestSteps.Count * taskPanelWidth), 0f);
		}

		internal void SetupQuestProgressBar(global::Kampai.Game.Quest quest)
		{
			global::Kampai.Game.TimedQuestDefinition timedQuestDefinition = quest.GetActiveDefinition() as global::Kampai.Game.TimedQuestDefinition;
			if (timedQuestDefinition != null)
			{
				questPanelProgressBar.Init(quest.UTCQuestStartTime + timedQuestDefinition.Duration, timeService);
			}
			global::Kampai.Game.LimitedQuestDefinition limitedQuestDefinition = quest.GetActiveDefinition() as global::Kampai.Game.LimitedQuestDefinition;
			if (limitedQuestDefinition != null)
			{
				questPanelProgressBar.Init(limitedQuestDefinition.ServerStopTimeUTC, timeService);
			}
		}

		internal void CloseView()
		{
			currentQuestView.RemoveCoroutine();
			foreach (global::Kampai.UI.View.QuestView questTab in questTabs)
			{
				questTab.RemoveCoroutine();
			}
			Close();
		}

		private void ClearTabs()
		{
			foreach (global::Kampai.UI.View.QuestView questTab in questTabs)
			{
				global::UnityEngine.Object.Destroy(questTab.gameObject);
			}
			questTabs.Clear();
		}

		internal void InitCurrentQuestImage(global::Kampai.Game.Quest currentQuest, global::Kampai.UI.IFancyUIService fancyUIService, global::Kampai.UI.DummyCharacterType characterType)
		{
			currentQuestView.Init(currentQuest, fancyUIService, characterType);
		}

		internal void SetCurrentQuestImage(global::Kampai.Game.Quest currentQuest, global::Kampai.UI.DummyCharacterType characterType)
		{
			currentQuestView.UpdateQuest(currentQuest, characterType);
		}

		internal void InitQuestTabs(global::System.Collections.Generic.ICollection<global::Kampai.Game.Quest> quests, int selectedQuestID)
		{
			ClearTabs();
			global::UnityEngine.GameObject gameObject = global::Kampai.Util.KampaiResources.Load("cmp_QuestBookIcon") as global::UnityEngine.GameObject;
			global::Kampai.UI.View.QuestView component = gameObject.GetComponent<global::Kampai.UI.View.QuestView>();
			questBookPadding = component.PaddingInPixels;
			global::UnityEngine.RectTransform rectTransform = gameObject.transform as global::UnityEngine.RectTransform;
			normalHeight = rectTransform.sizeDelta.y;
			int num = 0;
			foreach (global::Kampai.Game.Quest quest in quests)
			{
				if (quest.state == global::Kampai.Game.QuestState.RunningTasks && quest.GetActiveDefinition().SurfaceType != global::Kampai.Game.QuestSurfaceType.ProcedurallyGenerated && quest.ID != selectedQuestID)
				{
					InitQuestBookPrefab(gameObject, normalHeight, num, quest);
					num++;
				}
			}
			questTabScrollViewTransform.sizeDelta = new global::UnityEngine.Vector2(0f, (float)num * (normalHeight + questBookPadding));
		}

		internal void SwapQuest(global::Kampai.Game.Quest newQuest, int oldQuestID)
		{
			foreach (global::Kampai.UI.View.QuestView questTab in questTabs)
			{
				if (questTab.quest.ID == oldQuestID)
				{
					questTab.quest = newQuest;
					questTab.UpdateQuest();
					break;
				}
			}
		}

		private void InitQuestBookPrefab(global::UnityEngine.GameObject prefab, float height, int index, global::Kampai.Game.Quest quest)
		{
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(prefab) as global::UnityEngine.GameObject;
			global::UnityEngine.Transform transform = gameObject.transform;
			transform.SetParent(questTabScrollViewTransform);
			global::UnityEngine.RectTransform rectTransform = transform as global::UnityEngine.RectTransform;
			rectTransform.localPosition = global::UnityEngine.Vector3.zero;
			rectTransform.localScale = global::UnityEngine.Vector3.one;
			float num = (normalHeight + questBookPadding) * (float)index;
			rectTransform.offsetMin = new global::UnityEngine.Vector2(0f, num);
			rectTransform.offsetMax = new global::UnityEngine.Vector2(0f, num + height);
			global::Kampai.UI.View.QuestView component = gameObject.GetComponent<global::Kampai.UI.View.QuestView>();
			component.quest = quest;
			questTabs.Add(component);
		}

		public override void Init()
		{
			base.Init();
			global::UnityEngine.RectTransform rectTransform = base.transform as global::UnityEngine.RectTransform;
			rectTransform.offsetMin = global::UnityEngine.Vector2.zero;
			rectTransform.offsetMax = global::UnityEngine.Vector2.zero;
			float num = rectTransform.anchorMax.x - rectTransform.anchorMin.x;
			rectTransform = taskScrollViewTransform.parent as global::UnityEngine.RectTransform;
			taskPanelWidth = (rectTransform.anchorMax.x - rectTransform.anchorMin.x) * num * (float)global::UnityEngine.Screen.width / UIUtils.GetHeightScale() / 3f;
			taskPanelPrefab = global::Kampai.Util.KampaiResources.Load("cmp_TaskPanel") as global::UnityEngine.GameObject;
			Open();
		}

		private void SetupQuestPanelInfo(global::Kampai.Game.QuestDefinition def, global::Kampai.Game.Transaction.TransactionDefinition rewardTransaction)
		{
			if (def.LocalizedKey != null)
			{
				questName.text = localizationService.GetString(def.LocalizedKey);
			}
			else
			{
				questName.text = " ";
			}
			if (rewardTransaction != null)
			{
				cashReward.text = global::Kampai.Game.Transaction.TransactionUtil.SumOutputsForStaticItem(rewardTransaction, global::Kampai.Game.StaticItem.GRIND_CURRENCY_ID).ToString();
				expReward.text = global::Kampai.Game.Transaction.TransactionUtil.SumOutputsForStaticItem(rewardTransaction, global::Kampai.Game.StaticItem.XP_ID).ToString();
			}
		}
	}
}
