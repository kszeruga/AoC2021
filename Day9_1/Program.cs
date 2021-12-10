string s;
var lines = new List<string>();
while (!string.IsNullOrEmpty(s = System.Console.ReadLine())) lines.Add(s);
var Y = lines.First().Length;
var X = lines.Count();
var map = new int[X, Y];
for (int i = 0; i < X; i++)
    for (int j = 0; j < Y; j++)
        map[i, j] = lines[i][j] - '0';
var sum = 0;
for (int i = 0; i < X; i++)
    for (int j = 0; j < Y; j++)
    {
        var neighbors = new List<int>();
        if (i > 0) neighbors.Add(map[i-1,j]);
        if (j > 0) neighbors.Add(map[i, j-1]);
        if (i < X-1) neighbors.Add(map[i + 1, j]); 
        if (j < Y-1) neighbors.Add(map[i, j+1]);
        if (map[i, j] < neighbors.Min())
        {
            sum+=(map[i, j]+1);
        }

    }
    
System.Console.WriteLine(sum);