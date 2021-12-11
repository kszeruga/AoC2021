using System.Net;

string line;
var lines = new List<string>();
while (!string.IsNullOrEmpty(line = System.Console.ReadLine())) lines.Add(line);
var X = lines.First().Length;
var Y = lines.Count();
var a = new int[X+2, Y+2];
for (var i = 1; i <= X; i++)
    for (var j = 1; j <= Y; j++)
        a[i, j] = lines[i - 1][j - 1]-'0';

long counter = 0;
for (var step = 0; step < 100; step++)
{
    for (var i = 1; i <= X; i++)
    for (var j = 1; j <= Y; j++)
        ++a[i, j];
    bool repeat;
    do
    {
        repeat = false;

        for (var i = 1; i <= X; i++)
        for (var j = 1; j <= Y; j++)
        {
            if (a[i, j] > 9)
            {
                repeat |= fire(i, j);
                counter++;
            }
        }
    } while (repeat);

    for (var i = 1; i <= X; i++)
    for (var j = 1; j <= Y; j++)
        if (a[i,j]<0) a[i,j] = 0;


}

System.Console.WriteLine(counter);

bool fire(int i, int j)
{
    a[i, j] = -1000;
    var ret = false;
    ret |= ++a[i - 1, j - 1]>9;
    ret |= ++a[i - 1, j ]>9;
    ret |= ++a[i - 1, j + 1]>9;
    ret |= ++a[i, j - 1]>9;
    ret |= ++a[i, j + 1]>9;
    ret |= ++a[i + 1, j - 1]>9;
    ret |= ++a[i + 1, j]>9;
    ret |= ++a[i + 1, j + 1]>9;
    return ret;
}