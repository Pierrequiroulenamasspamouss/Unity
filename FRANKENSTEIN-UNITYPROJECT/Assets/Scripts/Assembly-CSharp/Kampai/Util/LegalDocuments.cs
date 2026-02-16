namespace Kampai.Util
{
	public static class LegalDocuments
	{
		public enum LegalType
		{
			EULA = 0,
			TOS = 1,
			PRIVACY = 2
		}

		public static void TermsOfServiceClicked(global::Kampai.Main.ILocalizationService loc, global::Kampai.Main.PlayGlobalSoundFXSignal soundFXSignal, global::Kampai.Util.ILogger logger)
		{
			soundFXSignal.Dispatch("Play_button_click_01");
			global::UnityEngine.Application.OpenURL(GetLocalizedURL(loc, global::Kampai.Util.LegalDocuments.LegalType.TOS, logger));
		}

		public static void PrivacyPolicyClicked(global::Kampai.Main.ILocalizationService loc, global::Kampai.Main.PlayGlobalSoundFXSignal soundFXSignal, global::Kampai.Util.ILogger logger)
		{
			soundFXSignal.Dispatch("Play_button_click_01");
			global::UnityEngine.Application.OpenURL(GetLocalizedURL(loc, global::Kampai.Util.LegalDocuments.LegalType.PRIVACY, logger));
		}

		public static void EulaClicked(global::Kampai.Main.ILocalizationService loc, global::Kampai.Main.PlayGlobalSoundFXSignal soundFXSignal, global::Kampai.Util.ILogger logger)
		{
			soundFXSignal.Dispatch("Play_button_click_01");
			global::UnityEngine.Application.OpenURL(GetLocalizedURL(loc, global::Kampai.Util.LegalDocuments.LegalType.EULA, logger));
		}

		public static string GetLocalizedURL(global::Kampai.Main.ILocalizationService loc, global::Kampai.Util.LegalDocuments.LegalType legalType, global::Kampai.Util.ILogger logger)
		{
			string text = loc.GetLanguage();
			if (text == "zh")
			{
				text = ((!(loc.GetLanguageKey() == "ZH-CN")) ? "tc" : "sc");
			}
			else if (text == "pt" && loc.GetLanguageKey() == "PT-BR")
			{
				text = "br";
			}
			string arg = "mobileeula";
			string arg2 = "PC";
			switch (legalType)
			{
			case global::Kampai.Util.LegalDocuments.LegalType.EULA:
				arg = "mobileeula";
				arg2 = ((global::UnityEngine.Application.platform != global::UnityEngine.RuntimePlatform.IPhonePlayer) ? "GM" : "OTHER");
				break;
			case global::Kampai.Util.LegalDocuments.LegalType.TOS:
				arg = "webterms";
				arg2 = "PC";
				break;
			case global::Kampai.Util.LegalDocuments.LegalType.PRIVACY:
				arg = "webprivacy";
				arg2 = "PC";
				break;
			default:
				logger.Error("Supported LegalType must be specified");
				break;
			}
			return string.Format("http://tos.ea.com/legalapp/{0}/US/{1}/{2}", arg, text, arg2);
		}
	}
}
