using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PrinterSolution.Common.Utils.Helper
{
    public static class GCodeHelper
    {
        public class FileData
        {
            public double EstimatedMaterial { get; set; }
            public double EstimatedPrintTime { get; set; }
        }

        /// <summary>
        /// Analyses the GCODE File and returns the estimated material used and print time for FDM printers
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static FileData EstimateFile(string filePath)
        {
            var fileName = System.IO.Path.GetFileName(filePath);
            double totalEstimatedMaterial = 0;
            double totalEstimatedPrintTime = 0;

            // Variable to calculate data line by line
            double estimatedTime = 0;
            double materialExtruded = 0;
            bool extruderModeAbsolute = false;

            // Regex to separate the commands
            Regex regex = new Regex(@"[FfgGXxYyZzEeMmSs][-\d.]+|(?<=[\s]*;[\s]*).*");
            var culture = new CultureInfo("en-US");

            // Print head movement data
            double feedRate = 0;
            double lastX = 0;
            double lastY = 0;
            double newX = 0;
            double newY = 0;

            // Open the GCode file
            using (var reader = new StreamReader(filePath))
            {
                var line = string.Empty;
                while ((line = reader.ReadLine()) != null)
                {
                    System.Console.WriteLine(line);
                    var matches = regex.Matches(line);

                    foreach (Match match in matches)
                    {
                        if (match.Value.StartsWith("G"))
                        {
                            if (match.Value == "G92")
                            {
                                totalEstimatedMaterial += materialExtruded;
                                materialExtruded = 0;
                            }
                        }
                        else if (match.Value.StartsWith("F"))
                        {
                            // Convert the feed rate to mm/ss
                            if (double.TryParse(match.Value.Replace("F", string.Empty), NumberStyles.Any, culture, out var value))
                            {
                                feedRate = value / 60;
                            }
                        }
                        else if (match.Value.StartsWith("X"))
                        {
                            if (double.TryParse(match.Value.Replace("X", string.Empty), NumberStyles.Any, culture, out var value))
                            {
                                newX = value;
                            }
                        }
                        else if (match.Value.StartsWith("Y"))
                        {
                            if (double.TryParse(match.Value.Replace("Y", string.Empty), NumberStyles.Any, culture, out var value))
                            {
                                newY = value;
                            }
                        }
                        else if (match.Value.StartsWith("M"))
                        {
                            if (match.Value == "M82")
                            {
                                extruderModeAbsolute = true;
                            }

                            if (match.Value == "M83")
                            {
                                extruderModeAbsolute = false;
                            }
                        }
                        else if (match.Value.StartsWith("E"))
                        {
                            if (double.TryParse(match.Value.Replace("E", string.Empty), NumberStyles.Any, culture, out var value))
                            {
                                if (value > 0)
                                {
                                    materialExtruded += extruderModeAbsolute ? value - materialExtruded : materialExtruded;
                                }
                            }
                        }
                    }

                    // Calculates the movement time of the print head
                    if (lastX != newX || lastY != newY)
                    {
                        var segmentDistance = Math.Abs(Math.Sqrt(Math.Pow(Convert.ToDouble(lastX - newX), 2) + Math.Pow(Convert.ToDouble(lastY - newY), 2)));

                        if (segmentDistance > 0)
                        {
                            estimatedTime += segmentDistance / feedRate;
                        }
                    }

                    lastX = newX;
                    lastY = newY;
                }

                // Adds extra time for accelerations and warmups
                totalEstimatedPrintTime = estimatedTime + (estimatedTime * 0.25); // TODO: Get this data from configurations / data correction from other printers

                totalEstimatedMaterial += materialExtruded;

                return new FileData
                {
                    EstimatedMaterial = totalEstimatedMaterial,
                    EstimatedPrintTime = totalEstimatedPrintTime
                };
            }
        }
    }

}
