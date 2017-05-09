using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace LeanExperimentResultsParser
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string experimentFolder;

            if (args.Length > 0)
            {
                experimentFolder = args[0];
            }
            else
            {
                experimentFolder = @"C:\Users\jjd\Desktop\LeanExperiment_2017-05-08_0552";
            }
            GenerateCSVFromBakctestsResults(experimentFolder);
        }

        private static void GenerateCSVFromBakctestsResults(string experimentFolder)
        {
            var isFirst = true;
            var csvContent = new StringBuilder();
            // Search for all JSON with backtest results and make CSV
            var backtestResultsFiles = Directory.GetFiles(experimentFolder, "BacktestResults_*.json",
                SearchOption.AllDirectories);
            foreach (var backtestResultFile in backtestResultsFiles)
            {
                var backtestResult = new BakctestResults();
                try
                {
                    // Parse JSON with backtest and training results.
                    backtestResult =
                        JsonConvert.DeserializeObject<BakctestResults>(File.ReadAllText(backtestResultFile));
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error with experiment {0}", backtestResultFile);
                    Console.ResetColor();
                    Console.WriteLine(ex.StackTrace);

                    continue;
                }

                // Parse genome in order to get generation.
                var flagsStart = backtestResultFile.IndexOf("Momentum_", StringComparison.Ordinal) + 9;
                var flagslenght = backtestResultFile.IndexOf(".json", StringComparison.Ordinal) - flagsStart;
                string[] flags;

                try
                {
                    flags = backtestResultFile.Substring(flagsStart, flagslenght).Split('_');
                }
                catch (Exception)
                {
                    throw new NotImplementedException("Error with the file name " + backtestResultFile);
                }

                // If is first time, make headers.
                if (isFirst)
                {
                    foreach (var subClass in typeof(BakctestResults).GetProperties())
                    {
                        var classInstance = subClass.GetValue(backtestResult);
                        foreach (var item in classInstance.GetType().GetProperties())
                        {
                            csvContent.Append(string.Format("{0},", item.Name));
                            /*
                             * In case you want to add CustomStatistics to the BacktestResults, you can use:
                             * csvContent.Append(string.Format("{0}{1},", subClass.Name, item.Name));
                             */
                        }
                    }
                    csvContent.Append("Broker,MaxExposure,Leverage,InitialCash,PairsToTrade\n");
                    isFirst = false;
                }

                foreach (var subClass in typeof(BakctestResults).GetProperties())
                {
                    var classInstance = subClass.GetValue(backtestResult);
                    foreach (var item in classInstance.GetType().GetProperties())
                    {
                        csvContent.Append(string.Format("{0},", item.GetValue(classInstance)));
                    }

                    foreach (var flag in flags)
                    {
                        csvContent.Append(string.Format("{0},", flag));
                    }
                    csvContent.Remove(csvContent.Length - 1, 1);
                    csvContent.Append("\n");
                }
            }
            File.WriteAllText(Path.Combine(experimentFolder, "ExperimentFullResults.csv"), csvContent.ToString());
            Console.Write(".");
        }
    }
}