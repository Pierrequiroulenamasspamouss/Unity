namespace Kampai.Download
{
	public class ShowDLCPanelCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public bool show { get; set; }

		[Inject(global::strange.extensions.context.api.ContextKeys.CONTEXT_VIEW)]
		public global::UnityEngine.GameObject rootObj { get; set; }

		public override void Execute()
		{
			global::Kampai.Download.DownloadPanelView componentInChildren = rootObj.GetComponentInChildren<global::Kampai.Download.DownloadPanelView>();
			if (show && componentInChildren == null)
			{
				global::Kampai.Download.DownloadRoot component = rootObj.GetComponent<global::Kampai.Download.DownloadRoot>();
				global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(component.PanelPrefab) as global::UnityEngine.GameObject;
				gameObject.transform.parent = rootObj.transform;
			}
			else if (componentInChildren != null)
			{
				global::UnityEngine.Object.Destroy(componentInChildren.gameObject);
			}
		}
	}
}
