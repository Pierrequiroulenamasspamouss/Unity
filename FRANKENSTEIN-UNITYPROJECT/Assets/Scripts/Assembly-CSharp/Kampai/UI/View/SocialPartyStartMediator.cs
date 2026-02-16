namespace Kampai.UI.View
{
	public class SocialPartyStartMediator : global::Kampai.UI.View.UIStackMediator<global::Kampai.UI.View.SocialPartyStartView>
	{
		private global::System.DateTime finishTime;

		private bool isEventAccepted;

		[Inject]
		public global::Kampai.UI.View.IGUIService guiService { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowSocialPartyFBConnectSignal showPartyFBConnectSignal { get; set; }

		[Inject]
		public global::Kampai.Game.ITimedSocialEventService timedSocialEventService { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService localService { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal globalSFX { get; set; }

		[Inject]
		public global::Kampai.UI.View.RemoveWayFinderSignal removeWayFinderSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.CreateWayFinderSignal createWayFinderSignal { get; set; }

		[Inject]
		public global::Kampai.Common.ICoppaService coppaService { get; set; }

		[Inject]
		public global::Kampai.UI.View.HideSkrimSignal hideSkrim { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowSocialPartyFillOrderSignal showPartyFillOrderSignal { get; set; }

		[Inject(global::Kampai.Game.SocialServices.FACEBOOK)]
		public global::Kampai.Game.ISocialService facebookService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Common.NetworkConnectionLostSignal networkConnectionLostSignal { get; set; }

		[Inject(global::Kampai.Game.GameElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable gameContext { get; set; }

		[Inject]
		public global::Kampai.Game.IZoomCameraModel zoomCameraModel { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeService timeService { get; set; }

		public override void OnRegister()
		{
			base.OnRegister();
			base.view.Init();
			base.view.goButton.ClickedSignal.AddListener(GoButton);
			base.view.OnMenuClose.AddListener(CloseAnimationComplete);
			base.view.itemCheckmark1.gameObject.SetActive(false);
			base.view.itemCheckmark2.gameObject.SetActive(false);
			base.view.itemCheckmark3.gameObject.SetActive(false);
			base.view.itemCheckmark4.gameObject.SetActive(false);
			SetupIcons();
			SetupText();
			StartTime();
			CloseWayfinder();
		}

		public override void OnRemove()
		{
			base.OnRemove();
			base.view.goButton.ClickedSignal.RemoveListener(GoButton);
			base.view.OnMenuClose.RemoveListener(CloseAnimationComplete);
			OpenWayfinder();
		}

		protected override void Close()
		{
			base.view.Close();
		}

		public void CloseAnimationComplete()
		{
			gameContext.injectionBinder.GetInstance<global::Kampai.Game.BuildingZoomSignal>().Dispatch(new global::Kampai.Game.BuildingZoomSettings(global::Kampai.Game.ZoomType.OUT, zoomCameraModel.LastZoomBuildingType));
			hideSkrim.Dispatch("StageSkrim");
			guiService.Execute(global::Kampai.UI.View.GUIOperation.Unload, "cmp_FlyOut_SocialParty");
			if (isEventAccepted)
			{
				showPartyFillOrderSignal.Dispatch();
			}
		}

		private void CloseWayfinder()
		{
			global::Kampai.Game.StageBuilding firstInstanceByDefinitionId = playerService.GetFirstInstanceByDefinitionId<global::Kampai.Game.StageBuilding>(3054);
			removeWayFinderSignal.Dispatch(firstInstanceByDefinitionId.ID);
		}

		private void OpenWayfinder()
		{
			global::Kampai.Game.StageBuilding firstInstanceByDefinitionId = playerService.GetFirstInstanceByDefinitionId<global::Kampai.Game.StageBuilding>(3054);
			createWayFinderSignal.Dispatch(new global::Kampai.UI.View.WayFinderSettings(firstInstanceByDefinitionId.ID));
		}

		private void SetupIcons()
		{
			global::System.Collections.Generic.IList<global::Kampai.Game.SocialEventOrderDefinition> orders = timedSocialEventService.GetCurrentSocialEvent().Orders;
			int num = 0;
			for (int i = 0; i < orders.Count; i++)
			{
				global::Kampai.Game.Transaction.TransactionDefinition transactionDefinition = definitionService.Get(orders[i].Transaction) as global::Kampai.Game.Transaction.TransactionDefinition;
				if (transactionDefinition == null || checkForDuplicate(orders, i))
				{
					continue;
				}
				global::Kampai.Game.DisplayableDefinition displayableDefinition = definitionService.Get(transactionDefinition.Inputs[0].ID) as global::Kampai.Game.DisplayableDefinition;
				if (displayableDefinition != null)
				{
					string image = displayableDefinition.Image;
					string mask = displayableDefinition.Mask;
					global::UnityEngine.Sprite sprite = UIUtils.LoadSpriteFromPath(image);
					global::UnityEngine.Sprite maskSprite = UIUtils.LoadSpriteFromPath(mask);
					switch (num)
					{
					case 0:
						base.view.itemImage1.sprite = sprite;
						base.view.itemImage1.maskSprite = maskSprite;
						break;
					case 1:
						base.view.itemImage2.sprite = sprite;
						base.view.itemImage2.maskSprite = maskSprite;
						break;
					case 2:
						base.view.itemImage3.sprite = sprite;
						base.view.itemImage3.maskSprite = maskSprite;
						break;
					case 3:
						base.view.itemImage4.sprite = sprite;
						base.view.itemImage4.maskSprite = maskSprite;
						break;
					}
					num++;
					if (num >= 4)
					{
						break;
					}
				}
			}
			removeUnusedIcons(num);
		}

		private bool checkForDuplicate(global::System.Collections.Generic.IList<global::Kampai.Game.SocialEventOrderDefinition> orders, int i)
		{
			global::Kampai.Game.Transaction.TransactionDefinition transactionDefinition = definitionService.Get(orders[i].Transaction) as global::Kampai.Game.Transaction.TransactionDefinition;
			for (i--; i >= 0; i--)
			{
				global::Kampai.Game.Transaction.TransactionDefinition transactionDefinition2 = definitionService.Get(orders[i].Transaction) as global::Kampai.Game.Transaction.TransactionDefinition;
				if (transactionDefinition2 != null && transactionDefinition2.ID == transactionDefinition.ID)
				{
					return true;
				}
			}
			return false;
		}

		private void removeUnusedIcons(int count)
		{
			if (count < 1)
			{
				base.view.itemImage1.gameObject.SetActive(false);
				base.view.itemBacking1.gameObject.SetActive(false);
			}
			if (count < 2)
			{
				base.view.itemImage2.gameObject.SetActive(false);
				base.view.itemBacking2.gameObject.SetActive(false);
			}
			if (count < 3)
			{
				base.view.itemImage3.gameObject.SetActive(false);
				base.view.itemBacking3.gameObject.SetActive(false);
			}
			if (count < 4)
			{
				base.view.itemImage4.gameObject.SetActive(false);
				base.view.itemBacking4.gameObject.SetActive(false);
			}
		}

		private void SetupText()
		{
			base.view.goButtonText.text = localService.GetString("socialpartystartbutton");
			base.view.itemTitle.text = localService.GetString("socialpartystarttitle");
			base.view.description.text = localService.GetString("socialpartystartdescription");
			base.view.timeTitle.text = localService.GetString("socialpartystarttimetitle");
			base.view.itemAmount1.gameObject.SetActive(false);
			base.view.itemAmount2.gameObject.SetActive(false);
			base.view.itemAmount3.gameObject.SetActive(false);
			base.view.itemAmount4.gameObject.SetActive(false);
		}

		public override void Initialize(global::Kampai.UI.View.GUIArguments args)
		{
		}

		private void StartTime()
		{
			long num = timedSocialEventService.GetCurrentSocialEvent().FinishTime;
			finishTime = new global::System.DateTime(1970, 1, 1, 0, 0, 0, 0, global::System.DateTimeKind.Utc);
			finishTime = finishTime.AddSeconds(num).ToLocalTime();
			UpdateTime();
		}

		private void UpdateTime()
		{
			global::System.DateTime dateTime = new global::System.DateTime(1970, 1, 1, 0, 0, 0, 0, global::System.DateTimeKind.Utc).AddSeconds(timeService.GameTimeSeconds()).ToLocalTime();
			global::System.TimeSpan timeSpan = finishTime.Subtract(dateTime);
			if (dateTime < finishTime)
			{
				base.view.timeRemaining.text = string.Format("{0:00}:{1:00}:{2:00}:{3:00}", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
			}
			else
			{
				base.view.timeRemaining.text = "Finished";
			}
		}

		public void Update()
		{
			UpdateTime();
		}

		private void GoButton()
		{
			global::UnityEngine.UI.Button component = base.view.goButton.GetComponent<global::UnityEngine.UI.Button>();
			component.interactable = false;
			globalSFX.Dispatch("Play_button_click_01");
			global::strange.extensions.signal.impl.Signal<global::Kampai.Game.SocialTeamResponse, global::Kampai.Game.ErrorResponse> signal = new global::strange.extensions.signal.impl.Signal<global::Kampai.Game.SocialTeamResponse, global::Kampai.Game.ErrorResponse>();
			signal.AddListener(OnCreateTeamResponse);
			timedSocialEventService.CreateSocialTeam(timedSocialEventService.GetCurrentSocialEvent().ID, signal);
		}

		public void OnCreateTeamResponse(global::Kampai.Game.SocialTeamResponse response, global::Kampai.Game.ErrorResponse error)
		{
			if (error != null)
			{
				networkConnectionLostSignal.Dispatch();
				return;
			}
			Close();
			if (response.Team != null)
			{
				isEventAccepted = true;
			}
			else
			{
				logger.Error("OnCreateTeamResponse has no team");
			}
		}
	}
}
