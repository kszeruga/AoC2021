using System.ComponentModel.DataAnnotations;
using System.Formats.Asn1;
using System.Linq.Expressions;
using System.Xml;

var map = new int[250, 250];
var nmap = new int[250, 250];
int si = 55;
int sj = 55;
for (int ii = 0; ii < 250; ii++)
    for (int jj = 0; jj < 250; jj++)
    {
        map[ii, jj] = Int32.MaxValue;
        nmap[ii, jj] = Int32.MaxValue;
    }

var rl = Console.ReadLine();
var rule = rl.Select((c, i1) =>
    {
        return (c, i1);
    })
    .ToDictionary(tuple =>
    {
        return tuple.i1;
    },
        tuple =>
        {
            return tuple.c == '#' ? 1 : 0;
        });

Console.ReadLine();
string l;
var li = si;
while (!string.IsNullOrEmpty(l = Console.ReadLine()))
{
    var j = sj;
    l.ToList().ForEach(c =>
    {
        map[li, j] = c == '#' ? 1 : 0;
        j++;
    });
    li++;
}

var infinity = 0;
for (var r = 0; r < 25; r++)
{
    ApplyRule(map, nmap);
//debugmap(nmap, 20);
    ApplyRule(nmap, map);
} //debugmap(map, 20);

void debugmap(int[,] mapp, int v)
{
    System.Diagnostics.Debug.WriteLine("");
    for (int i = 0; i < v; i++)
    {
        for (int j = 0; j < v; j++)
        {
            System.Diagnostics.Debug.Write(inf(mapp[i,j])==1?'#':' ');
        }
        System.Diagnostics.Debug.WriteLine("");
    }
};
var sum = 0;
for (var x = 1; x < 249; x++)
    for (var j = 1; j < 249; j++)
        sum += inf(map[x, j]);

System.Diagnostics.Debug.WriteLine(sum);



void ApplyRule(int[,] m, int[,] n)
{

    for (var i = 1; i < 249; i++)
        for (var j = 1; j < 249; j++)
        {
            if (m[i - 1, j - 1] == int.MaxValue &&
                m[i - 1, j] == int.MaxValue &&
                m[i - 1, j + 1] == int.MaxValue &&
                m[i, j - 1] == int.MaxValue &&
                m[i, j] == int.MaxValue &&
                m[i, j + 1] == int.MaxValue &&
                m[i + 1, j - 1] == int.MaxValue &&
                m[i + 1, j] == int.MaxValue &&
                m[i + 1, j + 1] == int.MaxValue)
            {
                n[i, j] = int.MaxValue;
            }
            else
            {
                n[i, j] = rule[
                    inf(m[i - 1, j - 1]) * 256 +
                    inf(m[i - 1, j]) * 128 +
                    inf(m[i - 1, j + 1]) * 64 +
                    inf(m[i, j - 1]) * 32 +
                    inf(m[i, j]) * 16 +
                    inf(m[i, j + 1]) * 8 +
                    inf(m[i + 1, j - 1]) * 4 +
                    inf(m[i + 1, j]) * 2 +
                    inf(m[i + 1, j + 1]) * 1];
            }

        }

    infinity = 1 - infinity;
}

int inf(int val)

{
    if (val == int.MaxValue) return infinity;
    return val;
}