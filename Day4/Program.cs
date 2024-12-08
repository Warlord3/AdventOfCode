

List<List<char>> input = [];
using(StreamReader reader = new StreamReader(Environment.CurrentDirectory + "/input.txt"))
{
    while(!reader.EndOfStream)
    {
        var inputChar = reader.ReadLine();
        input.Add(inputChar.ToCharArray().ToList());

    }
}
var result = 0;

for(int row = 0; row < input.Count; row++)
{
    for(int col = 0; col < input[row].Count - 3; col++)
    {
        if(input[row][col] == 'X' && input[row][col + 1] == 'M' && input[row][col + 2] == 'A' && input[row][col + 3] == 'S')
        {
            result++;
        }
        else if(input[row][col] == 'S' && input[row][col + 1] == 'A' && input[row][col + 2] == 'M' && input[row][col + 3] == 'X')
        {
            result++;
        }
    }
}

for(int col = 0; col < input.Count; col++)
{
    for(int row = 0; row < input[col].Count - 3; row++)
    {
        if(input[row][col] == 'X' && input[row + 1][col] == 'M' && input[row + 2][col] == 'A' && input[row + 3][col] == 'S')
        {
            result++;
        }
        else if(input[row][col] == 'S' && input[row + 1][col] == 'A' && input[row + 2][col] == 'M' && input[row + 3][col] == 'X')
        {
            result++;
        }
    }
}
for(int row = 0; row < input.Count - 3; row++)
{
    for(int col = 0; col < input[row].Count - 3; col++)
    {
        if(input[row][col] == 'X' && input[row + 1][col + 1] == 'M' && input[row + 2][col + 2] == 'A' && input[row + 3][col + 3] == 'S')
        {
            result++;
        }
        else if(input[row][col] == 'S' && input[row + 1][col + 1] == 'A' && input[row + 2][col + 2] == 'M' && input[row + 3][col + 3] == 'X')
        {
            result++;
        }
    }
}
for(int row = 0; row < input.Count - 3; row++)
{
    for(int col = input[row].Count - 1; col > 2; col--)
    {
        if(input[row][col] == 'X' && input[row + 1][col - 1] == 'M' && input[row + 2][col - 2] == 'A' && input[row + 3][col - 3] == 'S')
        {
            result++;
        }
        else if(input[row][col] == 'S' && input[row + 1][col - 1] == 'A' && input[row + 2][col - 2] == 'M' && input[row + 3][col - 3] == 'X')
        {
            result++;
        }
    }
}
Console.WriteLine(result);
result = 0;
for(int row = 1; row < input.Count - 1; row++)
{
    for(int col = 1; col < input[row].Count - 1; col++)
    {
        if(input[row][col] == 'A')
        {
            var rightLeft = false;
            if(input[row - 1][col - 1] == 'M' && input[row + 1][col + 1] == 'S' || input[row - 1][col - 1] == 'S' && input[row + 1][col + 1] == 'M')
            {
                rightLeft = true;
            }
            var leftRight = false;
            if(input[row - 1][col + 1] == 'M' && input[row + 1][col - 1] == 'S' || input[row - 1][col + 1] == 'S' && input[row + 1][col - 1] == 'M')
            {
                leftRight = true;
            }
            if(rightLeft && leftRight)
            {
                result++;
            }
        }
    }
}
Console.WriteLine(result);