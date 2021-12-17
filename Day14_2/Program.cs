const int N = 40;

var line = System.Console.ReadLine();
System.Console.ReadLine();
string s;
var rules = new Dictionary<string, string>();
while (!string.IsNullOrEmpty(s = Console.ReadLine()))
{
    var c = s.Split(' ').First();
    rules[c] = c[0] + s.Split(' ').Last() + c[1];
}

var storage = new Dictionary<(string, int),Dictionary<char,long>>();
foreach (var rule in rules)
{
    Compute(rule.Key);
}

var freq = new Dictionary<char, long>();
for(var i=0;i<line.Length-1; i++)
{
    foreach (var c in storage[(line.Substring(i, 2), N)])
    {
        if (!freq.ContainsKey(c.Key)) freq[c.Key] = 0;
        freq[c.Key] += c.Value;
    }
}

for (var i = 1; i < line.Length - 1; i++)
{
    freq[line[i]] -= 1;
}

Console.WriteLine(freq.Values.Max()-freq.Values.Min());

void Compute(string key)
{
    storage[(key,N)]=ComputeRec(key, N);
}

Dictionary<char, long> ComputeRec(string key, int v)
{
    if (v == 0)
    {
        if (key[0] == key[1])
        {
            var d = new Dictionary<char, long>()
            {
                { key[0], 2 },
            };
            storage[(key, 0)] = d;
            return d;

        }
        else
        {
            var d = new Dictionary<char, long>()
            {
                { key[0], 1 },
                { key[1], 1 }

            };
            storage[(key, 0)] = d;
            return d;

        }
    }
    if (storage.ContainsKey((key, v))) return storage[(key, v)];
    else
    {
        if (!rules.ContainsKey(key))
        {
            var dic =ComputeRec(key, v - 1);
            storage[(key, v)] = dic;
            return dic;
        }
        var dic1 = ComputeRec(rules[key].Substring(0, 2), v - 1);
        var dic2 = ComputeRec(rules[key].Substring(1, 2), v - 1);
        var AllKeys = dic1.Keys.Union(dic2.Keys);
        var dic3 = AllKeys.ToDictionary(k1 => k1, k2 => (dic1.Keys.Contains(k2) ? dic1[k2] : 0) + (dic2.Keys.Contains(k2) ? dic2[k2] : 0));
        dic3[rules[key][1]] -= 1;
        storage[(key, v)] = dic3;
        return dic3;
    }
}