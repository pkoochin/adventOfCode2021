
namespace day5
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var inFile = File.ReadAllLines("./sample.txt");
            var inFile = File.ReadAllLines("./input.txt");
            var lineSements = inFile
                .Select(l => l.Split(" -> ")
                .Select(cp => new Coord(cp)).ToList());

            PartOne(lineSements);
            PartTwo(lineSements);
            return;
        }

        public class Coord
        {
            public int X { get; }
            public int Y { get; }

            public Coord(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public Coord(string coordPair)
            {
                var cp = coordPair.Split(',').Select(Int32.Parse).ToList();
                this.X = cp[0];
                this.Y = cp[1];
            }
        }

        public static (int, int) GetMaxLength(IEnumerable<List<Coord>> segments)
        {
            int maxXIndex = 0;
            int maxYIndex = 0;

            foreach (var coordPair in segments)
            {
                if (coordPair[0].X > maxXIndex || coordPair[1].X > maxXIndex)
                {
                    maxXIndex = coordPair[0].X > coordPair[1].X ? coordPair[0].X : coordPair[1].X;
                }

                if (coordPair[0].Y > maxYIndex || coordPair[1].Y > maxYIndex)
                {
                    maxYIndex = coordPair[0].Y > coordPair[1].Y ? coordPair[0].Y : coordPair[1].Y;
                }
            }
            return (maxXIndex +1 , maxYIndex + 1 );
        }

        public static (int, int) GetStartPositions(Coord coord1, Coord coord2)
        {            
            var minX = (coord1.X < coord2.X) ? coord1.X : coord2.X;
            var minY = (coord1.Y < coord2.Y) ? coord1.Y : coord2.Y; 
            return (minX, minY);    
        }
        public static (int, int) GetEndPositions(Coord coord1, Coord coord2)
        {
            var maxX = (coord1.X > coord2.X) ? coord1.X : coord2.X;
            var maxY = (coord1.Y > coord2.Y) ? coord1.Y : coord2.Y;
            return (maxX  , maxY  );
        }
        public static int CalculateOverlaps(int[,] maps, int maxX, int maxY)
        {
            var overlaps = 0;
            for(int i=0; i< maxX; i++)
            {
                for(int j=0; j< maxY; j++)
                {
                    if(maps[i,j] > 1)
                    {
                        overlaps++;
                    }
                }
            }
            return overlaps;
        }

        public static void PartOne(IEnumerable<List<Coord>> lineSegments)
        {
            (var maxX, var maxY) = GetMaxLength(lineSegments);

            var map = new int[maxX, maxY];

            foreach (var coordPair in lineSegments)
            {
                if ((coordPair[0].X != coordPair[1].X) && (coordPair[0].Y != coordPair[1].Y))
                {
                    continue;
                }
                (var startXIndex, var startYIndex) = GetStartPositions(coordPair[0], coordPair[1]);
                (var endXIndex, var endYIndex) = GetEndPositions(coordPair[0], coordPair[1]);


                if ((coordPair[0].X == coordPair[1].X) && (coordPair[0].Y == coordPair[1].Y))
                {
                    map[coordPair[0].X, coordPair[0].Y]++;
                }

                if (coordPair[0].X == coordPair[1].X)
                {
                    for (int i = startYIndex; i <= endYIndex; i++)
                    {
                        map[i, coordPair[0].X]++;
                    }
                }

                if (coordPair[0].Y == coordPair[1].Y)
                {
                    for (int i = startXIndex; i <= endXIndex; i++)
                    {
                        map[coordPair[0].Y, i]++;
                    }
                }
            }

            Console.WriteLine("The number of overlaps is: " + CalculateOverlaps(map, maxX, maxY));
        }

        public static void PartTwo(IEnumerable<List<Coord>> lineSegments)
        {
            (var maxX, var maxY) = GetMaxLength(lineSegments);

            var map = new int[maxX, maxY];

            foreach (var coordPair in lineSegments)
            {                
                if ((coordPair[0].X != coordPair[1].X) && (coordPair[0].Y != coordPair[1].Y))
                {
                    var length = Math.Abs(coordPair[0].X - coordPair[1].X);
                    var xPos = coordPair[0].X;
                    var yPos = coordPair[0].Y;

                    for(int i = 0 ; i < length+1; i++)
                    {
                        map[yPos, xPos]++;
                        xPos = (coordPair[0].X > coordPair[1].X) ? xPos-1 : xPos+1;                            
                        yPos = (coordPair[0].Y > coordPair[1].Y) ? yPos-1 : yPos+1; 
                    }
                }
                else
                {
                    (var startXIndex, var startYIndex) = GetStartPositions(coordPair[0], coordPair[1]);
                    (var endXIndex, var endYIndex) = GetEndPositions(coordPair[0], coordPair[1]);
                    if ((coordPair[0].X == coordPair[1].X) && (coordPair[0].Y == coordPair[1].Y))
                    {
                        map[coordPair[0].X, coordPair[0].Y]++;
                    }

                    if (coordPair[0].X == coordPair[1].X)
                    {
                        for (int i = startYIndex; i <= endYIndex; i++)
                        {
                            map[i, coordPair[0].X]++;
                        }
                    }

                    if (coordPair[0].Y == coordPair[1].Y)
                    {
                        for (int i = startXIndex; i <= endXIndex; i++)
                        {
                            map[coordPair[0].Y, i]++;
                        }
                    }
                }
            }
            
            Console.WriteLine("The number of overlaps is: " + CalculateOverlaps(map, maxX, maxY));
        }
    }
}