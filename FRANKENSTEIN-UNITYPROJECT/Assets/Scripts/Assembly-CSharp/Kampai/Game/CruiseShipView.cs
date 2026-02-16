namespace Kampai.Game
{
	public class CruiseShipView : global::strange.extensions.mediation.impl.View
	{
		private enum CruiseShipState
		{
			Docked = 0,
			Departing = 1,
			Cruising = 2
		}

		public float CruiseTime;

		public global::UnityEngine.Transform CruiseSplineParent;

		public float DepartureTime;

		public global::UnityEngine.Transform DepartureSplineParent;

		private GoSpline cruiseSpline;

		private GoSpline departureSpline;

		private global::Kampai.Game.CruiseShipView.CruiseShipState currentState;

		private float currentDuration;

		private GoSpline currentSpline;

		private readonly global::UnityEngine.Transform[] routingSlots = new global::UnityEngine.Transform[2];

		public void Initialize(global::Kampai.Util.ILogger logger)
		{
			cruiseSpline = CreateSpline(CruiseSplineParent);
			departureSpline = CreateSpline(DepartureSplineParent);
			cruiseSpline.closePath();
			SetState(global::Kampai.Game.CruiseShipView.CruiseShipState.Cruising);
			GoCruise();
			int i = 0;
			for (int num = 2; i < num; i++)
			{
				global::UnityEngine.Transform transform = base.transform.Find("route" + i);
				if (transform == null)
				{
					logger.Error("CruiseShipView: Prefab is missing routing slot {0}", i);
				}
				routingSlots[i] = transform;
			}
		}

		public global::UnityEngine.Transform GetRoutingSlot(int index)
		{
			return routingSlots[index];
		}

		private GoSpline CreateSpline(global::UnityEngine.Transform splineParent)
		{
			global::UnityEngine.Vector3[] array = new global::UnityEngine.Vector3[splineParent.childCount];
			for (int i = 0; i < splineParent.childCount; i++)
			{
				array[i] = splineParent.GetChild(i).position;
			}
			GoSpline goSpline = new GoSpline(array);
			goSpline.buildPath();
			return goSpline;
		}

		private void SetState(global::Kampai.Game.CruiseShipView.CruiseShipState state)
		{
			currentState = state;
			switch (currentState)
			{
			case global::Kampai.Game.CruiseShipView.CruiseShipState.Docked:
				currentDuration = 0f;
				currentSpline = null;
				break;
			case global::Kampai.Game.CruiseShipView.CruiseShipState.Departing:
				currentDuration = DepartureTime;
				currentSpline = departureSpline;
				break;
			case global::Kampai.Game.CruiseShipView.CruiseShipState.Cruising:
				currentDuration = CruiseTime;
				currentSpline = cruiseSpline;
				break;
			}
		}

		private void GoCruise()
		{
			Go.to(base.transform, currentDuration, new GoTweenConfig().positionPath(currentSpline, false, GoLookAtType.NextPathNode).setIterations(-1, GoLoopType.RestartFromBeginning));
		}
	}
}
