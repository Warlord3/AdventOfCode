List<int> left = [];
List<int> right = [];
using(StreamReader reader = new StreamReader("E:\\Projekte\\AdventOfCode\\Day1\\input.txt"))
{
    while(!reader.EndOfStream)
    {
        var text = reader.ReadLine();
        var split = text?.Split(" ") ?? [];
        left.Add(int.Parse(split[0]));
        right.Add(int.Parse(split[^1]));
    }
}
left.Sort();
right.Sort();
long result1 = 0;
for(int i = 0; i < left.Count; i++)
{
    result1 += Math.Abs(right[i] - left[i]);
}
Console.WriteLine(result1); //1651298

var leftHash = new HashSet<int>(left).ToList();
var dict = new Dictionary<int, int>();
for(int i = 0; i < leftHash.Count; i++)
{
    dict.Add(leftHash[i], right.Count(x => x == leftHash[i]));
}

var result2 = 0;

foreach(var entry in left)
{
    result2 += entry * dict[entry];
}
Console.WriteLine(result2); //21306195

