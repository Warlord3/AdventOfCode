
string file = Environment.CurrentDirectory + "/input.txt";
//string file = Environment.CurrentDirectory + "/example.txt";
string line = File.ReadAllText(file);

List<long> stones = line.Split(" ").Select(long.Parse).ToList();

Dictionary<(long, int), long> chache = [];

long result = 0;
foreach(long stone in stones)
{
    result++;
    result += Splitted(stone, 0, 75);
}
Console.WriteLine(result);

long Splitted(long number, int recursion, int recursionLimit)
{
    if(chache.ContainsKey((number, recursion)))
    {
        return chache[(number, recursion)];
    }
    recursion++;
    if(recursion > recursionLimit)
    {
        return 0;
    }
    long result = 0;
    if(number == 0)
    {
        result += Splitted(1, recursion, recursionLimit);
    }
    else if(number.Digits() % 2 == 0)
    {
        var digits = number.Digits();
        var mask = (long)Math.Pow(10, digits / 2);
        var leftSide = number / mask;
        var rightSide = number % mask;
        result += Splitted(leftSide, recursion, recursionLimit);
        result += Splitted(rightSide, recursion, recursionLimit);
        result++;
    }
    else
    {
        result += Splitted(number * 2024, recursion, recursionLimit);
    }
    chache.TryAdd((number, recursion - 1), result);
    return result;
}

public static class LongHelper
{

    public static int Digits(this long n)
    {
        if(n >= 0)
        {
            if(n < 10L) return 1;
            if(n < 100L) return 2;
            if(n < 1000L) return 3;
            if(n < 10000L) return 4;
            if(n < 100000L) return 5;
            if(n < 1000000L) return 6;
            if(n < 10000000L) return 7;
            if(n < 100000000L) return 8;
            if(n < 1000000000L) return 9;
            if(n < 10000000000L) return 10;
            if(n < 100000000000L) return 11;
            if(n < 1000000000000L) return 12;
            if(n < 10000000000000L) return 13;
            if(n < 100000000000000L) return 14;
            if(n < 1000000000000000L) return 15;
            if(n < 10000000000000000L) return 16;
            if(n < 100000000000000000L) return 17;
            if(n < 1000000000000000000L) return 18;
            return 19;
        }
        else
        {
            if(n > -10L) return 2;
            if(n > -100L) return 3;
            if(n > -1000L) return 4;
            if(n > -10000L) return 5;
            if(n > -100000L) return 6;
            if(n > -1000000L) return 7;
            if(n > -10000000L) return 8;
            if(n > -100000000L) return 9;
            if(n > -1000000000L) return 10;
            if(n > -10000000000L) return 11;
            if(n > -100000000000L) return 12;
            if(n > -1000000000000L) return 13;
            if(n > -10000000000000L) return 14;
            if(n > -100000000000000L) return 15;
            if(n > -1000000000000000L) return 16;
            if(n > -10000000000000000L) return 17;
            if(n > -100000000000000000L) return 18;
            if(n > -1000000000000000000L) return 19;
            return 20;
        }
    }
}
