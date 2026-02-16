namespace Kampai.UI.View
{
	public class OrderBoardModalMediator : global::Kampai.UI.View.UIStackMediator<global::Kampai.UI.View.OrderBoardModalView>
	{
		private global::Kampai.Game.OrderBoard building;

		private int currentSelectedTickedIndex;

		private global::Kampai.Game.Transaction.TransactionDefinition currentTransactionDef;

		private global::System.Collections.Generic.IList<global::Kampai.Util.QuantityItem> currentMissingItems;

		private int currentFulfilledTicket = -1;

		private global::Kampai.Game.Prestige currentSelectedPrestige;

		private bool waitingDoobersToClose;

		private bool prestigeFull;

		private global::Kampai.UI.View.ModalSettings modalSettings = new global::Kampai.UI.View.ModalSettings();

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.UI.View.IGUIService guiService { get; set; }

		[Inject]
		public global::Kampai.UI.View.OrderBoardTicketClickedSignal ticketClicked { get; set; }

		[Inject]
		public global::Kampai.UI.View.OrderBoardTicketDeletedSignal ticketDeletedSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.OrderBoardPrestigeSlotFullSignal prestigeSlotFullSignal { get; set; }

		[Inject]
		public global::Kampai.Game.OrderBoardFillOrderSignal fillOrderSignal { get; set; }

		[Inject]
		public global::Kampai.Game.OrderBoardDeleteOrderSignal deleteOrderSignal { get; set; }

		[Inject]
		public global::Kampai.Game.OrderBoardRefillTicketSignal refillTicketSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.RushDialogConfirmationSignal dialogConfirmedSignal { get; set; }

		[Inject]
		public global::Kampai.Game.OrderBoardTransactionFailedSignal transactionFailedSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.LoadBuddyBarSignal loadBuddyBarSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.HideSkrimSignal hideSkrim { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeEventService timeEventService { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService localService { get; set; }

		[Inject]
		public global::Kampai.Game.IPrestigeService characterService { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal soundFXSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.OrderBoardStartFillingPrestigeBarSignal startFillingPrestigeBarSignal { get; set; }

		[Inject]
		public global::Kampai.Game.OrderBoardFillOrderCompleteSignal fillOrderCompleteSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.SetPremiumCurrencySignal setPremiumCurrencySignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.ResetDoubleTapSignal resetDoubleTapSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.SetFTUETextSignal setFTUETextSignal { get; set; }

		[Inject(global::Kampai.Game.GameElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable gameContext { get; set; }

		[Inject]
		public global::Kampai.UI.IPositionService positionService { get; set; }

		[Inject]
		public global::Kampai.UI.IFancyUIService fancyUIService { get; set; }

		[Inject]
		public global::Kampai.UI.View.DoobersFlownSignal doobersFlownSignal { get; set; }

		[Inject]
		public global::Kampai.Main.MoveAudioListenerSignal moveAudioListener { get; set; }

		public override void OnRegister()
		{
			base.OnRegister();
			base.view.CloseButton.ClickedSignal.AddListener(Close);
			base.view.FillOrderButton.ClickedSignal.AddListener(FillOrder);
			base.view.DeleteButton.ClickedSignal.AddListener(DeleteTicket);
			base.view.OnMenuClose.AddListener(OnMenuClose);
			ticketClicked.AddListener(TicketClicked);
			dialogConfirmedSignal.AddListener(ConfirmClicked);
			refillTicketSignal.AddListener(RefillTicket);
			transactionFailedSignal.AddListener(TransactionFailed);
			fillOrderCompleteSignal.AddListener(FillOrderComplete);
			resetDoubleTapSignal.AddListener(ResetDoubleTap);
			setFTUETextSignal.AddListener(SetFTUEText);
			doobersFlownSignal.AddListener(DoobersFlown);
			gameContext.injectionBinder.GetInstance<global::Kampai.Game.AwardLevelSignal>().AddListener(AwardLevel);
		}

		public override void OnRemove()
		{
			base.OnRemove();
			base.uiRemovedSignal.Dispatch(base.view.gameObject);
			ticketClicked.RemoveListener(TicketClicked);
			refillTicketSignal.RemoveListener(RefillTicket);
			base.view.CloseButton.ClickedSignal.RemoveListener(Close);
			base.view.FillOrderButton.ClickedSignal.RemoveListener(FillOrder);
			base.view.DeleteButton.ClickedSignal.RemoveListener(DeleteTicket);
			base.view.OnMenuClose.RemoveListener(OnMenuClose);
			dialogConfirmedSignal.RemoveListener(ConfirmClicked);
			transactionFailedSignal.RemoveListener(TransactionFailed);
			fillOrderCompleteSignal.RemoveListener(FillOrderComplete);
			resetDoubleTapSignal.RemoveListener(ResetDoubleTap);
			setFTUETextSignal.RemoveListener(SetFTUEText);
			doobersFlownSignal.RemoveListener(DoobersFlown);
			gameContext.injectionBinder.GetInstance<global::Kampai.Game.AwardLevelSignal>().RemoveListener(AwardLevel);
		}

		public override void Initialize(global::Kampai.UI.View.GUIArguments args)
		{
			base.view.SetDeleteButtonEnabled(!args.Contains<global::Kampai.UI.DisableDeleteOrderButton>());
			global::Kampai.Game.OrderBoard orderBoard = args.Get<global::Kampai.Game.OrderBoard>();
			modalSettings.enableTicketThrob = args.Contains<global::Kampai.UI.ThrobTicketButton>();
			base.view.modalSettings = modalSettings;
			base.view.Init(args.Get<OrderBoardBuildingTicketsView>(), positionService, guiService, orderBoard.Definition.TicketRepopTime, routineRunner);
			soundFXSignal.Dispatch("Play_menu_popUp_01");
			LoadTicketsFromBuilding(orderBoard);
			if (modalSettings.enableTicketThrob)
			{
				setFTUETextSignal.Dispatch("ftue_q6_order");
				HideButtons(true);
			}
		}

		internal void AwardLevel(global::Kampai.Game.Transaction.TransactionDefinition td)
		{
			routineRunner.StartCoroutine(PostLevelUp());
		}

		private global::System.Collections.IEnumerator PostLevelUp()
		{
			yield return null;
			foreach (global::Kampai.Game.OrderBoardTicket ticket in building.tickets)
			{
				AddTicket(ticket, ticket.StartTime >= 0, true);
			}
		}

		internal void DoobersFlown()
		{
			if (waitingDoobersToClose)
			{
				Close();
			}
		}

		internal void FillOrderComplete(int ticketIndex)
		{
			currentFulfilledTicket = -1;
			currentSelectedTickedIndex = ticketIndex;
			foreach (global::Kampai.Game.OrderBoardTicket ticket in building.tickets)
			{
				if (ticket.BoardIndex == currentSelectedTickedIndex)
				{
					AddTicket(ticket, false);
					CheckSingleTicketRequirementMatchingState(ticket);
				}
			}
			SetTicketClicks(true);
		}

		internal void ConfirmClicked()
		{
			FillOrder();
		}

		internal void TransactionFailed(global::Kampai.Game.Transaction.TransactionDefinition td)
		{
			if (td == currentTransactionDef)
			{
				SetDeleteOrderButton(true);
				base.view.FillOrderButton.SetFillOrderButtonState(global::Kampai.UI.View.OrderBoardButtonState.Enable);
				currentFulfilledTicket = -1;
			}
		}

		internal void CheckTicketRequirementMatchingState()
		{
			foreach (global::Kampai.Game.OrderBoardTicket ticket in building.tickets)
			{
				CheckSingleTicketRequirementMatchingState(ticket);
			}
		}

		internal void CheckSingleTicketRequirementMatchingState(global::Kampai.Game.OrderBoardTicket ticket)
		{
			global::Kampai.Game.Transaction.TransactionInstance transactionInst = ticket.TransactionInst;
			bool ticketCheckmark = true;
			int count = transactionInst.Inputs.Count;
			for (int i = 0; i < count; i++)
			{
				global::Kampai.Util.QuantityItem quantityItem = transactionInst.Inputs[i];
				uint quantity = quantityItem.Quantity;
				uint quantityByDefinitionId = playerService.GetQuantityByDefinitionId(quantityItem.ID);
				if (quantity > quantityByDefinitionId)
				{
					ticketCheckmark = false;
				}
			}
			base.view.TicketSlots[ticket.BoardIndex].SetTicketCheckmark(ticketCheckmark);
		}

		internal void DeleteTicket()
		{
			resetDoubleTapSignal.Dispatch(-1);
			SetDeleteOrderButton(false);
			base.view.FillOrderButton.SetFillOrderButtonState(global::Kampai.UI.View.OrderBoardButtonState.Disable);
			soundFXSignal.Dispatch("Play_delete_ticket_01");
			deleteOrderSignal.Dispatch(currentSelectedTickedIndex, currentTransactionDef, building);
			ticketDeletedSignal.Dispatch();
		}

		protected override void Close()
		{
			moveAudioListener.Dispatch(true, null);
			soundFXSignal.Dispatch("Play_menu_disappear_01");
			base.view.Close();
			gameContext.injectionBinder.GetInstance<global::Kampai.Game.BuildingZoomSignal>().Dispatch(new global::Kampai.Game.BuildingZoomSettings(global::Kampai.Game.ZoomType.OUT, global::Kampai.Game.BuildingZoomType.ORDERBOARD));
		}

		private void OnMenuClose()
		{
			base.view.DestoryTickets();
			hideSkrim.Dispatch("OrderBoardSkrim");
			guiService.Execute(global::Kampai.UI.View.GUIOperation.Unload, "screen_OrderBoard");
		}

		internal void FillOrder()
		{
			resetDoubleTapSignal.Dispatch(-1);
			soundFXSignal.Dispatch("Play_button_click_01");
			if (!base.view.FillOrderButton.isDoubleConfirmed())
			{
				return;
			}
			if (currentTransactionDef != null)
			{
				currentFulfilledTicket = currentSelectedTickedIndex;
				SetDeleteOrderButton(false);
				if (base.view.FillOrderButton.GetLastFillOrderButtonState() == global::Kampai.UI.View.OrderBoardButtonState.Rush)
				{
					int lastRushCost = base.view.FillOrderButton.GetLastRushCost();
					if (currentSelectedPrestige != null)
					{
						base.view.FillOrderButton.SetFillOrderButtonState(global::Kampai.UI.View.OrderBoardButtonState.Disable);
					}
					playerService.ProcessOrderFill(lastRushCost, currentMissingItems, true, RushTransactionCallback);
				}
				else
				{
					if (currentSelectedPrestige != null)
					{
						base.view.FillOrderButton.SetFillOrderButtonState(global::Kampai.UI.View.OrderBoardButtonState.Disable);
					}
					CheckIfItIsPrestigeTicketBeforeFillingOrder();
				}
				soundFXSignal.Dispatch("Play_button_click_01");
			}
			else
			{
				logger.Log(global::Kampai.Util.Logger.Level.Error, "Trying to start an empty black market transaction");
			}
		}

		private void RushTransactionCallback(global::Kampai.Game.PendingCurrencyTransaction pct)
		{
			if (pct.Success)
			{
				setPremiumCurrencySignal.Dispatch();
				CheckIfItIsPrestigeTicketBeforeFillingOrder();
			}
		}

		private void CheckIfItIsPrestigeTicketBeforeFillingOrder()
		{
			SetTicketClicks(false);
			if (currentSelectedPrestige != null)
			{
				int currentPrestigePoints = currentSelectedPrestige.CurrentPrestigePoints;
				int num = currentPrestigePoints + global::Kampai.Game.Transaction.TransactionUtil.ExtractQuantityFromTransaction(currentTransactionDef, 2);
				int neededPrestigePoints = currentSelectedPrestige.NeededPrestigePoints;
				if (num >= neededPrestigePoints)
				{
					base.view.FillOrderButton.SetFillOrderButtonState(global::Kampai.UI.View.OrderBoardButtonState.Disable);
					prestigeFull = true;
					num = neededPrestigePoints;
				}
				startFillingPrestigeBarSignal.Dispatch(num, FillOrderAfterBarIsFilled);
			}
			else
			{
				fillOrderSignal.Dispatch(currentSelectedTickedIndex, currentTransactionDef, building);
			}
		}

		private void SetTicketClicks(bool enabled)
		{
			base.view.SetTicketClicks(enabled);
		}

		private void FillOrderAfterBarIsFilled()
		{
			if (prestigeFull)
			{
				waitingDoobersToClose = true;
			}
			fillOrderSignal.Dispatch(currentSelectedTickedIndex, currentTransactionDef, building);
		}

		internal void GetToNextAvailableTicket(bool mute)
		{
			if (building.tickets.Count != 0 && !modalSettings.enableTicketThrob)
			{
				global::Kampai.UI.View.OrderBoardTicketView firstClickableTicketIndex = base.view.GetFirstClickableTicketIndex();
				if (firstClickableTicketIndex.IsCounting())
				{
					SetDeleteOrderButton(false);
					base.view.FillOrderButton.SetFillOrderButtonState(global::Kampai.UI.View.OrderBoardButtonState.Disable);
					ticketDeletedSignal.Dispatch();
				}
				else
				{
					ticketClicked.Dispatch(firstClickableTicketIndex.ticketInstance, firstClickableTicketIndex.Title, mute);
				}
			}
		}

		internal void RefillTicket(int negativeIndex)
		{
			routineRunner.StartCoroutine(GetNewTicket(-negativeIndex));
		}

		private global::System.Collections.IEnumerator GetNewTicket(int index)
		{
			yield return null;
			if (!(base.view != null))
			{
				yield break;
			}
			foreach (global::Kampai.Game.OrderBoardTicket ticket in building.tickets)
			{
				if (ticket.BoardIndex == index)
				{
					AddTicket(ticket, false);
					CheckSingleTicketRequirementMatchingState(ticket);
				}
			}
		}

		internal void TicketClicked(global::Kampai.Game.OrderBoardTicket ticketInstance, string title, bool mute)
		{
			resetDoubleTapSignal.Dispatch(-1);
			global::Kampai.Game.Transaction.TransactionInstance transactionInst = ticketInstance.TransactionInst;
			if (!mute)
			{
				soundFXSignal.Dispatch("Play_button_click_01");
			}
			base.view.TicketSlots[currentSelectedTickedIndex].SetTicketSelected(false);
			currentSelectedTickedIndex = ticketInstance.BoardIndex;
			base.view.TicketSlots[currentSelectedTickedIndex].SetTicketSelected(true);
			currentTransactionDef = transactionInst.ToDefinition();
			SetDeleteOrderButton(true);
			bool flag = false;
			int characterDefinitionId = ticketInstance.CharacterDefinitionId;
			if (characterDefinitionId != 0)
			{
				currentSelectedPrestige = characterService.GetPrestige(characterDefinitionId);
				if (currentSelectedPrestige == null)
				{
					logger.Error("You have a prestige ticket that doesn't have a prestige instance: {0}", characterDefinitionId);
					return;
				}
				global::Kampai.Game.PrestigeType type = currentSelectedPrestige.Definition.Type;
				if ((type == global::Kampai.Game.PrestigeType.Minion && characterService.IsTikiBarFull()) || (type == global::Kampai.Game.PrestigeType.Villain && characterService.GetEmptyCabana() == null))
				{
					flag = true;
					prestigeSlotFullSignal.Dispatch((type != global::Kampai.Game.PrestigeType.Minion) ? "VillainSlotFull" : "MinionSlotFull");
				}
			}
			else
			{
				currentSelectedPrestige = null;
			}
			if (currentFulfilledTicket == -1)
			{
				currentMissingItems = playerService.GetMissingItemListFromTransaction(currentTransactionDef);
				if (currentMissingItems.Count == 0)
				{
					base.view.FillOrderButton.SetFillOrderButtonState(waitingDoobersToClose ? global::Kampai.UI.View.OrderBoardButtonState.Disable : global::Kampai.UI.View.OrderBoardButtonState.MeetRequirement);
				}
				else
				{
					base.view.FillOrderButton.SetFillOrderButtonState(waitingDoobersToClose ? global::Kampai.UI.View.OrderBoardButtonState.Disable : global::Kampai.UI.View.OrderBoardButtonState.Rush, playerService.CalculateRushCost(currentMissingItems));
				}
			}
			if (!base.view.DeleteButton.gameObject.activeSelf)
			{
				base.view.DeleteButton.gameObject.SetActive(true);
			}
			if (flag)
			{
				base.view.FillOrderButton.SetFillOrderButtonState(global::Kampai.UI.View.OrderBoardButtonState.Hide);
			}
		}

		internal void SetDeleteOrderButton(bool active)
		{
			base.view.SetupDeleteOrderButton(active);
		}

		internal void LoadTicketsFromBuilding(global::Kampai.Game.OrderBoard building)
		{
			this.building = building;
			foreach (global::Kampai.Game.OrderBoardTicket ticket in building.tickets)
			{
				AddTicket(ticket, ticket.StartTime >= 0, true);
			}
			CheckTicketRequirementMatchingState();
			GetToNextAvailableTicket(true);
		}

		internal void AddTicket(global::Kampai.Game.OrderBoardTicket ticket, bool isInProgress, bool isInit = false)
		{
			string empty = string.Empty;
			if (ticket.CharacterDefinitionId == 0)
			{
				empty = building.Definition.OrderNames[ticket.OrderNameTableIndex];
			}
			else
			{
				global::Kampai.Game.PrestigeDefinition prestigeDefinition = definitionService.Get<global::Kampai.Game.PrestigeDefinition>(ticket.CharacterDefinitionId);
				empty = prestigeDefinition.LocalizedKey;
			}
			string locText = localService.GetString(empty);
			int eventDuration = timeEventService.GetEventDuration(-ticket.BoardIndex);
			base.view.AddTicket(ticket, isInProgress, eventDuration, locText, characterService, routineRunner, isInit, ticketClicked);
		}

		private void HideButtons(bool hide)
		{
			if (hide)
			{
				base.view.FillOrderButton.gameObject.SetActive(false);
				base.view.DeleteButton.gameObject.SetActive(false);
			}
			else
			{
				base.view.FillOrderButton.gameObject.SetActive(true);
				base.view.DeleteButton.gameObject.SetActive(true);
			}
		}

		private void ResetDoubleTap(int id)
		{
			base.view.ResetDoubleTap(id);
		}

		private void SetFTUEText(string title)
		{
			base.view.CloseButton.gameObject.SetActive(false);
		}
	}
}
