namespace Kampai.UI.Controller
{
	public class ShowForcedClientUpdateScreenCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Download.IDownloadService downloadService { get; set; }

		public override void Execute()
		{
			if (downloadService != null)
			{
				downloadService.Shutdown();
			}
			global::UnityEngine.Application.LoadLevel("ForcedUpgrade");
		}
	}
}
