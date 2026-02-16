namespace Kampai.Splash
{
	public class SavedTipsModel
	{
		public global::System.Collections.Generic.IList<global::Kampai.Splash.TipToShow> Tips = new global::System.Collections.Generic.List<global::Kampai.Splash.TipToShow>();

		private char delimeter = '\u001d';

		public SavedTipsModel()
		{
			InitTips();
		}

        private void InitTips()
        {
            if (global::UnityEngine.PlayerPrefs.HasKey("SavedLocTipsWithTimes"))
            {
                string text = global::UnityEngine.PlayerPrefs.GetString("SavedLocTipsWithTimes");
                if (string.IsNullOrEmpty(text))
                {
                    return;
                }

                string[] array = text.Split(delimeter);

                // FIX: Changed condition to 'i < array.Length - 1'
                // This ensures that 'i + 1' is always a valid index.
                for (int i = 0; i < array.Length - 1; i += 2)
                {
                    float result = 0f;
                    // We verify parsing succeeded before adding
                    if (float.TryParse(array[i + 1], out result))
                    {
                        global::Kampai.Splash.TipToShow item = new global::Kampai.Splash.TipToShow(array[i], result);
                        Tips.Add(item);
                    }
                }
            }
        }

        public void SaveTipsToDevice(global::System.Collections.Generic.IList<global::Kampai.Splash.TipToShow> tips)
		{
			string text = string.Empty;
			for (int i = 0; i < tips.Count; i++)
			{
				global::Kampai.Splash.TipToShow tipToShow = tips[i];
				if (i > 0)
				{
					text += delimeter;
				}
				text += tipToShow.Text;
				text += delimeter;
				text += tipToShow.Time;
			}
			global::UnityEngine.PlayerPrefs.SetString("SavedLocTipsWithTimes", text);
		}
	}
}
