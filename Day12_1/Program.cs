using System.Diagnostics.Metrics;
using Microsoft.VisualBasic;

string line;
UndirectedGraph g = new UndirectedGraph();
while (!string.IsNullOrEmpty(line = System.Console.ReadLine()))
{
    var a = line.Split('-');
    g.AddEdge(a.First(),a.Last());
}

var counter = 0;
GetAllPaths("start","end");
System.Console.WriteLine(counter);


void GetAllPaths(string s, string d)
{
    var isVisited = new Dictionary<string, bool>();
    List<string> pathList = new List<string>();

    // add source to path[]
    pathList.Add(s);

    // Call recursive utility
    printAllPathsUtil(s, d, isVisited, pathList);
}

void printAllPathsUtil(string u, string d,
    Dictionary<string, bool> isVisited,
    List<string> localPathList)
{

    if (u.Equals(d))
    {
        ++counter;
        // if match found then no need
        // to traverse more till depth
        return;
    }

    // Mark the current node
     isVisited[u] = true;

    // Recur for all the vertices
    // adjacent to current vertex
    foreach (string i in g.GetAdjacency(u))
    {
        var v = isVisited.ContainsKey(i) && isVisited[i];
        if (!v || char.IsUpper(i.First()))
        {
            // store current node
            // in path[]
            localPathList.Add(i);
            printAllPathsUtil(i, d, isVisited,
                localPathList);

            // remove current node
            // in path[]
            localPathList.RemoveAt(localPathList.Count-1);
        }
    }
    // Mark the current node
    isVisited[u] = false;
}