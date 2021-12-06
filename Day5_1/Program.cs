string line;
var map = new int[1000,1000];
while (!string.IsNullOrEmpty(line = System.Console.ReadLine()!))
{
    var coord1 = line.Substring(0, line.IndexOf('-')-1).Split(',').Select(int.Parse).ToArray();
    var coord2 = line.Substring(line.IndexOf('-') + 2).Split(',').Select(int.Parse).ToArray();
    if (coord1[0] == coord2[0])
    {
        for (var i = Math.Min(coord1[1], coord2[1]); i <= Math.Max(coord1[1], coord2[1]); i++)
            map[coord1[0], i]++;
    }

    if (coord1[1] == coord2[1])
    {
        for (var i = Math.Min(coord1[0], coord2[0]); i <= Math.Max(coord1[0], coord2[0]); i++)
            map[i, coord1[1]]++;
    }
}

var counter = 0;
for (int i = 0; i < 1000; i++)
{
    for (int j = 0; j < 1000; j++)
    {
        if (map[i, j] > 1) counter++;
    }
}
System.Console.WriteLine(counter);