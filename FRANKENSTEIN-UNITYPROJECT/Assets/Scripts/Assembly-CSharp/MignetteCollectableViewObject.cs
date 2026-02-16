public class MignetteCollectableViewObject : global::UnityEngine.MonoBehaviour
{
	public enum CollectableStates
	{
		None = 0,
		Spawn = 1,
		Bounce = 2,
		Collected = 3
	}

	[global::System.Serializable]
	public class PointsAndMaterials
	{
		public global::UnityEngine.Material materialForPoint;

		public int minPointsForMaterial;
	}

	public MignetteCollectableViewObject.CollectableStates CollectableState;

	public global::UnityEngine.Renderer CollectableRenderer;

	public global::UnityEngine.Animator CollectableAnimator;

	public global::UnityEngine.ParticleSystem CollectedParticle;

	public global::UnityEngine.ParticleSystem SparkleParticle;

	public global::UnityEngine.ParticleSystem GlowParticle;

	public global::UnityEngine.TrailRenderer CollectableTrail;

	public global::UnityEngine.Vector3 collectibleOffset = new global::UnityEngine.Vector3(0.1f, 0f, 0f);

	private global::UnityEngine.Transform mignetteCameraTransform;

	private global::UnityEngine.Transform collectableTransform;

	public MignetteCollectableViewObject.PointsAndMaterials[] MaterialsList;

	public void UpdateMaterialForPointValue(int points)
	{
		for (int i = 0; i < MaterialsList.Length; i++)
		{
			if (points >= MaterialsList[i].minPointsForMaterial)
			{
				CollectableRenderer.material = MaterialsList[i].materialForPoint;
				break;
			}
		}
	}

	public void ToggleModel()
	{
		CollectableRenderer.enabled = !CollectableRenderer.enabled;
	}

	public void Update()
	{
		if (mignetteCameraTransform != null && collectableTransform != null)
		{
			global::UnityEngine.Vector3 vector = base.transform.position - mignetteCameraTransform.position;
			GlowParticle.transform.position = collectableTransform.position + vector.normalized * 0.3f;
			SparkleParticle.transform.position = collectableTransform.position - vector.normalized * 1f;
			CollectedParticle.transform.position = collectableTransform.position + vector.normalized * collectibleOffset.x;
		}
	}

	public void SetState(MignetteCollectableViewObject.CollectableStates newState, global::UnityEngine.Camera mignetteCamera)
	{
		mignetteCameraTransform = mignetteCamera.transform;
		collectableTransform = CollectableRenderer.gameObject.transform;
		if (CollectableState != newState)
		{
			CollectableState = newState;
			switch (newState)
			{
			case MignetteCollectableViewObject.CollectableStates.Spawn:
				CollectableAnimator.SetTrigger("OnSpawn");
				CollectableTrail.enabled = true;
				break;
			case MignetteCollectableViewObject.CollectableStates.Bounce:
				CollectableAnimator.SetTrigger("OnBounce");
				CollectableTrail.enabled = false;
				break;
			case MignetteCollectableViewObject.CollectableStates.Collected:
				CollectableAnimator.SetTrigger("OnCollected");
				CollectedParticle.Play();
				CollectableTrail.enabled = false;
				break;
			}
		}
	}
}
