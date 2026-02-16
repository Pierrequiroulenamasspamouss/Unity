namespace Kampai.UI.View
{
	public class TikiBarParentWayFinderMediator : global::Kampai.UI.View.AbstractParentWayFinderMediator
	{
		[Inject]
		public global::Kampai.UI.View.TikiBarParentWayFinderView TikiBarParentWayFinderView { get; set; }

		[Inject]
		public global::Kampai.Common.ZoomPercentageSignal ZoomPercentageSignal { get; set; }

		[Inject]
		public global::Kampai.Common.RequestZoomPercentageSignal RequestZoomPercentageSignal { get; set; }

		public override global::Kampai.UI.View.IWayFinderView View
		{
			get
			{
				return TikiBarParentWayFinderView;
			}
		}

		public override void OnRegister()
		{
			base.OnRegister();
			ZoomPercentageSignal.AddListener(OnZoom);
			StartCoroutine(RunAfterAFrame());
		}

		private global::System.Collections.IEnumerator RunAfterAFrame()
		{
			yield return null;
			RequestZoomPercentageSignal.Dispatch();
		}

		public override void OnRemove()
		{
			base.OnRemove();
			ZoomPercentageSignal.RemoveListener(OnZoom);
		}

		private void OnZoom(float percentage)
		{
			TikiBarParentWayFinderView.UpdateZoomPercentage(percentage);
		}

		protected override void PanToInstance()
		{
			base.GameContext.injectionBinder.GetInstance<global::Kampai.Game.BuildingZoomSignal>().Dispatch(new global::Kampai.Game.BuildingZoomSettings(global::Kampai.Game.ZoomType.IN, global::Kampai.Game.BuildingZoomType.TIKIBAR));
		}
	}
}
