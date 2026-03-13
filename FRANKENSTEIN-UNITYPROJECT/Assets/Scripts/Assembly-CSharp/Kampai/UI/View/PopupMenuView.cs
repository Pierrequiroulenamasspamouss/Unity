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
			if (!isOpened)
			{
				isOpened = true;
				if (animator != null)
				{
					animator.Play("Open");
				}
				else
				{
					StartCoroutine(FallbackOpenAnimation());
				}
			}
		}

		private global::System.Collections.IEnumerator FallbackOpenAnimation()
		{
			float t = 0;
			global::UnityEngine.Vector3 targetScale = global::UnityEngine.Vector3.one;
			transform.localScale = global::UnityEngine.Vector3.zero;
			while (t < 1f)
			{
				t += global::UnityEngine.Time.deltaTime * 5f;
				transform.localScale = global::UnityEngine.Vector3.Lerp(global::UnityEngine.Vector3.zero, targetScale, t);
				yield return null;
			}
			transform.localScale = targetScale;
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
				StartCoroutine(FallbackCloseAnimation());
			}
		}

		private global::System.Collections.IEnumerator FallbackCloseAnimation()
		{
			float t = 0;
			global::UnityEngine.Vector3 startScale = transform.localScale;
			while (t < 1f)
			{
				t += global::UnityEngine.Time.deltaTime * 5f;
				transform.localScale = global::UnityEngine.Vector3.Lerp(startScale, global::UnityEngine.Vector3.zero, t);
				yield return null;
			}
			DestroyMenu();
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
