namespace Kampai.Game.View
{
	public class KevinView : global::Kampai.Game.View.NamedMinionView
	{
		private string WelcomeHutStateMachine;

		private global::strange.extensions.signal.impl.Signal<string, global::System.Type, object> welcomeHutAnimSignal = new global::strange.extensions.signal.impl.Signal<string, global::System.Type, object>();

		private global::Kampai.Game.MinionAnimationDefinition currentFrolicAnimation;

		private global::UnityEngine.RuntimeAnimatorController nextFrolicAnimationController;

		private global::UnityEngine.RuntimeAnimatorController defaultAnimationController;

		private global::strange.extensions.signal.impl.Signal animationCallback;

		private global::Kampai.Util.AI.Agent agent;

		private global::Kampai.Game.Angle currentFrolicRotation;

		public override global::strange.extensions.signal.impl.Signal<string, global::System.Type, object> AnimSignal
		{
			get
			{
				return welcomeHutAnimSignal;
			}
		}

		public override global::Kampai.Game.View.NamedCharacterObject Build(global::Kampai.Game.NamedCharacter character, global::UnityEngine.GameObject parent, global::Kampai.Util.ILogger logger, global::Kampai.Util.IMinionBuilder minionBuilder)
		{
			base.Build(character, parent, logger, minionBuilder);
			global::Kampai.Game.View.MinionObject.SetEyes(base.transform, 2u);
			global::Kampai.Game.View.MinionObject.SetBody(base.transform, global::Kampai.Game.MinionBody.TALL);
			global::Kampai.Game.View.MinionObject.SetHair(base.transform, global::Kampai.Game.MinionHair.SPROUT);
			base.gameObject.AddComponent<global::Kampai.Util.AI.SteerToAvoidCollisions>();
			global::Kampai.Util.AI.SteerToAvoidEnvironment steerToAvoidEnvironment = base.gameObject.AddComponent<global::Kampai.Util.AI.SteerToAvoidEnvironment>();
			steerToAvoidEnvironment.Modifier = 8;
			global::Kampai.Util.AI.SteerCharacterToSeek steerCharacterToSeek = base.gameObject.AddComponent<global::Kampai.Util.AI.SteerCharacterToSeek>();
			steerCharacterToSeek.enabled = false;
			steerCharacterToSeek.Threshold = 0.1f;
			agent = base.gameObject.GetComponent<global::Kampai.Util.AI.Agent>();
			if (agent == null)
			{
				agent = base.gameObject.AddComponent<global::Kampai.Util.AI.Agent>();
			}
			agent.Radius = 0.5f;
			agent.Mass = 1f;
			agent.MaxForce = 8f;
			agent.MaxSpeed = 0f;
			defaultAnimationController = base.gameObject.GetComponent<global::UnityEngine.Animator>().runtimeAnimatorController;
			if (defaultAnimationController == null)
			{
				logger.Error("No default animation controller found for {0}", base.name);
			}
			global::Kampai.Game.KevinCharacter kevinCharacter = character as global::Kampai.Game.KevinCharacter;
			WelcomeHutStateMachine = kevinCharacter.Definition.WelcomeHutStateMachine;
			base.gameObject.transform.position = (global::UnityEngine.Vector3)kevinCharacter.Definition.WanderAnimations[0].Location;
			return this;
		}

		internal void Wander(global::Kampai.Game.LocationIncidentalAnimationDefinition animationDefinition, global::Kampai.Game.MinionAnimationDefinition mad)
		{
			ClearActionQueue();
			EnqueueAction(new global::Kampai.Game.View.SetAnimatorAction(this, defaultAnimationController, logger, new global::System.Collections.Generic.Dictionary<string, object> { { "walk", true } }));
			currentFrolicAnimation = mad;
			currentFrolicRotation = animationDefinition.Rotation;
			global::Kampai.Util.AI.SteerCharacterToSeek component = base.gameObject.GetComponent<global::Kampai.Util.AI.SteerCharacterToSeek>();
			component.Target = (global::UnityEngine.Vector3)animationDefinition.Location;
			component.enabled = true;
			nextFrolicAnimationController = global::Kampai.Util.KampaiResources.Load<global::UnityEngine.RuntimeAnimatorController>(mad.StateMachine);
			if (nextFrolicAnimationController == null)
			{
				logger.Error("Unable to load animation controller {0} for {1}", mad.StateMachine, base.name);
			}
			agent.MaxSpeed = 1f;
		}

		internal void ArrivedAtWayPoint()
		{
			Walk(false);
			agent.MaxSpeed = 0f;
			if (currentFrolicAnimation != null)
			{
				if (currentFrolicRotation != null)
				{
					EnqueueAction(new global::Kampai.Game.View.RotateAction(this, currentFrolicRotation.Degrees, 360f, logger));
				}
				EnqueueAction(new global::Kampai.Game.View.SetAnimatorAction(this, nextFrolicAnimationController, logger, currentFrolicAnimation.arguments));
				if (currentFrolicAnimation.FaceCamera)
				{
					EnqueueAction(new global::Kampai.Game.View.RotateAction(this, global::UnityEngine.Camera.main.transform.eulerAngles.y - 180f, 360f, logger));
				}
				EnqueueAction(new global::Kampai.Game.View.WaitForMecanimStateAction(this, global::UnityEngine.Animator.StringToHash("Base Layer.Exit"), logger));
				EnqueueAction(new global::Kampai.Game.View.SetAnimatorAction(this, defaultAnimationController, logger));
				if (animationCallback != null)
				{
					EnqueueAction(new global::Kampai.Game.View.SendSignalAction(animationCallback, logger));
				}
			}
			else
			{
				logger.Error("No animaiton to play");
			}
		}

		internal void Walk(bool enable)
		{
			SetAnimBool("walk", enable);
		}

		public void GreetVillain(bool shouldGreet)
		{
			SetAnimBool("GreetVillain", shouldGreet);
		}

		public void WaveFarewell(bool shouldWave)
		{
			SetAnimBool("WaveFarewell", shouldWave);
		}

		internal void GotoWelcomeHut(global::UnityEngine.GameObject WelcomeHut, bool pop)
		{
			if (WelcomeHut == null)
			{
				return;
			}
			global::UnityEngine.Transform transform = WelcomeHut.transform.Find("route0");
			if (!(transform == null))
			{
				if (pop)
				{
					base.transform.position = transform.position;
				}
				ClearActionQueue();
				global::UnityEngine.RuntimeAnimatorController controller = global::Kampai.Util.KampaiResources.Load<global::UnityEngine.RuntimeAnimatorController>(WelcomeHutStateMachine);
				EnqueueAction(new global::Kampai.Game.View.SetAnimatorAction(this, controller, logger));
				base.IsIdle = true;
			}
		}

		public void SetAnimationCallback(global::strange.extensions.signal.impl.Signal callback)
		{
			animationCallback = callback;
		}
	}
}
