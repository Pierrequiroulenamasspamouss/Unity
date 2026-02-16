namespace Kampai.Splash
{
	public class LoadingBarView : global::strange.extensions.mediation.impl.View
	{
		internal global::UnityEngine.GameObject meter_fill;

		internal global::UnityEngine.GameObject txt_progressCounter;

		public void Init()
		{
			meter_fill = base.gameObject.FindChild("meter_fill");
			txt_progressCounter = base.gameObject.FindChild("txt_progressCounter");
		}

		public void SetProgressCounter(int counter)
		{
			txt_progressCounter.GetComponent<global::UnityEngine.UI.Text>().text = counter.ToString();
		}

		public void SetMeterFill(int fill)
		{
			float x = (float)fill / 100f;
			meter_fill.GetComponent<global::UnityEngine.RectTransform>().anchorMax = new global::UnityEngine.Vector2(x, 1f);
		}
	}
}
