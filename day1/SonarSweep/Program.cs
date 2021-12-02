// See https://aka.ms/new-console-template for more information

using SonarSweep;

string path = "./input.txt";
var measurements = new List<int>();
using (StreamReader sr = new StreamReader(path))
{
    while (sr.Peek() >= 0)
    {
        measurements.Add(int.Parse(sr.ReadLine()));                
    }
}


Console.WriteLine("Increases by measurement: " + DepthChecker.GetIncreasesByMeasurement(measurements));
Console.WriteLine("Increases by window: " + DepthChecker.GetIncreasesByWindow(measurements));
