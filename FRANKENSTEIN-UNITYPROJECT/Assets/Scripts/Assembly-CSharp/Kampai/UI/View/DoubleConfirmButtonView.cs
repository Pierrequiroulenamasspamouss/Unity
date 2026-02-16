namespace Kampai.UI.View
{
	public class DoubleConfirmButtonView : global::Kampai.UI.View.ButtonView, global::UnityEngine.EventSystems.IEventSystemHandler, global::UnityEngine.EventSystems.IPointerDownHandler, global::UnityEngine.EventSystems.IPointerUpHandler, global::Kampai.UI.View.IDoubleConfirmHandler
	{
		protected bool isInConfirmState;

		protected global::System.Collections.IEnumerator waitEnumerator;

		protected int tapCount;

		protected bool doubleTapConfirm = true;

		public global::UnityEngine.Animator animator;

		[Inject]
		public global::Kampai.Main.ILocalizationService localService { get; set; }

		[Inject]
		public ILocalPersistanceService localPersistService { get; set; }

		protected override void Start()
		{
			base.Start();
			animator = base.gameObject.GetComponent<global::UnityEngine.Animator>();
		}

		public override void OnClickEvent()
		{
			if (PlaySoundOnClick)
			{
				base.playSFXSignal.Dispatch("Play_button_click_01");
			}
			updateTapCount();
			if (!isDoubleConfirmed())
			{
				ShowConfirmMessage();
			}
			ClickedSignal.Dispatch();
		}

		public virtual void ResetTapState()
		{
			tapCount = 0;
			if (animator != null)
			{
				animator.SetBool("Pressed_Confirm", false);
			}
			isInConfirmState = false;
		}

		public virtual void updateTapCount()
		{
			if (tapCount < 2)
			{
				tapCount++;
			}
			else
			{
				tapCount = 1;
			}
		}

		public virtual void DisableDoubleConfirm()
		{
			doubleTapConfirm = false;
		}

		public virtual void EnableDoubleConfirm()
		{
			doubleTapConfirm = true;
		}

		public virtual void ShowConfirmMessage()
		{
			bool flag = doubleTapConfirm && localPersistService.GetDataIntPlayer("DoublePurchaseConfirm") != 0;
			if (animator != null && flag)
			{
				animator.SetBool("Pressed_Confirm", flag);
				isInConfirmState = flag;
				if (waitEnumerator != null)
				{
					StopCoroutine(waitEnumerator);
				}
				waitEnumerator = Wait();
				StartCoroutine(Wait());
			}
		}

		public virtual bool isDoubleConfirmed()
		{
			if (!doubleTapConfirm)
			{
				return true;
			}
			if (localPersistService.GetDataIntPlayer("DoublePurchaseConfirm") != 0)
			{
				return tapCount == 2;
			}
			return true;
		}

		public virtual void ResetAnim()
		{
			if (animator != null)
			{
				animator.Play("Normal", 0, 0f);
			}
		}

		private global::System.Collections.IEnumerator Wait()
		{
			yield return new global::UnityEngine.WaitForSeconds(2.5f);
			if (waitEnumerator != null)
			{
				ResetTapState();
				waitEnumerator = null;
			}
		}

		public virtual void OnPointerDown(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			if (waitEnumerator != null)
			{
				StopCoroutine(waitEnumerator);
				waitEnumerator = null;
			}
		}

		public virtual void OnPointerUp(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			if (isInConfirmState && waitEnumerator == null)
			{
				waitEnumerator = Wait();
				StartCoroutine(waitEnumerator);
			}
		}
	}
}
