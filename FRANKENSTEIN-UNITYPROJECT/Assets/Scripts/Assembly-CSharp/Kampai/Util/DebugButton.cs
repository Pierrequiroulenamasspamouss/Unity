namespace Kampai.Util
{
	public class DebugButton : global::strange.extensions.mediation.impl.View
	{
		private int counter;

		private bool visible;

		private global::UnityEngine.GameObject instance;

		private bool captured;

		[Inject]
		public global::Kampai.UI.View.IGUIService guiService { get; set; }

		[Inject]
		public global::Kampai.UI.View.HUDChangedSiblingIndexSignal hudChangedSiblingIndexSignal { get; set; }

		[Inject]
		public global::Kampai.Game.DebugKeyHitSignal openSignal { get; set; }

		protected override void Start()
		{
			base.Start();
			global::UnityEngine.RectTransform rectTransform = base.transform as global::UnityEngine.RectTransform;
			rectTransform.anchoredPosition = global::UnityEngine.Vector2.zero;
			hudChangedSiblingIndexSignal.AddListener(OnHudChangedIndex);
			openSignal.AddListener(ToggleOpen);
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			openSignal.RemoveListener(ToggleOpen);
		}

		private void OnHudChangedIndex(int index)
		{
			base.transform.SetAsFirstSibling();
		}

		public void OnClick(global::UnityEngine.UI.Button button)
		{
			ToggleOpen(global::Kampai.Util.DebugArgument.OPEN_CONSOLE);
		}

		private void ToggleOpen(global::Kampai.Util.DebugArgument arg)
		{
			if (arg != global::Kampai.Util.DebugArgument.OPEN_CONSOLE)
			{
				return;
			}
			if (captured)
			{
				visible = instance.activeSelf;
			}
			counter++;
			if (counter == 3 && !visible)
			{
				visible = true;
				counter = 0;
				if (!captured)
				{
					global::Kampai.UI.View.IGUICommand command = guiService.BuildCommand(global::Kampai.UI.View.GUIOperation.LoadStatic, "DebugConsole");
					instance = guiService.Execute(command);
					captured = true;
				}
				else
				{
					instance.SetActive(visible);
				}
			}
			if (counter == 1 && visible)
			{
				visible = false;
				instance.SetActive(visible);
				counter = 0;
			}
		}
	}
}
