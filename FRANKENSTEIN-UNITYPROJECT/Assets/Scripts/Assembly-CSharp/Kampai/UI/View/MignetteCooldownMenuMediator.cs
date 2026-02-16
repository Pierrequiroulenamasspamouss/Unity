namespace Kampai.UI.View
{
	public class MignetteCooldownMenuMediator : global::Kampai.UI.View.UIStackMediator<global::Kampai.UI.View.MignetteCooldownMenuView>
	{
		private global::Kampai.Game.MignetteBuilding mignetteBuilding;

		private int rushCost;

		[Inject]
		public global::Kampai.Game.BuildingChangeStateSignal buildingStateSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.IGUIService guiService { get; set; }

		[Inject]
		public global::Kampai.Game.Mignette.MignetteGameModel mignetteGameModel { get; set; }

		[Inject]
		public global::Kampai.Game.BuildingCooldownCompleteSignal onCooldownCompleteSignal { get; set; }

		[Inject]
		public global::Kampai.Common.DeselectAllMinionsSignal deselectMinionsSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowBuildingDetailMenuSignal showDetailMenuSignal { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeEventService timeEventService { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal playSFXSignal { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.UI.View.HideSkrimSignal hideSkrim { get; set; }

		public override void OnRegister()
		{
			base.OnRegister();
			base.view.modal.rushButton.ClickedSignal.AddListener(Rush);
			onCooldownCompleteSignal.AddListener(OnBuildingCooldownComplete);
		}

		public override void OnRemove()
		{
			base.OnRemove();
			onCooldownCompleteSignal.RemoveListener(OnBuildingCooldownComplete);
		}

		public override void Initialize(global::Kampai.UI.View.GUIArguments args)
		{
			global::Kampai.Game.MignetteBuilding building = args.Get<global::Kampai.Game.MignetteBuilding>();
			Init(building);
		}

		private void Init(global::Kampai.Game.MignetteBuilding building)
		{
			if (building != null)
			{
				if (!timeEventService.HasEventID(building.ID))
				{
					Exit(true);
					return;
				}
				mignetteBuilding = building;
				mignetteGameModel.BuildingId = mignetteBuilding.ID;
				base.view.StartTime(building.StateStartTime, building.StateStartTime + mignetteBuilding.GetCooldown());
				InvokeRepeating("UpdateTime", 0.001f, 1f);
			}
		}

		public void UpdateTime()
		{
			int timeRemaining = timeEventService.GetTimeRemaining(mignetteBuilding.ID);
			base.view.UpdateTime(timeRemaining);
			rushCost = timeEventService.CalculateRushCostForTimer(timeRemaining, global::Kampai.Game.RushActionType.COOLDOWN);
			base.view.SetRushCost(rushCost);
		}

		protected override void Close()
		{
			Exit(false);
		}

		private void Rush()
		{
			if (base.view.modal.rushButton.isDoubleConfirmed())
			{
				playerService.ProcessRush(rushCost, true, RushTransactionCallback, mignetteBuilding.ID);
			}
		}

		private void RushTransactionCallback(global::Kampai.Game.PendingCurrencyTransaction pct)
		{
			if (pct.Success)
			{
				timeEventService.RushEvent(mignetteBuilding.ID);
				playSFXSignal.Dispatch("Play_button_premium_01");
			}
		}

		private void OnBuildingCooldownComplete(int instanceId)
		{
			if (mignetteBuilding != null && instanceId == mignetteBuilding.ID)
			{
				Exit(true);
			}
		}

		private void Exit(bool goToCallMenu)
		{
			base.view.Close();
			hideSkrim.Dispatch("MignetteSkrim");
			guiService.Execute(global::Kampai.UI.View.GUIOperation.Unload, "MignetteCooldownMenu");
			if (goToCallMenu)
			{
				buildingStateSignal.Dispatch(mignetteBuilding.ID, global::Kampai.Game.BuildingState.Idle);
				deselectMinionsSignal.Dispatch();
				showDetailMenuSignal.Dispatch(mignetteBuilding);
			}
		}
	}
}
