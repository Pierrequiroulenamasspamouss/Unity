namespace Kampai.Game
{
	public class CruiseShipMediator : global::strange.extensions.mediation.impl.Mediator
	{
		[Inject]
		public global::Kampai.Game.CruiseShipView view { get; set; }

		[Inject]
		public global::Kampai.Game.VillainAttachToShipSignal attachSignal { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		public override void OnRegister()
		{
			view.Initialize(logger);
			attachSignal.AddListener(AttachToShip);
		}

		public override void OnRemove()
		{
			attachSignal.RemoveListener(AttachToShip);
		}

		public void AttachToShip(global::UnityEngine.Transform villainTransform, global::Kampai.Util.Tuple<int, int> indexAndId)
		{
			global::UnityEngine.Transform routingSlot = view.GetRoutingSlot(indexAndId.Item1);
			villainTransform.parent = routingSlot;
			villainTransform.localPosition = global::UnityEngine.Vector3.zero;
		}
	}
}
