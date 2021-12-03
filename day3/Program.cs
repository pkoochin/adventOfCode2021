using System;

public class Program
{
    public static void Main()
    {
        // See https://aka.ms/new-console-template for more information
        //var inFile = File.ReadAllLines("./sample.txt");
        var inFile = File.ReadAllLines("./input.txt");
        var diagnostics = new List<string>(inFile);
        var charCount = diagnostics[0].Length;

        var bitCounts = new int[charCount];

        diagnostics.ForEach(d =>
        {
            for (int i = 0; i < charCount; i++)
            {
                if (d[i] == '1')
                {
                    bitCounts[i]++;
                }
            }
        });

        string gamma = "";
        string epsilon = "";
        for (int j = 0; j < charCount; j++)
        {
            if (bitCounts[j] > (diagnostics.Count / 2))
            {
                gamma += "1";
                epsilon += "0";
            }
            else
            {
                gamma += "0";
                epsilon += "1";
            }
        }
        Console.WriteLine("Gamma: " + Convert.ToInt32(gamma, 2));
        Console.WriteLine("Epsilon: " + Convert.ToInt32(epsilon, 2));
        Console.WriteLine("Power Consumption: " + Convert.ToInt32(gamma, 2) * Convert.ToInt32(epsilon, 2));


        var tmpOxygen = diagnostics.Select(x => x).ToList();
        var tmpCO2 = diagnostics.Select(x => x).ToList();

        for (int k = 0; k < charCount; k++)
        {
            char oxygenCriteria = getCommonBit(tmpOxygen, k);
            char cO2Criteria = getCommonBit(tmpCO2, k) == '1'?'0':'1';

            diagnostics.ForEach(d =>
            {
                if ( (d[k] != oxygenCriteria) && (tmpOxygen.Count > 1))  
                { 
                    tmpOxygen.Remove(d);                 
                }
                if ( (d[k] != cO2Criteria) && (tmpCO2.Count > 1))
                {
                    tmpCO2.Remove(d); 
                }

            });                       
        }
        Console.WriteLine("Oxygen Generator Rating: " + Convert.ToInt32(tmpOxygen.FirstOrDefault(),2));
        Console.WriteLine("CO2 Scrubber Rating: " + Convert.ToInt32(tmpCO2.FirstOrDefault(), 2));
        Console.WriteLine("Life Support Rating: " + Convert.ToInt32(tmpOxygen.FirstOrDefault(), 2) * Convert.ToInt32(tmpCO2.FirstOrDefault(), 2));

        char getCommonBit(List<string> reading, int position)
        {
            
            int oneCount = reading.Where(r => r[position] == '1').ToList().Count;
            
            if (oneCount >= Math.Ceiling((double)reading.Count / 2))
            { return '1'; }
            else { return '0'; }
        }


    }

    
}

