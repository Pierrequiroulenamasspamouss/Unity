namespace Kampai.Util
{
	public class MinionBuilder : global::Kampai.Util.IMinionBuilder
	{
		private global::Kampai.Util.Boxed<global::Kampai.Util.TargetPerformance> FORCE_LOD;

		private global::Kampai.Util.TargetPerformance TargetLOD = global::Kampai.Util.TargetPerformance.LOW;

		private bool restart;

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Main.PlayLocalAudioSignal audioSignal { get; set; }

		[Inject]
		public global::Kampai.Main.StartLoopingAudioSignal startLoopingAudioSignal { get; set; }

		[Inject]
		public global::Kampai.Main.StopLocalAudioSignal stopAudioSignal { get; set; }

		[Inject]
		public global::Kampai.Main.PlayMinionStateAudioSignal minionStateAudioSignal { get; set; }

		public MinionBuilder()
		{
			SetLOD(TargetLOD);
		}

		public global::Kampai.Game.View.MinionObject BuildMinion(string baseModelPath, string animatorStateMachine, global::UnityEngine.GameObject parent)
		{
			if (FORCE_LOD != null)
			{
				logger.Log(global::Kampai.Util.Logger.Level.Warning, "Forced LOD to " + FORCE_LOD);
				restart = false;
				SetLOD(FORCE_LOD.Value);
				FORCE_LOD = null;
			}
			string path = string.Format("{0}_{1}", baseModelPath, TargetLOD);
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(global::Kampai.Util.KampaiResources.Load(path)) as global::UnityEngine.GameObject;
			if (parent != null)
			{
				gameObject.transform.parent = parent.transform;
			}
			gameObject.transform.localPosition = global::UnityEngine.Vector3.zero;
			gameObject.transform.localEulerAngles = global::UnityEngine.Vector3.zero;
			global::UnityEngine.Renderer[] componentsInChildren = gameObject.GetComponentsInChildren<global::UnityEngine.Renderer>();
			global::System.Array.Sort(componentsInChildren, (global::UnityEngine.Renderer x, global::UnityEngine.Renderer y) => x.gameObject.name.CompareTo(y.gameObject.name));
			global::UnityEngine.LOD[] array = new global::UnityEngine.LOD[componentsInChildren.Length];
			for (int num = 0; num < componentsInChildren.Length; num++)
			{
				array[num] = new global::UnityEngine.LOD(global::Kampai.Util.GameConstants.GetLODHeightsArray()[num], new global::UnityEngine.Renderer[1] { componentsInChildren[num] });
				componentsInChildren[num].castShadows = false;
				componentsInChildren[num].receiveShadows = false;
			}
			global::UnityEngine.LODGroup lODGroup = gameObject.AddComponent<global::UnityEngine.LODGroup>();
			lODGroup.SetLODS(array);
			lODGroup.RecalculateBounds();
			global::UnityEngine.Animator component = gameObject.GetComponent<global::UnityEngine.Animator>();
			component.applyRootMotion = false;
			component.runtimeAnimatorController = global::Kampai.Util.KampaiResources.Load<global::UnityEngine.RuntimeAnimatorController>(animatorStateMachine);
			component.cullingMode = global::UnityEngine.AnimatorCullingMode.BasedOnRenderers;
			global::UnityEngine.Transform transform = gameObject.transform.Find("minion:ROOT/minion:pelvis_jnt");
			global::UnityEngine.SkinnedMeshRenderer[] componentsInChildren2 = gameObject.GetComponentsInChildren<global::UnityEngine.SkinnedMeshRenderer>();
			foreach (global::UnityEngine.SkinnedMeshRenderer skinnedMeshRenderer in componentsInChildren2)
			{
				skinnedMeshRenderer.rootBone = transform;
			}
			gameObject.SetLayerRecursively(8);
			return SetupMinionObject(gameObject, transform);
		}

		private global::Kampai.Game.View.MinionObject SetupMinionObject(global::UnityEngine.GameObject minion, global::UnityEngine.Transform pelvis)
		{
			global::Kampai.Game.View.MinionObject minionObject = minion.AddComponent<global::Kampai.Game.View.MinionObject>();
			global::UnityEngine.Rigidbody rigidbody = minion.AddComponent<global::UnityEngine.Rigidbody>();
			rigidbody.useGravity = false;
			rigidbody.isKinematic = true;
			if (GetLOD() != global::Kampai.Util.TargetPerformance.LOW && GetLOD() != global::Kampai.Util.TargetPerformance.VERYLOW)
			{
				global::UnityEngine.GameObject original = global::Kampai.Util.KampaiResources.Load("MinionBlobShadow") as global::UnityEngine.GameObject;
				global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(original) as global::UnityEngine.GameObject;
				gameObject.transform.parent = minion.transform;
				gameObject.GetComponent<global::Kampai.Util.MinionBlobShadowView>().SetToTrack(pelvis);
				minionObject.SetBlobShadow(gameObject);
			}
			minion.AddComponent<global::Kampai.Util.AI.SteerToAvoidCollisions>();
			minion.AddComponent<global::Kampai.Util.AI.SteerToAvoidEnvironment>();
			minion.AddComponent<global::Kampai.Util.AI.SteerToTether>();
			minion.AddComponent<global::Kampai.Util.AI.SteerMinionToWander>();
			global::Kampai.Util.AI.SteerCharacterToSeek steerCharacterToSeek = minion.AddComponent<global::Kampai.Util.AI.SteerCharacterToSeek>();
			steerCharacterToSeek.enabled = false;
			steerCharacterToSeek.Threshold = 0.1f;
			global::Kampai.Util.AI.Agent agent = minion.GetComponent<global::Kampai.Util.AI.Agent>();
			if (agent == null)
			{
				agent = minion.AddComponent<global::Kampai.Util.AI.Agent>();
			}
			agent.Radius = 0.5f;
			agent.Mass = 1f;
			agent.MaxForce = 8f;
			agent.MaxSpeed = 1f;
			global::Kampai.Game.View.AnimEventHandler animEventHandler = minion.AddComponent<global::Kampai.Game.View.AnimEventHandler>();
			animEventHandler.Init(minionObject, minionObject.localAudioEmitter, audioSignal, stopAudioSignal, minionStateAudioSignal, startLoopingAudioSignal);
			return minionObject;
		}

		public global::Kampai.Util.TargetPerformance GetLOD()
		{
			return TargetLOD;
		}

		public void SetLOD(global::Kampai.Util.TargetPerformance targetPerformance)
		{
			if (targetPerformance != global::Kampai.Util.TargetPerformance.UNKNOWN && targetPerformance != global::Kampai.Util.TargetPerformance.UNSUPPORTED)
			{
				TargetLOD = targetPerformance;
			}
			else
			{
				TargetLOD = global::Kampai.Util.TargetPerformance.LOW;
				logger.Log(global::Kampai.Util.Logger.Level.Error, "Unsupported/Unknown device: {0}, setting to LOW", targetPerformance);
			}
			if (restart)
			{
				FORCE_LOD = new global::Kampai.Util.Boxed<global::Kampai.Util.TargetPerformance>(targetPerformance);
			}
			restart = true;
		}
	}
}
