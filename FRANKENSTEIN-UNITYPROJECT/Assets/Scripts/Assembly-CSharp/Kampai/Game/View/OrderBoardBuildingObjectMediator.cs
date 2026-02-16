namespace Kampai.Game.View
{
	public class OrderBoardBuildingObjectMediator : global::strange.extensions.mediation.impl.EventMediator
	{
		[Inject]
		public global::Kampai.Game.View.OrderBoardBuildingObjectView view { get; set; }

		[Inject]
		public global::Kampai.Game.OrderBoardRefillTicketSignal refillTicketSignal { get; set; }

		[Inject]
		public global::Kampai.Game.OrderBoardStartRefillTicketSignal startRefillTicketSignal { get; set; }

		[Inject]
		public global::Kampai.Game.OrderBoardSetNewTicketSignal setNewTicketSignal { get; set; }

		[Inject]
		public global::Kampai.Game.SetupOrderBoardServiceSignal setupOrderBoardServiceSignal { get; set; }

		[Inject]
		public global::Kampai.Game.PostTransactionSignal postTransactionSignal { get; set; }

		[Inject]
		public global::Kampai.Game.AwardLevelSignal awardLevelSignal { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeService timeService { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		[Inject]
		public global::Kampai.Game.IOrderBoardService orderBoardService { get; set; }

		[Inject]
		public global::Kampai.Game.IPrestigeService prestigeService { get; set; }

		[Inject]
		public global::Kampai.Game.OrderBoardUpdateTicketOnBoardSignal updateTicketOnBoardSignal { get; set; }

		[Inject]
		public global::Kampai.Game.ToggleHitboxSignal toggleHitboxSignal { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		public override void OnRegister()
		{
			startRefillTicketSignal.AddListener(StartRefillTicket);
			postTransactionSignal.AddListener(PostTransaction);
			awardLevelSignal.AddListener(AwardLevel);
			refillTicketSignal.AddListener(RefillTicket);
			updateTicketOnBoardSignal.AddListener(UpdateTicketState);
			toggleHitboxSignal.AddListener(ToggleHitbox);
			routineRunner.StartCoroutine(Init());
		}

		public override void OnRemove()
		{
			refillTicketSignal.RemoveListener(RefillTicket);
			startRefillTicketSignal.RemoveListener(StartRefillTicket);
			awardLevelSignal.RemoveListener(AwardLevel);
			updateTicketOnBoardSignal.RemoveListener(UpdateTicketState);
			postTransactionSignal.RemoveListener(PostTransaction);
			toggleHitboxSignal.RemoveListener(ToggleHitbox);
		}

		private global::System.Collections.IEnumerator Init()
		{
			yield return null;
			setupOrderBoardServiceSignal.Dispatch(view.orderBoard);
			view.ClearBoard();
		}

		private global::System.Collections.IEnumerator WaitAFrame(bool clearBoard)
		{
			yield return null;
			if (clearBoard)
			{
				view.ClearBoard();
			}
			UpdateTicketState();
		}

		internal void AwardLevel(global::Kampai.Game.Transaction.TransactionDefinition td)
		{
			logger.Debug("Award Level: {0}", td.ID);
			routineRunner.StartCoroutine(PostLevelUp());
		}

		private void ToggleHitbox(global::Kampai.Game.BuildingZoomType zoomBuildingType, bool enable)
		{
			if (zoomBuildingType == global::Kampai.Game.BuildingZoomType.ORDERBOARD)
			{
				view.ToggleHitbox(enable);
			}
		}

		private global::System.Collections.IEnumerator PostLevelUp()
		{
			yield return null;
			UpdateTicketState();
		}

		internal void PostTransaction(global::Kampai.Game.Transaction.TransactionUpdateData update)
		{
			routineRunner.StartCoroutine(WaitAFrame(false));
		}

		internal void UpdateTicketState()
		{
			orderBoardService.UpdateLevelBand();
			foreach (global::Kampai.Game.OrderBoardTicket ticket in view.orderBoard.tickets)
			{
				if (ticket.StartTime != -1)
				{
					view.SetTicketState(ticket.BoardIndex, global::Kampai.Game.OrderBoardTicketState.TIMER);
					continue;
				}
				bool flag = false;
				global::Kampai.Game.Transaction.TransactionInstance transactionInst = ticket.TransactionInst;
				int count = transactionInst.Inputs.Count;
				for (int i = 0; i < count; i++)
				{
					global::Kampai.Util.QuantityItem quantityItem = transactionInst.Inputs[i];
					uint quantity = quantityItem.Quantity;
					uint quantityByDefinitionId = playerService.GetQuantityByDefinitionId(quantityItem.ID);
					if (quantity > quantityByDefinitionId)
					{
						flag = true;
					}
				}
				global::Kampai.Game.OrderBoardTicketState orderBoardTicketState = global::Kampai.Game.OrderBoardTicketState.NOT_AVAILABLE;
				if (ticket.CharacterDefinitionId != 0)
				{
					global::Kampai.Game.Prestige prestige = prestigeService.GetPrestige(ticket.CharacterDefinitionId);
					orderBoardTicketState = ((prestige.Definition.Type != global::Kampai.Game.PrestigeType.Villain) ? ((!flag) ? global::Kampai.Game.OrderBoardTicketState.PRESTIGE_CHECKED : global::Kampai.Game.OrderBoardTicketState.PRESTIGE_UNCHECKED) : ((!flag) ? global::Kampai.Game.OrderBoardTicketState.VILLAIN_CHECKED : global::Kampai.Game.OrderBoardTicketState.VILLAIN_UNCHECKED));
				}
				else
				{
					orderBoardTicketState = (flag ? global::Kampai.Game.OrderBoardTicketState.UNCHECKED : global::Kampai.Game.OrderBoardTicketState.CHECKED);
				}
				view.SetTicketState(ticket.BoardIndex, orderBoardTicketState);
			}
		}

		private void StartRefillTicket(global::Kampai.Util.Tuple<int, int, float> tuple)
		{
			UpdateTicketState();
		}

		private void RefillTicket(int index)
		{
			setNewTicketSignal.Dispatch(index, false);
			routineRunner.StartCoroutine(WaitAFrame(false));
		}
	}
}
