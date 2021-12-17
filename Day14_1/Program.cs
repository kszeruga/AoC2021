
var line = System.Console.ReadLine();
System.Console.ReadLine();
string s;
var rules = new Dictionary<string, string>();
while (!string.IsNullOrEmpty(s = Console.ReadLine()))
{
    var c = s.Split(' ').First();
    rules[c] = c[0] + s.Split(' ').Last()+ c[1];
}

for (var i = 0; i < 10; i++)
{
    line = string.Join("",
        line.Zip(line.Skip(1)).Select(tuple =>
        {
            var x = "" + tuple.First + tuple.Second;
            return rules.ContainsKey(x) ? rules[x].Substring(0,2) : "" + tuple.First;
        })) + line.Last();

}
System.Console.WriteLine(line.GroupBy(c =>c ).Max(chars => chars.Count()) - line.GroupBy(c => c).Min(chars => chars.Count()));
System.Console.ReadLine();