namespace Ea.Sharkbite.HttpPlugin.Http.Api
{
	public interface IRequest
	{
		string Uri { get; set; }

		string Method { get; set; }

		byte[] Body { get; set; }

		string Accept { get; set; }

		string ContentType { get; set; }

		string Username { get; set; }

		string Password { set; }

		global::System.Collections.Generic.List<global::System.Collections.Generic.KeyValuePair<string, string>> QueryParams { get; set; }

		global::System.Collections.Generic.List<global::System.Collections.Generic.KeyValuePair<string, string>> Headers { get; set; }

		global::System.Collections.Generic.List<global::System.Collections.Generic.KeyValuePair<string, string>> FormParams { get; set; }

		bool CanRetry { get; set; }

		int RetryCount { get; set; }

		global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse> ResponseSignal { get; set; }

		global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.DownloadProgress, global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest> NotifyProgress { get; set; }

		global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse Get();

		void Get(global::System.Action<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse> callback);

		global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse Head();

		void Head(global::System.Action<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse> callback);

		global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse Options();

		void Options(global::System.Action<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse> callback);

		global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse Post();

		void Post(global::System.Action<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse> callback);

		global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse Put();

		void Put(global::System.Action<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse> callback);

		global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse Delete();

		void Delete(global::System.Action<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse> callback);

		global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse Execute();

		void Execute(global::System.Action<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse> callback);

		global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest WithContentType(string contentType);

		global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest WithAccept(string accept);

		global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest WithQueryParam(string key, string value);

		global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest WithHeaderParam(string key, string value);

		global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest WithFormParam(string key, string value);

		global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest WithBasicAuth(string username, string password);

		global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest WithBody(byte[] body);

		global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest WithPreprocessor(global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequestPreprocessor preprocessor);

		global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest WithMethod(string method);

		global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest WithResponseSignal(global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse> responseSignal);

		global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest WithNotifyProgress(global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.DownloadProgress, global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest> notify);

		global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest WithRetry(bool retry = true, int times = 3);

		global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest WithEntity(object entity);

		void Abort();

		bool IsAborted();
	}
}
