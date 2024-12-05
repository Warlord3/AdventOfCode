List<List<int>> levels = [];
using(StreamReader reader = new StreamReader("E:\\Projekte\\AdventOfCode\\Day2\\input.txt"))
{
    while(!reader.EndOfStream)
    {
        var text = reader.ReadLine();
        var split = text?.Split(" ").ToList() ?? [];
        var level = split.Select(x => int.Parse(x)).ToList();
        levels.Add(level);
    }
}

int result1 = 0;
int result2 = 0;
for(int i = 0; i < levels.Count; i++)
{
    if(IsSafe(levels[i]))
    {
        result1++;
    }
    if(IsSafe2(levels[i]))
    {
        result2++;
    }
}
Console.WriteLine(result1); // 549
Console.WriteLine(result2); // 589
bool IsSafe(List<int> level)
{
    if(!forward(level))
    {
        for(int i = 0; i < level.Count - 1; i++)
        {
            if(level[i + 1] > level[i])
                return false;

            var distance = level[i] - level[i + 1];
            if(distance < 1 || distance > 3)
                return false;
        }
    }
    else
    {
        for(int i = 0; i < level.Count - 1; i++)
        {
            if(level[i + 1] < level[i])
                return false;

            var distance = level[i + 1] - level[i];
            if(distance < 1 || distance > 3)
                return false;
        }
    }
    return true;
}

bool IsSafe2(List<int> level)
{
    var fault = 0;
    if(!forward(level))
    {
        var current = level[0];
        for(int i = 0; i < level.Count - 1; i++)
        {
            if(level[i + 1] >= current)
            {
                if(fault > 0)
                {
                    return false;
                }
                fault++;
                continue;
            }
            var distance = current - level[i + 1];
            if(distance < 1 || distance > 3)
            {

                if(fault > 0)
                {
                    return false;
                }
                fault++;
            }

            current = level[i + 1];
        }
    }
    else
    {
        var current = level[0];
        for(int i = 0; i < level.Count - 1; i++)
        {
            if(level[i + 1] <= current)
            {
                if(fault > 0)
                {
                    return false;
                }
                fault++;
                continue;
            }
            var distance = level[i + 1] - current;

            if(distance < 1 || distance > 3)
            {
                if(fault > 0)
                {
                    return false;
                }
                fault++;
            }
            current = level[i + 1];
        }
    }
    return true;
}

bool forward(List<int> level)
{
    int forward = 0;
    int backward = 0;
    for(int i = 0; i < level.Count - 1; i++)
    {
        if(level[i] < level[i + 1])
        {
            forward += 1;
        }
        else if(level[i] > level[i + 1])
        {
            backward += 1;
        }
    }
    return forward >= backward;
}