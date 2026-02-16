namespace Kampai.Common
{
	public class StartAndroidBackButtonCommand : global::strange.extensions.command.impl.Command
	{
		[Inject(global::strange.extensions.context.api.ContextKeys.CONTEXT_DISPATCHER)]
		public global::strange.extensions.dispatcher.eventdispatcher.api.IEventDispatcher dispatcher { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		[Inject]
		public global::Kampai.Common.AndroidBackButtonSignal androidBackButtonSignal { get; set; }

		public override void Execute()
		{
			routineRunner.StartCoroutine(Update());
		}

		private global::System.Collections.IEnumerator Update()
		{
			while (true)
			{
				if (global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.Escape))
				{
					androidBackButtonSignal.Dispatch();
				}
				yield return null;
			}
		}
	}
}
