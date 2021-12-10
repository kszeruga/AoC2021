var list = System.Console.ReadLine().Split(',').Select(int.Parse);
var median = list.OrderBy(x=>x).ElementAt(list.Count()/2);
System.Diagnostics.Debug.WriteLine(list.Sum(x => Math.Abs(x - median)));
