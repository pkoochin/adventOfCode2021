public class Program
{
    public static void Main(string[] args)
    {
        //var infile = File.ReadLines("./sample.txt");
        var infile = File.ReadAllLines("./input.txt");
        PartOne(infile);
        PartTwo(infile);
    }

    public static void PartOne(IEnumerable<string>allLines)
    {
        var outPuts = allLines.Select(x => x.Split('|')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList());
        
        var nums = 0;
        foreach(var output in outPuts)
        {
            var uniqueSegs = output.Where(y => (y.Length == 2) || (y.Length == 3) || (y.Length == 4) || (y.Length == 7)).Count();
            nums += uniqueSegs;
        }

        Console.WriteLine("Total Unique Count: "  + nums);
        return;
    }

    public static void PartTwo(IEnumerable<string>allLines)
    {
        var patterns = allLines.Select(x => x.Split('|')[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList());
        var outPuts = allLines.Select(x => x.Split('|')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList()).ToList();
        var lineIndex = 0;
        var total = 0;
        foreach (var pattern in patterns)
        {
            var numberDict = new Dictionary<int, string>();
            numberDict.Add(1, pattern.Where(x => x.Length == 2).First());
            numberDict.Add(4, pattern.Where(x => x.Length == 4).First());
            numberDict.Add(7, pattern.Where(x => x.Length == 3).First());
            numberDict.Add(8, pattern.Where(x => x.Length == 7).First());

            var length5 = pattern.Where(x => x.Length == 5).ToList();
            var length6 = pattern.Where(x => x.Length == 6).ToList();

            var threePattern = length5.Where(x => numberDict[7].All(x.Contains)).First();
            numberDict.Add(3, threePattern);
            length5.Remove(threePattern);

            var fivePattern = "";
            foreach( var l5 in length5)
            {
                var numMatches = 0;
                foreach (var c in numberDict[4])
                {
                    if (l5.Contains(c))
                    {
                        numMatches++;
                    }
                }
                if( numMatches == 3 )
                {
                    fivePattern = l5;
                }
            }
            numberDict.Add(5, fivePattern);
            length5.Remove(fivePattern);
            numberDict.Add(2, length5.First());

            var ninePattern = length6.Where(x => numberDict[4].All(x.Contains)).First();
            numberDict.Add(9, ninePattern);
            length6.Remove(ninePattern);

            var zeroPattern = "";
            foreach (var l6 in length6)
            {
                var numMatches = 0;
                foreach (var c in numberDict[7])
                {
                    if (l6.Contains(c))
                    {
                        numMatches++;
                    }
                }
                if (numMatches == 3)
                {
                    zeroPattern = l6;
                }
            }
            numberDict.Add(0, zeroPattern);
            length6.Remove(zeroPattern);
            numberDict.Add(6, length6.First());
                        
            var valueString = "";
            foreach( var value in outPuts[lineIndex])
            {
                var sortedOp = String.Concat(value.OrderBy(c => c));
                var digit = numberDict.Where(x => String.Concat(x.Value.OrderBy(c => c)).Equals(sortedOp)).First();
                valueString += digit.Key;
            }
            Console.WriteLine(valueString);
            total+=int.Parse(valueString);
            lineIndex++;
        }
        Console.WriteLine("Total Output: " + total);
        return;
    }
}