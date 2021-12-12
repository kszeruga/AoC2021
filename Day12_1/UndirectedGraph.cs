using System.Text;

public class UndirectedGraph
{

    // array of adjacency lists
    private Dictionary<string,List<String>> _adj;

    public UndirectedGraph()
    {

        // create array of lists
        // initialise all lists to empty
        this._adj = new Dictionary<string, List<string>>();
    }

    
    public void AddEdge(string v, string w)
    {
        if (!_adj.ContainsKey(v)) _adj[v] = new List<string>();
        if (!_adj.ContainsKey(w)) _adj[w] = new List<string>();
        // add to adjacency lists
        this._adj[v].Add(w);
        this._adj[w].Add(v);
        
    }

    /// <summary>
    /// Get an adjacency list for a vertex.
    /// </summary>
    /// <param name="v">The vertex.</param>
    /// <returns></returns>
    public List<string> GetAdjacency(string v)
    {
        return _adj[v];
    }

    /// <summary>
    /// Get a string representation of the graph's adjacency lists.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendLine("");
        foreach (var v in _adj)
        {
            builder.Append(v.Key + ": ");
            foreach (var w in v.Value)
            {
                builder.Append(w + " ");
            }
            builder.AppendLine(string.Empty);
        }

        return builder.ToString();
    }
}