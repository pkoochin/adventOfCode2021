using System.Text;

public class Program
{
    public static void Main(string[] args)
    {
        //var infile = File.ReadLines("./sample.txt");
        var infile = File.ReadAllLines("./input.txt");
        PartOne(infile);
        PartTwo(infile);
    }

    public static void PartOne(IEnumerable<string> lines)
    {
        Console.WriteLine("*** Part One ***");
        var points = 0;
        foreach( var line in lines)
        {
            var chunks = new Stack<char>();
            foreach( var c in line)
            {
                if (IsOpeningChunk(c))
                {
                    chunks.Push(c);
                }
                else
                {
                    var openingChar = chunks.Pop();
                    if( c != PairedBracket(openingChar))
                    {                        
                        points += GetPartOnePoints(c);
                        continue;
                    }
                }
            }
        }
        Console.WriteLine("Calculated Points: " + points);
    }

    public static void PartTwo(IEnumerable<string> lines)
    {
        Console.WriteLine("*** Part Two ***");
        var incompleteLines = new List<string>();

        // Find the incomplete lines
        foreach (var line in lines)
        {
            var chunks = new Stack<char>();
            var isIncomplete = true;
            foreach (var c in line)
            {
                if (IsOpeningChunk(c))
                {
                    chunks.Push(c);
                }
                else
                {
                    var openingChar = chunks.Pop();
                    if (c != PairedBracket(openingChar))
                    {
                        isIncomplete = false;
                        continue;
                    }
                    
                }                
            }
            if( isIncomplete)
            {
                incompleteLines.Add(line);                
            }
        }

        // Calculate the score for each incomplete line
        var lineScores = new List<long>();
        foreach( var incLine in incompleteLines)
        {
            var chunks = new Stack<char>();            
            foreach (var c in incLine)
            {
                if (IsOpeningChunk(c))
                {
                    chunks.Push(c);
                }
                else
                {
                    chunks.Pop();
                }                
            }
            
            var builder = new StringBuilder();
            while (chunks.TryPop(out char r))
            {
                builder.Append(PairedBracket(r));
            }
                
            lineScores.Add(GetPointsForCompletedString(builder.ToString()));            
        }
        lineScores.Sort();        
        Console.WriteLine("Middle Score: " + lineScores.Skip(lineScores.Count() / 2).Take(1).First());
    }

    private static char PairedBracket(char c) =>
        c switch
        {
            '(' => ')',
            '[' => ']',
            '{' => '}',
            '<' => '>'
        };
    private static bool IsOpeningChunk(char c)
    {
        switch (c)
        {
            case '(':
            case '[':
            case '{':
            case '<':
                return true;
            default:
                return false;
        }
    }
    private static int GetPartOnePoints(char c) =>
        c switch
        {
            ')' => 3,
            ']' => 57,
            '}' => 1197,
            '>' => 25137
        };
    private static long GetPointsForCompletedString(string  completed)
    {
        long score = 0;
        foreach( var c in completed)
        {
            score = score * 5;
            switch (c)
            {
                case ')':
                    score += 1;
                    break;
                case ']':
                    score += 2;
                    break;
                case '}':
                    score += 3;
                    break;
                case '>':
                    score += 4;
                    break;
            }
        }
        return score;
    }

}