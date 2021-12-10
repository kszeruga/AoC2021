var list = System.Console.ReadLine().Split(',').Select(int.Parse);
var result = Enumerable.Range(1, list.Max()).Min(i => list.Sum(x => fuel(Math.Abs(x - i))));
System.Diagnostics.Debug.WriteLine(result);

long fuel(int v)
{
    return ((v * (v + 1)) / 2);
}