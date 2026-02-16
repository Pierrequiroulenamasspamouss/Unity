using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Kampai.Main
{
    /// <summary>
    /// A fake localization service that never crashes.
    /// Always returns the key as the "translated" value.
    /// Useful for tests, editor mode, and fallback scenarios.
    /// </summary>
    public class MockHALService : ILocalizationService
    {
        private bool _initialized;
        private string _language = "en";

        // --------------------
        // Core translation API
        // --------------------

        public string GetString(string key)
        {
            return key ?? string.Empty;
        }

        public string GetString(string key, params object[] args)
        {
            if (string.IsNullOrEmpty(key))
                return string.Empty;

            // Optional: try formatting, but never throw
            if (args != null && args.Length > 0)
            {
                try
                {
                    return string.Format(key, args);
                }
                catch
                {
                    // If formatting fails, just return the key
                    return key;
                }
            }

            return key;
        }

        // --------------------
        // Initialization
        // --------------------

        public void Init(string language)
        {
            _language = string.IsNullOrEmpty(language) ? "en" : language;
            _initialized = true;
        }

        public void Initialize(string langCode)
        {
            Init(langCode);
        }

        public bool IsInitialized()
        {
            return _initialized;
        }

        // --------------------
        // Language info
        // --------------------

        public string GetLanguage()
        {
            return _language;
        }

        public string GetLanguageKey()
        {
            return _language;
        }

        public bool IsLanguageSupported()
        {
            // Mock supports any language code
            return true;
        }

        public bool IsRightToLeft()
        {
            // Mock assumes LTR languages only
            return false;
        }

        // --------------------
        // Key lookup
        // --------------------

        public bool Contains(string key)
        {
            // In a mock, we pretend all keys exist
            return !string.IsNullOrEmpty(key);
        }

        // --------------------
        // Update loop (no-op)
        // --------------------

        public void Update()
        {
            // Intentionally empty
        }
    }
}
