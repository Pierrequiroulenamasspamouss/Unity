namespace Kampai.UI.View
{
	public class PopupMenuView : global::strange.extensions.mediation.impl.View
	{
		public global::strange.extensions.signal.impl.Signal OnMenuClose = new global::strange.extensions.signal.impl.Signal();

		internal global::UnityEngine.Animator animator;

		protected bool isOpened;

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		public virtual void Init()
		{
			animator = GetComponent<global::UnityEngine.Animator>();
			isOpened = false;
			if (animator == null)
			{
				logger.Log(global::Kampai.Util.Logger.Level.Error, "PopupMenuView has a NULL animator on Init!!!");
			}
		}

		internal void Open()
		{
			if (!isOpened && animator != null)
			{
				animator.Play("Open");
				isOpened = true;
			}
		}

		internal void Close(bool instant = false)
		{
			if (isOpened)
			{
				isOpened = false;
				if (instant)
				{
					DestroyMenu();
					return;
				}
				if (animator != null)
				{
					animator.Play("Close");
					return;
				}
				logger.Log(global::Kampai.Util.Logger.Level.Error, "PopupMenuView has a NULL animator on Close!!!");
				DestroyMenu();
			}
		}

		public void DestroyMenu()
		{
			OnMenuClose.Dispatch();
			StopAllCoroutines();
		}

		public bool IsAnimationPlaying(string animationState)
		{
			global::UnityEngine.AnimatorStateInfo currentAnimatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
			if (currentAnimatorStateInfo.IsName(animationState) && currentAnimatorStateInfo.normalizedTime < 1f)
			{
				return true;
			}
			if (currentAnimatorStateInfo.loop || currentAnimatorStateInfo.normalizedTime < 1f)
			{
				return true;
			}
			return false;
		}

		public void AnimationDoneCallback(string animationState, global::System.Action callback)
		{
			StartCoroutine(CheckAnimationComplete(animationState, callback));
		}

		internal global::System.Collections.IEnumerator CheckAnimationComplete(string animationState, global::System.Action callback)
		{
			yield return null;
			while (IsAnimationPlaying(animationState))
			{
				yield return null;
			}
			callback();
		}
	}
}
