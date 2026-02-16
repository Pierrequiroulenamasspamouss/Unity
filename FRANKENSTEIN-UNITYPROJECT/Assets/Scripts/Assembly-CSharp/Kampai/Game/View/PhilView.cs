namespace Kampai.Game.View
{
	public class PhilView : global::Kampai.Game.View.NamedMinionView
	{
		private string TikiBarStateMachine;

		private global::strange.extensions.signal.impl.Signal<string, global::System.Type, object> tikiBarAnimSignal = new global::strange.extensions.signal.impl.Signal<string, global::System.Type, object>();

		private global::System.Collections.Generic.List<global::UnityEngine.Vector3> introPath;

		private global::UnityEngine.Vector3 introEulers;

		private float introTime;

		public override global::strange.extensions.signal.impl.Signal<string, global::System.Type, object> AnimSignal
		{
			get
			{
				return tikiBarAnimSignal;
			}
		}

		protected override string AttentionString
		{
			get
			{
				return "bartender_IsGetAttention";
			}
		}

		public override global::Kampai.Game.View.NamedCharacterObject Build(global::Kampai.Game.NamedCharacter character, global::UnityEngine.GameObject parent, global::Kampai.Util.ILogger logger, global::Kampai.Util.IMinionBuilder minionBuilder)
		{
			base.Build(character, parent, logger, minionBuilder);
			global::Kampai.Game.PhilCharacter philCharacter = character as global::Kampai.Game.PhilCharacter;
			TikiBarStateMachine = philCharacter.Definition.TikiBarStateMachine;
			introPath = new global::System.Collections.Generic.List<global::UnityEngine.Vector3>(philCharacter.Definition.IntroPath);
			introEulers = philCharacter.Definition.IntroEulers;
			introTime = philCharacter.Definition.IntroTime;
			return this;
		}

		internal void GoToStartLocation()
		{
			Animate("walk");
			GoSpline path = new GoSpline(introPath);
			GoTween tween = new GoTween(base.transform, 0.25f, new GoTweenConfig().rotation(introEulers));
			GoTween tween2 = new GoTween(base.transform, introTime, new GoTweenConfig().setEaseType(GoEaseType.Linear).positionPath(path, false, GoLookAtType.NextPathNode).onComplete(delegate(AbstractGoTween thisTween)
			{
				thisTween.destroy();
				StopWalking();
			}));
			GoTweenFlow goTweenFlow = new GoTweenFlow();
			goTweenFlow.insert(0f, tween2).insert(1f, tween).play();
		}

		internal void Activate(bool activate)
		{
			string type = "bartender_IsActivated";
			SetAnimBool(type, activate);
			tikiBarAnimSignal.Dispatch(type, typeof(bool), activate);
			if (!activate && GetAnimBool("IsSeated"))
			{
				base.IsIdle = true;
			}
			else
			{
				base.IsIdle = false;
			}
		}

		internal void SitAtBar(object sit, object teleportSignal)
		{
			bool flag = (bool)sit;
			global::Kampai.Game.TeleportCharacterToTikiBarSignal teleportCharacterToTikiBarSignal = (global::Kampai.Game.TeleportCharacterToTikiBarSignal)teleportSignal;
			teleportCharacterToTikiBarSignal.Dispatch(this, 0);
			string type = "bartender_IsSeated";
			SetAnimBool(type, flag);
			tikiBarAnimSignal.Dispatch(type, typeof(bool), flag);
			if (flag && !GetAnimBool("isActivated"))
			{
				base.IsIdle = true;
			}
			else
			{
				base.IsIdle = false;
			}
		}

		internal void Celebrate()
		{
			string type = "bartender_OnCelebrate";
			SetAnimBool(type, true);
			tikiBarAnimSignal.Dispatch(type, typeof(bool), true);
		}

		internal void GetAttention(bool enable)
		{
			base.IsAtAttention = enable;
			SetAnimBool(AttentionStartString, enable);
			SetAnimBool(AttentionString, enable);
			tikiBarAnimSignal.Dispatch(AttentionStartString, typeof(bool), enable);
			tikiBarAnimSignal.Dispatch(AttentionString, typeof(bool), enable);
		}

		internal void BeginIntroLoop()
		{
			EnqueueAction(new global::Kampai.Game.View.WaitForMecanimStateAction(this, global::UnityEngine.Animator.StringToHash("Base Layer.NewMinionIntro"), logger));
			EnqueueAction(new global::Kampai.Game.View.WaitForMecanimStateAction(this, global::UnityEngine.Animator.StringToHash("Base Layer.Occupied"), logger));
			EnqueueAction(new global::Kampai.Game.View.DelegateAction(IntroComplete, logger));
			string type = "OnNewMinionAppear";
			string type2 = "NewMinionSequence";
			SetAnimBool(type, true);
			SetAnimBool(type2, true);
			tikiBarAnimSignal.Dispatch(type, typeof(bool), true);
			tikiBarAnimSignal.Dispatch(type2, typeof(bool), true);
		}

		internal void PlayIntro()
		{
			string type = "OnNewMinionIntro";
			SetAnimBool(type, true);
			tikiBarAnimSignal.Dispatch(type, typeof(bool), true);
		}

		internal void Animate(string animation)
		{
			SetAnimBool(animation, true);
			tikiBarAnimSignal.Dispatch(animation, typeof(bool), true);
		}

		internal void StopWalking()
		{
			string type = "walk";
			SetAnimBool(type, false);
			tikiBarAnimSignal.Dispatch(type, typeof(bool), false);
		}

		internal void GotoTikiBar(global::UnityEngine.GameObject tikiBar, global::Kampai.Game.TeleportCharacterToTikiBarSignal teleportSignal, global::Kampai.Util.IRoutineRunner routineRunner)
		{
			global::UnityEngine.Transform transform = tikiBar.transform.Find("route0");
			base.transform.position = transform.position;
			base.transform.eulerAngles = transform.eulerAngles;
			SetAnimBool("goToBar", true);
			routineRunner.StartCoroutine(WaitAFrame());
			ClearActionQueue();
			EnqueueAction(new global::Kampai.Game.View.WaitForMecanimStateAction(this, global::UnityEngine.Animator.StringToHash("Base Layer.idleAtBar"), logger));
			EnqueueAction(new global::Kampai.Game.View.DuelParameterizedDelegateAction(SitAtBar, true, teleportSignal, logger));
		}

		internal void EnableTikiBarController()
		{
			ClearActionQueue();
			global::UnityEngine.RuntimeAnimatorController controller = global::Kampai.Util.KampaiResources.Load<global::UnityEngine.RuntimeAnimatorController>(TikiBarStateMachine);
			EnqueueAction(new global::Kampai.Game.View.SetAnimatorAction(this, controller, logger));
		}

		private void IntroComplete()
		{
			string type = "NewMinionSequence";
			SetAnimBool(type, false);
			tikiBarAnimSignal.Dispatch(type, typeof(bool), false);
		}

		private global::System.Collections.IEnumerator WaitAFrame()
		{
			yield return new global::UnityEngine.WaitForEndOfFrame();
			UpdateBlobShadowPosition();
		}
	}
}
