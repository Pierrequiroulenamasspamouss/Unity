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
            // Wait for end of frame to ensure additive scenes are fully loaded
            yield return new global::UnityEngine.WaitForEndOfFrame();

            // Now try to find and enable the camera
            global::UnityEngine.Camera mainCam = global::UnityEngine.Camera.main;

            // If Camera.main is null, try finding by tag
            if (mainCam == null)
            {
                global::UnityEngine.GameObject camGo = global::UnityEngine.GameObject.FindWithTag("MainCamera");
                if (camGo != null)
                {
                    mainCam = camGo.GetComponent<global::UnityEngine.Camera>();
                }
            }

            if (mainCam != null)
            {
                mainCam.enabled = true;
                logger.Info("AppStartCompleteCommand: Camera enabled - " + mainCam.gameObject.name);
            }
            else
            {
                logger.Error("AppStartCompleteCommand: No camera found after scene load!");
            }
        }
    }
}
