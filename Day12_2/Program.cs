using System.Diagnostics.Metrics;
using System.Linq;
using Microsoft.VisualBasic;

string line;
UndirectedGraph g = new UndirectedGraph();
while (!string.IsNullOrEmpty(line = System.Console.ReadLine()))
{
    var a = line.Split('-');
    g.AddEdge(a.First(), a.Last());
}

var counter = 0;
GetAllPaths("start", "end");
System.Console.WriteLine(counter);


void GetAllPaths(string s, string d)
{
    var isVisited = new Dictionary<string, int>();
    foreach (var n in g.GetNodes())
    {
        isVisited[n] = 0;
    }
    List<string> pathList = new List<string>();

    // add source to path[]
    pathList.Add(s);

    // Call recursive utility
    printAllPathsUtil(s, d, isVisited, pathList);
}

void printAllPathsUtil(string u, string d,
    Dictionary<string, int> isVisited,
    List<string> localPathList)
{

    if (u.Equals(d))
    {
        //Console.WriteLine(string.Join(" ", localPathList));
        ++counter;
        // if match found then no need
        // to traverse more till depth
        return;
    }

    // Mark the current node
    isVisited[u]++;

    // Recur for all the vertices
    // adjacent to current vertex
    foreach (string i in g.GetAdjacency(u))
    {
        if (CanBeVisited(i,isVisited))
        {
            // store current node
            // in path[]
            localPathList.Add(i);
            printAllPathsUtil(i, d, isVisited,
                localPathList);

            // remove current node
            // in path[]
            localPathList.RemoveAt(localPathList.Count - 1);
        }
    }
    // Mark the current node
    isVisited[u]--;
}

bool CanBeVisited(string i, Dictionary<string, int> isVisited)
{
    var ret= char.IsUpper(i.First()) ||
           isVisited[i] == 0 ||
           (isVisited[i] == 1 && (isVisited.Count(pair => char.IsLower(pair.Key.First()) && pair.Value ==2)<1) &&  (i!="end") && (i!="start"));
    return ret;
}