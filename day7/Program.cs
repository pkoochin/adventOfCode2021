public class Program
{
    public static void Main(string[] args)
    {
        //var infile = File.ReadLines("./sample.txt");
        var infile = File.ReadAllLines("./input.txt");

        var initialPositions = infile.Select(x => x.Split(',').Select(Int32.Parse)).ToList().FirstOrDefault().ToList();

        PartOne(initialPositions);
        PartTwo(initialPositions);
    }

    public static void PartOne(List<int> initialPositions)
    {
        var maxPos = initialPositions.Max();

        var lowFuel = int.MaxValue;
        var hPos = 0;

        for( int i = 0;i<maxPos;i++)
        {
            var tmpFuel = 0;
            initialPositions.ForEach(p => {
                tmpFuel += Math.Abs(p - i);
            });
            if(tmpFuel < lowFuel)
            {
                hPos = i;
                lowFuel = tmpFuel;
            }
        }
        Console.WriteLine("Cheapest Position: " + hPos);
        Console.WriteLine("Fuel Count: " + lowFuel);
    }

    public static void PartTwo(List<int> initialPositions)
    {
        var maxPos = initialPositions.Max();

        var lowFuel = int.MaxValue;
        var hPos = 0;

        for (int i = 0; i < maxPos; i++)
        {
            var tmpFuel = 0;
            initialPositions.ForEach(p => {
                var steps = Math.Abs(p - i);
                tmpFuel += (steps * (steps + 1 )/2);
            });
            if (tmpFuel < lowFuel)
            {
                hPos = i;
                lowFuel = tmpFuel;
            }
        }
        Console.WriteLine("Cheapest Position: " + hPos);
        Console.WriteLine("Fuel Count: " + lowFuel);
    }
}