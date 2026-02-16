namespace Kampai.Game.View
{
	public class KeyboardZoomMediator : global::Kampai.Game.View.ZoomMediator
	{
		[Inject]
		public global::Kampai.Game.View.KeyboardZoomView view { get; set; }

		[Inject]
		public global::Kampai.Common.RequestZoomPercentageSignal requestSignal { get; set; }

		public override void OnRegister()
		{
			view.zoomSignal.AddListener(DispatchZoom);
			requestSignal.AddListener(RequestZoom);
			base.OnRegister();
		}

		public override void OnRemove()
		{
			base.OnRemove();
			view.zoomSignal.RemoveListener(DispatchZoom);
			requestSignal.RemoveListener(RequestZoom);
		}

		public override void OnGameInput(global::UnityEngine.Vector3 position, int input)
		{
			if (!blocked)
			{
				if ((input & 2) != 0)
				{
					view.CalculateBehaviour(position);
				}
				else
				{
					view.ResetBehaviour();
				}
				view.PerformBehaviour(base.cameraUtils);
				view.Decay();
			}
		}

		public override void OnDisableBehaviour(int behaviour)
		{
			int num = 2;
			if ((behaviour & num) == num)
			{
				if (!blocked)
				{
					blocked = true;
					view.ResetBehaviour();
				}
				if ((base.model.CurrentBehaviours & num) == num)
				{
					base.model.CurrentBehaviours ^= num;
				}
			}
		}

		public override void OnEnableBehaviour(int behaviour)
		{
			int num = 2;
			if ((behaviour & num) == num)
			{
				if (blocked)
				{
					blocked = false;
				}
				if ((base.model.CurrentBehaviours & num) != num)
				{
					base.model.CurrentBehaviours ^= num;
				}
			}
		}

		private void DispatchZoom(float zoomPercent)
		{
			base.zoomSignal.Dispatch(zoomPercent);
		}

		private void RequestZoom()
		{
			base.zoomSignal.Dispatch(view.fraction);
		}

		public override void SetupAutoZoom(float zoomTo)
		{
			view.SetupAutoZoom(zoomTo);
		}

		public override void PerformAutoZoom(float delta)
		{
			view.PerformAutoZoom(delta);
		}

		public override global::Kampai.Game.View.ZoomView GetView()
		{
			return view;
		}
	}
}
