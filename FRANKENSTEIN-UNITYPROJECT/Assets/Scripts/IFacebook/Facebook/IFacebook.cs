namespace Facebook
{
	public interface IFacebook
	{
		string UserId { get; }

		string AccessToken { get; }

		global::System.DateTime AccessTokenExpiresAt { get; }

		bool IsInitialized { get; }

		bool IsLoggedIn { get; }

		int DialogMode { get; set; }

		bool LimitEventUsage { get; set; }

		void Init(global::Facebook.InitDelegate onInitComplete, string appId, bool cookie = false, bool logging = true, bool status = true, bool xfbml = false, string channelUrl = "", string authResponse = null, bool frictionlessRequests = false, global::Facebook.HideUnityDelegate hideUnityDelegate = null);

		void Login(string scope = "", global::Facebook.FacebookDelegate callback = null);

		void Logout();

		void GetAuthResponse(global::Facebook.FacebookDelegate callback = null);

		void AppRequest(string message, string[] to = null, global::System.Collections.Generic.List<object> filters = null, string[] excludeIds = null, int? maxRecipients = null, string data = "", string title = "", global::Facebook.FacebookDelegate callback = null);

		void AppRequest(string message, global::Facebook.OGActionType actionType, string objectId, string[] to = null, global::System.Collections.Generic.List<object> filters = null, string[] excludeIds = null, int? maxRecipients = null, string data = "", string title = "", global::Facebook.FacebookDelegate callback = null);

		void FeedRequest(string toId = "", string link = "", string linkName = "", string linkCaption = "", string linkDescription = "", string picture = "", string mediaSource = "", string actionName = "", string actionLink = "", string reference = "", global::System.Collections.Generic.Dictionary<string, string[]> properties = null, global::Facebook.FacebookDelegate callback = null);

		void Pay(string product, string action = "purchaseitem", int quantity = 1, int? quantityMin = null, int? quantityMax = null, string requestId = null, string pricepointId = null, string testCurrency = null, global::Facebook.FacebookDelegate callback = null);

		void GameGroupCreate(string name, string description, string privacy = "CLOSED", global::Facebook.FacebookDelegate callback = null);

		void GameGroupJoin(string id, global::Facebook.FacebookDelegate callback = null);

		void API(string query, global::Facebook.HttpMethod method, global::System.Collections.Generic.Dictionary<string, string> formData = null, global::Facebook.FacebookDelegate callback = null);

		void API(string query, global::Facebook.HttpMethod method, global::UnityEngine.WWWForm formData, global::Facebook.FacebookDelegate callback = null);

		void AppEventsLogEvent(string logEvent, float? valueToSum = null, global::System.Collections.Generic.Dictionary<string, object> parameters = null);

		void AppEventsLogPurchase(float logPurchase, string currency = "USD", global::System.Collections.Generic.Dictionary<string, object> parameters = null);

		[global::System.Obsolete("use ActivateApp")]
		void PublishInstall(string appId, global::Facebook.FacebookDelegate callback = null);

		void ActivateApp(string appId = null);

		void GetDeepLink(global::Facebook.FacebookDelegate callback);
	}
}
