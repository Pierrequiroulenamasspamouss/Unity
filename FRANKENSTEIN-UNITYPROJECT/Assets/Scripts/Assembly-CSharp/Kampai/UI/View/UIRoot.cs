namespace Kampai.UI.View
{
	public class UIRoot : global::strange.extensions.context.impl.ContextView
	{
		private void Awake()
		{
			context = new global::Kampai.UI.View.UIContext(this, false); // autoStartup = false
			StartCoroutine(WaitForGameContextThenStart());
		}

		private global::System.Collections.IEnumerator WaitForGameContextThenStart()
	{
		// Wait for GameContext to finish initializing
		// GameContext binds itself with name GameElement.CONTEXT
		while (global::strange.extensions.context.impl.Context.firstContext == null)
		{
			yield return null;
		}

		// Wait for GameContext to bind CONTEXT
		global::strange.extensions.context.impl.MVCSContext gameContext = global::strange.extensions.context.impl.Context.firstContext as global::strange.extensions.context.impl.MVCSContext;
		while (gameContext == null || gameContext.injectionBinder.GetBinding<global::strange.extensions.context.api.ICrossContextCapable>(global::Kampai.Game.GameElement.CONTEXT) == null)
		{
			yield return null;
			gameContext = global::strange.extensions.context.impl.Context.firstContext as global::strange.extensions.context.impl.MVCSContext;
		}

		// Now GameContext is ready, start UIContext
		context.Start();
	}
	}
}
