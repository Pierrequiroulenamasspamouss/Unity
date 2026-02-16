public class SocialPartyFillOrderButtonMediator : global::Kampai.UI.View.KampaiMediator
{
	public struct SocialPartyFillOrderButtonMediatorData
	{
		public global::Kampai.Game.SocialTeam team;

		public global::Kampai.Game.SocialOrderProgress progress;

		public global::Kampai.Game.SocialEventOrderDefinition orderDefintion;

		public global::UnityEngine.GameObject parent;

		public global::Kampai.Game.Transaction.TransactionDefinition orderTransactionDefinition;

		public int index;

		public global::UnityEngine.Vector3 centerPos;
	}

	private SocialPartyFillOrderButtonMediator.SocialPartyFillOrderButtonMediatorData data;

	private global::Kampai.Game.Transaction.TransactionDefinition orderTransactionDefinition;

	private uint playerItemQuantity;

	private uint requiredItemQuantity;

	private int index;

	public float ButtonWidth = 0.32f;

	public float ButtonHeight = 0.32f;

	[Inject]
	public global::Kampai.UI.View.SocialPartyFillOrderButtonView view { get; set; }

	[Inject]
	public global::Kampai.Game.ITimedSocialEventService timedSocialEventService { get; set; }

	[Inject]
	public global::Kampai.Game.IDefinitionService definitionService { get; set; }

	[Inject]
	public global::Kampai.Game.IPlayerService playerService { get; set; }

	[Inject(global::Kampai.Game.SocialServices.FACEBOOK)]
	public global::Kampai.Game.ISocialService facebookService { get; set; }

	[Inject]
	public global::Kampai.UI.View.IGUIService guiService { get; set; }

	[Inject]
	public global::Kampai.UI.View.ShowSocialPartyRewardSignal socialPartyRewardSignal { get; set; }

	[Inject]
	public global::Kampai.Common.ITelemetryService telemetryService { get; set; }

	[Inject]
	public global::Kampai.Main.PlayGlobalSoundFXSignal globalSFX { get; set; }

	[Inject]
	public global::Kampai.Util.ILogger logger { get; set; }

	[Inject]
	public global::Kampai.Game.LoadRushDialogSignal loadRushDialogSignal { get; set; }

	[Inject(global::Kampai.UI.View.UIElement.CAMERA)]
	public global::UnityEngine.Camera uiCamera { get; set; }

	[Inject]
	public global::Kampai.UI.View.SocialPartyFillOrderButtonMediatorUpdateSignal socialPartyFillOrderButtonMediatorUpdateSignal { get; set; }

	[Inject]
	public global::Kampai.Main.PlayGlobalSoundFXSignal soundFXSignal { get; set; }

	[Inject]
	public global::Kampai.Main.ILocalizationService localService { get; set; }

	[Inject]
	public global::Kampai.Common.NetworkConnectionLostSignal networkConnectionLostSignal { get; set; }

	[Inject]
	public global::Kampai.UI.View.SocialPartyFillOrderSetupUISignal socialPartyFillOrderSetupUISignal { get; set; }

	[Inject]
	public global::Kampai.UI.View.SpawnDooberSignal tweenSignal { get; set; }

	[Inject]
	public global::Kampai.UI.View.HideSkrimSignal hideSignal { get; set; }

	[Inject]
	public global::Kampai.UI.View.SetStorageCapacitySignal setStorageCapacitySignal { get; set; }

	public override void Initialize(global::Kampai.UI.View.GUIArguments args)
	{
		data = args.Get<SocialPartyFillOrderButtonMediator.SocialPartyFillOrderButtonMediatorData>();
		index = data.index;
		int num = index / 3;
		int num2 = num;
		int num3 = index % 3;
		global::UnityEngine.RectTransform rectTransform = view.transform as global::UnityEngine.RectTransform;
		rectTransform.SetParent(data.parent.transform);
		rectTransform.localPosition = global::UnityEngine.Vector3.zero;
		rectTransform.localScale = global::UnityEngine.Vector3.one;
		rectTransform.offsetMin = new global::UnityEngine.Vector2(0f, 0f);
		rectTransform.offsetMax = new global::UnityEngine.Vector2(0f, 0f);
		rectTransform.anchorMin = new global::UnityEngine.Vector2((0.5f - 0.5f * ButtonWidth) * (float)num3, 1f + 0.5f * (ButtonHeight - 1f) * (float)num2 - ButtonHeight);
		rectTransform.anchorMax = new global::UnityEngine.Vector2((0.5f - 0.5f * ButtonWidth) * (float)num3 + ButtonWidth, 1f + 0.5f * (ButtonHeight - 1f) * (float)num2);
		view.fillOrderButton.Init();
		SetupOrder();
		global::Kampai.Game.DisplayableDefinition displayableDefinition = definitionService.Get(orderTransactionDefinition.Inputs[0].ID) as global::Kampai.Game.DisplayableDefinition;
		view.CreatFillOrderPopupIndicator(displayableDefinition.Image, displayableDefinition.Mask);
	}

	public override void OnRegister()
	{
		socialPartyFillOrderButtonMediatorUpdateSignal.AddListener(UpdateDetails);
		view.fillOrderButton.ClickedSignal.AddListener(FillOrderButton);
	}

	public override void OnRemove()
	{
		view.fillOrderButton.ClickedSignal.RemoveListener(FillOrderButton);
		socialPartyFillOrderButtonMediatorUpdateSignal.RemoveListener(UpdateDetails);
	}

	public void UpdateDetails(SocialPartyFillOrderButtonMediator.SocialPartyFillOrderButtonMediatorData details)
	{
		if (details.index == data.index)
		{
			data = details;
			SetupOrder();
		}
	}

	public void SetupOrder()
	{
		orderTransactionDefinition = definitionService.Get(data.orderDefintion.Transaction) as global::Kampai.Game.Transaction.TransactionDefinition;
		data.orderTransactionDefinition = orderTransactionDefinition;
		if (data.progress == null || data.progress.CompletedByUserId == null)
		{
			SetupIncompleteOrder();
		}
		else
		{
			SetupCompleteOrder();
		}
	}

	private void SetupIncompleteOrder()
	{
		view.orderOpenPanel.SetActive(true);
		view.orderClosedPanel.SetActive(false);
		playerItemQuantity = playerService.GetQuantityByDefinitionId(orderTransactionDefinition.Inputs[0].ID);
		requiredItemQuantity = orderTransactionDefinition.Inputs[0].Quantity;
		view.orderOpenTextAmount.text = string.Format("{0}/{1}", playerItemQuantity, requiredItemQuantity);
		foreach (global::Kampai.Util.QuantityItem output in orderTransactionDefinition.Outputs)
		{
			if (output.ID == 0)
			{
				view.grindReward.text = output.Quantity.ToString();
			}
			else if (output.ID == 2)
			{
				view.xpReward.text = output.Quantity.ToString();
			}
		}
		if (playerItemQuantity == requiredItemQuantity)
		{
			view.fillOrderButton.SetFillOrderButtonState(global::Kampai.UI.View.OrderBoardButtonState.MeetRequirement);
			return;
		}
		view.missingItems = playerService.GetMissingItemListFromTransaction(orderTransactionDefinition);
		int rushCost = playerService.CalculateRushCost(view.missingItems);
		view.fillOrderButton.SetFillOrderButtonState(global::Kampai.UI.View.OrderBoardButtonState.Rush, rushCost);
	}

	private void SetupCompleteOrder()
	{
		view.orderOpenPanel.SetActive(false);
		view.orderClosedPanel.SetActive(true);
		if (facebookService.isLoggedIn)
		{
			foreach (global::Kampai.Game.UserIdentity member in data.team.Members)
			{
				if (member.UserID == data.progress.CompletedByUserId)
				{
					global::Kampai.Game.FBDownloadPicture fBDownloadPicture = new global::Kampai.Game.FBDownloadPicture(logger);
					StartCoroutine(fBDownloadPicture.GetPicture(member.ExternalID, facebookService.accessToken, 256, 256, OnFacebookPictureComplete));
				}
			}
			return;
		}
		view.orderClosedImagePicture.sprite = UIUtils.LoadSpriteFromPath("icn_questGiver_minPhil_fill");
	}

	private void FillOrderButton()
	{
		if (view.fillOrderButton.isDoubleConfirmed())
		{
			if (view.fillOrderButton.GetLastRushCost() > 0)
			{
				soundFXSignal.Dispatch("Play_button_premium_01");
				playerService.ProcessRush(view.fillOrderButton.GetLastRushCost(), view.missingItems, true, RushItemCallBack, true);
			}
			else
			{
				soundFXSignal.Dispatch("Play_button_click_01");
				FinshButtonClick();
			}
		}
	}

	private void RushItemCallBack(global::Kampai.Game.PendingCurrencyTransaction pendingTransaction)
	{
		if (pendingTransaction.Success)
		{
			FinshButtonClick();
		}
	}

	private void FinshButtonClick()
	{
		global::strange.extensions.signal.impl.Signal<global::Kampai.Game.SocialTeamResponse, global::Kampai.Game.ErrorResponse> signal = new global::strange.extensions.signal.impl.Signal<global::Kampai.Game.SocialTeamResponse, global::Kampai.Game.ErrorResponse>();
		signal.AddListener(FillOrderResponse);
		timedSocialEventService.FillOrder(data.team.SocialEventId, data.team.ID, data.orderDefintion.OrderID, signal);
		PlayTweens();
	}

	private void FillOrderResponse(global::Kampai.Game.SocialTeamResponse response, global::Kampai.Game.ErrorResponse error)
	{
		if (response.Error != null && response.Error.Type == global::Kampai.Game.SocialEventError.ErrorType.ORDER_ALREADY_FILLED)
		{
			FillOrderComplete(response, null);
		}
		else if (response.Error == null)
		{
			playerService.RunEntireTransaction(orderTransactionDefinition, global::Kampai.Game.TransactionTarget.NO_VISUAL, delegate(global::Kampai.Game.PendingCurrencyTransaction pct)
			{
				FillOrderComplete(response, pct);
			});
		}
		else
		{
			networkConnectionLostSignal.Dispatch();
		}
	}

	private void FillOrderComplete(global::Kampai.Game.SocialTeamResponse response, global::Kampai.Game.PendingCurrencyTransaction pct)
	{
		if (pct != null && !pct.Success)
		{
			logger.Warning("Fill Order transaction failed.");
		}
		else
		{
			global::Kampai.Game.ItemDefinition itemDefinition = definitionService.Get<global::Kampai.Game.ItemDefinition>(orderTransactionDefinition.Inputs[0].ID);
			telemetryService.Send_Telemetry_EVT_SOCIAL_EVENT_CONTRIBUTION(itemDefinition.Description, (int)orderTransactionDefinition.Inputs[0].Quantity, response.Team.Members.Count);
		}
		setStorageCapacitySignal.Dispatch();
		if (!response.UserEvent.RewardClaimed && response.Team.OrderProgress.Count == timedSocialEventService.GetCurrentSocialEvent().Orders.Count)
		{
			hideSignal.Dispatch("SocialSkrim");
			guiService.Execute(global::Kampai.UI.View.GUIOperation.Unload, "SocialPartyFillOrderScreen");
			socialPartyRewardSignal.Dispatch(timedSocialEventService.GetCurrentSocialEvent().ID);
		}
		else
		{
			socialPartyFillOrderSetupUISignal.Dispatch(response.Team);
		}
	}

	private void PlayTweens()
	{
		tweenSignal.Dispatch(uiCamera.WorldToScreenPoint(view.grindImage.transform.position), global::Kampai.UI.View.DestinationType.GRIND, 0, false);
		tweenSignal.Dispatch(uiCamera.WorldToScreenPoint(view.xpImage.transform.position), global::Kampai.UI.View.DestinationType.XP, 2, false);
	}

	public void OnFacebookPictureComplete(global::UnityEngine.Texture texture, string id)
	{
		if (texture != null)
		{
			global::UnityEngine.Sprite sprite = global::UnityEngine.Sprite.Create(texture as global::UnityEngine.Texture2D, new global::UnityEngine.Rect(0f, 0f, texture.width, texture.height), new global::UnityEngine.Vector2(0f, 0f));
			view.orderClosedImagePicture.sprite = sprite;
		}
		else
		{
			logger.Warning("OnFacebookPictureComplete texture is null");
		}
	}
}
