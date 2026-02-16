namespace Kampai.UI.View
{
	public class OrderBoardTicketView : global::strange.extensions.mediation.impl.View
	{
		public global::Kampai.UI.View.OrderBoardTicketTimerView TicketMeter;

		public global::Kampai.UI.View.ButtonView TicketButton;

		public global::UnityEngine.GameObject CheckMark;

		public global::UnityEngine.GameObject NormalPanel;

		public global::UnityEngine.GameObject PrestigePanel;

		public global::Kampai.UI.View.KampaiImage CharacterImage;

		public global::UnityEngine.GameObject FirstTimePrestigeBadge;

		public global::UnityEngine.UI.Text StarText;

		public global::UnityEngine.UI.Text CurrencyText;

		public int Index;

		private bool isThrobing;

		private global::UnityEngine.Animator ticketAnimator;

		private global::UnityEngine.Animator rootAnimator;

		private global::Kampai.Util.IRoutineRunner routineRunner;

		internal global::Kampai.Game.OrderBoardTicket ticketInstance { get; set; }

		internal bool IsSelected { get; set; }

		internal string Title { get; set; }

		internal void Init(global::Kampai.Util.IRoutineRunner routineRunner)
		{
			this.routineRunner = routineRunner;
			IsSelected = false;
			isThrobing = false;
			ticketAnimator = TicketButton.GetComponent<global::UnityEngine.Animator>();
			rootAnimator = GetComponent<global::UnityEngine.Animator>();
			SetRootAnimation(true);
		}

		internal void OnRemove()
		{
			routineRunner.StopCoroutine(WaitAFrame());
		}

		internal void SetTicketState(bool showTicketButton)
		{
			SetTicketMeterState(showTicketButton);
			TicketButton.gameObject.SetActive(showTicketButton);
			if (showTicketButton)
			{
				SetTicketSelected(false);
			}
		}

		private void SetTicketMeterState(bool showTicketButton)
		{
			if (showTicketButton)
			{
				TicketMeter.RushButton.ResetAnim();
				routineRunner.StartCoroutine(WaitAFrame());
			}
			else
			{
				TicketMeter.gameObject.SetActive(true);
			}
		}

		private global::System.Collections.IEnumerator WaitAFrame()
		{
			yield return new global::UnityEngine.WaitForEndOfFrame();
			TicketMeter.gameObject.SetActive(false);
		}

		internal void SetCharacterImage(global::UnityEngine.Sprite Image, global::UnityEngine.Sprite mask, bool firstTimePrestige)
		{
			CharacterImage.sprite = Image;
			CharacterImage.maskSprite = mask;
			FirstTimePrestigeBadge.SetActive(firstTimePrestige);
		}

		internal void SetTicketSelected(bool isSelected)
		{
			if (IsSelected != isSelected)
			{
				IsSelected = isSelected;
				ticketAnimator.SetBool("Normal", !isSelected);
				ticketAnimator.SetBool("Highlighted", isSelected);
			}
		}

		internal void SetTicketCheckmark(bool show)
		{
			if (show)
			{
				CheckMark.SetActive(true);
			}
			else
			{
				CheckMark.SetActive(false);
			}
		}

		internal void SetTicketClick(bool enabled)
		{
			TicketButton.GetComponent<global::Kampai.UI.View.KampaiButton>().enabled = enabled;
		}

		internal void StartTimer(int index, int duration)
		{
			SetTicketState(false);
			TicketMeter.StartTimer(index, duration);
		}

		internal void SetRootAnimation(bool isOpen)
		{
			rootAnimator.Play((!isOpen) ? "Close" : "Open");
		}

		internal void SetTicketInstance(global::Kampai.Game.OrderBoardTicket ti)
		{
			ticketInstance = ti;
		}

		internal bool IsCounting()
		{
			return !TicketButton.gameObject.activeSelf;
		}

		internal void HighlightTicket(bool highlight)
		{
			if (isThrobing != highlight)
			{
				isThrobing = highlight;
				if (highlight)
				{
					TicketButton.GetComponent<global::UnityEngine.Animator>().enabled = false;
					global::UnityEngine.Vector3 originalScale = global::UnityEngine.Vector3.one;
					global::Kampai.Util.TweenUtil.Throb(TicketButton.transform, 0.85f, 0.5f, out originalScale);
				}
				else
				{
					TicketButton.GetComponent<global::UnityEngine.Animator>().enabled = true;
					Go.killAllTweensWithTarget(TicketButton.transform);
					TicketButton.transform.localScale = global::UnityEngine.Vector3.one;
				}
			}
		}
	}
}
