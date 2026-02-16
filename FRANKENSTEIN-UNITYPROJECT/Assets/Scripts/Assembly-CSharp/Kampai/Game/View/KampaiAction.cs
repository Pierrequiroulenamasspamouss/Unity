namespace Kampai.Game.View
{
	public abstract class KampaiAction
	{
		protected readonly global::Kampai.Util.ILogger logger;

		public bool Done { get; protected set; }

		public KampaiAction(global::Kampai.Util.ILogger logger)
		{
			this.logger = logger;
		}

		public virtual void Execute()
		{
		}

		public virtual void Abort()
		{
			Done = true;
		}

		public virtual void Update()
		{
		}

		public virtual void LateUpdate()
		{
		}
	}
}
