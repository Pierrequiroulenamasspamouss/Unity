namespace Kampai.UI.View
{
	public class OrderBoardTicketDetailMediator : global::strange.extensions.mediation.impl.Mediator
	{
		private const float waitInBetween = 0.05f;

		private int currentPrestigePoints;

		private int neededPrestigePoints;

		private int currentPrestigeLevel;

		private int updateTimes;

		private global::System.Collections.IEnumerator fillBarRoutine;

		private bool completeFinished;

		private global::System.Action fillOrderCallBack;

		private global::System.Collections.IEnumerator PointerDownWait;

		[Inject]
		public global::Kampai.UI.View.OrderBoardTicketDetailView view { get; set; }

		[Inject]
		public global::Kampai.UI.View.OrderBoardTicketClickedSignal ticketClickedSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.OrderBoardTicketDeletedSignal ticketDeletedSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.OrderBoardPrestigeSlotFullSignal slotFullSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.OrderBoardStartFillingPrestigeBarSignal startFillingPrestigeBarSignal { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService localService { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Game.IPrestigeService characterService { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.UI.View.SetFTUETextSignal setFTUETextSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.HideItemPopupSignal hideItemPopupSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.DisplayItemPopupSignal displayItemPopupSignal { get; set; }

		[Inject]
		public global::Kampai.UI.IFancyUIService fancyUIService { get; set; }

		[Inject]
		public global::Kampai.Common.AppPauseSignal pauseSignal { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Main.MoveAudioListenerSignal moveAudioListener { get; set; }

		public override void OnRegister()
		{
			view.Init(localService);
			updateTimes = 15;
			ticketClickedSignal.AddListener(GetTicketClicked);
			ticketDeletedSignal.AddListener(ClearTicket);
			setFTUETextSignal.AddListener(SetFTUEText);
			slotFullSignal.AddListener(SetSlotFullText);
			startFillingPrestigeBarSignal.AddListener(StartFillingPrestige);
			pauseSignal.AddListener(OnPause);
		}

		public override void OnRemove()
		{
			hideItemPopupSignal.Dispatch();
			view.ClearDummyObject();
			ticketClickedSignal.RemoveListener(GetTicketClicked);
			ticketDeletedSignal.RemoveListener(ClearTicket);
			setFTUETextSignal.RemoveListener(SetFTUEText);
			slotFullSignal.RemoveListener(SetSlotFullText);
			startFillingPrestigeBarSignal.RemoveListener(StartFillingPrestige);
			pauseSignal.RemoveListener(OnPause);
			global::System.Collections.Generic.List<global::Kampai.UI.View.OrderBoardRequiredItemView> itemList = view.GetItemList();
			if (itemList != null)
			{
				foreach (global::Kampai.UI.View.OrderBoardRequiredItemView item in itemList)
				{
					if (item != null)
					{
						item.pointerUpSignal.RemoveListener(PointerUp);
						item.pointerDownSignal.RemoveListener(PointerDown);
					}
				}
			}
			if (fillBarRoutine != null && !completeFinished)
			{
				routineRunner.StopCoroutine(fillBarRoutine);
				fillOrderCallBack();
			}
		}

		private void ClearTicket()
		{
			view.TicketName.gameObject.SetActive(false);
			view.SetSlotFullText(localService.GetString("NoTicketSelected"));
			view.SetupItemCount(0);
			view.PrestigePanel.SetActive(false);
		}

		private void StartFillingPrestige(int targetBarValue, global::System.Action FillOrderCallback)
		{
			fillOrderCallBack = FillOrderCallback;
			fillBarRoutine = FillProgreeBarThenCall(targetBarValue - currentPrestigePoints, FillOrderCallback);
			routineRunner.StartCoroutine(fillBarRoutine);
		}

		private global::System.Collections.IEnumerator FillProgreeBarThenCall(int valueOffset, global::System.Action completeCallback)
		{
			float increment = (float)valueOffset / (float)updateTimes;
			float myPrestigePoints = 0f;
			view.GlowAnimation.SetActive(true);
			for (int i = 1; i <= updateTimes; i++)
			{
				myPrestigePoints = (float)currentPrestigePoints + (float)i * increment;
				view.SetPrestigeProgress(myPrestigePoints, neededPrestigePoints);
				yield return new global::UnityEngine.WaitForSeconds(0.05f);
			}
			yield return new global::UnityEngine.WaitForSeconds(0.25f);
			completeFinished = true;
			completeCallback();
		}

		private void GetTicketClicked(global::Kampai.Game.OrderBoardTicket ticket, string title, bool mute)
		{
			global::Kampai.Game.Transaction.TransactionInstance transactionInst = ticket.TransactionInst;
			int count = transactionInst.Inputs.Count;
			view.SetupItemCount(count);
			int xp = global::Kampai.Game.Transaction.TransactionUtil.ExtractQuantityFromTransaction(transactionInst, 2);
			int grind = global::Kampai.Game.Transaction.TransactionUtil.ExtractQuantityFromTransaction(transactionInst, 0);
			view.SetReward(grind, xp);
			if (ticket.CharacterDefinitionId != 0)
			{
				SetupCharacterDetail(ticket.CharacterDefinitionId);
			}
			else
			{
				view.ClearDummyObject();
				view.SetPanelState(false);
				view.SetTitle(title);
				currentPrestigePoints = 0;
			}
			for (int i = 0; i < count; i++)
			{
				global::Kampai.Util.QuantityItem quantityItem = transactionInst.Inputs[i];
				global::Kampai.Game.ItemDefinition itemDefinition = definitionService.Get<global::Kampai.Game.ItemDefinition>(quantityItem.ID);
				uint quantity = quantityItem.Quantity;
				global::UnityEngine.Sprite icon = UIUtils.LoadSpriteFromPath(itemDefinition.Image);
				if (string.IsNullOrEmpty(itemDefinition.Mask))
				{
					logger.Log(global::Kampai.Util.Logger.Level.Error, "Your Item Definition: {0} doesn' have a mask image defined for the item icon: {1}", itemDefinition.ID, itemDefinition.Image);
					itemDefinition.Mask = "btn_Circle01_mask";
				}
				global::UnityEngine.Sprite mask = UIUtils.LoadSpriteFromPath(itemDefinition.Mask);
				uint quantityByDefinitionId = playerService.GetQuantityByDefinitionId(quantityItem.ID);
				global::Kampai.UI.View.OrderBoardRequiredItemView orderBoardRequiredItemView = view.CreateRequiredItem(i, quantity, quantityByDefinitionId, icon, mask);
				orderBoardRequiredItemView.ItemDefinitionID = quantityItem.ID;
				orderBoardRequiredItemView.pointerUpSignal.AddListener(PointerUp);
				orderBoardRequiredItemView.pointerDownSignal.AddListener(PointerDown);
			}
		}

		private void SetupCharacterDetail(int characterDefID)
		{
			global::Kampai.Game.Prestige prestige = characterService.GetPrestige(characterDefID);
			currentPrestigePoints = prestige.CurrentPrestigePoints;
			neededPrestigePoints = prestige.NeededPrestigePoints;
			currentPrestigeLevel = prestige.CurrentPrestigeLevel;
			view.SetPanelState(true, currentPrestigeLevel, prestige);
			view.SetPrestigeProgress(currentPrestigePoints, neededPrestigePoints);
			global::Kampai.UI.DummyCharacterType characterType = fancyUIService.GetCharacterType(characterDefID);
			global::Kampai.Game.View.DummyCharacterObject character = fancyUIService.CreateCharacter(characterType, global::Kampai.UI.DummyCharacterAnimationState.Happy, view.MinionSlot.transform, view.MinionSlot.VillainScale, view.MinionSlot.VillainPositionOffset, characterDefID);
			view.SetCharacter(character);
			moveAudioListener.Dispatch(false, view.MinionSlot.transform);
		}

		private void SetSlotFullText(string locKey)
		{
			string slotFullText = localService.GetString(locKey);
			view.SetSlotFullText(slotFullText);
		}

		private void SetFTUEText(string title)
		{
			string fTUEText = localService.GetString(title);
			view.SetFTUEText(fTUEText);
		}

		private void PointerDown(int itemDefinitionID, global::UnityEngine.RectTransform rectTransform)
		{
			if (PointerDownWait != null)
			{
				routineRunner.StopCoroutine(PointerDownWait);
				PointerDownWait = null;
			}
			displayItemPopupSignal.Dispatch(itemDefinitionID, rectTransform, global::Kampai.UI.View.UIPopupType.GENERIC);
		}

		private void PointerUp()
		{
			if (PointerDownWait == null)
			{
				PointerDownWait = WaitASecond();
				routineRunner.StartCoroutine(PointerDownWait);
			}
		}

		private global::System.Collections.IEnumerator WaitASecond()
		{
			yield return new global::UnityEngine.WaitForSeconds(0.5f);
			hideItemPopupSignal.Dispatch();
		}

		private void OnPause()
		{
			hideItemPopupSignal.Dispatch();
		}
	}
}
