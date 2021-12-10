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
        var maxX = lines.Count();
        var maxY = lines.FirstOrDefault().Length;

        int [,] heightMap = new int[maxX, maxY];
        var x = 0;
        foreach (var line in lines)
        {            
            for(var y = 0; y < maxY; y++)
            {
                heightMap[x,y] = (int)Char.GetNumericValue(line[y]);
            }
            x++;
        }

        int riskLevels = 0;
        for( var i = 0; i < maxX; i++)
        {            
            for(var j = 0;j < maxY; j++)
            {
                var minUp = i == 0
                    ? true
                    : heightMap[i, j] < heightMap[i - 1, j];
                var minDown = i < maxX - 1
                    ? heightMap[i, j] < heightMap[i + 1, j]
                    : true;

                var minLeft = j == 0
                    ? true
                    : heightMap[i,j] < heightMap[i, j - 1];

                var minRight = j < maxY - 1
                    ? heightMap[i, j] < heightMap[i, j + 1]
                    : true;

                if ( minUp && minDown && minLeft && minRight)
                {
                    riskLevels += (heightMap[i, j] + 1);
                }

            }
        }
        Console.WriteLine("Sum of all risk levels: " + riskLevels);
        return;
    }

    public static void PartTwo(IEnumerable<string> lines)
    {
        var maxX = lines.Count();
        var maxY = lines.FirstOrDefault().Length;

        int[,] heightMap = new int[maxX, maxY];
        var x = 0;
        // Load data into heightMap
        foreach (var line in lines)
        {
            for (var y = 0; y < maxY; y++)
            {
                heightMap[x, y] = (int)Char.GetNumericValue(line[y]);
            }
            x++;
        }

        // Find the lowPoints
        var lowPoints = new List<Point>();
        for (var i = 0; i < maxX; i++)
        {
            for (var j = 0; j < maxY; j++)
            {
                var minUp = i == 0
                    ? true
                    : heightMap[i, j] < heightMap[i - 1, j];
                var minDown = i < maxX - 1
                    ? heightMap[i, j] < heightMap[i + 1, j]
                    : true;

                var minLeft = j == 0
                    ? true
                    : heightMap[i, j] < heightMap[i, j - 1];

                var minRight = j < maxY - 1
                    ? heightMap[i, j] < heightMap[i, j + 1]
                    : true;

                if (minUp && minDown && minLeft && minRight)
                {
                    lowPoints.Add(new Point(i, j));                    
                }

            }
        }

        // Calculate the basin sizes
        var basinSizes = new List<int>();
        foreach (var lp in lowPoints)
        {            
            var basinPoints = Expand(lp, heightMap, new List<Point>());            
            basinSizes.Add(basinPoints.Count());
        }
        var top3 = basinSizes.OrderByDescending(bs => bs).ToList().Take(3);
        Console.WriteLine("The product of the three largest basins:" + top3.Aggregate((agg,x)=> agg * x)) ;
        return;
    }
    public class Point : IEquatable<Point>
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool Equals(Point? other)
        {
            if( X == other.X && Y == other.Y)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            int hashX = X == null ? 0 : X.GetHashCode();
            int hashY = Y == null ? 0 : Y.GetHashCode();

            return hashX ^ hashY;
        }
    }

    public static List<Point> Expand( Point point, int[,]heightMap, List<Point> basinList)
    {
        if( basinList.Contains(point))
        {
            return basinList;
        }

        var newList = basinList.Select(x => x).ToList();
        newList.Add(point);

        // Expand Up
        if( (point.X > 0) && ( heightMap[point.X -1, point.Y] < 9))
        {
            var upList = Expand(new Point(point.X - 1, point.Y), heightMap, newList);
            newList = newList.Concat(upList).Distinct().ToList();
        }

        // Expand Left
        if( (point.Y > 0) && (heightMap[point.X,point.Y-1] < 9))
        {
            var leftList = Expand(new Point(point.X, point.Y - 1), heightMap, newList);
            newList = newList.Concat(leftList).Distinct().ToList(); 
        }

        // Expand Down
        if( (point.X < heightMap.GetLength(0) -1) && ( heightMap[point.X + 1, point.Y] < 9))
        {
            var downList = Expand(new Point(point.X + 1, point.Y), heightMap, newList);
            newList = newList.Concat(downList).Distinct().ToList(); 
        }

        //Expand Right
        if( (point.Y < heightMap.GetLength(1) -1) && ( heightMap[point.X,point.Y+1]< 9))
        {
            var rightList = Expand( new Point(point.X, point.Y + 1), heightMap, newList);
            newList = newList.Concat(rightList).Distinct().ToList();
        }
        return newList;
    }
    
}