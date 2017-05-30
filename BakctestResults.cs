using Newtonsoft.Json;

namespace LeanExperimentResultsParser
{
    public class BakctestResults
    {
        public BacktestStatistics BacktestStatistics { get; set; }
        public CustomStatistics CustomStatistics { get; set; }
    }

    public class CustomStatistics
    {
        [JsonProperty("ID")]
        public int ID { get; set; }
    }

    public class BacktestStatistics
    {
        [JsonProperty("Total Trades")]
        public int TotalTrades { get; set; }

        [JsonProperty("Average Win")]
        public string AverageWin { get; set; }

        [JsonProperty("Average Loss")]
        public string AverageLoss { get; set; }

        [JsonProperty("Compounding Annual Return")]
        public string CompoundingAnnualReturn { get; set; }

        [JsonProperty("Drawdown")]
        public string Drawdown { get; set; }

        [JsonProperty("Expectancy")]
        public double Expectancy { get; set; }

        [JsonProperty("Net Profit")]
        public string NetProfit { get; set; }

        [JsonProperty("Sharpe Ratio")]
        public double SharpeRatio { get; set; }

        [JsonProperty("Loss Rate")]
        public string LossRate { get; set; }

        [JsonProperty("Win Rate")]
        public string WinRate { get; set; }

        [JsonProperty("Profit-Loss Ratio")]
        public string ProfitLossRatio { get; set; }

        [JsonProperty("Alpha")]
        public double Alpha { get; set; }

        [JsonProperty("Beta")]
        public double Beta { get; set; }

        [JsonProperty("Annual Standard Deviation")]
        public double AnnualStandardDeviation { get; set; }

        [JsonProperty("Annual Variance")]
        public double AnnualVariance { get; set; }

        [JsonProperty("Information Ratio")]
        public double InformationRatio { get; set; }

        [JsonProperty("Tracking Error")]
        public double TrackingError { get; set; }

        [JsonProperty("Treynor Ratio")]
        public double TreynorRatio { get; set; }

        [JsonProperty("Total Fees")]
        public string TotalFees { get; set; }
    }
}