using System.Security.Cryptography.X509Certificates;
using System.Transactions;

var lines = new List<string>();
string s;
while (!string.IsNullOrEmpty(s = Console.ReadLine())) lines.Add(s);

var N = lines.Count*5;
var graph = new int[N + 2, N + 2];
for (var i = 0; i <= N + 1; i++)
    for (var j = 0; j <= N + 1; j++) graph[i, j] = Int32.MaxValue;
for (var i = 1; i <= N; i++)
    for (var j = 1; j <= N; j++)
        graph[i, j] = ((lines[(i - 1) % (N / 5)][(j - 1) % (N / 5)] - '0') + (((i - 1) / (N / 5)) + ((j - 1) / (N / 5))));
for (var i = 1; i <= N; i++)
    for (var j = 1; j <= N; j++)
        if (graph[i, j] >= 10)
            graph[i, j] -= 9;


var path = A_Star((1,1),(N,N), distance);
System.Console.WriteLine(path.Sum(tuple => graph[tuple.Item1, tuple.Item2]) -graph[1,1]);



List<(int, int)> A_Star((int x, int y) start, (int x, int y) end, Func<(int x, int y), int> h)
{
    var open_set = new HashSet<(int, int)>();
    open_set.Add(start);
    var came_from = new Dictionary<(int, int), (int, int)>();
    var gScore = new Dictionary<(int, int), int>();
    for (var i = 1; i <= N; i++)
        for (var j = 1; j <= N; j++)
            gScore[(i, j)] = int.MaxValue;
    gScore[start] = 0;

    var fScore = new Dictionary<(int, int), int>();
    for (var i = 1; i <= N; i++)
        for (var j = 1; j <= N; j++)
            fScore[(i, j)] = int.MaxValue;
    fScore[start] = h(start);

    while (open_set.Count > 0)
    {
        var current = open_set.MinBy(tuple => fScore[tuple]);
        if (current == end)
            return reconstruct(came_from, current);
        open_set.Remove(current);
        foreach (var neighbor in neighbors(current))
        {
            var tentative_gScore = gScore[current] + graph[neighbor.Item1, neighbor.Item2];
            if (tentative_gScore < gScore[neighbor])
            {
                came_from[neighbor] = current;
                gScore[neighbor] = tentative_gScore;
                fScore[neighbor] = tentative_gScore + h(neighbor);
                open_set.Add(neighbor);
            }
        }
    }

    return null;

}

List<(int, int)> reconstruct(Dictionary<(int, int), (int, int)> came_from, (int, int) b)
{
    var current = b;
    var total_path = new List<(int,int)>(){current};
    while (came_from.ContainsKey(current))
    {
        current  = came_from[current];
        total_path.Add(current);
    }

    return total_path;

}

IEnumerable<(int, int)> neighbors((int x, int y) p)
{
    return new List<(int, int)>
    {
        (p.x - 1, p.y),
        (p.x + 1, p.y),
        (p.x, p.y - 1),
        (p.x, p.y + 1),
    }.Where(a => a.Item1 >= 1 && a.Item1 <= N && a.Item2 >= 1 && a.Item2 <= N);
}
int distance((int x, int y) p)
{
    return (N - p.x) + (N - p.y);
}