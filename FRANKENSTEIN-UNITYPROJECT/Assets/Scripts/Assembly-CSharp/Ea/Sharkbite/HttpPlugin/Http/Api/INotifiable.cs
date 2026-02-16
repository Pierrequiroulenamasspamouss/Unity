namespace Ea.Sharkbite.HttpPlugin.Http.Api
{
	public interface INotifiable
	{
		void RegisterNotifiable(global::System.Action<global::Ea.Sharkbite.HttpPlugin.Http.Api.DownloadProgress, global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest> notifyAction);
	}
}
