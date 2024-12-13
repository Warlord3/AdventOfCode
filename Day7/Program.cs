List<(long, long[])> inputData = [];

using(StreamReader reader = new StreamReader(Environment.CurrentDirectory + "/input.txt"))
{
    while(!reader.EndOfStream)
    {
        var inputChar = reader.ReadLine();
        var split = inputChar.Split(':');
        var result = long.Parse(split[0]);
        var numbers = split[1][1..].Split(' ').Select(long.Parse).ToArray();
        inputData.Add((result, numbers));
    }
}

long calibrationResult = 0;
foreach(var input in inputData)
{
    for(int i = 0; i < (2 << input.Item2.Length - 1); i++)
    {
        long result = input.Item2[0];
        for(int j = 1; j < input.Item2.Length; j++)
        {
            if((i & (1 << j - 1)) > 0)
            {
                result += input.Item2[j];
            }
            else
            {
                result *= input.Item2[j];
            }
        }
        if(result == input.Item1)
        {
            calibrationResult += result;
            break;
        }
    }
}
Console.WriteLine(calibrationResult);
var calibrationResult2 = 0L;
foreach(var input in inputData)
{
    for(int i = 0; i < Math.Pow(3, input.Item2.Length); i += 3)
    {
        var temp = i;
        long result = input.Item2[0];
        for(int j = 1; j < input.Item2.Length; j++)
        {
            var power = (int)Math.Pow(3, input.Item2.Length - j);
            var test = temp / power;
            temp -= power * test;

            switch(test)
            {
                case 0:
                    result += input.Item2[j];
                    break;
                case 1:
                    result *= input.Item2[j];
                    break;
                case 2:
                    result = result * (long)(Math.Pow(10, input.Item2[j].DigitsLength())) + input.Item2[j];
                    break;
                default:
                    break;
            }
        }
        if(result == input.Item1)
        {
            calibrationResult2 += result;
            break;
        }
    }
}
Console.WriteLine(calibrationResult2);


public static class Extension
{
    public static int DigitsLength(this long n)
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
