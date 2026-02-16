namespace Kampai.UI.View
{
	public class LevelUpRewardView : global::strange.extensions.mediation.impl.View
	{
		public global::UnityEngine.UI.Text title;

		public global::UnityEngine.UI.Text level;

		public global::UnityEngine.RectTransform rewardScrollViewTransform;

		public global::UnityEngine.RectTransform unlockScrollViewTransform;

		public global::UnityEngine.RectTransform unlockPanel;

		public global::UnityEngine.Animator titleAnimator;

		public global::UnityEngine.Animator rewardsAnimator;

		public global::UnityEngine.Animator unlocksAnimator;

		public float initialDelay = 1.5f;

		public float animationDelay = 1.5f;

		private global::Kampai.Game.IDefinitionService definitionService;

		private global::Kampai.Main.ILocalizationService localService;

		private float padding = 10f;

		private int unlockCount;

		internal global::System.Collections.Generic.List<global::Kampai.UI.View.RewardSliderView> rewardViewList = new global::System.Collections.Generic.List<global::Kampai.UI.View.RewardSliderView>();

		internal global::strange.extensions.signal.impl.Signal StartUnlockMinionSignal = new global::strange.extensions.signal.impl.Signal();

		internal global::strange.extensions.signal.impl.Signal closeSignal = new global::strange.extensions.signal.impl.Signal();

		internal global::strange.extensions.signal.impl.Signal tweenSignal = new global::strange.extensions.signal.impl.Signal();

		internal void Init(global::Kampai.Game.IPlayerService playerService, global::Kampai.Game.IDefinitionService definitionService, global::Kampai.Main.ILocalizationService localization, global::Kampai.Main.PlayGlobalSoundFXSignal playSoundFX)
		{
			PlayAnimationSequence(playSoundFX);
			this.definitionService = definitionService;
			localService = localization;
			level.text = playerService.GetQuantity(global::Kampai.Game.StaticItem.LEVEL_ID).ToString();
		}

		internal void Display(global::System.Collections.Generic.List<global::Kampai.Game.View.RewardQuantity> quantityChange)
		{
			global::UnityEngine.GameObject gameObject = global::Kampai.Util.KampaiResources.Load("cmp_RewardSlider") as global::UnityEngine.GameObject;
			float x = (gameObject.transform as global::UnityEngine.RectTransform).sizeDelta.x;
			int num = 0;
			foreach (global::Kampai.Game.View.RewardQuantity item in quantityChange)
			{
				global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(gameObject) as global::UnityEngine.GameObject;
				global::Kampai.UI.View.RewardSliderView component = gameObject2.GetComponent<global::Kampai.UI.View.RewardSliderView>();
				global::UnityEngine.RectTransform rectTransform = gameObject2.transform as global::UnityEngine.RectTransform;
				global::Kampai.Game.DisplayableDefinition displayableDefinition;
				if (item.IsReward)
				{
					gameObject2.transform.SetParent(rewardScrollViewTransform, false);
					rectTransform.offsetMin = new global::UnityEngine.Vector2(x * (float)num + padding * (float)num, 0f);
					rectTransform.offsetMax = new global::UnityEngine.Vector2(x * (float)(num + 1) + padding * (float)num, 0f);
					displayableDefinition = definitionService.Get<global::Kampai.Game.ItemDefinition>(item.ID);
					if (item.ID == 5)
					{
						component.itemQuantity.gameObject.SetActive(true);
						component.itemQuantity.text = item.Quantity.ToString();
					}
					else
					{
						component.currencyQuantity.gameObject.SetActive(true);
						component.currencyQuantity.text = item.Quantity.ToString();
					}
					component.ID = item.ID;
					rewardViewList.Add(component);
					num++;
				}
				else
				{
					gameObject2.transform.SetParent(unlockScrollViewTransform, false);
					rectTransform.offsetMin = new global::UnityEngine.Vector2(x * (float)unlockCount + padding * (float)unlockCount, 0f);
					rectTransform.offsetMax = new global::UnityEngine.Vector2(x * (float)(unlockCount + 1) + padding * (float)unlockCount, 0f);
					global::Kampai.Game.UnlockDefinition unlockDefinition = definitionService.Get<global::Kampai.Game.UnlockDefinition>(item.ID);
					displayableDefinition = definitionService.Get<global::Kampai.Game.DisplayableDefinition>(unlockDefinition.ReferencedDefinitionID);
					unlockCount++;
				}
				component.description.text = getDisplayableString(displayableDefinition, item);
				component.icon.sprite = UIUtils.LoadSpriteFromPath(displayableDefinition.Image);
				component.icon.maskSprite = UIUtils.LoadSpriteFromPath(displayableDefinition.Mask);
			}
			SetScrollViewDimensions((int)x, num);
		}

		private void SetScrollViewDimensions(int width, int rewardCount)
		{
			int num = 3 * width;
			int num2 = rewardCount * (width + (int)padding);
			int num3 = unlockCount * (width + (int)padding);
			int num4 = 0;
			if (num2 > num)
			{
				num4 = (num2 - num) / 2;
			}
			rewardScrollViewTransform.sizeDelta = new global::UnityEngine.Vector2(num2, 0f);
			rewardScrollViewTransform.localPosition = new global::UnityEngine.Vector2(num4, rewardScrollViewTransform.localPosition.y);
			if (num3 > num)
			{
				num4 = (num3 - num) / 2;
			}
			unlockScrollViewTransform.sizeDelta = new global::UnityEngine.Vector2(num3, 0f);
			unlockScrollViewTransform.localPosition = new global::UnityEngine.Vector2(num4, unlockScrollViewTransform.localPosition.y);
		}

		private string getDisplayableString(global::Kampai.Game.DisplayableDefinition def, global::Kampai.Game.View.RewardQuantity qty)
		{
			string empty = string.Empty;
			if (def.ID == 5)
			{
				if (qty.Quantity > 1)
				{
					return localService.GetString("MinionsPluralUpper");
				}
				return localService.GetString("MinionsSingularUpper");
			}
			return localService.GetString(def.LocalizedKey);
		}

		private void PlayAnimationSequence(global::Kampai.Main.PlayGlobalSoundFXSignal playSoundFX)
		{
			titleAnimator.Play("Open");
			StartCoroutine(DisplayRewards(playSoundFX));
		}

		private global::System.Collections.IEnumerator DisplayRewards(global::Kampai.Main.PlayGlobalSoundFXSignal playSoundFX)
		{
			yield return new global::UnityEngine.WaitForSeconds(initialDelay);
			rewardsAnimator.Play("Open");
			playSoundFX.Dispatch("Play_UI_levelUp_rewards_01");
			yield return new global::UnityEngine.WaitForSeconds(animationDelay);
			unlockPanel.gameObject.SetActive(true);
			tweenSignal.Dispatch();
			rewardsAnimator.Play("Close");
			if (unlockCount == 0)
			{
				titleAnimator.Play("Close");
				playSoundFX.Dispatch("Play_UI_levelUp_last_01");
				StartUnlockMinionSignal.Dispatch();
				yield return new global::UnityEngine.WaitForSeconds(animationDelay);
				closeSignal.Dispatch();
				yield return true;
			}
			playSoundFX.Dispatch("Play_UI_levelUp_unlocks_01");
			unlocksAnimator.Play("Open");
			yield return new global::UnityEngine.WaitForSeconds(animationDelay);
			playSoundFX.Dispatch("Play_UI_levelUp_last_01");
			titleAnimator.Play("Close");
			unlocksAnimator.Play("Close");
			StartUnlockMinionSignal.Dispatch();
			yield return new global::UnityEngine.WaitForSeconds(animationDelay);
			closeSignal.Dispatch();
		}
	}
}
