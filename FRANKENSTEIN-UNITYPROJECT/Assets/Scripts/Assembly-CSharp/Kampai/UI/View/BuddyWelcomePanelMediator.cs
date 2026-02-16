namespace Kampai.UI.View
{
	public class BuddyWelcomePanelMediator : global::Kampai.UI.View.KampaiMediator
	{
		private global::Kampai.UI.View.CharacterWelcomeState currentState;

		[Inject]
		public global::Kampai.UI.View.BuddyWelcomePanelView view { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService localizationService { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal playSFXSignal { get; set; }

		[Inject]
		public global::Kampai.Game.IPrestigeService prestigeService { get; set; }

		[Inject]
		public global::Kampai.UI.View.FTUECloseBuddySignal closeBuddySignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.IGUIService guiService { get; set; }

		[Inject]
		public global::Kampai.Game.UnlockMinionsSignal unlockMinionsSignal { get; set; }

		[Inject]
		public global::Kampai.Game.IZoomCameraModel zoomCameraModel { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		[Inject]
		public global::Kampai.Game.CameraAutoMoveToInstanceSignal cameraAutoMoveToInstanceSignal { get; set; }

		[Inject]
		public global::Kampai.UI.IPositionService positionService { get; set; }

		public override void OnRegister()
		{
			base.OnRegister();
			view.OnMenuClose.AddListener(OnMenuClose);
			view.SetUpInjections(positionService);
		}

		public override void OnRemove()
		{
			base.OnRemove();
			view.OnMenuClose.RemoveListener(OnMenuClose);
		}

		public override void Initialize(global::Kampai.UI.View.GUIArguments args)
		{
			unlockMinionsSignal.Dispatch();
			global::Kampai.Game.Prestige prestige = args.Get<global::Kampai.Game.Prestige>();
			global::Kampai.UI.View.CharacterWelcomeState state = args.Get<global::Kampai.UI.View.CharacterWelcomeState>();
			Load(prestige, state);
			routineRunner.StartCoroutine(AnimSequence(prestige, state));
		}

		private global::System.Collections.IEnumerator AnimSequence(global::Kampai.Game.Prestige prestige, global::Kampai.UI.View.CharacterWelcomeState state)
		{
			yield return new global::UnityEngine.WaitForSeconds(0.2f);
			if (prestige.Definition.Type == global::Kampai.Game.PrestigeType.Minion || state == global::Kampai.UI.View.CharacterWelcomeState.Welcome)
			{
				global::Kampai.Game.PanInstructions panInstructions = new global::Kampai.Game.PanInstructions(prestige.trackedInstanceId)
				{
					ZoomDistance = new global::Kampai.Util.Boxed<float>(1f),
					CameraMovementSettings = new global::Kampai.Game.CameraMovementSettings(global::Kampai.Game.CameraMovementSettings.Settings.None, null, null)
				};
				cameraAutoMoveToInstanceSignal.Dispatch(panInstructions);
			}
			global::Kampai.Game.View.ActionableObject actionableObject = global::Kampai.Game.View.ActionableObjectManagerView.GetFromAllObjects(prestige.trackedInstanceId);
			view.SetUpCharacterObject(actionableObject as global::Kampai.Game.View.CharacterObject);
			yield return new global::UnityEngine.WaitForSeconds(0.8f);
			view.Initialized = true;
			view.Open();
			yield return new global::UnityEngine.WaitForSeconds(view.FadeOutTime);
			Close();
		}

		private void OnMenuClose()
		{
			closeBuddySignal.Dispatch();
			guiService.Execute(global::Kampai.UI.View.GUIOperation.Unload, "popup_CharacterState");
		}

		private void Close()
		{
			playSFXSignal.Dispatch("Play_menu_disappear_01");
			view.Close();
		}

		public void Load(global::Kampai.Game.Prestige prestige, global::Kampai.UI.View.CharacterWelcomeState state)
		{
			currentState = state;
			PlayAudio(prestige);
			global::Kampai.Game.PrestigeDefinition prestigeDefinition = ((prestige != null) ? prestige.Definition : null);
			switch (state)
			{
			case global::Kampai.UI.View.CharacterWelcomeState.Farewell:
			{
				string title = localizationService.GetString("FarewellTitle");
				string text = localizationService.GetString(prestigeDefinition.LocalizedKey);
				view.Init(title, text);
				break;
			}
			case global::Kampai.UI.View.CharacterWelcomeState.Welcome:
			{
				string title = ((prestige.CurrentPrestigeLevel <= 0) ? localizationService.GetString("WelcomeTitle") : localizationService.GetString("RePrestigeTitle"));
				string text = localizationService.GetString(prestigeDefinition.LocalizedKey);
				view.Init(title, text);
				break;
			}
			}
		}

		private void PlayAudio(global::Kampai.Game.Prestige prestige)
		{
			global::Kampai.Game.PrestigeType type = prestige.Definition.Type;
			if (currentState == global::Kampai.UI.View.CharacterWelcomeState.Welcome && type == global::Kampai.Game.PrestigeType.Minion)
			{
				playSFXSignal.Dispatch("Play_minionArrives_01");
			}
			else if (currentState == global::Kampai.UI.View.CharacterWelcomeState.Farewell && type == global::Kampai.Game.PrestigeType.Minion)
			{
				playSFXSignal.Dispatch("Play_minionUnlock_01");
			}
			else if (currentState == global::Kampai.UI.View.CharacterWelcomeState.Welcome && type == global::Kampai.Game.PrestigeType.Villain)
			{
				playSFXSignal.Dispatch("Play_villainArrives_01");
			}
			else if (currentState == global::Kampai.UI.View.CharacterWelcomeState.Farewell && type == global::Kampai.Game.PrestigeType.Villain)
			{
				playSFXSignal.Dispatch("Play_villainLeaves_01");
			}
		}
	}
}
