namespace Kampai.UI.View
{
	public class UIModel
	{
		private struct UIStackElement
		{
			public int id;

			public global::System.Action callback;
		}

		private global::System.Collections.Generic.List<global::Kampai.UI.View.UIModel.UIStackElement> objectStack = new global::System.Collections.Generic.List<global::Kampai.UI.View.UIModel.UIStackElement>();

		public bool UIOpen
		{
			get
			{
				return objectStack.Count > 0;
			}
		}

		public bool AllowMultiTouch { get; set; }

		private void CheckAllowMultitouch()
		{
			global::UnityEngine.Input.multiTouchEnabled = !UIOpen || AllowMultiTouch;
		}

		public void AddUI(int id, global::System.Action callback)
		{
			int num = objectStack.FindIndex((global::Kampai.UI.View.UIModel.UIStackElement x) => x.id == id);
			if (num != -1)
			{
				objectStack.RemoveAt(num);
			}
			global::Kampai.UI.View.UIModel.UIStackElement item = new global::Kampai.UI.View.UIModel.UIStackElement
			{
				id = id,
				callback = callback
			};
			objectStack.Insert(0, item);
			CheckAllowMultitouch();
		}

		public void RemoveUI(int id)
		{
			int num = objectStack.FindIndex((global::Kampai.UI.View.UIModel.UIStackElement x) => x.id == id);
			if (num != -1)
			{
				objectStack.RemoveAt(num);
			}
			CheckAllowMultitouch();
		}

		public global::System.Action RemoveTopUI()
		{
			if (objectStack.Count > 0)
			{
				global::System.Action callback = objectStack[0].callback;
				objectStack.RemoveAt(0);
				CheckAllowMultitouch();
				return callback;
			}
			return null;
		}
	}
}
