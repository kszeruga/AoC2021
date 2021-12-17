using System.Collections;
using System.Diagnostics.Metrics;
using System.Globalization;

var input = Console.ReadLine();
var bytes = new byte[input.Length / 2 + 1];
for (int i = 0; i < input.Length; i += 2)
{
    bytes[i / 2] = reverse(byte.Parse(input.Substring(i, (i + 1) >= input.Length ? 1 : 2), NumberStyles.HexNumber));
}
var b = new BitArray(bytes);
Packet packet = Parse(b, 0, out var x);


System.Console.WriteLine(GetValue(packet));
System.Diagnostics.Debug.WriteLine(GetValue(packet));

long GetValue(Packet pt)
{
    switch (pt.typeid)
    {
        case 0:
            return pt.subPackets.Sum(GetValue);
        case 1:
            return pt.subPackets.Aggregate(1L, (i, packet1) => i * GetValue(packet1));
        case 2:
            return pt.subPackets.Min(GetValue);
        case 3:
            return pt.subPackets.Max(GetValue);
        case 4:
            return pt.value;
        case 5:
            return GetValue(pt.subPackets[0]) > GetValue(pt.subPackets[1]) ? 1 : 0;
        case 6:
            return GetValue(pt.subPackets[0]) < GetValue(pt.subPackets[1]) ? 1 : 0;
        case 7:
            return (GetValue(pt.subPackets[0]) == GetValue(pt.subPackets[1])) ? 1 : 0;
        default:
            return 0;
    }
}

Packet Parse(BitArray array, int from, out int parsed)
{
    parsed = 0;
    var counter = from;
    var p = new Packet();
    p.version = ParseNumber(array, counter, 3);
    counter += 3;
    p.typeid = ParseNumber(array, counter, 3);
    counter += 3;
    if (p.typeid == 4)
    {
        p.value = ParseLitNumber(array, counter, out var read);
        counter += read;
    }
    else
    {
        p.lenghttypeid = ParseNumber(array, counter, 1);
        counter++;
        if (p.lenghttypeid == 0)
        {
            p.length = ParseNumber(array, counter, 15);
            p.subPackets = new List<Packet>();
            counter += 15;
            var readbytes = 0;
            do
            {
                var np = Parse(array, counter, out var length);
                readbytes += length;
                counter += length;
                p.subPackets.Add(np);

            } while (readbytes < p.length);
        }
        else
        {
            p.length = ParseNumber(array, counter, 11);
            counter += 11;
            p.subPackets = new List<Packet>();
            for (var i = 0; i < p.length; i++)
            {
                var np = Parse(array, counter, out var len);
                counter += len;
                p.subPackets.Add(np);
            }
        }
    }

    parsed = counter - from;
    return p;
}

long ParseLitNumber(BitArray array, int from, out int read)
{
    var val = "";
    var cont = true;
    var counter = from;
    while (cont)
    {
        string s;
        (s, cont) = ParseGroup(array, counter);
        counter += 5;
        val += s;
    }

    read = counter - from;
    return Convert.ToInt64(val,2);
}

(string s, bool cont) ParseGroup(BitArray array, int counter)
{
    var c = ParseNumber(array, counter, 1) == 1;
    var s = String.Join("", Enumerable.Range(counter + 1, 4).Select(i => array[i] ? "1" : "0"));
    return (s, c);
}

int ParseNumber(BitArray array, int from, int lenght)
{
    return Convert.ToInt32(String.Join("", Enumerable.Range(from, lenght).Select(i => array[i] ? "1" : "0")), 2);
}

byte reverse(byte b)
{
    b = (byte)((b & 0xF0) >> 4 | (b & 0x0F) << 4);
    b = (byte)((b & 0xCC) >> 2 | (b & 0x33) << 2);
    b = (byte)((b & 0xAA) >> 1 | (b & 0x55) << 1);
    return b;
}

public class Packet
{
    public int version;
    public int typeid;
    public long value;
    public int lenghttypeid;
    public int length;
    public List<Packet> subPackets;
}