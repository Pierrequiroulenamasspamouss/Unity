namespace Kampai.Game.View.Audio
{
	public class PositionalAudioListenerView : global::strange.extensions.mediation.impl.View
	{
		public void UpdatePosition(global::UnityEngine.Vector3 newPosition)
		{
			base.transform.position = newPosition;
		}
	}
}
