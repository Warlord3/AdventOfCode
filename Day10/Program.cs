
List<string> lines = [];

string file = Environment.CurrentDirectory + "/input.txt";
//string file = Environment.CurrentDirectory + "/example.txt";
using(StreamReader reader = new StreamReader(file))
{
    while(!reader.EndOfStream)
    {
        lines.Add(reader.ReadLine()!);
    }
}
int[][] inputData = new int[lines[0].Length][];
for(int i = 0; i < lines.Count; i++)
{
    inputData[i] = lines[i].Select(x => x - '0').ToArray();
}
int maxX = inputData.Length;
int maxY = inputData[0].Length;
long totalScore = 0;
long totalRating = 0;
HashSet<(int, int)> heads = [];
for(int x = 0; x < inputData.Length; x++)
{
    for(int y = 0; y < inputData[x].Length; y++)
    {
        if(inputData[x][y] == 0)
        {
            heads.Clear();
            FindHeads(x, y, 0);
            totalScore += heads.Count;
            totalRating += GetRating(x, y, 0);
        }
    }
}
Console.WriteLine(totalScore);
Console.WriteLine(totalRating);

void FindHeads(int x, int y, int height)
{
    if(inputData[x][y] == 9)
    {
        heads.Add((x, y));
    }
    //Up
    if(x - 1 >= 0 && inputData[x - 1][y] - height == 1)
    {
        FindHeads(x - 1, y, height + 1);
    }
    //Down
    if(x + 1 < maxX && inputData[x + 1][y] - height == 1)
    {
        FindHeads(x + 1, y, height + 1);
    }
    //Left
    if(y - 1 >= 0 && inputData[x][y - 1] - height == 1)
    {
        FindHeads(x, y - 1, height + 1);
    }
    //Right
    if(y + 1 < maxY && inputData[x][y + 1] - height == 1)
    {
        FindHeads(x, y + 1, height + 1);
    }
}


int GetRating(int x, int y, int height)
{
    int score = 0;
    if(inputData[x][y] == 9)
    {
        return 1;
    }
    //Up
    if(x - 1 >= 0 && inputData[x - 1][y] - height == 1)
    {
        score += GetRating(x - 1, y, height + 1);
    }
    //Down
    if(x + 1 < maxX && inputData[x + 1][y] - height == 1)
    {
        score += GetRating(x + 1, y, height + 1);
    }
    //Left
    if(y - 1 >= 0 && inputData[x][y - 1] - height == 1)
    {
        score += GetRating(x, y - 1, height + 1);
    }
    //Right
    if(y + 1 < maxY && inputData[x][y + 1] - height == 1)
    {
        score += GetRating(x, y + 1, height + 1);
    }
    return score;
}