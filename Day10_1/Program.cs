using System.ComponentModel.Design;

var matching = new Dictionary<char, char>()
{
    { '[', ']' },
    { '(', ')' },
    { '{', '}' },
    { '<', '>' }
};
var score = new Dictionary<char, int>()
{
    { ']' ,57 },
    { ')' ,3 },
    { '}', 1197 },
    { '>', 25137 }
};
string line;
var totalscore = 0;
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
                totalscore += score[c];
                break;
            }
        }
    }
}
System.Console.WriteLine(totalscore);


