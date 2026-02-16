public class EtceteraAndroidManager : global::Prime31.AbstractManager
{
	public static event global::System.Action<string> alertButtonClickedEvent;

	public static event global::System.Action alertCancelledEvent;

	public static event global::System.Action<string> promptFinishedWithTextEvent;

	public static event global::System.Action promptCancelledEvent;

	public static event global::System.Action<string, string> twoFieldPromptFinishedWithTextEvent;

	public static event global::System.Action twoFieldPromptCancelledEvent;

	public static event global::System.Action webViewCancelledEvent;

	public static event global::System.Action albumChooserCancelledEvent;

	public static event global::System.Action<string> albumChooserSucceededEvent;

	public static event global::System.Action photoChooserCancelledEvent;

	public static event global::System.Action<string> photoChooserSucceededEvent;

	public static event global::System.Action<string> videoRecordingSucceededEvent;

	public static event global::System.Action videoRecordingCancelledEvent;

	public static event global::System.Action ttsInitializedEvent;

	public static event global::System.Action ttsFailedToInitializeEvent;

	public static event global::System.Action askForReviewWillOpenMarketEvent;

	public static event global::System.Action askForReviewRemindMeLaterEvent;

	public static event global::System.Action askForReviewDontAskAgainEvent;

	public static event global::System.Action<string> inlineWebViewJSCallbackEvent;

	public static event global::System.Action<string> notificationReceivedEvent;

	public static event global::System.Action<global::System.Collections.Generic.List<EtceteraAndroid.Contact>> contactsLoadedEvent;

	static EtceteraAndroidManager()
	{
		global::Prime31.AbstractManager.initialize(typeof(EtceteraAndroidManager));
	}

	public void alertButtonClicked(string positiveButton)
	{
		if (EtceteraAndroidManager.alertButtonClickedEvent != null)
		{
			EtceteraAndroidManager.alertButtonClickedEvent(positiveButton);
		}
	}

	public void alertCancelled(string empty)
	{
		if (EtceteraAndroidManager.alertCancelledEvent != null)
		{
			EtceteraAndroidManager.alertCancelledEvent();
		}
	}

	public void promptFinishedWithText(string text)
	{
		string[] array = text.Split(new string[1] { "|||" }, global::System.StringSplitOptions.None);
		if (array.Length == 1 && EtceteraAndroidManager.promptFinishedWithTextEvent != null)
		{
			EtceteraAndroidManager.promptFinishedWithTextEvent(array[0]);
		}
		if (array.Length == 2 && EtceteraAndroidManager.twoFieldPromptFinishedWithTextEvent != null)
		{
			EtceteraAndroidManager.twoFieldPromptFinishedWithTextEvent(array[0], array[1]);
		}
	}

	public void promptCancelled(string empty)
	{
		if (EtceteraAndroidManager.promptCancelledEvent != null)
		{
			EtceteraAndroidManager.promptCancelledEvent();
		}
	}

	public void twoFieldPromptCancelled(string empty)
	{
		if (EtceteraAndroidManager.twoFieldPromptCancelledEvent != null)
		{
			EtceteraAndroidManager.twoFieldPromptCancelledEvent();
		}
	}

	public void webViewCancelled(string empty)
	{
		if (EtceteraAndroidManager.webViewCancelledEvent != null)
		{
			EtceteraAndroidManager.webViewCancelledEvent();
		}
	}

	public void albumChooserCancelled(string empty)
	{
		if (EtceteraAndroidManager.albumChooserCancelledEvent != null)
		{
			EtceteraAndroidManager.albumChooserCancelledEvent();
		}
	}

	public void albumChooserSucceeded(string path)
	{
		if (EtceteraAndroidManager.albumChooserSucceededEvent != null)
		{
			if (global::System.IO.File.Exists(path))
			{
				EtceteraAndroidManager.albumChooserSucceededEvent(path);
			}
			else if (EtceteraAndroidManager.albumChooserCancelledEvent != null)
			{
				EtceteraAndroidManager.albumChooserCancelledEvent();
			}
		}
	}

	public void photoChooserCancelled(string empty)
	{
		if (EtceteraAndroidManager.photoChooserCancelledEvent != null)
		{
			EtceteraAndroidManager.photoChooserCancelledEvent();
		}
	}

	public void photoChooserSucceeded(string path)
	{
		if (EtceteraAndroidManager.photoChooserSucceededEvent != null)
		{
			if (global::System.IO.File.Exists(path))
			{
				EtceteraAndroidManager.photoChooserSucceededEvent(path);
			}
			else if (EtceteraAndroidManager.photoChooserCancelledEvent != null)
			{
				EtceteraAndroidManager.photoChooserCancelledEvent();
			}
		}
	}

	public void videoRecordingSucceeded(string path)
	{
		if (EtceteraAndroidManager.videoRecordingSucceededEvent != null)
		{
			EtceteraAndroidManager.videoRecordingSucceededEvent(path);
		}
	}

	public void videoRecordingCancelled(string empty)
	{
		if (EtceteraAndroidManager.videoRecordingCancelledEvent != null)
		{
			EtceteraAndroidManager.videoRecordingCancelledEvent();
		}
	}

	public void ttsInitialized(string result)
	{
		bool flag = result == "1";
		if (flag && EtceteraAndroidManager.ttsInitializedEvent != null)
		{
			EtceteraAndroidManager.ttsInitializedEvent();
		}
		if (!flag && EtceteraAndroidManager.ttsFailedToInitializeEvent != null)
		{
			EtceteraAndroidManager.ttsFailedToInitializeEvent();
		}
	}

	public void ttsUtteranceCompleted(string utteranceId)
	{
		global::UnityEngine.Debug.Log("utterance completed: " + utteranceId);
	}

	public void askForReviewWillOpenMarket(string empty)
	{
		if (EtceteraAndroidManager.askForReviewWillOpenMarketEvent != null)
		{
			EtceteraAndroidManager.askForReviewWillOpenMarketEvent();
		}
	}

	public void askForReviewRemindMeLater(string empty)
	{
		if (EtceteraAndroidManager.askForReviewRemindMeLaterEvent != null)
		{
			EtceteraAndroidManager.askForReviewRemindMeLaterEvent();
		}
	}

	public void askForReviewDontAskAgain(string empty)
	{
		if (EtceteraAndroidManager.askForReviewDontAskAgainEvent != null)
		{
			EtceteraAndroidManager.askForReviewDontAskAgainEvent();
		}
	}

	public void inlineWebViewJSCallback(string message)
	{
		global::Prime31.ActionExtensions.fire(EtceteraAndroidManager.inlineWebViewJSCallbackEvent, message);
	}

	public void notificationReceived(string extraData)
	{
		global::Prime31.ActionExtensions.fire(EtceteraAndroidManager.notificationReceivedEvent, extraData);
	}

	private void contactsLoaded(string json)
	{
		if (EtceteraAndroidManager.contactsLoadedEvent != null)
		{
			global::System.Collections.Generic.List<EtceteraAndroid.Contact> obj = global::Prime31.Json.decode<global::System.Collections.Generic.List<EtceteraAndroid.Contact>>(json);
			EtceteraAndroidManager.contactsLoadedEvent(obj);
		}
	}
}
