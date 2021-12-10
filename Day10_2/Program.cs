using System.ComponentModel.Design;

var matching = new Dictionary<char, char>()
{
    { '[', ']' },
    { '(', ')' },
    { '{', '}' },
    { '<', '>' }
};
var score = new Dictionary<char, long>()
{
    { '[' ,2 },
    { '(' ,1 },
    { '{', 3 },
    { '<', 4 }
};
string line;
var scores = new List<long>();
while (!string.IsNullOrEmpty(line = System.Console.ReadLine()))
{
    var stack = new Stack<char>();
    foreach (var c in line)
    {
        if (matching.ContainsKey(c))
        {
            stack.Push(c);
        }
        else
        {
            var a = stack.Pop();
            if (!matching.ContainsKey(a) || matching[a] != c)
            {
                stack.Clear();
                break;
            }
        }
    }

    var rest = stack.ToList();
    var linescore = rest.Aggregate(0L, (current, c) => (current * 5) + score[c]);
    if (linescore>0) scores.Add(linescore);
}
System.Console.WriteLine(scores.OrderBy(i => i).ElementAt(scores.Count/2));


