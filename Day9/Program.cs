string sequence = File.ReadAllText(Environment.CurrentDirectory + "/input.txt");
//string sequence = File.ReadAllText(Environment.CurrentDirectory + "/example.txt");


LinkedList<int> filesStack = new();

bool AppendFile = true;
int id = 0;
foreach(var character in sequence)
{
    int number = character - '0';
    if(AppendFile)
    {
        for(int i = 0; i < number; i++)
        {
            filesStack.AddLast(id);
        }
        id++;
    }
    else
    {
        for(int i = 0; i < number; i++)
        {
            filesStack.AddLast(-1);
        }
    }
    AppendFile = !AppendFile;
}
List<int> newString = [];
int index = 0;
while(filesStack.Count > 0)
{
    var temp = filesStack.First.Value;
    filesStack.RemoveFirst();
    if(temp == -1)
    {
        newString.Add(GetLast());
    }
    else
    {
        newString.Add(temp);
    }
}

long result = 0;
for(int i = 0; i < newString.Count; i++)
{
    result += i * newString[i];
}
Console.WriteLine(result);


// Part2
// (size,id)
List<(int, int)> data = [];
AppendFile = true;
id = 0;
foreach(var character in sequence)
{
    int number = character - '0';
    if(AppendFile)
    {
        data.Add((number, id));
        id++;
    }
    else
    {
        data.Add((number, -1));
    }
    AppendFile = !AppendFile;
}
var x = data.Count;
while(x > 0)
{
    x--;
    if(data[x].Item2 == -1)
    {
        continue;
    }
    for(int j = 0; j < x; j++)
    {
        if(data[j].Item2 == -1 && data[x].Item1 <= data[j].Item1)
        {
            var restSize = data[j].Item1 - data[x].Item1;
            data[j] = data[x];
            data[x] = (data[x].Item1, -1);
            if(restSize > 0)
            {
                x++;
                data.Insert(j + 1, (restSize, -1));
            }
            break;
        }
    }
}

newString.Clear();
foreach(var item in data)
{
    if(item.Item2 == -1)
    {
        for(int i = 0; i < item.Item1; i++)
        {
            newString.Add(0);
        }
    }
    else
    {
        for(int i = 0; i < item.Item1; i++)
        {
            newString.Add(item.Item2);
        }
    }
}
result = 0;
for(int i = 0; i < newString.Count; i++)
{
    result += i * newString[i];
}
Console.WriteLine(result);
int GetLast()
{
    int x = 0;
    while((x = filesStack.Last.Value) == -1)
    {
        filesStack.RemoveLast();
    }
    filesStack.RemoveLast();
    return x;
}
