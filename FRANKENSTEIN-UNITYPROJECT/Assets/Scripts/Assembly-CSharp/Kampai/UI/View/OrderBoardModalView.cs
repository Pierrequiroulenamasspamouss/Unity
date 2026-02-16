namespace Kampai.UI.View
{
	public class OrderBoardModalView : global::Kampai.UI.View.PopupMenuView
	{
		private const int TICKET_NUMBER = 9;

		public global::Kampai.UI.View.ButtonView CloseButton;

		public FillOrderButtonView FillOrderButton;

		public global::Kampai.UI.View.ButtonView DeleteButton;

		internal global::System.Collections.Generic.List<global::Kampai.UI.View.OrderBoardTicketView> TicketSlots = new global::System.Collections.Generic.List<global::Kampai.UI.View.OrderBoardTicketView>(9);

		internal global::Kampai.UI.View.ModalSettings modalSettings;

		private bool enabledDeleteButton = true;

		private bool isClosing;

		private float ticketRepopTime;

		public void Init(OrderBoardBuildingTicketsView ticketsView, global::Kampai.UI.IPositionService positionService, global::Kampai.UI.View.IGUIService guiService, float ticketRepopTime, global::Kampai.Util.IRoutineRunner routineRunner)
		{
			base.Init();
			this.ticketRepopTime = ticketRepopTime;
			for (int i = 0; i < 9; i++)
			{
				global::Kampai.UI.PositionData positionData = positionService.GetPositionData(ticketsView.GetTicketPosition(i));
				CreateTicket(i, modalSettings, positionData.WorldPositionInUI, guiService, routineRunner);
			}
			foreach (global::Kampai.UI.View.OrderBoardTicketView ticketSlot in TicketSlots)
			{
				ticketSlot.gameObject.SetActive(false);
			}
			FillOrderButton.Init();
			Open();
		}

		public void DestoryTickets(bool destory = true)
		{
			foreach (global::Kampai.UI.View.OrderBoardTicketView ticketSlot in TicketSlots)
			{
				if (destory)
				{
					global::UnityEngine.Object.Destroy(ticketSlot.gameObject);
				}
				else
				{
					ticketSlot.gameObject.SetActive(false);
				}
			}
			if (destory)
			{
				TicketSlots.Clear();
			}
		}

		public new void Close(bool IsInstant = false)
		{
			isClosing = true;
			DestoryTickets(false);
			base.Close(IsInstant);
		}

		private void CreateTicket(int index, global::Kampai.UI.View.ModalSettings modalSettings, global::UnityEngine.Vector3 position, global::Kampai.UI.View.IGUIService guiService, global::Kampai.Util.IRoutineRunner routineeRunner)
		{
			global::UnityEngine.GameObject gameObject = guiService.Execute(global::Kampai.UI.View.GUIOperation.LoadUntrackedInstance, "cmp_TicketPrefab");
			global::UnityEngine.RectTransform rectTransform = gameObject.transform as global::UnityEngine.RectTransform;
			rectTransform.position = position;
			global::Kampai.UI.View.OrderBoardTicketView component = gameObject.GetComponent<global::Kampai.UI.View.OrderBoardTicketView>();
			component.Index = index;
			component.Init(routineeRunner);
			if (modalSettings.enableTicketThrob)
			{
				component.HighlightTicket(true);
			}
			TicketSlots.Add(component);
		}

		internal void SetupDeleteOrderButton(bool active)
		{
			global::UnityEngine.UI.Button component = DeleteButton.GetComponent<global::UnityEngine.UI.Button>();
			component.interactable = enabledDeleteButton && active;
		}

		internal void AddTicket(global::Kampai.Game.OrderBoardTicket ticket, bool isInProgress, int duration, string locText, global::Kampai.Game.IPrestigeService prestigeService, global::Kampai.Util.IRoutineRunner routineRunner, bool isInit, global::Kampai.UI.View.OrderBoardTicketClickedSignal ticketClicked)
		{
			if (!isClosing)
			{
				global::Kampai.UI.View.OrderBoardTicketView orderBoardTicketView = TicketSlots[ticket.BoardIndex];
				orderBoardTicketView.gameObject.SetActive(true);
				if (isInit)
				{
					SetTicketInfo(orderBoardTicketView, ticket, isInProgress, duration, locText, prestigeService, null);
				}
				else
				{
					routineRunner.StartCoroutine(ChangeTicket(orderBoardTicketView, ticket, isInProgress, duration, locText, prestigeService, ticketClicked));
				}
			}
		}

		private global::System.Collections.IEnumerator ChangeTicket(global::Kampai.UI.View.OrderBoardTicketView view, global::Kampai.Game.OrderBoardTicket ticket, bool isInProgress, int duration, string locText, global::Kampai.Game.IPrestigeService prestigeService, global::Kampai.UI.View.OrderBoardTicketClickedSignal ticketClicked)
		{
			view.SetRootAnimation(false);
			yield return new global::UnityEngine.WaitForSeconds(ticketRepopTime);
			SetTicketInfo(view, ticket, isInProgress, duration, locText, prestigeService, ticketClicked);
			view.SetRootAnimation(true);
		}

		private void SetTicketInfo(global::Kampai.UI.View.OrderBoardTicketView view, global::Kampai.Game.OrderBoardTicket ticket, bool isInProgress, int duration, string locText, global::Kampai.Game.IPrestigeService prestigeService, global::Kampai.UI.View.OrderBoardTicketClickedSignal ticketClicked)
		{
			global::Kampai.Game.Transaction.TransactionInstance transactionInst = ticket.TransactionInst;
			int quantity = (int)transactionInst.Outputs[0].Quantity;
			int quantity2 = (int)transactionInst.Outputs[1].Quantity;
			view.Title = locText;
			view.CurrencyText.text = quantity.ToString();
			view.StarText.text = quantity2.ToString();
			view.SetTicketInstance(ticket);
			view.NormalPanel.SetActive(ticket.CharacterDefinitionId == 0);
			view.PrestigePanel.SetActive(ticket.CharacterDefinitionId != 0);
			if (isInProgress)
			{
				view.StartTimer(ticket.BoardIndex, duration);
			}
			else
			{
				view.SetTicketState(true);
				if (ticket.CharacterDefinitionId != 0)
				{
					int characterDefinitionId = ticket.CharacterDefinitionId;
					global::UnityEngine.Sprite characterImage;
					global::UnityEngine.Sprite characterMask;
					prestigeService.GetCharacterImageBasedOnMood(characterDefinitionId, global::Kampai.Game.CharacterImageType.SmallAvatarIcon, out characterImage, out characterMask);
					global::Kampai.Game.Prestige prestige = prestigeService.GetPrestige(characterDefinitionId);
					view.SetCharacterImage(characterImage, characterMask, prestige.CurrentPrestigeLevel == 0);
				}
			}
			if (ticketClicked != null)
			{
				ticketClicked.Dispatch(ticket, locText, false);
			}
		}

		internal void SetTicketClicks(bool enabled)
		{
			foreach (global::Kampai.UI.View.OrderBoardTicketView ticketSlot in TicketSlots)
			{
				ticketSlot.SetTicketClick(enabled);
			}
		}

		internal global::Kampai.UI.View.OrderBoardTicketView GetFirstClickableTicketIndex()
		{
			for (int i = 0; i < TicketSlots.Count; i++)
			{
				if (TicketSlots[i].gameObject.activeSelf && !TicketSlots[i].IsCounting())
				{
					return TicketSlots[i];
				}
			}
			return TicketSlots[0];
		}

		internal void SetDeleteButtonEnabled(bool isEnabled)
		{
			enabledDeleteButton = isEnabled;
			DeleteButton.GetComponent<global::UnityEngine.UI.Button>().interactable = isEnabled;
		}

		internal void ResetDoubleTap(int viewId)
		{
			foreach (global::Kampai.UI.View.OrderBoardTicketView ticketSlot in TicketSlots)
			{
				if (ticketSlot.Index != viewId)
				{
					ticketSlot.TicketMeter.RushButton.ResetTapState();
					ticketSlot.TicketMeter.RushButton.ResetAnim();
				}
			}
		}
	}
}
