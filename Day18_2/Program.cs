

string l;
List<INode> trees = new List<INode>();
while (!string.IsNullOrEmpty(l = System.Console.ReadLine()))
{
    var (t, _) = Parse(l, 0);
    trees.Add(t);
}

long max = 0;
for (int i = 0; i < trees.Count; i++)
    for (int j = 0; j < trees.Count; j++)
    {
        if (i == j) continue;
        var s = Add(trees[i].Clone(),trees[j].Clone());
        while (Reduce(s))
        {

        }

        var m = Magnitude(s);
        if (m>max) max = m;
    }

Console.WriteLine(max);

long Magnitude(INode tree)
{
    if (tree.GetType() == typeof(Terminal)) return ((tree as Terminal)!).Value;
    return 3 * Magnitude(((tree as NonTerminal)!).LeftNode) + 2 * Magnitude(((tree as NonTerminal)!).RightNode);
}

bool Reduce(INode tree)
{
    var reduced = Explode(ref tree, null, null);
    if (!reduced) reduced = Split(ref tree);
    return reduced;
}

INode LeftMost(INode tree)
{
    if (tree.GetType() == typeof(Terminal)) return tree;
    return LeftMost((tree as NonTerminal).LeftNode);
}

INode RightMost(INode tree)
{
    if (tree.GetType() == typeof(Terminal)) return tree;
    return RightMost((tree as NonTerminal).RightNode);
}
bool Split(ref INode tree)
{
    if (tree.GetType() == typeof(Terminal))
    {
        if ((tree as Terminal).Value >= 10)
        {
            var v = (tree as Terminal).Value;
            tree = new NonTerminal(tree.Depth,
                new Terminal(tree.Depth + 1, (int)Math.Round(v / 2.0, MidpointRounding.ToZero)),
                new Terminal(tree.Depth + 1, (int)Math.Round(v / 2.0, MidpointRounding.AwayFromZero)));
            return true;
        }

        return false;


    }
    else
    {
        var split = Split(ref ((tree as NonTerminal).LeftNode));
        if (!split) split = Split(ref ((tree as NonTerminal).RightNode));
        return split;
    }
}

bool Explode(ref INode tree, INode leftmostleaf, INode rightmostleaf)
{
    if (tree.GetType() == typeof(Terminal)) return false;
    if (tree.Depth == 4)
    {
        var al = (((tree as NonTerminal).LeftNode) as Terminal).Value;
        if (leftmostleaf != null) (leftmostleaf as Terminal).Value += al;

        var ar = (((tree as NonTerminal).RightNode) as Terminal).Value;
        if (rightmostleaf != null) (rightmostleaf as Terminal).Value += ar;

        tree = new Terminal(tree.Depth, 0);
        return true;
    }
    else
    {
        var ex = Explode(ref (tree as NonTerminal).LeftNode, leftmostleaf, LeftMost((tree as NonTerminal).RightNode));
        if (ex == false) ex = Explode(ref (tree as NonTerminal).RightNode, RightMost((tree as NonTerminal).LeftNode), rightmostleaf);
        return ex;
    }

}

INode Add(INode tree, INode add)
{
    AddDepth(tree);
    AddDepth(add);
    return new NonTerminal(0, tree, add);
}

void AddDepth(INode tree)
{
    tree.Depth++;
    if (tree.GetType() == typeof(NonTerminal))
    {
        AddDepth(((NonTerminal)tree).LeftNode);
        AddDepth(((NonTerminal)tree).RightNode);
    }
}
(INode, int) Parse(string? line, int depth)
{

    if (char.IsDigit(line[0]))
    {
        return (new Terminal(depth, line[0] - '0'), 1);
    }
    else
    {
        var consumed = 1; // [
        var (leftpair, c) = Parse(line.Substring(consumed), depth + 1);
        consumed += c;
        consumed++; //,
        var (rightpair, c2) = Parse(line.Substring(consumed), depth + 1);
        consumed += c2;
        consumed++; //]
        return (new NonTerminal(depth, leftpair, rightpair), consumed);
    }

}

public interface INode
{
    int Depth { get; set; }
    string ToString();
    INode Clone();
}


public class Terminal : INode
{
    public int Value;
    public int Depth { get; set; }

    public Terminal(int depth, int value)
    {
        Depth = depth;
        Value = value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public INode Clone()
    {
        return new Terminal(Depth, Value);
    }
}

public class NonTerminal : INode
{
    public INode LeftNode;
    public INode RightNode;
    public int Depth { get; set; }

    public NonTerminal(int depth, INode leftpair, INode rightpair)
    {
        Depth = depth;
        LeftNode = leftpair;
        RightNode = rightpair;


    }
    public override string ToString()
    {

        return "[" + LeftNode.ToString() + "," + RightNode.ToString() + "]";
    }

    public INode Clone()
    {
        return new NonTerminal(Depth, LeftNode.Clone(), RightNode.Clone());
    }
}

