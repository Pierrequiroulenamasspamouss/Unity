using UnityEngine;
using System.Collections;

namespace Kampai.Game
{
    /// <summary>
    /// ContextView that bootstraps the GameContext when the Game scene loads.
    /// 
    /// CRITICAL: GameContext must be created AFTER MainContext (firstContext).
    /// MainContext binds CrossContext services (LoadPlayerSignal, IUserSessionService, etc.)
    /// that GameContext's commands need during Launch(). If GameContext is created first,
    /// it becomes firstContext (wrong!) and MainContext's bindings aren't available yet.
    /// 
    /// Original game flow: Initialize scene loads first → MainRoot.Start() creates MainContext,
    /// then Game scene is loaded additively → GameContextView creates GameContext as a child.
    /// </summary>
    public class GameContextView : strange.extensions.context.impl.ContextView
    {
        void Start()
        {
            Debug.Log("[GameContextView] ========== Start() ==========");
            StartCoroutine(WaitForMainContextAndInit());
        }

        private IEnumerator WaitForMainContextAndInit()
        {
            // Wait until MainContext has been created and set as firstContext.
            // MainRoot.Start() creates MainContext — Start() order is indeterminate,
            // so we must wait until firstContext exists and is NOT us.
            int waitFrames = 0;

            while (true)
            {
                if (strange.extensions.context.impl.Context.firstContext != null)
                {
                    // Check if IPlayerService is bound in the CrossContextBinder.
                    // This confirms MainContext has run its MapBindings() and populated the shared binder.
                    // Cast to ICrossContextCapable to access injectionBinder (IContext interface doesn't expose it).
                    var crossAndCapable = strange.extensions.context.impl.Context.firstContext as global::strange.extensions.context.api.ICrossContextCapable;
                    if (crossAndCapable != null && crossAndCapable.injectionBinder != null)
                    {
                        // key: Kampai.Game.IPlayerService
                        // Use GetBinding instead of GetInstance to avoid InjectionException if not found yet.
                        var playerServiceBinding = crossAndCapable.injectionBinder.GetBinding<global::Kampai.Game.IPlayerService>();
                        if (playerServiceBinding != null)
                        {
                            break;
                        }
                    }
                }

                waitFrames++;
                if (waitFrames % 60 == 0 && strange.extensions.context.impl.Context.firstContext != null)
                {
                    Debug.Log("[GameContextView] Waiting... firstContext is: " + strange.extensions.context.impl.Context.firstContext.GetType().Name);
                }

                if (waitFrames > 600) // 10 seconds at 60fps (increased timeout)
                {
                    Debug.LogError("[GameContextView] Timed out waiting for MainContext (IPlayerService)!");
                    if (strange.extensions.context.impl.Context.firstContext != null)
                         Debug.LogError("[GameContextView] firstContext was: " + strange.extensions.context.impl.Context.firstContext.GetType().Name);
                    yield break;
                }
                yield return null;
            }
            
            Debug.Log("[GameContextView] firstContext exists: " + strange.extensions.context.impl.Context.firstContext.GetType().Name);
            
            try
            {
                // Create GameContext as a CHILD context (firstContext = MainContext already exists).
                // autoStartup = false in constructor, we set it true before Start() so Launch() runs.
                context = new GameContext(this, false);
                Debug.Log("[GameContextView] GameContext created successfully! (child of " 
                    + strange.extensions.context.impl.Context.firstContext.GetType().Name + ")");
                
                // Enable autoStartup so Context.Start() calls Launch() at the end,
                // which triggers BaseContext.Launch() → StartSignal.Dispatch().
                ((strange.extensions.context.impl.Context)context).autoStartup = true;
                context.Start();
                Debug.Log("[GameContextView] context.Start() completed!");
            }
            catch (System.Exception ex)
            {
                Debug.LogError("[GameContextView] EXCEPTION: " + ex);
            }
        }
    }
}
