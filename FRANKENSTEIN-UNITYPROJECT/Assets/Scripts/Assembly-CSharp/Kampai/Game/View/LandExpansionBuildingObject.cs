namespace Kampai.Game.View
{
	public class LandExpansionBuildingObject : global::Kampai.Game.View.BuildingObject
	{
		private global::Kampai.Game.BurnLandExpansionSignal burnSignal;

		private string vfxGrassClearing;

		private string burntPrefab;

		private global::UnityEngine.ParticleSystem grassClearingParticles;

		private float seconds = 3f;

		public float BurnTimer { get; set; }

		internal void Burn(global::Kampai.Game.BurnLandExpansionSignal burnSignal, int ID, string vfxGrassClearing)
		{
			this.burnSignal = burnSignal;
			this.ID = ID;
			this.vfxGrassClearing = vfxGrassClearing;
			StartCoroutine(BurnSequence());
		}

		private global::System.Collections.IEnumerator BurnSequence()
		{
			IncrementMaterialRenderQueue(1);
			Go.to(this, seconds, new GoTweenConfig().floatProp("BurnTimer", 0.5f).setEaseType(GoEaseType.Linear).onUpdate(delegate
			{
				SetMaterialShaderFloat("_AlphaClip", BurnTimer);
			}));
			global::UnityEngine.GameObject clearingGO = global::UnityEngine.Object.Instantiate(global::Kampai.Util.KampaiResources.Load<global::UnityEngine.GameObject>(vfxGrassClearing)) as global::UnityEngine.GameObject;
			clearingGO.transform.parent = base.transform;
			clearingGO.transform.localPosition = new global::UnityEngine.Vector3(1f, 1f, -1f);
			grassClearingParticles = clearingGO.GetComponent<global::UnityEngine.ParticleSystem>();
			yield return new global::UnityEngine.WaitForSeconds(1f);
			grassClearingParticles.Stop();
			yield return new global::UnityEngine.WaitForSeconds(seconds);
			burnSignal.Dispatch(ID);
		}
	}
}
