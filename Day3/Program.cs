


using System.Text.RegularExpressions;

var input = File.ReadAllText("E:\\Projekte\\AdventOfCode\\Day3\\input.txt");

var result1 = 0;
Regex regex = new Regex("mul\\((\\d{1,3}),(\\d{1,3})\\)", RegexOptions.Compiled);

foreach(Match match in regex.Matches(input))
{
    int first = int.Parse(match.Groups[1].Value);
    int second = int.Parse(match.Groups[2].Value);
    result1 += first * second;
}
Console.WriteLine(result1);

var result2 = 0;
bool enabled = true;
regex = new Regex("mul\\((\\d{1,3}),(\\d{1,3})\\)|(do\\(\\))|(don't\\(\\))", RegexOptions.Compiled);
foreach(Match match in regex.Matches(input))
{
    if(match.Groups[3].Value == "do()")
    {
        enabled = true;
    }
    else if(match.Groups[4].Value == "don't()")
    {
        enabled = false;
    }
    else
    {
        if(!enabled)
            continue;
        int first = int.Parse(match.Groups[1].Value);
        int second = int.Parse(match.Groups[2].Value);
        result2 += first * second;
    }
}
Console.WriteLine(result2);