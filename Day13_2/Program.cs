var a = new bool[2000, 2000];
var pairs = new List<Tuple<int, int>>();
string line;
while (!string.IsNullOrWhiteSpace(line = Console.ReadLine()))
{
    var (x, y) = (int.Parse(line.Split(',').First()), int.Parse(line.Split(',').Last()));
    pairs.Add(new Tuple<int, int>(x, y));
    a[int.Parse(line.Split(',').First()), int.Parse(line.Split(',').Last())] = true;
}

while (!string.IsNullOrWhiteSpace(line = Console.ReadLine()))
{
    var fold_x = line.Split(' ')[2].Split('=').First() == "x";
    var fold_line = int.Parse(line.Split(' ')[2].Split('=').Last());
    if (!fold_x)
    {
        pairs = pairs.Select(tuple =>
            (tuple.Item2 >= fold_line) ? (new Tuple<int, int>(tuple.Item1, 2*fold_line-tuple.Item2)): tuple).Distinct().ToList();
    }
    else
    {
        pairs = pairs.Select(tuple =>
            (tuple.Item1 >= fold_line) ? (new Tuple<int, int>(2 * fold_line  - tuple.Item1, tuple.Item2)) : tuple).Distinct().ToList();
    }
}

var output = new char[41][];
for (var i = 0; i < 41; i++) output[i] = new string(' ',41).ToCharArray();
foreach (var tuple in pairs)
{
    output[tuple.Item2][tuple.Item1] = '#';
}
for (var i = 0; i < 41; i++) System.Console.WriteLine(output[i]);

