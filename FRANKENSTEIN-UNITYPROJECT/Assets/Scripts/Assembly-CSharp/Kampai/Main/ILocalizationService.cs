namespace Kampai.Main
{
	public interface ILocalizationService
	{
		void Initialize(string langCode);

		bool IsInitialized();

		void Update();

		string GetLanguage();

		bool IsLanguageSupported();

		string GetString(string key, params object[] args);

		string GetLanguageKey();

		bool Contains(string key);
	}
}
