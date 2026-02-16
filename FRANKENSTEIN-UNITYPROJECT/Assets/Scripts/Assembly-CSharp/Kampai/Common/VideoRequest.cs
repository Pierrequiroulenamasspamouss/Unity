namespace Kampai.Common
{
	internal sealed class VideoRequest
	{
		public string locale;

		public bool showControls;

		public bool closeOnTouch;

		public int retries = 3;

		public bool inFlight;

		public int progressBarStart;

		public int progressBarNow = 30;

		public global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest networkRequest;

		public global::System.Action callback;

		public string videoUriTemplate;
	}
}
