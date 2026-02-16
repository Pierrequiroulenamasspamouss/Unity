namespace Kampai.UI.View
{
	public class BuildMenuView : global::strange.extensions.mediation.impl.View
	{
		public global::Kampai.UI.View.ButtonView MenuButton;

		public global::UnityEngine.RectTransform BackGround;

		public global::UnityEngine.GameObject Root;

		public global::Kampai.UI.View.TabMenuView TabMenu;

		public global::Kampai.UI.View.StoreBadgeView StoreBadge;

		public global::UnityEngine.RectTransform Backing;

		public global::UnityEngine.RectTransform BackingGlow;

		internal bool isOpen;

		private global::UnityEngine.Animator animator;

		internal void Init()
		{
			animator = GetComponent<global::UnityEngine.Animator>();
			MenuButton.PlaySoundOnClick = false;
		}

		public void MoveMenu()
		{
			MoveMenu(!isOpen);
		}

		internal void MoveMenu(bool show)
		{
			animator.SetBool("OnOpen", show);
			isOpen = show;
			ToggleBadgeCounterVisibility(isOpen);
		}

		internal void IncreaseBadgeCounter()
		{
			StoreBadge.IncreaseInventoryCounter();
		}

		internal void ToggleBadgeCounterVisibility(bool isHide)
		{
			StoreBadge.ToggleBadgeCounterVisibility(isHide);
		}

		internal void RemoveUnlockBadge(int count)
		{
			StoreBadge.RemoveUnlockBadge(count);
		}

		internal void SetUnlockBadge(int count)
		{
			StoreBadge.SetNewUnlockCounter(count);
		}

		internal void SetInventoryCount(int count)
		{
			StoreBadge.SetInventoryCount(count);
		}

		internal void Show()
		{
			animator.SetBool("OnHide", false);
		}

		internal void Hide()
		{
			animator.SetBool("OnHide", true);
		}

		public void SetBuildMenuButtonEnabled(bool isEnabled)
		{
			if (MenuButton != null && Backing != null && BackingGlow != null)
			{
				MenuButton.gameObject.SetActive(isEnabled);
				Backing.gameObject.SetActive(isEnabled);
				BackingGlow.gameObject.SetActive(isEnabled);
			}
		}
	}
}
