namespace DeltaDNA.Messaging
{
	public class Popup : global::DeltaDNA.Messaging.IPopup
	{
		private global::UnityEngine.GameObject _gameObject;

		private global::DeltaDNA.Messaging.SpriteMap _spritemap;

		private string _name = "Popup";

		private int _depth;

		public global::System.Collections.Generic.Dictionary<string, object> Configuration { get; private set; }

		public bool IsReady { get; private set; }

		public bool IsShowing { get; private set; }

		public event global::System.EventHandler BeforePrepare;

		public event global::System.EventHandler AfterPrepare;

		public event global::System.EventHandler BeforeShow;

		public event global::System.EventHandler BeforeClose;

		public event global::System.EventHandler AfterClose;

		public event global::System.EventHandler<global::DeltaDNA.Messaging.PopupEventArgs> Dismiss;

		public event global::System.EventHandler<global::DeltaDNA.Messaging.PopupEventArgs> Action;

		public Popup()
			: this(new global::System.Collections.Generic.Dictionary<string, object>())
		{
		}

		public Popup(global::System.Collections.Generic.Dictionary<string, object> options)
		{
			object value;
			if (options.TryGetValue("name", out value))
			{
				_name = (string)value;
			}
			object value2;
			if (options.TryGetValue("depth", out value2))
			{
				_depth = (int)value2;
			}
		}

		public void Prepare(global::System.Collections.Generic.Dictionary<string, object> configuration)
		{
			try
			{
				if (this.BeforePrepare != null)
				{
					this.BeforePrepare(this, new global::System.EventArgs());
				}
				_gameObject = new global::UnityEngine.GameObject(_name);
				global::DeltaDNA.Messaging.SpriteMap spriteMap = _gameObject.AddComponent<global::DeltaDNA.Messaging.SpriteMap>();
				spriteMap.Init(configuration);
				spriteMap.LoadResource(delegate
				{
					IsReady = true;
					if (this.AfterPrepare != null)
					{
						this.AfterPrepare(this, new global::System.EventArgs());
					}
				});
				_spritemap = spriteMap;
				Configuration = configuration;
			}
			catch (global::System.Exception ex)
			{
				global::DeltaDNA.Logger.LogError("Preparing popup configuration failed: " + ex.Message);
			}
		}

		public void Show()
		{
			if (!IsReady)
			{
				return;
			}
			try
			{
				if (this.BeforeShow != null)
				{
					this.BeforeShow(this, new global::System.EventArgs());
				}
				object value;
				if (Configuration.TryGetValue("shim", out value))
				{
					global::System.Collections.Generic.Dictionary<string, object> config = value as global::System.Collections.Generic.Dictionary<string, object>;
					global::DeltaDNA.Messaging.ShimLayer shimLayer = _gameObject.AddComponent<global::DeltaDNA.Messaging.ShimLayer>();
					shimLayer.Init(this, config, _depth);
				}
				object value2;
				if (Configuration.TryGetValue("layout", out value2))
				{
					global::System.Collections.Generic.Dictionary<string, object> dictionary = value2 as global::System.Collections.Generic.Dictionary<string, object>;
					object value3;
					if (dictionary.TryGetValue("landscape", out value3) || dictionary.TryGetValue("portrait", out value3))
					{
						global::System.Collections.Generic.Dictionary<string, object> dictionary2 = value3 as global::System.Collections.Generic.Dictionary<string, object>;
						global::DeltaDNA.Messaging.BackgroundLayer backgroundLayer = _gameObject.AddComponent<global::DeltaDNA.Messaging.BackgroundLayer>();
						backgroundLayer.Init(this, dictionary2, _spritemap.GetBackground(), _depth - 1);
						global::DeltaDNA.Messaging.ButtonsLayer buttonsLayer = _gameObject.AddComponent<global::DeltaDNA.Messaging.ButtonsLayer>();
						buttonsLayer.Init(this, dictionary2, _spritemap.GetButtons(), backgroundLayer, _depth - 2);
						IsShowing = true;
					}
					else
					{
						global::DeltaDNA.Logger.LogError("No layout orientation found.");
					}
				}
				else
				{
					global::DeltaDNA.Logger.LogError("No layout found.");
				}
			}
			catch (global::System.Exception ex)
			{
				global::DeltaDNA.Logger.LogError("Showing popup failed: " + ex.Message);
			}
		}

		public void Close()
		{
			if (IsShowing)
			{
				if (this.BeforeClose != null)
				{
					this.BeforeClose(this, new global::System.EventArgs());
				}
				global::DeltaDNA.Messaging.Layer[] components = _gameObject.GetComponents<global::DeltaDNA.Messaging.Layer>();
				foreach (global::DeltaDNA.Messaging.Layer obj in components)
				{
					global::UnityEngine.Object.Destroy(obj);
				}
				if (this.AfterClose != null)
				{
					this.AfterClose(this, new global::System.EventArgs());
				}
				IsShowing = false;
			}
		}

		public void OnDismiss(global::DeltaDNA.Messaging.PopupEventArgs eventArgs)
		{
			if (this.Dismiss != null)
			{
				this.Dismiss(this, eventArgs);
			}
		}

		public void OnAction(global::DeltaDNA.Messaging.PopupEventArgs eventArgs)
		{
			if (this.Action != null)
			{
				this.Action(this, eventArgs);
			}
		}
	}
}
