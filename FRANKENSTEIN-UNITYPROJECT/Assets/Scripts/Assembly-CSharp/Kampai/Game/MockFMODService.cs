using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kampai.Common.Service.Audio; // Ensure we use the right namespace

namespace Kampai.Game
{
    public class MockFMODService : IFMODService
    {
        
        public string GetGuid(string eventName)
        {
           //  throw new System.NotImplementedException();
           return string.Empty;
        }

        public IEnumerator InitializeSystem()
        {
            //throw new System.NotImplementedException();
            return null;
        }

        public bool LoadFromAssetBundle(string bundleName)
        {
            //throw new System.NotImplementedException();
            return false;
        }
    }
}