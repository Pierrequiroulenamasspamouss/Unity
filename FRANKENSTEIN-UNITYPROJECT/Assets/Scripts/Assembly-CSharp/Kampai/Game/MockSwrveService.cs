using Kampai.Common;
using Kampai.Game.Transaction;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Kampai.Game
{
    /// <summary>
    /// Mock implementation of ISwrveService.
    /// Does nothing except optionally log calls for debugging.
    /// Safe for editor, tests, and offline builds.
    /// </summary>
    public class MockSwrveService : ISwrveService
    {
        private bool _initialized;
        private bool _sessionActive;
        private bool _sharingEnabled = true;

        // --------------------
        // Lifecycle
        // --------------------

        public void Init()
        {
            _initialized = true;
#if UNITY_EDITOR
            Debug.Log("[MockSwrve] Init");
#endif
        }

        public void SessionStart()
        {
            _sessionActive = true;
#if UNITY_EDITOR
            Debug.Log("[MockSwrve] SessionStart");
#endif
        }

        public void SessionEnd()
        {
            _sessionActive = false;
#if UNITY_EDITOR
            Debug.Log("[MockSwrve] SessionEnd");
#endif
        }

        // --------------------
        // Events
        // --------------------

        public void LogEvent(string name)
        {
#if UNITY_EDITOR
            Debug.Log("[MockSwrve] LogEvent: " + name);
#endif
        }

        public void LogEvent(string name, Dictionary<string, string> payload)
        {
#if UNITY_EDITOR
            if (payload == null || payload.Count == 0)
            {
                Debug.Log("[MockSwrve] LogEvent: " + name);
            }
            else
            {
                Debug.Log("[MockSwrve] LogEvent:" +name  + "Payload: " +FormatPayload(payload) );
            }
#endif
        }

        public void SendEvent(TelemetryEvent gameEvent)
        {
#if UNITY_EDITOR
            Debug.Log("[MockSwrve] SendEvent: "+gameEvent);
#endif
        }

        // --------------------
        // Economy / IAP
        // --------------------

        public void SendInAppPurchaseEventOnPurchaseComplete(IapTelemetryEvent iapTelemetryEvent)
        {
#if UNITY_EDITOR
            Debug.Log("[MockSwrve] IAP Purchase Complete: "+iapTelemetryEvent);
#endif
        }

        public void SendInAppPurchaseEventOnProductDelivery(string sku, TransactionDefinition reward)
        {
#if UNITY_EDITOR
            Debug.Log("[MockSwrve] IAP Product Delivered: SKU= "+ sku + "Reward= "+ reward);
#endif
        }

        // --------------------
        // User / system
        // --------------------

        public void UpdateResources()
        {
#if UNITY_EDITOR
            Debug.Log("[MockSwrve] UpdateResources");
#endif
        }

        public void SendUserStatsUpdate()
        {
#if UNITY_EDITOR
            Debug.Log("[MockSwrve] SendUserStatsUpdate");
#endif
        }

        public void SetPlayerServiceReference(global::Kampai.Game.IPlayerService playerService)
        {
#if UNITY_EDITOR
            Debug.Log("[MockSwrve] SetPlayerServiceReference");
#endif
        }

        public void COPPACompliance()
        {
#if UNITY_EDITOR
            Debug.Log("[MockSwrve] COPPACompliance");
#endif
        }

        public void SharingUsage(bool enabled)
        {
            _sharingEnabled = enabled;
#if UNITY_EDITOR
            Debug.Log("[MockSwrve] SharingUsage: "+enabled);
#endif
        }

        // --------------------
        // Helpers
        // --------------------

#if UNITY_EDITOR
        private string FormatPayload(Dictionary<string, string> payload)
        {
            var parts = new List<string>();
            foreach (var kvp in payload)
            {
                parts.Add(kvp.Key+ "=" + kvp.Value);
            }
            return string.Join(", ", parts.ToArray());
        }
#endif
    }
}