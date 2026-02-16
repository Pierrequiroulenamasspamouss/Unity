namespace Kampai.Game.View
{
	public class OrderBoardBuildingObjectView : global::Kampai.Game.View.BuildingObject, global::strange.extensions.mediation.api.IView
	{
		public global::Kampai.Game.OrderBoard orderBoard;

		private OrderBoardBuildingTicketsView ticketViews;

		private bool ticketsReady;

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

		internal override void Init(global::Kampai.Game.Building building, global::Kampai.Util.ILogger logger, global::System.Collections.Generic.IDictionary<string, global::UnityEngine.RuntimeAnimatorController> controllers, global::Kampai.Game.IDefinitionService definitionService)
		{
			base.Init(building, logger, controllers, definitionService);
			orderBoard = building as global::Kampai.Game.OrderBoard;
			ticketViews = base.gameObject.GetComponent<OrderBoardBuildingTicketsView>();
			ticketsReady = false;
		}

		internal void ClearBoard()
		{
			if (ticketViews == null)
			{
				return;
			}
			if (!ticketsReady)
			{
				ticketsReady = ticketViews.IsOrderboardSetupCorrectly();
				if (!ticketsReady)
				{
					logger.Error("Tickets are not assign on the prefab. Please make sure that they do.");
					return;
				}
			}
			ticketViews.DisableTickets();
		}

		public void ToggleHitbox(bool enable)
		{
			global::UnityEngine.Collider[] components = GetComponents<global::UnityEngine.Collider>();
			foreach (global::UnityEngine.Collider collider in components)
			{
				collider.enabled = enable;
			}
		}

		internal void SetTicketState(int ticketIndex, global::Kampai.Game.OrderBoardTicketState state)
		{
			if (ticketViews == null)
			{
				return;
			}
			if (!ticketsReady)
			{
				ticketsReady = ticketViews.IsOrderboardSetupCorrectly();
				if (!ticketsReady)
				{
					logger.Error("Tickets are not assign on the prefab. Please make sure that they do.");
					return;
				}
			}
			ticketViews.SetTicketState(ticketIndex, state);
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
