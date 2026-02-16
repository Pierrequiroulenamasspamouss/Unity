namespace Kampai.UI.View
{
	public class MailboxIconMediator : global::strange.extensions.mediation.impl.Mediator
	{
		private global::Kampai.Game.MailboxSelectedSignal mailboxSelectedSignal;

		[Inject]
		public global::Kampai.UI.View.MailboxIconView MailboxIconView { get; set; }

		[Inject(global::Kampai.Game.GameElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable GameContext { get; set; }

		public override void OnRegister()
		{
			mailboxSelectedSignal = GameContext.injectionBinder.GetInstance<global::Kampai.Game.MailboxSelectedSignal>();
			MailboxIconView.button.ClickedSignal.AddListener(OnClick);
		}

		public override void OnRemove()
		{
			MailboxIconView.button.ClickedSignal.RemoveListener(OnClick);
		}

		private void OnClick()
		{
			mailboxSelectedSignal.Dispatch();
		}
	}
}
