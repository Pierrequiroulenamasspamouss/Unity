namespace Kampai.UI
{
	public interface IMailboxIconService
	{
		void CreateMailboxIcon();

		int GetRefreshFrequencyInSeconds();

		bool MailboxIconExists();

		void RemoveMailboxIcon();
	}
}
