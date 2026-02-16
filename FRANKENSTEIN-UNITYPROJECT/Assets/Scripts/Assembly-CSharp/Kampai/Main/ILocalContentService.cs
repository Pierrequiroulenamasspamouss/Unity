namespace Kampai.Main
{
	public interface ILocalContentService
	{
		bool IsLocalAsset(string name);

		bool IsStreamingAsset(string name);

		string GetAssetPath(string name);

		global::System.Collections.Generic.List<string> GetStreamingAudioBanks();
	}
}
