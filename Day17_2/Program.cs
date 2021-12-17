/// x=209..238, y = -86..-59
var counter = 0;
var list = new List<Tuple<int, int>>();

for (var vx = 0; vx < 239; vx++)
    for (var vy = -100; vy < 100; vy++)
    {
        if (InTarget(vx, vy))
        {
            counter++;

            list.Add(new Tuple<int, int>(vx, vy));
        }
    }
System.Console.WriteLine(counter);

bool InTarget(int vx, int vy)
{
    //const int xmin = 20;
    //const int xmax = 30;
    //const int ymin = -10;
    //const int ymax = -5;

    const int xmin = 209;
    const int xmax = 238;
    const int ymin = -86;
    const int ymax = -59;

    var x = 0;
    var y = 0;
    var dx = vx;
    var dy = vy;
    do
    {
        if (x >= xmin && x <= xmax && y >= ymin && y <= ymax) return true;
        if (x > xmax || y < ymin) return false;
        x += dx;
        y += dy;
        dx = (dx > 0) ? dx - 1 : dx;
        dy--;
    } while (true);
}
