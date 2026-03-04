namespace Kampai.Game
{
	public class TimedSocialEventService : global::Kampai.Game.ITimedSocialEventService
	{
		public const string SOCIAL_EVENT_TEAM_BY_USER_ENDPOINT = "/rest/tse/event/{0}/team/user/{1}";

		public const string SOCIAL_EVENT_INVITE_FRIENDS_ENDPOINT = "/rest/tse/event/{0}/team/{1}/user/{2}/invite";

		public const string SOCIAL_EVENT_REJECT_INVITE_ENDPOINT = "/rest/tse/event/{0}/team/{1}/user/{2}/reject";

		public const string SOCIAL_EVENT_JOIN_TEAM_ENDPOINT = "/rest/tse/event/{0}/team/{1}/user/{2}/join";

		public const string SOCIAL_EVENT_LEAVE_TEAM_ENDPOINT = "/rest/tse/event/{0}/team/{1}/user/{2}/leave";

		public const string SOCIAL_EVENT_FILL_ORDER_ENDPOINT = "/rest/tse/event/{0}/team/{1}/user/{2}/order";

		public const string SOCIAL_EVENT_CLAIM_REWARD_ENDPOINT = "/rest/tse/event/{0}/team/{1}/user/{2}/reward";

		public const string SOCIAL_EVENT_TEAMS_ENDPOINT = "/rest/tse/event/{0}/teams";

		private global::System.Collections.Generic.Dictionary<int, global::Kampai.Game.SocialTeamResponse> socialEventCache;

		private bool rewardCutscene;

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeService timeService { get; set; }

		[Inject]
		public global::Kampai.Game.IUserSessionService userSessionService { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject("game.server.host")]
		public string ServerUrl { get; set; }

		[Inject]
		public global::Kampai.Download.IDownloadService downloadService { get; set; }

		public TimedSocialEventService()
		{
			socialEventCache = new global::System.Collections.Generic.Dictionary<int, global::Kampai.Game.SocialTeamResponse>();
		}

		public void ClearCache()
		{
			socialEventCache.Clear();
		}

		public global::Kampai.Game.TimedSocialEventDefinition GetCurrentSocialEvent()
		{
			global::System.Collections.Generic.IList<global::Kampai.Game.TimedSocialEventDefinition> all = definitionService.GetAll<global::Kampai.Game.TimedSocialEventDefinition>();
			int num = timeService.GameTimeSeconds();
			foreach (global::Kampai.Game.TimedSocialEventDefinition item in all)
			{
				if (item.StartTime <= num && item.FinishTime >= num)
				{
					return item;
				}
			}
			return null;
		}

		public global::Kampai.Game.TimedSocialEventDefinition GetSocialEvent(int id)
		{
			global::System.Collections.Generic.IList<global::Kampai.Game.TimedSocialEventDefinition> all = definitionService.GetAll<global::Kampai.Game.TimedSocialEventDefinition>();
			if (all == null)
			{
				logger.Warning("GetSocialEvent not found");
				return null;
			}
			foreach (global::Kampai.Game.TimedSocialEventDefinition item in all)
			{
				if (item.ID == id)
				{
					return item;
				}
			}
			logger.Warning("GetSocialEvent not found with id {0}", id);
			return null;
		}

		public void GetSocialEventState(int eventID, global::strange.extensions.signal.impl.Signal<global::Kampai.Game.SocialTeamResponse, global::Kampai.Game.ErrorResponse> resultSignal)
		{
			global::Kampai.Game.UserSession userSession = userSessionService.UserSession;
			if (userSession == null)
			{
				logger.Error("User is not logged in. Can't get social team for event {0}", eventID);
				return;
			}
			string userID = userSession.UserID;
			global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse> signal = new global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse>();
			signal.AddListener(delegate(global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse response)
			{
				OnGetTeamResponse(resultSignal, response);
			});
			string uri = ServerUrl + string.Format("/rest/tse/event/{0}/team/user/{1}", eventID, userID);
			global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest request = new global::Ea.Sharkbite.HttpPlugin.Http.Impl.DefaultRequest(uri).WithHeaderParam("user_id", userSession.UserID).WithHeaderParam("session_key", userSession.SessionID).WithResponseSignal(signal);
			downloadService.Perform(request);
		}

		public global::Kampai.Game.SocialTeamResponse GetSocialEventStateCached(int eventID)
		{
			if (socialEventCache != null)
			{
				if (socialEventCache.ContainsKey(eventID))
				{
					return socialEventCache[eventID];
				}
				logger.Error("Social event not found in cache {0}", eventID);
			}
			return null;
		}

		public void CreateSocialTeam(int eventID, global::strange.extensions.signal.impl.Signal<global::Kampai.Game.SocialTeamResponse, global::Kampai.Game.ErrorResponse> resultSignal)
		{
			global::Kampai.Game.UserSession userSession = userSessionService.UserSession;
			if (userSession == null)
			{
				logger.Error("User is not logged in. Can't get social team for event {0}", eventID);
				return;
			}
			string userID = userSession.UserID;
			global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse> signal = new global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse>();
			signal.AddListener(delegate(global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse response)
			{
				OnGetTeamResponse(resultSignal, response);
			});
			string uri = ServerUrl + string.Format("/rest/tse/event/{0}/team/user/{1}", eventID, userID);
			global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest request = new global::Ea.Sharkbite.HttpPlugin.Http.Impl.DefaultRequest(uri).WithHeaderParam("user_id", userSession.UserID).WithHeaderParam("session_key", userSession.SessionID).WithMethod(global::Ea.Sharkbite.HttpPlugin.Http.Impl.DefaultRequest.METHOD_POST)
				.WithResponseSignal(signal);
			downloadService.Perform(request);
		}

		public void JoinSocialTeam(int eventID, long teamID, global::strange.extensions.signal.impl.Signal<global::Kampai.Game.SocialTeamResponse, global::Kampai.Game.ErrorResponse> resultSignal)
		{
			global::Kampai.Game.UserSession userSession = userSessionService.UserSession;
			if (userSession == null)
			{
				logger.Error("User is not logged in. Can't get social team for event {0}", eventID);
				return;
			}
			string userID = userSession.UserID;
			global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse> signal = new global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse>();
			signal.AddListener(delegate(global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse response)
			{
				OnGetTeamResponse(resultSignal, response);
			});
			string uri = ServerUrl + string.Format("/rest/tse/event/{0}/team/{1}/user/{2}/join", eventID, teamID, userID);
			global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest request = new global::Ea.Sharkbite.HttpPlugin.Http.Impl.DefaultRequest(uri).WithHeaderParam("user_id", userSession.UserID).WithHeaderParam("session_key", userSession.SessionID).WithContentType("application/json")
				.WithMethod(global::Ea.Sharkbite.HttpPlugin.Http.Impl.DefaultRequest.METHOD_POST)
				.WithResponseSignal(signal);
			downloadService.Perform(request);
		}

		public void LeaveSocialTeam(int eventID, long teamID, global::strange.extensions.signal.impl.Signal<global::Kampai.Game.SocialTeamResponse, global::Kampai.Game.ErrorResponse> resultSignal)
		{
			global::Kampai.Game.UserSession userSession = userSessionService.UserSession;
			if (userSession == null)
			{
				logger.Error("User is not logged in. Can't get social team for event {0}", eventID);
				return;
			}
			string userID = userSession.UserID;
			global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse> signal = new global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse>();
			signal.AddListener(delegate(global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse response)
			{
				OnGetTeamResponse(resultSignal, response);
			});
			string uri = ServerUrl + string.Format("/rest/tse/event/{0}/team/{1}/user/{2}/leave", eventID, teamID, userID);
			global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest request = new global::Ea.Sharkbite.HttpPlugin.Http.Impl.DefaultRequest(uri).WithHeaderParam("user_id", userSession.UserID).WithHeaderParam("session_key", userSession.SessionID).WithContentType("application/json")
				.WithMethod(global::Ea.Sharkbite.HttpPlugin.Http.Impl.DefaultRequest.METHOD_POST)
				.WithResponseSignal(signal);
			downloadService.Perform(request);
		}

		public void InviteFriends(int eventID, long teamID, global::Kampai.Game.IdentityType identityType, global::System.Collections.Generic.IList<string> externalIDs, global::strange.extensions.signal.impl.Signal<global::Kampai.Game.SocialTeamResponse, global::Kampai.Game.ErrorResponse> resultSignal)
		{
			global::Kampai.Game.UserSession userSession = userSessionService.UserSession;
			if (userSession == null)
			{
				logger.Error("User is not logged in. Can't get social team for event {0}", eventID);
				return;
			}
			string userID = userSession.UserID;
			global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse> signal = new global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse>();
			signal.AddListener(delegate(global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse response)
			{
				OnGetTeamResponse(resultSignal, response);
			});
			global::Kampai.Game.InviteFriendsRequest inviteFriendsRequest = new global::Kampai.Game.InviteFriendsRequest();
			inviteFriendsRequest.IdentityType = identityType;
			inviteFriendsRequest.ExternalIds = externalIDs;
			string uri = ServerUrl + string.Format("/rest/tse/event/{0}/team/{1}/user/{2}/invite", eventID, teamID, userID);
			global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest request = new global::Ea.Sharkbite.HttpPlugin.Http.Impl.DefaultRequest(uri).WithHeaderParam("user_id", userSession.UserID).WithHeaderParam("session_key", userSession.SessionID).WithContentType("application/json")
				.WithMethod(global::Ea.Sharkbite.HttpPlugin.Http.Impl.DefaultRequest.METHOD_POST)
				.WithEntity(inviteFriendsRequest)
				.WithResponseSignal(signal);
			downloadService.Perform(request);
		}

		public void RejectInvitation(int eventID, long teamID, global::strange.extensions.signal.impl.Signal<global::Kampai.Game.SocialTeamResponse, global::Kampai.Game.ErrorResponse> resultSignal)
		{
			global::Kampai.Game.UserSession userSession = userSessionService.UserSession;
			if (userSession == null)
			{
				logger.Error("User is not logged in. Can't get social team for event {0}", eventID);
				return;
			}
			string userID = userSession.UserID;
			global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse> signal = new global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse>();
			signal.AddListener(delegate(global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse response)
			{
				OnGetTeamResponse(resultSignal, response);
			});
			string uri = ServerUrl + string.Format("/rest/tse/event/{0}/team/{1}/user/{2}/reject", eventID, teamID, userID);
			global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest request = new global::Ea.Sharkbite.HttpPlugin.Http.Impl.DefaultRequest(uri).WithHeaderParam("user_id", userSession.UserID).WithHeaderParam("session_key", userSession.SessionID).WithContentType("application/json")
				.WithMethod(global::Ea.Sharkbite.HttpPlugin.Http.Impl.DefaultRequest.METHOD_POST)
				.WithResponseSignal(signal);
			downloadService.Perform(request);
		}

		public void FillOrder(int eventID, long teamID, int orderID, global::strange.extensions.signal.impl.Signal<global::Kampai.Game.SocialTeamResponse, global::Kampai.Game.ErrorResponse> resultSignal)
		{
			global::Kampai.Game.UserSession userSession = userSessionService.UserSession;
			if (userSession == null)
			{
				logger.Error("User is not logged in. Can't get social team for event {0}", eventID);
				return;
			}
			string userID = userSession.UserID;
			global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse> signal = new global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse>();
			signal.AddListener(delegate(global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse response)
			{
				OnFillOrderResponse(resultSignal, response);
			});
			global::Kampai.Game.FillOrderRequest fillOrderRequest = new global::Kampai.Game.FillOrderRequest();
			fillOrderRequest.OrderID = orderID;
			string uri = ServerUrl + string.Format("/rest/tse/event/{0}/team/{1}/user/{2}/order", eventID, teamID, userID);
			global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest request = new global::Ea.Sharkbite.HttpPlugin.Http.Impl.DefaultRequest(uri).WithHeaderParam("user_id", userSession.UserID).WithHeaderParam("session_key", userSession.SessionID).WithContentType("application/json")
				.WithMethod(global::Ea.Sharkbite.HttpPlugin.Http.Impl.DefaultRequest.METHOD_POST)
				.WithEntity(fillOrderRequest)
				.WithResponseSignal(signal);
			downloadService.Perform(request);
		}

		public void ClaimReward(int eventID, long teamID, global::strange.extensions.signal.impl.Signal<global::Kampai.Game.SocialTeamResponse, global::Kampai.Game.ErrorResponse> resultSignal)
		{
			global::Kampai.Game.UserSession userSession = userSessionService.UserSession;
			if (userSession == null)
			{
				logger.Error("User is not logged in. Can't get social team for event {0}", eventID);
				return;
			}
			string userID = userSession.UserID;
			global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse> signal = new global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse>();
			signal.AddListener(delegate(global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse response)
			{
				OnGetTeamResponse(resultSignal, response);
			});
			string uri = ServerUrl + string.Format("/rest/tse/event/{0}/team/{1}/user/{2}/reward", eventID, teamID, userID);
			global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest request = new global::Ea.Sharkbite.HttpPlugin.Http.Impl.DefaultRequest(uri).WithHeaderParam("user_id", userSession.UserID).WithHeaderParam("session_key", userSession.SessionID).WithContentType("application/json")
				.WithMethod(global::Ea.Sharkbite.HttpPlugin.Http.Impl.DefaultRequest.METHOD_POST)
				.WithResponseSignal(signal);
			downloadService.Perform(request);
		}

		public void GetSocialTeams(int eventID, global::System.Collections.Generic.IList<long> teamIds, global::strange.extensions.signal.impl.Signal<global::System.Collections.Generic.Dictionary<long, global::Kampai.Game.SocialTeam>> resultSignal)
		{
			global::Kampai.Game.UserSession userSession = userSessionService.UserSession;
			global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse> signal = new global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse>();
			signal.AddListener(delegate(global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse response)
			{
				OnGetTeamsResponse(resultSignal, response);
			});
			global::Kampai.Game.GetTeamsRequest getTeamsRequest = new global::Kampai.Game.GetTeamsRequest();
			getTeamsRequest.TeamIDs = teamIds;
			string uri = ServerUrl + string.Format("/rest/tse/event/{0}/teams", eventID);
			global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest request = new global::Ea.Sharkbite.HttpPlugin.Http.Impl.DefaultRequest(uri).WithHeaderParam("user_id", userSession.UserID).WithHeaderParam("session_key", userSession.SessionID).WithContentType("application/json")
				.WithMethod(global::Ea.Sharkbite.HttpPlugin.Http.Impl.DefaultRequest.METHOD_POST)
				.WithEntity(getTeamsRequest)
				.WithResponseSignal(signal);
			downloadService.Perform(request);
		}

		private void OnGetTeamResponse(global::strange.extensions.signal.impl.Signal<global::Kampai.Game.SocialTeamResponse, global::Kampai.Game.ErrorResponse> resultSignal, global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse response)
		{
			if (response.Success)
			{
				string body = response.Body;

                // FIX: Use FastJsonParser instead of FastJSONDeserializer to get a generic object dictionary
                global::System.Collections.Generic.Dictionary<string, object> dict = global::Kampai.Util.FastJsonParser.Deserialize(body) as global::System.Collections.Generic.Dictionary<string, object>;
                global::Kampai.Game.SocialTeamResponse socialTeamResponse = new global::Kampai.Game.SocialTeamResponse();
                
                if (dict != null)
                {
                    if (dict.ContainsKey("eventId"))
                    {
                        socialTeamResponse.EventId = global::System.Convert.ToInt32(dict["eventId"]);
                    }
                    if (dict.ContainsKey("team") && dict["team"] != null)
                    {
                        var teamDict = dict["team"] as global::System.Collections.Generic.Dictionary<string, object>;
                        if (teamDict != null)
                        {
                            global::Kampai.Game.TimedSocialEventDefinition def = null;
                            if (teamDict.ContainsKey("socialEventId"))
                            {
                                int socialEventId = global::System.Convert.ToInt32(teamDict["socialEventId"]);
                                def = definitionService.Get<global::Kampai.Game.TimedSocialEventDefinition>(socialEventId);
                            }
                            
                            // Initialize SocialTeam using its explicit parameterized constructor
                            socialTeamResponse.Team = new global::Kampai.Game.SocialTeam(def);

                            // Bypass string serialization entirely. FastJsonParser parses arrays to List<object>, which FastJSONSerializer chokes on.
                            // Convert the dictionary directly to a JToken and deserialize it.
                            try 
                            {
                                global::Newtonsoft.Json.Linq.JToken token = global::Kampai.Util.FastJsonParser.ConvertToJToken(teamDict);
                                global::Newtonsoft.Json.JsonReader reader = new global::Newtonsoft.Json.Linq.JTokenReader(token);
                                JsonConverters converters = new JsonConverters();
                                socialTeamResponse.Team.Deserialize(reader, converters);
                            }
                            catch (global::System.Exception e)
                            {
                                logger.Error("TSE Team parse fallback failed: " + e.Message);
                            }
                        }
                    }
                    if (dict.ContainsKey("userEvent") && dict["userEvent"] != null)
                    {
                        try 
                        {
                            global::Newtonsoft.Json.Linq.JToken token = global::Kampai.Util.FastJsonParser.ConvertToJToken(dict["userEvent"]);
                            socialTeamResponse.UserEvent = token.ToObject<global::Kampai.Game.SocialTeamUserEvent>();
                        }
                        catch {}
                    }
                }
				socialEventCache[socialTeamResponse.EventId] = socialTeamResponse;
				UpdateClaimRewardForPastEvent(socialTeamResponse);
				if (resultSignal != null)
				{
					resultSignal.Dispatch(socialTeamResponse, null);
				}
			}
			else
			{
				logger.Warning("Failed to get social team", response.Code);
				if (resultSignal != null)
				{
					global::Kampai.Game.ErrorResponse errorResponse = GetErrorResponse(response);
					resultSignal.Dispatch(null, errorResponse);
				}
			}
		}

		private void OnFillOrderResponse(global::strange.extensions.signal.impl.Signal<global::Kampai.Game.SocialTeamResponse, global::Kampai.Game.ErrorResponse> resultSignal, global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse response)
		{
			string body = response.Body;
			if (response.Success)
			{
				global::Kampai.Game.SocialTeamResponse socialTeamResponse = global::Newtonsoft.Json.JsonConvert.DeserializeObject<global::Kampai.Game.SocialTeamResponse>(body, new global::Newtonsoft.Json.JsonConverter[1]
				{
					new global::Kampai.Game.SocialTeamConverter(definitionService)
				});
				socialEventCache[socialTeamResponse.EventId] = socialTeamResponse;
				if (resultSignal != null)
				{
					resultSignal.Dispatch(socialTeamResponse, null);
				}
			}
			else
			{
				logger.Warning("Failed to fill order in social event");
				if (resultSignal != null)
				{
					global::Kampai.Game.ErrorResponse errorResponse = GetErrorResponse(response);
					resultSignal.Dispatch(null, errorResponse);
				}
			}
		}

		private void OnGetTeamsResponse(global::strange.extensions.signal.impl.Signal<global::System.Collections.Generic.Dictionary<long, global::Kampai.Game.SocialTeam>> resultSignal, global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse response)
		{
			if (response.Success)
			{
				if (resultSignal != null)
				{
					string body = response.Body;
					global::System.Collections.Generic.Dictionary<long, global::Kampai.Game.SocialTeam> type = global::Newtonsoft.Json.JsonConvert.DeserializeObject<global::System.Collections.Generic.Dictionary<long, global::Kampai.Game.SocialTeam>>(body, new global::Newtonsoft.Json.JsonConverter[1]
					{
						new global::Kampai.Game.SocialTeamConverter(definitionService)
					});
					resultSignal.Dispatch(type);
				}
			}
			else
			{
				logger.Warning("Failed to get list of social teams", response.Code);
				if (resultSignal != null)
				{
					resultSignal.Dispatch(null);
				}
			}
		}

		private global::Kampai.Game.ErrorResponse GetErrorResponse(global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse response)
		{
			string body = response.Body;
			global::Kampai.Game.ErrorResponse errorResponse = null;
			try
			{
				errorResponse = global::Newtonsoft.Json.JsonConvert.DeserializeObject<global::Kampai.Game.ErrorResponse>(body);
			}
			catch (global::System.Exception)
			{
				errorResponse = new global::Kampai.Game.ErrorResponse();
				global::Kampai.Game.ErrorResponseContent errorResponseContent = new global::Kampai.Game.ErrorResponseContent();
				errorResponseContent.ResponseCode = response.Code;
				errorResponseContent.Code = 0;
				errorResponseContent.Message = "uknown";
				errorResponse.Error = errorResponseContent;
			}
			return errorResponse;
		}

		public global::Kampai.Game.TimedSocialEventDefinition GetCurrentTimedSocialEventDefinition()
		{
			global::System.Collections.Generic.IList<global::Kampai.Game.TimedSocialEventDefinition> all = definitionService.GetAll<global::Kampai.Game.TimedSocialEventDefinition>();
			int num = timeService.GameTimeSeconds();
			foreach (global::Kampai.Game.TimedSocialEventDefinition item in all)
			{
				int startTime = item.StartTime;
				int finishTime = item.FinishTime;
				if (num >= startTime && num < finishTime)
				{
					return item;
				}
			}
			return null;
		}

		public void setRewardCutscene(bool cutscene)
		{
			rewardCutscene = cutscene;
		}

		public bool isRewardCutscene()
		{
			return rewardCutscene;
		}

		public global::System.Collections.Generic.IList<int> GetPastEventsWithUnclaimedReward()
	{

		global::System.Collections.Generic.List<int> list = new global::System.Collections.Generic.List<int>();
		
		// CRASH FIX: Add null checks for all injected services
		if (timeService == null)
		{

			return list;
		}

		
		if (definitionService == null)
		{
			return list;
		}
		
		if (playerService == null)
		{
			return list;
		}
		
		int num = timeService.GameTimeSeconds();
		
		global::System.Collections.Generic.IList<global::Kampai.Game.TimedSocialEventDefinition> all = definitionService.GetAll<global::Kampai.Game.TimedSocialEventDefinition>();
		
		// CRASH FIX: Check for null definitions list
		if (all == null)
		{
			return list;
		}
		
		// CRASH FIX: Wrap iteration in try-catch
		try
		{
			foreach (global::Kampai.Game.TimedSocialEventDefinition item in all)
			{
				if (item == null)
				{
					continue;
				}
				
				int iD = item.ID;
				
				if (item.FinishTime >= num)
				{
					continue;
				}
				
				global::Kampai.Game.SocialClaimRewardItem.ClaimState claimState = playerService.GetSocialClaimReward(iD);
				
				switch (claimState)
				{
				case global::Kampai.Game.SocialClaimRewardItem.ClaimState.EVENT_COMPLETED_REWARD_NOT_CLAIMED:
					list.Add(iD);
					if (!socialEventCache.ContainsKey(iD))
					{
						GetSocialEventState(iD, null);
					}
					break;
				case global::Kampai.Game.SocialClaimRewardItem.ClaimState.UNKNOWN:
					GetSocialEventState(iD, null);
					break;
				}
			}
		}
		catch (global::System.Exception ex)
		{
		}
		
		return list;
	}

		private void UpdateClaimRewardForPastEvent(global::Kampai.Game.SocialTeamResponse teamResponse)
		{
			global::Kampai.Game.TimedSocialEventDefinition timedSocialEventDefinition = definitionService.Get<global::Kampai.Game.TimedSocialEventDefinition>(teamResponse.EventId);
			int num = timeService.GameTimeSeconds();
			if (timedSocialEventDefinition == null || timedSocialEventDefinition.FinishTime > num)
			{
				return;
			}
			if (teamResponse.UserEvent == null)
			{
				playerService.AddSocialClaimReward(teamResponse.EventId, global::Kampai.Game.SocialClaimRewardItem.ClaimState.EVENT_NOT_COMPLETED);
			}
			else if (teamResponse.UserEvent.RewardClaimed)
			{
				playerService.AddSocialClaimReward(teamResponse.EventId, global::Kampai.Game.SocialClaimRewardItem.ClaimState.REWARD_CLAIMED);
			}
			else if (teamResponse.Team != null && teamResponse.Team.OrderProgress != null)
			{
				if (teamResponse.Team.OrderProgress.Count == timedSocialEventDefinition.Orders.Count)
				{
					playerService.AddSocialClaimReward(teamResponse.EventId, global::Kampai.Game.SocialClaimRewardItem.ClaimState.EVENT_COMPLETED_REWARD_NOT_CLAIMED);
				}
				else
				{
					playerService.AddSocialClaimReward(teamResponse.EventId, global::Kampai.Game.SocialClaimRewardItem.ClaimState.EVENT_NOT_COMPLETED);
				}
			}
		}
	}
}
