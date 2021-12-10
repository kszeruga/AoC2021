string s;
var lines = new List<string>();
while (!string.IsNullOrEmpty(s = System.Console.ReadLine())) lines.Add(s);
var Y = lines.First().Length;
var X = lines.Count();
var map = new int[X, Y];
for (int i = 0; i < X; i++)
    for (int j = 0; j < Y; j++)
        map[i, j] = lines[i][j] - '0';
var basins = new List<int>();

for (int i = 0; i < X; i++)
    for (int j = 0; j < Y; j++)
    {
        if (map[i, j] == 9) continue;
        var count = 0;
        var q = new Queue<Tuple<int, int>>();
        q.Enqueue(new(i, j));
        while (q.Count > 0)
        {
            var (x, y) = q.Dequeue();
            if (map[x,y]==9) continue;
            count++;
            map[x, y] = 9;
            if (x > 0 && map[x - 1, y] != 9) q.Enqueue(new(x - 1, y));
            if (y > 0 && map[x, y - 1] != 9) q.Enqueue(new(x, y - 1));
            if (x < X - 1 && map[x + 1, y] != 9) q.Enqueue(new(x + 1, y));
            if (y < Y - 1 && map[x, y + 1] != 9) q.Enqueue(new(x, y + 1));
        }
        basins.Add(count);
    }

System.Console.WriteLine(basins.OrderByDescending(i => i).First() *
                         basins.OrderByDescending(i => i).Skip(1).First() * 
                         basins.OrderByDescending(i => i).Skip(2).First());