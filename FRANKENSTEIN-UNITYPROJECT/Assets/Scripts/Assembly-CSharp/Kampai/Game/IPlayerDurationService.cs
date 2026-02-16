namespace Kampai.Game
{
	public interface IPlayerDurationService
	{
		int TotalSecondsSinceLevelUp { get; }

		int GameplaySecondsSinceLevelUp { get; }

		void InitLevelUpUTC();

		void MarkLevelUpUTC();

		void SetLastGameStartUTC();

		void UpdateGameplayDuration();
	}
}
