using Newtonsoft.Json;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CsvHelper;

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
                experimentFolder = @"C:\Users\jjd\Desktop\LeanExperiment_2017-05-29_1124";
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
            var allRecords = new List<BakctestResults>();
            foreach (var backtestResultFile in backtestResultsFiles)
            {
                try
                {
                    // Parse JSON with backtest and training results.
                    allRecords.Add(JsonConvert.DeserializeObject<BakctestResults>(File.ReadAllText(backtestResultFile)));
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error with experiment {0}", backtestResultFile);
                    Console.ResetColor();
                    Console.WriteLine(ex.StackTrace);
                    continue;
                }
            }
            var fileName = Path.Combine(experimentFolder, "FullResutls.csv");
            using (TextWriter writer = new StreamWriter(fileName))
            {
                var csv = new CsvWriter(writer);
                csv.WriteRecords(allRecords);
            }
        }
    }
}