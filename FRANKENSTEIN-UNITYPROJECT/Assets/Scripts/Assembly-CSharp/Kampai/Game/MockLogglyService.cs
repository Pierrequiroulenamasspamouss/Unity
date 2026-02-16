using UnityEngine;
using System.Collections;
using Kampai.Util.Logging.Hosted; // Check namespace if red
using Kampai.Util;

namespace Kampai.Game 
{
    public class MockLogglyService : ILogglyService
    {
        public void Initialize(IConfigurationsService configurationsService, IUserSessionService userSessionService, ILogglyDtoCache logglyDtoCache, ILocalPersistanceService localPersistService)
        {
            //throw new System.NotImplementedException();

        }



        
        public void Log(Logger.Level level, string text, bool isFatal)
        {
            //throw new System.NotImplementedException();
        }

        public void OnConfigurationsLoaded(bool init)
        {
            //throw new System.NotImplementedException();
        }

        public void OnUserSessionGranted()
        {
            //throw new System.NotImplementedException();
        }

        public IEnumerator Process(float interval)
        {
            //throw new System.NotImplementedException();
            return null;
        }

        public void ShipAll(bool force) {}

        public IEnumerator ShipAllOnInterval(float interval)
        {
            //throw new System.NotImplementedException();
            return null;
        }

        public void UpdateKillSwitch()
        {
            //throw new System.NotImplementedException();
        }
    }
}