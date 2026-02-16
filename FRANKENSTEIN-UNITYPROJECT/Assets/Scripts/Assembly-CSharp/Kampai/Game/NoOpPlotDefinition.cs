namespace Kampai.Game
{
	internal sealed class NoOpPlotDefinition : global::Kampai.Game.PlotDefinition
	{
		public override global::Kampai.Game.Plot Instantiate()
		{
			return new global::Kampai.Game.NoOpPlot(this);
		}
	}
}
