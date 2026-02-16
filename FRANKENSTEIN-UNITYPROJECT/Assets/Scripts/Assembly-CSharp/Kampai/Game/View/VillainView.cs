namespace Kampai.Game.View
{
	public class VillainView : global::Kampai.Game.View.NamedCharacterObject
	{
		private const string ASM_PARAM_INTRO = "Intro";

		private const string ASM_PARAM_ACTIVE = "Active";

		private const string ASM_PARAM_MOVE_INTO_CABANA = "MoveIntoCabana";

		private const string ASM_PARAM_CABANA_INDEX = "CabanaIndex";

		private global::UnityEngine.RuntimeAnimatorController cabanaController;

		private global::UnityEngine.RuntimeAnimatorController farewellController;

		private global::UnityEngine.RuntimeAnimatorController boatController;

		private int loopCountMin;

		private int loopCountMax;

		private global::strange.extensions.signal.impl.Signal<string, global::System.Type, object> villainAnimSignal = new global::strange.extensions.signal.impl.Signal<string, global::System.Type, object>();

		public override global::strange.extensions.signal.impl.Signal<string, global::System.Type, object> AnimSignal
		{
			get
			{
				return villainAnimSignal;
			}
		}

		public override global::Kampai.Game.View.NamedCharacterObject Build(global::Kampai.Game.NamedCharacter character, global::UnityEngine.GameObject parent, global::Kampai.Util.ILogger logger, global::Kampai.Util.IMinionBuilder minionBuilder)
		{
			base.Build(character, parent, logger, minionBuilder);
			global::Kampai.Game.VillainDefinition villainDefinition = character.Definition as global::Kampai.Game.VillainDefinition;
			cabanaController = global::Kampai.Util.KampaiResources.Load<global::UnityEngine.RuntimeAnimatorController>(villainDefinition.AsmCabana);
			farewellController = global::Kampai.Util.KampaiResources.Load<global::UnityEngine.RuntimeAnimatorController>(villainDefinition.AsmFarewell);
			boatController = global::Kampai.Util.KampaiResources.Load<global::UnityEngine.RuntimeAnimatorController>(villainDefinition.AsmBoat);
			global::Kampai.Game.VillainDefinition villainDefinition2 = character.Definition as global::Kampai.Game.VillainDefinition;
			loopCountMin = villainDefinition2.LoopCountMin;
			loopCountMax = villainDefinition2.LoopCountMax;
			return this;
		}

		protected override void SetupLODs()
		{
		}

		protected override string GetRootJointPath(global::Kampai.Game.Character character)
		{
			return string.Format("{0}:{0}/{0}:ROOT/{0}:Pelvis_M", (character as global::Kampai.Game.NamedCharacter).Definition.Prefab);
		}

		public void PlayWelcome()
		{
			SetAnimController(defaultController);
		}

		public void GotoCabana(int index, global::UnityEngine.Transform transform)
		{
			setLocation(transform.position);
			setRotation(transform.eulerAngles);
			SetAnimController(cabanaController);
			SetAnimInteger("CabanaIndex", index);
		}

		public void PlayFarewell()
		{
			SetAnimController(farewellController);
		}

		public void PlayBoat()
		{
			SetAnimController(boatController);
		}

		protected override void TransitionComplete()
		{
			global::UnityEngine.AnimatorStateInfo? animatorStateInfo = GetAnimatorStateInfo(0);
			if (animatorStateInfo.HasValue)
			{
				int num = base.randomService.NextInt(loopCountMin, loopCountMax + 1);
				StartCoroutine(ReturnToRelaxation(animatorStateInfo.Value.length * (float)num));
			}
		}

		private global::System.Collections.IEnumerator ReturnToRelaxation(float time)
		{
			yield return new global::UnityEngine.WaitForSeconds(time);
			SetAnimBool(PlayAlternateString, false);
		}

		public override global::UnityEngine.Vector3 GetIndicatorPosition()
		{
			return base.transform.position + global::Kampai.Util.GameConstants.UI.VILLAIN_UI_OFFSET;
		}
	}
}
