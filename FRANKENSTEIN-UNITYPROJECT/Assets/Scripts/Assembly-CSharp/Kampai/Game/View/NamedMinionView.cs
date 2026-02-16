namespace Kampai.Game.View
{
	public abstract class NamedMinionView : global::Kampai.Game.View.NamedCharacterObject, global::strange.extensions.mediation.api.IView
	{
		private bool _requiresContext = true;

		protected bool registerWithContext = true;

		public bool requiresContext
		{
			get
			{
				return _requiresContext;
			}
			set
			{
				_requiresContext = value;
			}
		}

		public bool registeredWithContext { get; set; }

		public virtual bool autoRegisterWithContext
		{
			get
			{
				return registerWithContext;
			}
			set
			{
				registerWithContext = value;
			}
		}

		protected void Awake()
		{
			if (autoRegisterWithContext && !registeredWithContext)
			{
				bubbleToContext(this, true, false);
			}
		}

		protected void Start()
		{
			if (autoRegisterWithContext && !registeredWithContext)
			{
				bubbleToContext(this, true, true);
			}
		}

		protected void OnDestroy()
		{
			bubbleToContext(this, false, false);
		}

		protected virtual void bubbleToContext(global::UnityEngine.MonoBehaviour view, bool toAdd, bool finalTry)
		{
			int num = 0;
			global::UnityEngine.Transform parent = view.gameObject.transform;
			while (parent.parent != null && num < 100)
			{
				num++;
				parent = parent.parent;
				global::UnityEngine.GameObject key = parent.gameObject;
				global::strange.extensions.context.api.IContext value;
				if (global::strange.extensions.context.impl.Context.knownContexts.TryGetValue(key, out value))
				{
					if (toAdd)
					{
						value.AddView(view);
						registeredWithContext = true;
					}
					else
					{
						value.RemoveView(view);
					}
					return;
				}
			}
			if (requiresContext && finalTry)
			{
				if (global::strange.extensions.context.impl.Context.firstContext == null)
				{
					string text = ((num != 100) ? "A view was added with no context. Views must be added into the hierarchy of their ContextView lest all hell break loose." : "A view couldn't find a context. Loop limit reached.");
					text = text + "\nView: " + view.ToString();
					throw new global::strange.extensions.mediation.impl.MediationException(text, global::strange.extensions.mediation.api.MediationExceptionType.NO_CONTEXT);
				}
				global::strange.extensions.context.impl.Context.firstContext.AddView(view);
				registeredWithContext = true;
			}
		}
	}
}
