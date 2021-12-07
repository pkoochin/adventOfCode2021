using System.Numerics;

public class Program
{
    public static void Main(string[] args)
    {
        var infile = File.ReadLines("./sample.txt");
        //var infile = File.ReadAllLines("./input.txt");
        var allFish = infile.Select(x=>x.Split(',').Select(Int32.Parse)).ToList().FirstOrDefault().ToList();
        

        Console.WriteLine("Total Fish For 80 Days:" + Calculate(allFish,80));
        Console.WriteLine("Total Fish For 256 Days:" + Calculate(allFish, 256));
    }

    public static long Calculate(List<Int32> allFish, int days)
    {
        var summedDictionary = allFish.OrderBy(days => days).GroupBy(x => x).ToDictionary(x => x.Key,x=>(Int64)x.Count());
        for(int i = 0; i <= 8; i++)
        {
            if(!summedDictionary.ContainsKey(i))
            {
                summedDictionary[i] = 0;
            }
        }
        
        for(int i = 0; i < days; i++)
        {
            var newFish = summedDictionary[0];
            foreach (var item in summedDictionary)
            {                
                if(item.Key > 0)
                {                    
                    summedDictionary[item.Key - 1] = item.Value;
                    summedDictionary[item.Key] = 0;                    
                }                                       
            }

            summedDictionary[6] += newFish;
            summedDictionary[8] += newFish;
        }

        var totalFish = summedDictionary.Select(x => x.Value).Aggregate((currentSum,item)=>currentSum+item);
        
        return totalFish;
    }
}