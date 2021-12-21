var (a, b) = Wins(0, 0, 8, 2, true);

System.Console.WriteLine(a);
System.Console.WriteLine(b);
var cache = new Tuple<long, long>[31, 31, 11, 11, 2];

(long, long) Wins(int score1, int score2, int p1, int p2, bool p1tomove)
{
    if (score1 >= 21) return (1, 0);
    if (score2 >= 21) return (0, 1);

    if (p1tomove)
    {
        var np1 = new int[10];
        var a = new long[10];
        var b = new long[10];
        //roll 3
                
        for (int i = 3; i <= 9; i++)
        {
            np1[i] = p1 + i;
            np1[i]= (np1[i] - 1) % 10 + 1;
            (a[i],b[i]) = Wins(score1 + np1[i], score2, np1[i], p2, !p1tomove);
        }

        return (1 * a[3] + 3 * a[4] + 6 * a[5] + 7 * a[6] + 6 * a[7] + 3 * a[8] + 1 * a[9],
            1 * b[3] + 3 * b[4] + 6 * b[5] + 7 * b[6] + 6 * b[7] + 3 * b[8] + 1 * b[9]);
    }
    else
    {
        var np2 = new int[10];
        var a = new long[10];
        var b = new long[10];
        //roll 3

        for (int i = 3; i <= 9; i++)
        {
            np2[i] = p2 + i;
            np2[i] = (np2[i] - 1) % 10 + 1;
            (a[i], b[i]) = Wins(score1, score2 + np2[i], p1, np2[i], !p1tomove);
        }

        return (1 * a[3] + 3 * a[4] + 6 * a[5] + 7 * a[6] + 6 * a[7] + 3 * a[8] + 1 * a[9],
            1 * b[3] + 3 * b[4] + 6 * b[5] + 7 * b[6] + 6 * b[7] + 3 * b[8] + 1 * b[9]);

    }
}