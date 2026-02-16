namespace Kampai.Util
{
	public interface IMinionBuilder
	{
		global::Kampai.Game.View.MinionObject BuildMinion(string baseModelPath, string animatorStateMachine, global::UnityEngine.GameObject parent = null);

		void SetLOD(global::Kampai.Util.TargetPerformance targetPerformance);

		global::Kampai.Util.TargetPerformance GetLOD();
	}
}
