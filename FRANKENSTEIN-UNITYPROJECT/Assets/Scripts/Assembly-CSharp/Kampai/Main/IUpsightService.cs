namespace Kampai.Main
{
	public interface IUpsightService : global::Kampai.Common.IIapTelemetryService, global::Kampai.Common.ITelemetrySender
	{
		void Init(bool optOutStatus);

		void SendContentRequest(global::Kampai.Game.UpsightPromoTrigger.Placement placementId);

		void PreloadContentRequest(global::Kampai.Game.UpsightPromoTrigger.Placement placementId);

		bool CanLoadContent(global::Kampai.Game.UpsightPromoTrigger.Placement placementId);

		bool HasPreloadedContent(global::Kampai.Game.UpsightPromoTrigger.Placement placementId);

		void OnGameReloadCallback();

		void OnGameResumeCallback();
	}
}
