namespace Kampai.UI.View
{
	public class KampaiScrollView : global::strange.extensions.mediation.impl.View, global::System.Collections.IEnumerable, global::System.Collections.Generic.IEnumerable<global::strange.extensions.mediation.impl.View>
	{
		public enum MoveDirection
		{
			Top = 0,
			Bottom = 1,
			LastLocation = 2
		}

		[global::UnityEngine.SerializeField]
		private float m_colunmNumber = 1f;

		[global::UnityEngine.SerializeField]
		private float m_rowNumber = 1f;

		public global::UnityEngine.UI.ScrollRect ScrollRect;

		public global::UnityEngine.RectTransform ItemContainer;

		public global::System.Collections.Generic.IList<global::strange.extensions.mediation.impl.View> ItemViewList = new global::System.Collections.Generic.List<global::strange.extensions.mediation.impl.View>();

		public float ColumnNumber
		{
			get
			{
				return m_colunmNumber;
			}
			set
			{
				m_colunmNumber = value;
			}
		}

		public float RowNumber
		{
			get
			{
				return m_rowNumber;
			}
			set
			{
				m_rowNumber = value;
			}
		}

		public global::strange.extensions.mediation.impl.View this[int index]
		{
			get
			{
				return ItemViewList[index];
			}
			set
			{
				ItemViewList[index] = value;
			}
		}

		public float ItemHeight { get; private set; }

		global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		internal void SetupScrollView(float columns = -1f, global::Kampai.UI.View.KampaiScrollView.MoveDirection moveDirection = global::Kampai.UI.View.KampaiScrollView.MoveDirection.LastLocation)
		{
			if (columns == -1f)
			{
				columns = m_colunmNumber;
			}
			if (ItemViewList == null)
			{
				return;
			}
			float y = ItemContainer.anchoredPosition.y;
			int count = ItemViewList.Count;
			ScrollRect.verticalNormalizedPosition = 1f;
			int num = count % global::UnityEngine.Mathf.FloorToInt(columns);
			float num2 = ItemHeight * (float)(count / global::UnityEngine.Mathf.FloorToInt(columns) + ((num != 0) ? 1 : 0));
			ItemContainer.offsetMin = new global::UnityEngine.Vector2(0f, 0f - num2);
			ItemContainer.offsetMax = new global::UnityEngine.Vector2(0f, 0f);
			if ((float)count <= columns * m_rowNumber)
			{
				ScrollRect.movementType = global::UnityEngine.UI.ScrollRect.MovementType.Clamped;
				moveDirection = global::Kampai.UI.View.KampaiScrollView.MoveDirection.Top;
			}
			else
			{
				ScrollRect.movementType = global::UnityEngine.UI.ScrollRect.MovementType.Elastic;
			}
			switch (moveDirection)
			{
			case global::Kampai.UI.View.KampaiScrollView.MoveDirection.Top:
				ItemContainer.anchoredPosition = new global::UnityEngine.Vector2(ItemContainer.anchoredPosition.x, 0f);
				break;
			case global::Kampai.UI.View.KampaiScrollView.MoveDirection.Bottom:
				ItemContainer.anchoredPosition = new global::UnityEngine.Vector2(ItemContainer.anchoredPosition.x, y);
				if (num != 0)
				{
					TweenToPosition(new global::UnityEngine.Vector2(0f, 0f), 1f);
				}
				break;
			case global::Kampai.UI.View.KampaiScrollView.MoveDirection.LastLocation:
				ItemContainer.anchoredPosition = new global::UnityEngine.Vector2(ItemContainer.anchoredPosition.x, y);
				break;
			}
		}

		internal global::UnityEngine.Vector3 GetViewSize()
		{
			global::UnityEngine.RectTransform rectTransform = ScrollRect.transform as global::UnityEngine.RectTransform;
			return (!(rectTransform == null)) ? new global::UnityEngine.Vector3(rectTransform.rect.width / m_colunmNumber, rectTransform.rect.height / m_rowNumber) : global::UnityEngine.Vector3.zero;
		}

		private global::UnityEngine.RectTransform PositionItem(global::strange.extensions.mediation.impl.View view, int index)
		{
			global::UnityEngine.RectTransform rectTransform = view.transform as global::UnityEngine.RectTransform;
			if (rectTransform == null)
			{
				return null;
			}
			float num = 1f;
			int num2 = global::UnityEngine.Mathf.FloorToInt(m_colunmNumber);
			int num3 = index % num2;
			int num4 = index / num2;
			rectTransform.sizeDelta = GetViewSize();
			ItemHeight = rectTransform.sizeDelta.y;
			rectTransform.SetParent(ItemContainer, false);
			rectTransform.offsetMin = new global::UnityEngine.Vector2(0f, (float)(-num4 - 1) * ItemHeight);
			rectTransform.offsetMax = new global::UnityEngine.Vector2(0f, (float)(-num4) * ItemHeight);
			rectTransform.anchorMin = new global::UnityEngine.Vector2(num / m_colunmNumber * (float)num3, 1f);
			rectTransform.anchorMax = new global::UnityEngine.Vector2(num / m_colunmNumber * (float)(num3 + 1), 1f);
			rectTransform.localScale = global::UnityEngine.Vector3.one;
			return rectTransform;
		}

		public void ClearItems()
		{
			foreach (global::strange.extensions.mediation.impl.View itemView in ItemViewList)
			{
				global::UnityEngine.Object.Destroy(itemView.gameObject);
			}
			ItemViewList.Clear();
		}

		public void AddList<T>(global::System.Collections.Generic.IList<T> items, global::System.Func<int, T, global::strange.extensions.mediation.impl.View> createItemFunc, global::System.Func<T, bool> hasItemFunc = null, bool setupScrollAfter = true) where T : global::Kampai.Game.Instance
		{
			if (createItemFunc == null)
			{
				SetupScrollView();
				return;
			}
			foreach (T item in items)
			{
				if (hasItemFunc == null || hasItemFunc(item))
				{
					global::strange.extensions.mediation.impl.View slotView = createItemFunc(ItemViewList.Count, item);
					AddItem(slotView);
				}
			}
			if (setupScrollAfter)
			{
				SetupScrollView();
			}
		}

		public void AddItem(global::strange.extensions.mediation.impl.View slotView)
		{
			if (!(slotView == null))
			{
				PositionItem(slotView, ItemViewList.Count);
				ItemViewList.Add(slotView);
			}
		}

		public void TweenToPosition(global::UnityEngine.Vector2 newPosition, float tweenTime)
		{
			if (tweenTime > 0f)
			{
				GoTweenConfig config = new GoTweenConfig().vector2Prop("normalizedPosition", newPosition).setEaseType(GoEaseType.SineOut);
				GoTween tween = new GoTween(ScrollRect, tweenTime, config);
				Go.addTween(tween);
			}
			else
			{
				ScrollRect.normalizedPosition = newPosition;
			}
		}

		public void EnableScrolling(bool horizontal, bool vertial)
		{
			ScrollRect.horizontal = horizontal;
			ScrollRect.vertical = vertial;
		}

		public global::System.Collections.Generic.IEnumerator<global::strange.extensions.mediation.impl.View> GetEnumerator()
		{
			return ItemViewList.GetEnumerator();
		}
	}
}
