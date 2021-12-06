// See https://aka.ms/new-console-template for more information

var numbers = Console.ReadLine()!.Split(',').Select(int.Parse);
var bingos = new List<int[][]>();

string line;
int counter = 0;
var bingo = new int[5][];

while ((line = Console.ReadLine()!) != null)
{
    if (string.IsNullOrEmpty(line)) continue;
    bingo[counter] = line.Split(' ').Where(s =>!string.IsNullOrWhiteSpace(s)).Select(int.Parse).ToArray();
    counter++;
    if (counter > 4)
    {
        counter = 0;
        bingos.Add(bingo);
        bingo = new int[5][];
    }
}

foreach (var number in numbers)
{
    var wins = new List<int[][]>();
    foreach (var b in bingos)
    {
        if (CheckBingo(b, number))
        {
            if (bingos.Count > 1)
            {
                wins.Add(b);
            }
            else
            {
                System.Console.WriteLine(Multi(b)*number);
                System.Console.ReadLine();
            }
        }
    }

    foreach (var w in wins)
    {
        bingos.Remove(w);
    }
}

long Multi(int[][] card)
{
    var result = 0;
    for (var i = 0; i < 5; i++)
    for (var j = 0; j < 5; j++)
    {
            result += card[i][j];
    }

    return result;
}

bool CheckBingo(int[][] card, int number)
{
    for (var i = 0; i < 5; i++)
    for (var j = 0; j < 5; j++)
    {
        if (card[i][j] == number)
        {
            card[i][j] = 0;
        }
    }

    for (var i = 0; i < 5; i++)
    {
        if (card[i][0] == 0 && card[i][1] == 0 && card[i][2] == 0 && card[i][3] == 0 && card[i][4] == 0)
            return true;
        if (card[0][i] == 0 && card[1][i] == 0 && card[2][i] == 0 && card[3][i] == 0 && card[4][i] == 0)
            return true;

    }

    return false;
}