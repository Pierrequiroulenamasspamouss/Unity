public class LoadDefinitionsCommand : global::strange.extensions.command.impl.Command
{
	public class LoadDefinitionsData
	{
		public string Path { get; set; }

		public string Json { get; set; }
	}

	private string path;

	[Inject]
	public LoadDefinitionsCommand.LoadDefinitionsData defData { get; set; }

	[Inject]
	public global::Kampai.Util.ILogger logger { get; set; }

	[Inject]
	public global::Kampai.Game.IDefinitionService service { get; set; }

	[Inject]
	public global::Kampai.Game.DefinitionsChangedSignal definitionsChangedSignal { get; set; }

	[Inject]
	public global::Kampai.Common.ITelemetryService telemetryService { get; set; }

	[Inject]
	public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

	[Inject]
	public global::Kampai.Util.IInvokerService invokerService { get; set; }

	[Inject]
	public global::Kampai.Game.IPlayerService playerService { get; set; }

	[Inject]
	public global::Kampai.Splash.SplashProgressUpdateSignal splashProgressUpdateSignal { get; set; }

	public override void Execute()
	{
		logger.EventStart("LoadDefinitionsCommand.Execute");
		path = FetchDefinitionsCommand.GetDefinitionsPath();
		string json = defData.Json;
		if (json == null)
		{
			try
			{
				logger.Debug("LoadDefinitionsCommand:Execute reading from path " + path);
				json = global::System.IO.File.ReadAllText(path);
			}
			catch (global::System.IO.IOException ex)
			{
				logger.Log(global::Kampai.Util.Logger.Level.Error, "Unable to read {0} : {1}", path, ex.Message);
			}
		}
		telemetryService.Send_Telemetry_EVT_USER_GAME_LOAD_FUNNEL("80 - Loaded Definitions", playerService.SWRVEGroup);
		if (json == null)
		{
			logger.FatalNoThrow(global::Kampai.Util.FatalCode.DS_UNABLE_TO_LOAD);
		}
		else
		{
			logger.Debug("LoadDefinitions: Starting deserialization");
			routineRunner.StartAsyncConditionTask(() => DeserializeDefinitions(json), delegate
			{
				logger.Debug("LoadDefinitions: Deserialized successfully");
                //global::Kampai.Common.TelemetryService telemetryService = this.telemetryService as global::Kampai.Common.TelemetryService;
                //if (telemetryService != null)
                //{
                //	telemetryService.SetDefinitionServiceReference(service);
                //}
                var concreteTelemetryService =
    this.telemetryService as global::Kampai.Common.TelemetryService;

                if (concreteTelemetryService != null)
                {
                    concreteTelemetryService.SetDefinitionServiceReference(service);
                }

                definitionsChangedSignal.Dispatch();
				splashProgressUpdateSignal.Dispatch(35, 10f);
			});
		}
		logger.EventStop("LoadDefinitionsCommand.Execute");
	}

    private bool DeserializeDefinitions(string json)
    {
        try
        {
            service.Deserialize(json);
            return true;
        }
        catch (global::Newtonsoft.Json.JsonReaderException jsonEx)
        {
            logger.Error("=== JSON PARSE ERROR ===");
            logger.Error("Error at Line {0}, Position {1}: {2}",
                jsonEx.LineNumber, jsonEx.LinePosition, jsonEx.Message);

            // Split into lines and show context
            string[] lines = json.Split(new[] { '\r', '\n' }, global::System.StringSplitOptions.None);
            logger.Error("Total lines in JSON: {0}", lines.Length);

            if (jsonEx.LineNumber > 0 && jsonEx.LineNumber <= lines.Length)
            {
                int lineIndex = jsonEx.LineNumber - 1;
                int startLine = global::System.Math.Max(0, lineIndex - 5);
                int endLine = global::System.Math.Min(lines.Length - 1, lineIndex + 5);

                logger.Error("=== Context (lines {0} to {1}) ===", startLine + 1, endLine + 1);
                for (int i = startLine; i <= endLine; i++)
                {
                    string marker = (i == lineIndex) ? ">>>" : "   ";
                    logger.Error("{0} {1,5}: {2}", marker, i + 1, lines[i]);
                }

                // Show the exact position with a pointer
                if (jsonEx.LinePosition > 0 && lineIndex < lines.Length)
                {
                    string problematicLine = lines[lineIndex];
                    if (jsonEx.LinePosition <= problematicLine.Length)
                    {
                        string pointer = new string(' ', jsonEx.LinePosition + 10) + "^";
                        logger.Error("{0}", pointer);

                        char problemChar = problematicLine[jsonEx.LinePosition - 1];
                        logger.Error("Problem character: '{0}' (ASCII: {1}, Unicode: U+{2:X4})",
                            problemChar, (int)problemChar, (int)problemChar);

                        // Show characters around the problem
                        int contextStart = global::System.Math.Max(0, jsonEx.LinePosition - 20);
                        int contextEnd = global::System.Math.Min(problematicLine.Length, jsonEx.LinePosition + 20);
                        logger.Error("Context: '{0}'", problematicLine.Substring(contextStart, contextEnd - contextStart));
                    }
                }
            }

            // Also save to file for manual inspection
            string debugPath = global::System.IO.Path.Combine(
                global::UnityEngine.Application.persistentDataPath,
                "failed_json_" + global::System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt"
            );

            try
            {
                global::System.IO.File.WriteAllText(debugPath, json);
                logger.Error("Full JSON saved to: {0}", debugPath);
            }
            catch { }

            invokerService.Add(delegate
            {
                logger.FatalNoThrow(global::Kampai.Util.FatalCode.DS_PARSE_ERROR, 0,
                    "JSON Parse Error at Line {0}, Pos {1}: {2}",
                    jsonEx.LineNumber, jsonEx.LinePosition, jsonEx.Message);
            });
        }
        catch (global::Kampai.Util.FatalException ex)
        {
            logger.Error("Can't deserialize: {0}", ex);
            invokerService.Add(delegate
            {
                logger.FatalNoThrow(ex.FatalCode, ex.ReferencedId,
                    "Message: {0}, Reason: {1}", ex.Message, ex.InnerException ?? ex);
            });
        }
        catch (global::System.Exception ex)
        {
            logger.Error("Can't deserialize: {0}", ex);
            invokerService.Add(delegate
            {
                logger.FatalNoThrow(global::Kampai.Util.FatalCode.DS_PARSE_ERROR, 0, "Reason: {0}", ex);
            });
        }
        return false;
    }
}
