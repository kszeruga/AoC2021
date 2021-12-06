string line;
const int N = 1001;
var map = new int[N, N];
while (!string.IsNullOrEmpty(line = System.Console.ReadLine()!))
{
    var coord1 = line.Substring(0, line.IndexOf('-') - 1).Split(',').Select(int.Parse).ToArray();
    var coord2 = line.Substring(line.IndexOf('-') + 2).Split(',').Select(int.Parse).ToArray();
    if (coord1[0] == coord2[0])
    {
        for (var i = Math.Min(coord1[1], coord2[1]); i <= Math.Max(coord1[1], coord2[1]); i++)
            map[coord1[0], i]++;
    } 
    else if (coord1[1] == coord2[1])
    {
        for (var i = Math.Min(coord1[0], coord2[0]); i <= Math.Max(coord1[0], coord2[0]); i++)
            map[i, coord1[1]]++;
    }
    else
    {
        var i = coord1[0];
        var step_i = 1;
        if (coord1[0] > coord2[0])
            step_i = -1;
        
        var j = coord1[1]; 
        var step_j = 1;
        if (coord1[1] > coord2[1])
            step_j = -1;
        var loop = Math.Abs(coord1[1] - coord2[1]) + 1;
        for (var l = 1;l<=loop;l++)
        {
            map[i, j]++;
            j += step_j;
            i += step_i;
        }

    }
}

var counter = 0;
for (int i = 0; i <N; i++)
{
    for (int j = 0; j <N; j++)
    {
        if (map[i, j] > 1) counter++;
    }
}
System.Console.WriteLine(counter);