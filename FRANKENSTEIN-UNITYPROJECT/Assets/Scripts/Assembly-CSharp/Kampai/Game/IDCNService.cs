namespace Kampai.Game
{
	public interface IDCNService
	{
		void Perform(global::System.Func<global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest> request, bool isTokenRequest = false);

		void SetToken(global::Kampai.Game.DCNToken token);

		string GetToken();

		void SetFeaturedContentId(int featuredContentId);

		int GetFeaturedContentId();
	}
}
