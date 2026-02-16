namespace Kampai.Game.View
{
	public class CraftableBuildingObject : global::Kampai.Game.View.AnimatingBuildingObject, global::Kampai.Game.View.IRequiresBuildingScaffolding
	{
		internal void SetWorking()
		{
			if (!IsInAnimatorState(GetHashAnimationState("Base Layer.Loop")))
			{
				if (IsInAnimatorState(GetHashAnimationState("Base Layer.Wait")))
				{
					EnqueueAction(new global::Kampai.Game.View.TriggerBuildingAnimationAction(this, OnlyStateEnabled("OnStop"), logger));
				}
				EnqueueAction(new global::Kampai.Game.View.TriggerBuildingAnimationAction(this, "OnLoop", logger));
			}
		}

		internal void SetWait()
		{
			if (IsInAnimatorState(GetHashAnimationState("Base Layer.Loop")))
			{
				EnqueueAction(new global::Kampai.Game.View.TriggerBuildingAnimationAction(this, OnlyStateEnabled("OnWait"), logger));
			}
		}

		internal void SetIdle()
		{
			if (IsInAnimatorState(GetHashAnimationState("Base Layer.Loop")) || IsInAnimatorState(GetHashAnimationState("Base Layer.Wait")))
			{
				EnqueueAction(new global::Kampai.Game.View.TriggerBuildingAnimationAction(this, OnlyStateEnabled("OnStop"), logger));
			}
		}

		public override void SetState(global::Kampai.Game.BuildingState newState)
		{
			base.SetState(newState);
			if (buildingState != newState)
			{
				buildingState = newState;
				switch (newState)
				{
				case global::Kampai.Game.BuildingState.Working:
				case global::Kampai.Game.BuildingState.HarvestableAndWorking:
					SetWorking();
					break;
				case global::Kampai.Game.BuildingState.Inactive:
				case global::Kampai.Game.BuildingState.Idle:
					SetIdle();
					break;
				case global::Kampai.Game.BuildingState.Harvestable:
					SetWait();
					break;
				}
			}
		}
	}
}
