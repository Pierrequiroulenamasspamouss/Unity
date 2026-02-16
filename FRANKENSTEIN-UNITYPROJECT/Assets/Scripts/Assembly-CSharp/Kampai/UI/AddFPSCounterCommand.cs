namespace Kampai.UI
{
	internal sealed class AddFPSCounterCommand : global::strange.extensions.command.impl.Command
	{
		[Inject(global::Kampai.Main.MainElement.UI_GLASSCANVAS)]
		public global::UnityEngine.GameObject GlassCanvas { get; set; }

		public override void Execute()
		{
			global::UnityEngine.GameObject gameObject = new global::UnityEngine.GameObject("FPSCounter", typeof(global::UnityEngine.RectTransform));
			gameObject.transform.parent = GlassCanvas.transform;
			global::UnityEngine.UI.Text text = gameObject.AddComponent<global::UnityEngine.UI.Text>();
			text.rectTransform.sizeDelta = global::UnityEngine.Vector2.zero;
			text.rectTransform.anchorMin = new global::UnityEngine.Vector2(0.48f, 0.002f);
			text.rectTransform.anchorMax = new global::UnityEngine.Vector2(0.62f, 0.1f);
			text.rectTransform.anchoredPosition = new global::UnityEngine.Vector2(0.5f, 0.5f);
			text.text = "30";
			text.font = global::UnityEngine.Resources.FindObjectsOfTypeAll<global::UnityEngine.Font>()[0];
			text.fontSize = 40;
			text.color = global::UnityEngine.Color.black;
			text.alignment = global::UnityEngine.TextAnchor.LowerCenter;
			global::Kampai.UI.KampaiFPSCounter kampaiFPSCounter = gameObject.AddComponent<global::Kampai.UI.KampaiFPSCounter>();
			kampaiFPSCounter.TextComponent = text;
		}
	}
}
