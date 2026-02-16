public class FillOrderButtonView : global::Kampai.UI.View.DoubleConfirmButtonView
{
	public global::UnityEngine.RectTransform FillOrderText;

	public global::UnityEngine.RectTransform FillOrderRushIcon;

	public global::UnityEngine.UI.Text FillOrderRushCost;

	private global::Kampai.UI.View.OrderBoardButtonState previousState;

	private float fillOrderTextAnchorMaxXForRushing;

	private int currentRushCost;

	[Inject]
	public global::Kampai.UI.View.PopupMessageSignal popupMessageSignal { get; set; }

	public override void OnClickEvent()
	{
		updateTapCount();
		if (!isDoubleConfirmed())
		{
			ShowConfirmMessage();
		}
		ClickedSignal.Dispatch();
	}

	public void Init()
	{
		fillOrderTextAnchorMaxXForRushing = FillOrderRushIcon.anchorMin.x;
	}

	public override void updateTapCount()
	{
		if (previousState == global::Kampai.UI.View.OrderBoardButtonState.Rush)
		{
			base.updateTapCount();
		}
	}

	public override bool isDoubleConfirmed()
	{
		if (previousState == global::Kampai.UI.View.OrderBoardButtonState.Rush)
		{
			if (base.localPersistService.GetDataIntPlayer("DoublePurchaseConfirm") != 0)
			{
				return tapCount == 2;
			}
			return true;
		}
		return true;
	}

	internal global::Kampai.UI.View.OrderBoardButtonState GetLastFillOrderButtonState()
	{
		return previousState;
	}

	internal int GetLastRushCost()
	{
		return currentRushCost;
	}

	internal void SetFillOrderButtonState(global::Kampai.UI.View.OrderBoardButtonState state, int rushCost = -1)
	{
		if (previousState == state && currentRushCost == rushCost)
		{
			if (state == global::Kampai.UI.View.OrderBoardButtonState.Rush)
			{
				ResetTapState();
			}
			return;
		}
		if (state == global::Kampai.UI.View.OrderBoardButtonState.Enable)
		{
			state = global::Kampai.UI.View.OrderBoardButtonState.Rush;
			rushCost = currentRushCost;
		}
		if (state == global::Kampai.UI.View.OrderBoardButtonState.Rush && rushCost == 0)
		{
			state = global::Kampai.UI.View.OrderBoardButtonState.MeetRequirement;
		}
		if (previousState == global::Kampai.UI.View.OrderBoardButtonState.Hide)
		{
			base.gameObject.SetActive(true);
		}
		if (previousState == global::Kampai.UI.View.OrderBoardButtonState.Disable)
		{
			SetupFillOrderButton(true);
		}
		DisableDoubleConfirm();
		switch (state)
		{
		case global::Kampai.UI.View.OrderBoardButtonState.Disable:
			animator.Play("Disabled");
			SetupFillOrderButton(false);
			FillOrderRushIcon.gameObject.SetActive(false);
			FillOrderRushCost.gameObject.SetActive(false);
			FillOrderText.anchorMax = global::UnityEngine.Vector2.one;
			break;
		case global::Kampai.UI.View.OrderBoardButtonState.Hide:
			base.gameObject.SetActive(false);
			break;
		case global::Kampai.UI.View.OrderBoardButtonState.MeetRequirement:
			currentRushCost = 0;
			FillOrderText.anchorMax = global::UnityEngine.Vector2.one;
			FillOrderRushIcon.gameObject.SetActive(false);
			FillOrderRushCost.gameObject.SetActive(false);
			animator.Play("MeetRequirement");
			SetupFillOrderButton(true);
			break;
		case global::Kampai.UI.View.OrderBoardButtonState.Rush:
			ResetTapState();
			EnableDoubleConfirm();
			FillOrderText.anchorMax = new global::UnityEngine.Vector2(fillOrderTextAnchorMaxXForRushing, 1f);
			FillOrderRushIcon.gameObject.SetActive(true);
			FillOrderRushCost.gameObject.SetActive(true);
			FillOrderRushCost.text = rushCost.ToString();
			animator.Play("Rush");
			break;
		}
		previousState = state;
		if (rushCost != -1)
		{
			currentRushCost = rushCost;
		}
	}

	private void SetupFillOrderButton(bool active)
	{
		global::UnityEngine.UI.Button component = GetComponent<global::UnityEngine.UI.Button>();
		component.interactable = active;
	}
}
