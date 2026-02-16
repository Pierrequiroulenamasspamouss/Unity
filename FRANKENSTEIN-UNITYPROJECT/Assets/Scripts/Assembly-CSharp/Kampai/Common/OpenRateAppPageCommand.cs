namespace Kampai.Common
{
	public class OpenRateAppPageCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		public override void Execute()
		{
			global::UnityEngine.Application.OpenURL("market://details?id=com.ea.gp.minions");
		}
	}
}
