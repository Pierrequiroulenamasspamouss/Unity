public class GPGManager : global::Prime31.AbstractManager
{
	public static event global::System.Action<string> authenticationSucceededEvent;

	public static event global::System.Action<string> authenticationFailedEvent;

	public static event global::System.Action userSignedOutEvent;

	public static event global::System.Action<string> reloadDataForKeyFailedEvent;

	public static event global::System.Action<string> reloadDataForKeySucceededEvent;

	public static event global::System.Action licenseCheckFailedEvent;

	public static event global::System.Action<string> profileImageLoadedAtPathEvent;

	public static event global::System.Action<string> finishedSharingEvent;

	public static event global::System.Action<GPGPlayerInfo, string> loadPlayerCompletedEvent;

	public static event global::System.Action<string> loadCloudDataForKeyFailedEvent;

	public static event global::System.Action<int, string> loadCloudDataForKeySucceededEvent;

	public static event global::System.Action<string> updateCloudDataForKeyFailedEvent;

	public static event global::System.Action<int, string> updateCloudDataForKeySucceededEvent;

	public static event global::System.Action<string> clearCloudDataForKeyFailedEvent;

	public static event global::System.Action<string> clearCloudDataForKeySucceededEvent;

	public static event global::System.Action<string> deleteCloudDataForKeyFailedEvent;

	public static event global::System.Action<string> deleteCloudDataForKeySucceededEvent;

	public static event global::System.Action<string, string> unlockAchievementFailedEvent;

	public static event global::System.Action<string, bool> unlockAchievementSucceededEvent;

	public static event global::System.Action<string, string> incrementAchievementFailedEvent;

	public static event global::System.Action<string, bool> incrementAchievementSucceededEvent;

	public static event global::System.Action<string, string> revealAchievementFailedEvent;

	public static event global::System.Action<string> revealAchievementSucceededEvent;

	public static event global::System.Action<string, string> submitScoreFailedEvent;

	public static event global::System.Action<string, global::System.Collections.Generic.Dictionary<string, object>> submitScoreSucceededEvent;

	public static event global::System.Action<string, string> loadScoresFailedEvent;

	public static event global::System.Action<global::System.Collections.Generic.List<GPGScore>> loadScoresSucceededEvent;

	public static event global::System.Action<global::System.Collections.Generic.List<GPGEvent>> allEventsLoadedEvent;

	public static event global::System.Action<GPGQuest> questListLauncherAcceptedQuestEvent;

	public static event global::System.Action<GPGQuestMilestone> questClaimedRewardsForQuestMilestoneEvent;

	public static event global::System.Action<GPGQuest> questCompletedEvent;

	public GPGManager()
	{
		global::Prime31.AbstractManager.initialize(typeof(GPGManager));
	}

	private void fireEventWithIdentifierAndError(global::System.Action<string, string> theEvent, string json)
	{
		if (theEvent != null)
		{
			global::System.Collections.Generic.Dictionary<string, object> dictionary = global::Prime31.JsonExtensions.dictionaryFromJson(json);
			if (dictionary != null && dictionary.ContainsKey("identifier") && dictionary.ContainsKey("error"))
			{
				theEvent(dictionary["identifier"].ToString(), dictionary["error"].ToString());
			}
			else
			{
				global::UnityEngine.Debug.LogError("json could not be deserialized to an identifier and an error: " + json);
			}
		}
	}

	private void fireEventWithIdentifierAndBool(global::System.Action<string, bool> theEvent, string param)
	{
		if (theEvent != null)
		{
			string[] array = param.Split(',');
			if (array.Length == 2)
			{
				theEvent(array[0], array[1] == "1");
			}
			else
			{
				global::UnityEngine.Debug.LogError("param could not be deserialized to an identifier and an error: " + param);
			}
		}
	}

	private void userSignedOut(string empty)
	{
		global::Prime31.ActionExtensions.fire(GPGManager.userSignedOutEvent);
	}

	private void reloadDataForKeyFailed(string error)
	{
		global::Prime31.ActionExtensions.fire(GPGManager.reloadDataForKeyFailedEvent, error);
	}

	private void reloadDataForKeySucceeded(string param)
	{
		global::Prime31.ActionExtensions.fire(GPGManager.reloadDataForKeySucceededEvent, param);
	}

	private void licenseCheckFailed(string param)
	{
		global::Prime31.ActionExtensions.fire(GPGManager.licenseCheckFailedEvent);
	}

	private void profileImageLoadedAtPath(string path)
	{
		global::Prime31.ActionExtensions.fire(GPGManager.profileImageLoadedAtPathEvent, path);
	}

	private void finishedSharing(string errorOrNull)
	{
		global::Prime31.ActionExtensions.fire(GPGManager.finishedSharingEvent, errorOrNull);
	}

	private void loadPlayerCompleted(string playerOrError)
	{
		if (GPGManager.loadPlayerCompletedEvent != null)
		{
			if (playerOrError.StartsWith("{"))
			{
				GPGManager.loadPlayerCompletedEvent(global::Prime31.Json.decode<GPGPlayerInfo>(playerOrError), null);
			}
			else
			{
				GPGManager.loadPlayerCompletedEvent(null, playerOrError);
			}
		}
	}

	private void loadCloudDataForKeyFailed(string error)
	{
		global::Prime31.ActionExtensions.fire(GPGManager.loadCloudDataForKeyFailedEvent, error);
	}

	private void loadCloudDataForKeySucceeded(string json)
	{
		global::System.Collections.Generic.Dictionary<string, object> dictionary = global::Prime31.JsonExtensions.dictionaryFromJson(json);
		global::Prime31.ActionExtensions.fire(GPGManager.loadCloudDataForKeySucceededEvent, int.Parse(dictionary["key"].ToString()), dictionary["data"].ToString());
	}

	private void updateCloudDataForKeyFailed(string error)
	{
		global::Prime31.ActionExtensions.fire(GPGManager.updateCloudDataForKeyFailedEvent, error);
	}

	private void updateCloudDataForKeySucceeded(string json)
	{
		global::System.Collections.Generic.Dictionary<string, object> dictionary = global::Prime31.JsonExtensions.dictionaryFromJson(json);
		global::Prime31.ActionExtensions.fire(GPGManager.updateCloudDataForKeySucceededEvent, int.Parse(dictionary["key"].ToString()), dictionary["data"].ToString());
	}

	private void clearCloudDataForKeyFailed(string error)
	{
		global::Prime31.ActionExtensions.fire(GPGManager.clearCloudDataForKeyFailedEvent, error);
	}

	private void clearCloudDataForKeySucceeded(string param)
	{
		global::Prime31.ActionExtensions.fire(GPGManager.clearCloudDataForKeySucceededEvent, param);
	}

	private void deleteCloudDataForKeyFailed(string error)
	{
		global::Prime31.ActionExtensions.fire(GPGManager.deleteCloudDataForKeyFailedEvent, error);
	}

	private void deleteCloudDataForKeySucceeded(string param)
	{
		global::Prime31.ActionExtensions.fire(GPGManager.deleteCloudDataForKeySucceededEvent, param);
	}

	private void unlockAchievementFailed(string json)
	{
		fireEventWithIdentifierAndError(GPGManager.unlockAchievementFailedEvent, json);
	}

	private void unlockAchievementSucceeded(string param)
	{
		fireEventWithIdentifierAndBool(GPGManager.unlockAchievementSucceededEvent, param);
	}

	private void incrementAchievementFailed(string json)
	{
		fireEventWithIdentifierAndError(GPGManager.incrementAchievementFailedEvent, json);
	}

	private void incrementAchievementSucceeded(string param)
	{
		string[] array = param.Split(',');
		if (array.Length == 2)
		{
			global::Prime31.ActionExtensions.fire(GPGManager.incrementAchievementSucceededEvent, array[0], array[1] == "1");
		}
	}

	private void revealAchievementFailed(string json)
	{
		fireEventWithIdentifierAndError(GPGManager.revealAchievementFailedEvent, json);
	}

	private void revealAchievementSucceeded(string achievementId)
	{
		global::Prime31.ActionExtensions.fire(GPGManager.revealAchievementSucceededEvent, achievementId);
	}

	private void submitScoreFailed(string json)
	{
		fireEventWithIdentifierAndError(GPGManager.submitScoreFailedEvent, json);
	}

	private void submitScoreSucceeded(string json)
	{
		if (GPGManager.submitScoreSucceededEvent != null)
		{
			global::System.Collections.Generic.Dictionary<string, object> dictionary = global::Prime31.JsonExtensions.dictionaryFromJson(json);
			string arg = "Unknown";
			if (dictionary.ContainsKey("leaderboardId"))
			{
				arg = dictionary["leaderboardId"].ToString();
			}
			GPGManager.submitScoreSucceededEvent(arg, dictionary);
		}
	}

	private void loadScoresFailed(string json)
	{
		fireEventWithIdentifierAndError(GPGManager.loadScoresFailedEvent, json);
	}

	private void loadScoresSucceeded(string json)
	{
		if (GPGManager.loadScoresSucceededEvent != null)
		{
			GPGManager.loadScoresSucceededEvent(global::Prime31.Json.decode<global::System.Collections.Generic.List<GPGScore>>(json));
		}
	}

	private void authenticationSucceeded(string param)
	{
		global::Prime31.ActionExtensions.fire(GPGManager.authenticationSucceededEvent, param);
	}

	private void authenticationFailed(string error)
	{
		global::Prime31.ActionExtensions.fire(GPGManager.authenticationFailedEvent, error);
	}

	private void allEventsLoaded(string json)
	{
		if (GPGManager.allEventsLoadedEvent != null)
		{
			GPGManager.allEventsLoadedEvent(global::Prime31.Json.decode<global::System.Collections.Generic.List<GPGEvent>>(json));
		}
	}

	private void questListLauncherClaimedRewardsForQuestMilestone(string json)
	{
		if (GPGManager.questClaimedRewardsForQuestMilestoneEvent != null)
		{
			GPGManager.questClaimedRewardsForQuestMilestoneEvent(global::Prime31.Json.decode<GPGQuestMilestone>(json));
		}
	}

	private void questCompleted(string json)
	{
		if (GPGManager.questCompletedEvent != null)
		{
			GPGManager.questCompletedEvent(global::Prime31.Json.decode<GPGQuest>(json));
		}
	}

	private void questListLauncherAcceptedQuest(string json)
	{
		if (GPGManager.questListLauncherAcceptedQuestEvent != null)
		{
			GPGManager.questListLauncherAcceptedQuestEvent(global::Prime31.Json.decode<GPGQuest>(json));
		}
	}
}
