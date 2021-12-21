// See https://aka.ms/new-console-template for more information

var d = new Die();
var p1 = 8;
var p2 = 2;
var score1 = 0;
var score2 = 0;

while (true)
{
    p1 += d.Get3Roll();
    p1 = (p1 - 1) % 10 + 1;
    score1 += p1;
    if (score1 >= 1000) break;

    p2 += d.Get3Roll();
    p2 = (p2 - 1) % 10 + 1;
    score2 += p2;
    if (score2 >= 1000) break;
}

System.Console.WriteLine(d.GetRolls() * Math.Min(score1,score2));

public class Die
{
    private int _next = 1;
    private int rolls=0;
    public int Get3Roll()
    {
        return GetAndInc() + GetAndInc() + GetAndInc();
    }

    public int GetRolls() => rolls;
    private int GetAndInc()
    {
        ++rolls;
        var r = _next;
        _next++; if (_next >100 ) _next = 1;
        return r;
    }
}