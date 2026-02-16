namespace Kampai.Util.AI
{
	[global::UnityEngine.RequireComponent(typeof(global::Kampai.Game.View.MinionObject))]
	public class SteerMinionToWander : global::Kampai.Util.AI.SteerToWander
	{
		private global::Kampai.Game.View.MinionObject obj;

		private float timer;

		public float minRestTime = 5f;

		public float maxRestTime = 8f;

		[Inject]
		public global::Kampai.Game.IncidentalAnimationSignal animSignal { get; set; }

		public override global::UnityEngine.Vector3 Force
		{
			get
			{
				timer -= global::UnityEngine.Time.deltaTime;
				if (timer < 0f)
				{
					timer = global::UnityEngine.Random.Range(minRestTime, maxRestTime);
					animSignal.Dispatch(obj.ID);
					return global::UnityEngine.Vector3.zero;
				}
				return base.Force;
			}
		}

		protected override void Start()
		{
			base.Start();
			obj = GetComponent<global::Kampai.Game.View.MinionObject>();
			timer = global::UnityEngine.Random.Range(minRestTime, maxRestTime);
		}
	}
}
