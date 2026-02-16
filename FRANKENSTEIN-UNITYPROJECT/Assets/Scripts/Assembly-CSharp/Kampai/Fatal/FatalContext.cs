namespace Kampai.Fatal
{
	public class FatalContext : global::Kampai.Util.BaseContext
	{
		public FatalContext()
		{
		}

		public FatalContext(global::UnityEngine.MonoBehaviour view, bool autoStartup)
			: base(view, autoStartup)
		{
		}

		protected override void MapBindings()
		{
			injectionBinder.Bind<global::Kampai.Game.EnvironmentState>().ToSingleton().CrossContext();
			
			// [FIX] LoadNewGameLevelSignal is already bound in BaseContext.cs line 384 - no need to bind again
		}
	}
}
