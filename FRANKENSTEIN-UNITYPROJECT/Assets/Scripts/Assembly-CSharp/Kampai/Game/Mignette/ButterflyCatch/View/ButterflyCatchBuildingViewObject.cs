namespace Kampai.Game.Mignette.ButterflyCatch.View
{
	public class ButterflyCatchBuildingViewObject : global::Kampai.Game.Mignette.View.MignetteBuildingViewObject
	{
		public global::UnityEngine.GameObject ButterflyParticleParent;

		private global::UnityEngine.ParticleSystem[] AmbientButterflies;

		public void Start()
		{
			AmbientButterflies = ButterflyParticleParent.GetComponentsInChildren<global::UnityEngine.ParticleSystem>();
			ToggleAmbientButterflies(true);
			base.gameObject.AddComponent<global::Kampai.Game.Mignette.View.MignetteBuildingCooldownView>();
		}

		public override void ResetCooldownView(global::Kampai.Main.PlayLocalAudioSignal localAudioSignal)
		{
			ToggleAmbientButterflies(true);
		}

		public override void UpdateCooldownView(global::Kampai.Main.PlayLocalAudioSignal localAudioSignal, int buildingData, float pctDone)
		{
			if (pctDone < 1f)
			{
				ToggleAmbientButterflies(false);
			}
		}

		public void ToggleAmbientButterflies(bool enable)
		{
			if (AmbientButterflies != null)
			{
				global::UnityEngine.ParticleSystem[] ambientButterflies = AmbientButterflies;
				foreach (global::UnityEngine.ParticleSystem particleSystem in ambientButterflies)
				{
					particleSystem.enableEmission = enable;
				}
			}
		}
	}
}
