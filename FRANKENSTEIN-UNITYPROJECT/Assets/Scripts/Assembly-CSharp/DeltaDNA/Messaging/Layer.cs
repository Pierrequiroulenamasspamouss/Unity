namespace DeltaDNA.Messaging
{
	internal class Layer : global::UnityEngine.MonoBehaviour
	{
		protected global::DeltaDNA.Messaging.IPopup _popup;

		protected global::System.Collections.Generic.List<global::System.Action> _actions = new global::System.Collections.Generic.List<global::System.Action>();

		protected int _depth;

		protected void RegisterAction()
		{
			_actions.Add(delegate
			{
			});
		}

		protected void RegisterAction(global::System.Collections.Generic.Dictionary<string, object> action, string id)
		{
			object valueObj;
			action.TryGetValue("value", out valueObj);
			object value;
			if (!action.TryGetValue("type", out value))
			{
				return;
			}
			global::DeltaDNA.Messaging.PopupEventArgs eventArgs = new global::DeltaDNA.Messaging.PopupEventArgs(id, (string)value, (string)valueObj);
			switch ((string)value)
			{
			case "none":
				_actions.Add(delegate
				{
				});
				break;
			case "action":
				_actions.Add(delegate
				{
					if (valueObj != null)
					{
						_popup.OnAction(eventArgs);
					}
					_popup.Close();
				});
				break;
			case "link":
				_actions.Add(delegate
				{
					_popup.OnAction(eventArgs);
					if (valueObj != null)
					{
						global::UnityEngine.Application.OpenURL((string)valueObj);
					}
					_popup.Close();
				});
				break;
			default:
				_actions.Add(delegate
				{
					_popup.OnDismiss(eventArgs);
					_popup.Close();
				});
				break;
			}
		}
	}
}
