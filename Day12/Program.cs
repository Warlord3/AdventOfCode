
string file = Environment.CurrentDirectory + "/input.txt";
//string file = Environment.CurrentDirectory + "/example.txt";
List<string> lines = File.ReadAllText(file).Replace("\r", "").Split("\n").ToList();

char[][] garden = new char[lines.Count][];
char[][] gardenSearched = new char[lines.Count][];

for(int i = 0; i < lines.Count; i++)
{
    garden[i] = lines[i].ToCharArray();
    gardenSearched[i] = garden[i].ToArray();
}

int maxX = garden.Length;
int maxY = garden[0].Length;

List<Dictionary<(int, int), char>> plantGroups = [];

for(int x = 0; x < maxX; x++)
{
    for(int y = 0; y < maxY; y++)
    {
        if(gardenSearched[x][y] == '0')
        {
            continue;
        }
        plantGroups.Add([]);
        searchField(garden[x][y], x, y);
    }
}

int price = 0;
int price2 = 0;
foreach(var plantField in plantGroups)
{
    int area = 0;
    int perimeter = 0;
    var value = plantField.First().Value;
    int x, y;
    foreach(var plant in plantField)
    {
        x = plant.Key.Item1;
        y = plant.Key.Item2;
        area++;

        if(x - 1 < 0 || garden[x - 1][y] != value)
        {
            perimeter++;
        }
        if(y - 1 < 0 || garden[x][y - 1] != value)
        {
            perimeter++;
        }
        if(x + 1 >= maxX || garden[x + 1][y] != value)
        {
            perimeter++;
        }
        if(y + 1 >= maxY || garden[x][y + 1] != value)
        {
            perimeter++;
        }
    }
    (x, y) = plantField.First().Key;
    int startX = x;
    int startY = y;
    Direction direction = Direction.Right;
    Direction newDirection;
    bool finshed = false;
    int sides = 1;
    while(!finshed)
    {
        switch(direction)
        {
            case Direction.Up:
                if(y - 1 >= 0 && garden[x][y - 1] == value)
                {
                    y--;
                    sides++;
                    direction = Direction.Left;
                }
                else if(x - 1 >= 0 && garden[x - 1][y] == value)
                {
                    x--;
                }
                else if(y + 1 < maxY && garden[x][y + 1] == value)
                {
                    y++;
                    sides++;
                    direction = Direction.Right;
                }
                else
                {
                    sides++;
                    direction = Direction.Right;
                }
                break;
            case Direction.Right:
                if(x - 1 >= 0 && garden[x - 1][y] == value)
                {
                    x--;
                    sides++;
                    direction = Direction.Up;
                }
                else if(y + 1 < maxY && garden[x][y + 1] == value)
                {
                    y++;
                }
                else if(x + 1 < maxX && garden[x + 1][y] == value)
                {
                    x++;
                    sides++;
                    direction = Direction.Down;
                }
                else
                {
                    sides++;
                    direction = Direction.Down;
                }
                break;
            case Direction.Down:
                if(y + 1 < maxY && garden[x][y + 1] == value)
                {
                    y++;
                    sides++;
                    direction = Direction.Right;
                }
                else if(x + 1 < maxX && garden[x + 1][y] == value)
                {
                    x++;
                }
                else if(y - 1 >= 0 && garden[x][y - 1] == value)
                {
                    y--;
                    sides++;
                    direction = Direction.Left;
                }
                else
                {
                    sides++;
                    direction = Direction.Left;
                }
                break;
            case Direction.Left:
                if(x + 1 < maxX && garden[x + 1][y] == value)
                {
                    x++;
                    sides++;
                    direction = Direction.Down;
                }
                else if(y - 1 >= 0 && garden[x][y - 1] == value)
                {
                    y--;
                }
                else if(x - 1 >= 0 && garden[x - 1][y] == value)
                {
                    x--;
                    sides++;
                    direction = Direction.Up;
                }
                else
                {
                    sides++;
                    direction = Direction.Up;
                }
                break;
            default:
                break;
        }
        if(x == startX && y == startY)
        {
            if(direction == Direction.Up)
            {
                finshed = true;
            }
        }
    }
    Draw(garden, value);
    var temp = area * perimeter;
    price += temp;
    temp = area * sides;
    Console.WriteLine($"{value}: {area} {sides} = {temp}");
    price2 += temp;
}

Console.WriteLine(price);
Console.WriteLine(price2);

void Draw(char[][] garden, char value)
{
    string output = "";
    for(int i = 0; i < garden.Length; i++)
    {
        for(int j = 0; j < garden[i].Length; j++)
        {
            if(garden[i][j] == value)
            {
                output += value;
            }
            else
            {
                output += ".";
            }
        }
        output += "\n";
    }
    File.WriteAllText(Environment.CurrentDirectory + "/output.txt", output);
}

void searchField(char plant, int x, int y)
{
    if(plant == gardenSearched[x][y])
    {
        gardenSearched[x][y] = '0';
        plantGroups[^1].Add((x, y), plant);
    }
    if(x - 1 >= 0 && gardenSearched[x - 1][y] == plant)
    {
        searchField(plant, x - 1, y);
    }
    if(y - 1 >= 0 && gardenSearched[x][y - 1] == plant)
    {
        searchField(plant, x, y - 1);
    }
    if(x + 1 < maxX && gardenSearched[x + 1][y] == plant)
    {
        searchField(plant, x + 1, y);
    }
    if(y + 1 < maxY && gardenSearched[x][y + 1] == plant)
    {
        searchField(plant, x, y + 1);
    }
}

enum Direction
{
    Up,
    Right,
    Down,
    Left,
}