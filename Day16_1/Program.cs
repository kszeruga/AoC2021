using System.Collections;
using System.Diagnostics.Metrics;
using System.Globalization;

var input = Console.ReadLine();
var bytes = new byte[input.Length/2+1];
for (int i = 0; i < input.Length; i += 2)
{
    bytes[i / 2] = reverse(byte.Parse(input.Substring(i, (i+1)>=input.Length?1:2), NumberStyles.HexNumber));
}
var b = new BitArray(bytes);
Packet packet = Parse(b,0, out var x);


System.Console.WriteLine(GetVersion(packet));
System.Diagnostics.Debug.WriteLine(GetVersion(packet));

int GetVersion(Packet pt)
{
    if (pt.subPackets != null) return pt.subPackets.Sum(GetVersion) + pt.version;
    return pt.version;
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

string ParseLitNumber(BitArray array, int from, out int read)
{
    var val = "";
    var cont = true;
    var counter = from;
    while (cont)
    {
        string s;
        (s, cont) = ParseGroup(array,counter);
        counter += 5;
        val += s;
    }

    read = counter-from;
    return val;
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
    public string value;
    public int lenghttypeid;
    public int length;
    public List<Packet> subPackets;
}