
var inFile = File.ReadAllLines("./input.txt");
var directions = new List<string>(inFile);

var totalDepth = 0;
var totalHorizontal = 0;

foreach (var direction in directions)
{
    string[] bearing = direction.Split(' ');
    var heading = bearing[0];
    var distance = int.Parse(bearing[1]);

    switch (heading)
    {
        case "forward":
            totalHorizontal += distance; break;          
        case "down":
            totalDepth += distance; break;
        case "up":
            totalDepth -= distance; break;
    }
}
Console.WriteLine("Part One: " + totalDepth * totalHorizontal);

totalDepth = 0;
totalHorizontal = 0;
var aim = 0;
foreach (var direction in directions)
{
    string[] bearing = direction.Split(' ');
    var heading = bearing[0];
    var distance = int.Parse(bearing[1]);

    switch(heading)
    {
        case "forward":
            totalHorizontal += distance; 
            totalDepth += (aim * distance);
            break;
        case "down":
            aim += distance; break;
        case "up":
            aim -= distance; break;
    }
}

Console.WriteLine("Part Two: " + totalDepth*totalHorizontal);