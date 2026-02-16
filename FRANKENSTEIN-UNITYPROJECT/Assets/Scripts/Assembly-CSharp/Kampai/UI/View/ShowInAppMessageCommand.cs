namespace Kampai.UI.View
{
	public class ShowInAppMessageCommand : global::strange.extensions.command.impl.Command
	{
		public override void Execute()
		{
			NimbleBridge_InAppMessage.GetComponent().ShowInAppMessage();
		}
	}
}
