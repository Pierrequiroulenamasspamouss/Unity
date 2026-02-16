namespace Kampai.Game
{
	public class LoadCruiseShipCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject(global::strange.extensions.context.api.ContextKeys.CONTEXT_VIEW)]
		public global::UnityEngine.GameObject contextView { get; set; }

		public override void Execute()
		{
			global::UnityEngine.GameObject gameObject = global::Kampai.Util.KampaiResources.Load<global::UnityEngine.GameObject>("Unique_CruiseShip_Prefab");
			if (gameObject == null)
			{
				logger.Error("Unable to load Cruise Ship");
				return;
			}
			global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(gameObject) as global::UnityEngine.GameObject;
			if (gameObject2 == null)
			{
				logger.Error("Unable to instantiate Cruise Ship");
				return;
			}
			gameObject2.transform.position = new global::UnityEngine.Vector3(129.7f, 0f, 192.5f);
			gameObject2.transform.parent = contextView.transform;
		}
	}
}
