namespace Kampai.Game.View
{
	public class StuartView : global::Kampai.Game.View.NamedMinionView
	{
		private global::strange.extensions.signal.impl.Signal<string, global::System.Type, object> stageAnimSignal = new global::strange.extensions.signal.impl.Signal<string, global::System.Type, object>();

		private global::UnityEngine.RuntimeAnimatorController stageController;

		private global::Kampai.Game.MinionAnimationDefinition onStageAnimation;

		private global::Kampai.Game.MinionAnimationDefinition celebrateAnimation;

		public override global::strange.extensions.signal.impl.Signal<string, global::System.Type, object> AnimSignal
		{
			get
			{
				return stageAnimSignal;
			}
		}

		public override global::Kampai.Game.View.NamedCharacterObject Build(global::Kampai.Game.NamedCharacter character, global::UnityEngine.GameObject parent, global::Kampai.Util.ILogger logger, global::Kampai.Util.IMinionBuilder minionBuilder)
		{
			global::Kampai.Game.StuartCharacter stuartCharacter = character as global::Kampai.Game.StuartCharacter;
			stageController = global::Kampai.Util.KampaiResources.Load<global::UnityEngine.RuntimeAnimatorController>(stuartCharacter.Definition.StageStateMachine);
			celebrateAnimation = stuartCharacter.Definition.CelebrateAnimation;
			onStageAnimation = stuartCharacter.Definition.OnStageAnimation;
			base.Build(character, parent, logger, minionBuilder);
			return this;
		}

		internal void AddToStage(global::UnityEngine.Vector3 position, global::UnityEngine.Quaternion rotation, global::Kampai.Game.StuartStageAnimationType animType, global::System.Action startParty)
		{
			base.transform.position = position;
			base.transform.localRotation = rotation;
			switch (animType)
			{
			case global::Kampai.Game.StuartStageAnimationType.CELEBRATE:
			case global::Kampai.Game.StuartStageAnimationType.FIRSTUNLOCK:
				EnqueueAction(new global::Kampai.Game.View.SetAnimatorAction(this, stageController, logger, onStageAnimation.arguments), true);
				EnqueueAction(new global::Kampai.Game.View.SetAnimatorAction(this, null, logger, celebrateAnimation.arguments));
				if (animType == global::Kampai.Game.StuartStageAnimationType.FIRSTUNLOCK)
				{
					EnqueueAction(new global::Kampai.Game.View.WaitForMecanimStateAction(this, global::UnityEngine.Animator.StringToHash("Base Layer.OnStage_Idle"), logger));
					EnqueueAction(new global::Kampai.Game.View.DelegateAction(startParty, logger));
				}
				break;
			case global::Kampai.Game.StuartStageAnimationType.IDLEONSTAGE:
				EnqueueAction(new global::Kampai.Game.View.SetAnimatorAction(this, stageController, logger, onStageAnimation.arguments), true);
				break;
			default:
				EnqueueAction(new global::Kampai.Game.View.SetAnimatorAction(this, stageController, logger), true);
				break;
			}
			base.IsIdle = true;
		}

		internal void GetOnStage(bool enable)
		{
			SetAnimBool("goToStage", enable);
			base.IsIdle = !enable;
		}

		internal void GetOnStageImmediate(bool enable)
		{
			SetAnimTrigger("Immediate");
			SetAnimBool("goToStage", enable);
			base.IsIdle = !enable;
		}

		internal void Perform(global::Kampai.Util.SignalCallback<global::strange.extensions.signal.impl.Signal> finishedCallback)
		{
			base.IsIdle = false;
			SetAnimController(stageController);
			SetAnimBool("goToStage", true);
			SetAnimTrigger("Perform");
			EnqueueAction(new global::Kampai.Game.View.WaitForMecanimStateAction(this, global::UnityEngine.Animator.StringToHash("Base Layer.Perform_Stop"), logger));
			EnqueueAction(new global::Kampai.Game.View.SetAnimatorAction(this, null, "Celebrate", logger));
			EnqueueAction(new global::Kampai.Game.View.WaitForMecanimStateAction(this, global::UnityEngine.Animator.StringToHash("Base Layer.Celebrate"), logger));
			EnqueueAction(new global::Kampai.Game.View.SetAnimatorAction(this, null, "goToStage", logger, false));
			EnqueueAction(new global::Kampai.Game.View.WaitForMecanimStateAction(this, global::UnityEngine.Animator.StringToHash("Base Layer.Actions"), logger));
			EnqueueAction(new global::Kampai.Game.View.DelegateAction(SetToIdle, logger));
			EnqueueAction(new global::Kampai.Game.View.SendSignalAction(finishedCallback.PromiseDispatch(), logger));
		}

		internal void Celebrate(bool enable)
		{
			SetAnimBool("Celebrate", enable);
		}

		private void SetToIdle()
		{
			base.IsIdle = true;
		}

		internal void GetAttention(bool enable)
		{
			SetAnimBool("isGetAttention", enable);
		}

		internal void Walk(bool enable)
		{
			SetAnimBool("walk", enable);
		}
	}
}
