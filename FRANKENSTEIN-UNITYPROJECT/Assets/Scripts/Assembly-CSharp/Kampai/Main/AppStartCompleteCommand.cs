namespace Kampai.Main
{
    public class AppStartCompleteCommand : global::strange.extensions.command.impl.Command
    {
        [Inject]
        public global::Kampai.Util.ILogger logger { get; set; }

        [Inject]
        public global::Kampai.Main.TriggerUpsightPromoSignal triggerUpsightPromoSignal { get; set; }

        [Inject]
        public ILocalPersistanceService localPersistanceService { get; set; }

        [Inject]
        public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

        public override void Execute()
        {
            // The Game scene was just loaded additively, but it takes a frame to fully initialize
            // Wait one frame before looking for the camera
            routineRunner.StartCoroutine(EnableCameraAfterSceneLoads());
        }

        private global::System.Collections.IEnumerator EnableCameraAfterSceneLoads()
        {
            global::UnityEngine.Camera mainCam = null;
            int maxRetries = 60; // Wait up to 60 frames (approx 1-2 seconds) for the camera to spawn
            int currentRetry = 0;

            while (mainCam == null && currentRetry < maxRetries)
            {
                yield return new global::UnityEngine.WaitForEndOfFrame();
                mainCam = global::UnityEngine.Camera.main;

                if (mainCam == null)
                {
                    global::UnityEngine.GameObject camGo = global::UnityEngine.GameObject.FindWithTag("MainCamera");
                    if (camGo != null)
                    {
                        mainCam = camGo.GetComponent<global::UnityEngine.Camera>();
                    }
                }
                currentRetry++;
            }

            if (mainCam != null)
            {
                mainCam.enabled = true;
                logger.Info("AppStartCompleteCommand: Camera enabled on frame " + currentRetry + " - " + mainCam.gameObject.name);
            }
            else
            {
                logger.Error("AppStartCompleteCommand: No camera found after scene load! (Timed out after " + maxRetries + " frames)");
            }
        }
    }
}
