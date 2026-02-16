using Kampai.Common;
using Kampai.Main; // Important pour IUpsightService
using Kampai.Game.Transaction;

namespace Kampai.Game
{
    // FIX: On implémente IUpsightService, pas ISwrveService !
    // On ajoute aussi ITelemetrySender et IIapTelemetryService car SetupUpsightCommand les utilise.
    public class MockUpsightService : IUpsightService, ITelemetrySender, IIapTelemetryService
    {
        // --- Méthodes requises par IUpsightService ---

        public void Init(bool optOutStatus)
        {
            //UnityEngine.Debug.Log("MOCK UPSIGHT: Init called (OptOut: " + optOutStatus + ")");
        }

        public void COPPACompliance()
        {
            //UnityEngine.Debug.Log("MOCK UPSIGHT: COPPACompliance called");
        }

        public void SharingUsage(bool enabled)
        {
            // Rien à faire
        }

        // --- Méthodes requises par ITelemetrySender / IIapTelemetryService ---

        public void SendEvent(TelemetryEvent gameEvent)
        {
            // Mock silencieux
        }

        public void SendInAppPurchaseEventOnProductDelivery(string sku, TransactionDefinition reward)
        {
            // Mock silencieux
        }

        public void SendInAppPurchaseEventOnPurchaseComplete(IapTelemetryEvent iapTelemetryEvent)
        {
            // Mock silencieux
        }

        public void SendUserStatsUpdate()
        {
            // Mock silencieux
        }

        public void UpdateResources()
        {
            // Mock silencieux
        }

        public void SendContentRequest(UpsightPromoTrigger.Placement placementId)
        {
            //throw new System.NotImplementedException();
        }

        public void PreloadContentRequest(UpsightPromoTrigger.Placement placementId)
        {
            //throw new System.NotImplementedException();
        }

        public bool CanLoadContent(UpsightPromoTrigger.Placement placementId)
        {
            //throw new System.NotImplementedException();
            return true; //UNSURE
        }

        public bool HasPreloadedContent(UpsightPromoTrigger.Placement placementId)
        {
            //throw new System.NotImplementedException();
            return true; //UNSURE
        }

        public void OnGameReloadCallback()
        {
            //throw new System.NotImplementedException();
        }

        public void OnGameResumeCallback()
        {
            //throw new System.NotImplementedException();
        }
    
    }
}