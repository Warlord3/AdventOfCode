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

int sizeX = garden.Length;
int sizeY = garden[0].Length;

List<Dictionary<(int, int), char>> plantGroups = [];

for(int x = 0; x < sizeX; x++)
{
    for(int y = 0; y < sizeY; y++)
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
    foreach(var plant in plantField)
    {
        int x = plant.Key.Item1;
        int y = plant.Key.Item2;
        area++;

        if(x - 1 < 0 || garden[x - 1][y] != value)
        {
            perimeter++;
        }
        if(y - 1 < 0 || garden[x][y - 1] != value)
        {
            perimeter++;
        }
        if(x + 1 >= sizeX || garden[x + 1][y] != value)
        {
            perimeter++;
        }
        if(y + 1 >= sizeY || garden[x][y + 1] != value)
        {
            perimeter++;
        }
    }
    int sides = 0;

    int minX = plantField.Keys.Min(x => x.Item1);
    int maxX = plantField.Keys.Max(x => x.Item1);
    int minY = plantField.Keys.Min(x => x.Item2);
    int maxY = plantField.Keys.Max(x => x.Item2);

    for(int x = minX; x <= maxX; x++)
    {
        bool top = false;
        bool bottom = false;

        List<(int, int)> row = plantField.Where(p => p.Key.Item1 == x).Select(p => p.Key).ToList();
        if(x == minX)
        {
            for(int y = minY; y <= maxY; y++)
            {
                bool newBottom = row.Contains((x, y));

                if(newBottom && newBottom != bottom)
                {
                    sides++;
                }

                bottom = newBottom;
            }
        }
        bottom = false;
        for(int y = minY; y <= maxY; y++)
        {
            bool newTop = row.Contains((x, y));
            bool newBottom = x + 1 < sizeX && plantField.ContainsKey((x + 1, y));
            if(newTop ^ newBottom)
            {
                if(top != newTop)
                {
                    sides++;
                }
                else if(bottom != newBottom)
                {
                    sides++;
                }
            }
            top = newTop;
            bottom = newBottom;
        }
    }
    for(int y = minY; y <= maxY; y++)
    {
        bool left = false;
        bool right = false;

        List<(int, int)> column = plantField.Where(p => p.Key.Item2 == y).Select(p => p.Key).ToList();
        if(y == minY)
        {
            for(int x = minX; x <= maxX; x++)
            {
                bool newRight = column.Contains((x, y));
                if(newRight && newRight != right)
                {
                    sides++;
                }
                right = newRight;
            }
        }
        right = false;
        for(int x = minX; x <= maxX; x++)
        {
            bool newLeft = column.Contains((x, y));
            bool newRight = y + 1 < sizeY && plantField.ContainsKey((x, y + 1));
            if(newLeft ^ newRight)
            {
                if(left != newLeft)
                {
                    sides++;
                }
                else if(right != newRight)
                {
                    sides++;
                }
            }
            left = newLeft;
            right = newRight;
        }
    }
    price += area * perimeter;
    price2 += area * sides;
}

Console.WriteLine(price);
Console.WriteLine(price2);

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
    if(x + 1 < sizeX && gardenSearched[x + 1][y] == plant)
    {
        searchField(plant, x + 1, y);
    }
    if(y + 1 < sizeY && gardenSearched[x][y + 1] == plant)
    {
        searchField(plant, x, y + 1);
    }
}
