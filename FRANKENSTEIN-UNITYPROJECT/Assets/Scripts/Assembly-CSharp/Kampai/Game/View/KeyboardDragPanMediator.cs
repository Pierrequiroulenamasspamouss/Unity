namespace Kampai.Game.View
{
	public class KeyboardDragPanMediator : global::Kampai.Game.View.PanMediator
	{
		[Inject]
		public global::Kampai.Game.View.KeyboardDragPanView view { get; set; }

		public override void OnGameInput(global::UnityEngine.Vector3 position, int input)
		{
			if (!blocked)
			{
				if ((input & 1) != 0)
				{
					view.CalculateBehaviour(position);
				}
				else
				{
					view.ResetBehaviour();
				}
				view.PerformBehaviour(position);
			}
		}

		public override void OnDisableBehaviour(int behaviour)
		{
			int num = 8;
			if ((behaviour & num) == num)
			{
				if (!blocked)
				{
					blocked = true;
				}
				if ((base.model.CurrentBehaviours & num) == num)
				{
					base.model.CurrentBehaviours ^= num;
				}
			}
		}

		public override void OnEnableBehaviour(int behaviour)
		{
			int num = 8;
			if ((behaviour & num) == num)
			{
				if (blocked)
				{
					blocked = false;
				}
				if ((base.model.CurrentBehaviours & num) != num)
				{
					base.model.CurrentBehaviours ^= num;
				}
			}
		}
	}
}
