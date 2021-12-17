var a = new bool[2000, 2000];
var pairs = new List<Tuple<int,int>>();
string line;
while (!string.IsNullOrWhiteSpace(line = Console.ReadLine()))
{
    var (x,y) = (int.Parse(line.Split(',').First()), int.Parse(line.Split(',').Last()));
    pairs.Add( new Tuple<int, int>(x,y));
    a[int.Parse(line.Split(',').First()), int.Parse(line.Split(',').Last())] = true;
}

line = Console.ReadLine();
var fold_x = line.Split(' ')[2].Split('=').First()=="x";
var fold_line = int.Parse(line.Split(' ')[2].Split('=').Last());

System.Console.WriteLine( pairs.Distinct(new DotComparer(fold_x,fold_line)).Count());

internal class DotComparer : IEqualityComparer<Tuple<int, int>>
{
    private bool fold_x;
    private int fold_line;

    public DotComparer(bool fold_x, int fold_line)
    {
        this.fold_x = fold_x;
        this.fold_line = fold_line;
    }

    public bool Equals(Tuple<int, int> a, Tuple<int, int> b)
    {
        if (ReferenceEquals(a, b)) return true;
        if (ReferenceEquals(a, null)) return false;
        if (ReferenceEquals(b, null)) return false;
        if (a.GetType() != b.GetType()) return false;
        return (a.Item1 == b.Item1 && a.Item2 == b.Item2) || 
               ( fold_x && a.Item2 == b.Item2 && Math.Abs(fold_line-a.Item1) == Math.Abs(fold_line - b.Item1)) ||
                (!fold_x && a.Item1 == b.Item1 && Math.Abs(fold_line - a.Item2) == Math.Abs(fold_line - b.Item2));
    }

    public int GetHashCode(Tuple<int, int> obj)
    {
        return 0;
    }
}