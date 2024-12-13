List<List<char>> input = [];


var positionX = 0;
var positionY = 0;
var direction = Direction.None;

readInput();

bool finished = false;
int uniqueSteps = 0;
while(!finished)
{
    if(input[positionY][positionX] != 'X')
    {
        input[positionY][positionX] = 'X';
        uniqueSteps++;
    }
    switch(NextInput())
    {
        case '.':
        case 'X':
            StepForward();
            break;
        case '#':
            TurnRight();
            break;
        default:
            finished = true;
            break;
    }

}
Console.WriteLine(uniqueSteps);
readInput();
finished = false;
var startX = positionX;
var startY = positionY;

var loop = 0;
for(int x = 0; x < input[0].Count; x++)
{
    for(int y = 0; y < input.Count; y++)
    {
        Direction[,] visited = new Direction[input[0].Count, input.Count];
        if(input[y][x] != '#')
        {
            input[y][x] = '#';
        }
        else
        {
            continue;
        }
        positionX = startX;
        positionY = startY;
        direction = Direction.Up;
        finished = false;
        //writeOutput();
        while(!finished)
        {
            if((visited[positionX, positionY] & direction) > 0)
            {
                loop++;
                break;
            }
            visited[positionX, positionY] |= direction;
            switch(NextInput())
            {
                case '.':
                    StepForward();
                    break;
                case '#':
                    TurnRight();
                    break;
                default:
                    finished = true;
                    break;
            }
        }
        input[y][x] = '.';
    }
}
Console.WriteLine(loop);
void readInput()
{
    input.Clear();
    using(StreamReader reader = new StreamReader(Environment.CurrentDirectory + "/input.txt"))
    {
        while(!reader.EndOfStream)
        {
            var inputChar = reader.ReadLine();
            input.Add(inputChar.ToCharArray().ToList());
            var position = inputChar.IndexOf('^');
            if(position != -1)
            {
                positionX = position;
                positionY = input.Count - 1;
                direction = GetDirection(input[positionY][positionX]);
                input[positionY][positionX] = '.';
            }
        }
    }
}
void writeOutput()
{
    using(StreamWriter writer = new StreamWriter(Environment.CurrentDirectory + "/output.txt"))
    {
        foreach(var row in input)
        {
            writer.WriteLine(string.Join("", row));
        }
    }
}

char NextInput()
{
    switch(direction)
    {
        case Direction.Up:
            if(ValidPosition(positionY - 1, positionX))
                return input[positionY - 1][positionX];
            break;
        case Direction.Down:
            if(ValidPosition(positionY + 1, positionX))
                return input[positionY + 1][positionX];
            break;
        case Direction.Left:
            if(ValidPosition(positionY, positionX - 1))
                return input[positionY][positionX - 1];
            break;
        case Direction.Right:
            if(ValidPosition(positionY, positionX + 1))
                return input[positionY][positionX + 1];
            break;
        case Direction.None:
            break;
    }
    return ' ';
}


void StepForward()
{
    switch(direction)
    {
        case Direction.Up:
            positionY--;
            break;
        case Direction.Down:
            positionY++;
            break;
        case Direction.Left:
            positionX--;
            break;
        case Direction.Right:
            positionX++;
            break;
        case Direction.None:
            break;
    }
}

bool ValidPosition(int y, int x)
{
    return x >= 0 && x < input[0].Count && y >= 0 && y < input.Count;
}
void TurnRight()
{
    switch(direction)
    {
        case Direction.Up:
            direction = Direction.Right;
            break;
        case Direction.Down:
            direction = Direction.Left;
            break;
        case Direction.Left:
            direction = Direction.Up;
            break;
        case Direction.Right:
            direction = Direction.Down;
            break;

        case Direction.None:
            break;
    }
}

Direction GetDirection(char inputChar) => inputChar switch
{

    '^' => Direction.Up,
    'v' => Direction.Down,
    '>' => Direction.Right,
    '<' => Direction.Left,
    _ => Direction.None
};


enum Direction
{
    None = 0,
    Up = 1,
    Down = 2,
    Left = 4,
    Right = 8
}

