namespace Kampai.Common
{
	public class LoggingTelemetryService : global::Kampai.Common.TelemetryService
	{
		[Inject]
		public global::Kampai.Common.ICoppaService coppaService { get; set; }

		public override void COPPACompliance()
		{
			base.COPPACompliance();
			string text = null;
			global::System.DateTime birthdate;
			if (!coppaService.GetBirthdate(out birthdate))
			{
				birthdate = global::System.DateTime.Now;
			}
			text = DateAsString(birthdate.Year, birthdate.Month);
			base.logger.Info("ageGateDob " + text);
		}

		public override void LogGameEvent(global::Kampai.Common.TelemetryEvent gameEvent)
		{
			base.LogGameEvent(gameEvent);
			if (gameEvent == null || !base.logger.IsAllowedLevel(global::Kampai.Util.Logger.Level.Info))
			{
				return;
			}
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
			stringBuilder.Append(gameEvent.Type.ToString());
			stringBuilder.Append(" - ");
			foreach (global::Kampai.Common.TelemetryParameter parameter in gameEvent.Parameters)
			{
				stringBuilder.Append(parameter.name).Append('(').Append(parameter.keyType)
					.Append(")=")
					.Append(parameter.value)
					.Append(", ");
			}
			base.logger.Info("Game event: {0}", stringBuilder.ToString());
		}

		protected string DateAsString(int year, int month)
		{
			return year + "-" + ((month >= 10) ? month.ToString() : ("0" + month));
		}
	}
}
