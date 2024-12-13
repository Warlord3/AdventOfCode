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
List<(int, int)> startPosition = [];
finished = false;
input[positionY][positionX] = '.';
var startX = positionX;
var startY = positionY;
Direction lastDirection = Direction.None;
Direction[,] visitedGlobal = new Direction[input[0].Count, input.Count];
while(!finished)
{
    visitedGlobal[positionX, positionY] |= direction;
    switch(NextInput())
    {
        case '.':
            if(TurnToRight())
            {
                var copyList = Copy(input);
                AddTurn();
                lastDirection = direction;
                startPosition.Add((positionX, positionY));
                var tempX = positionX;
                var tempY = positionY;
                bool exitLoop = false;
                int turned = 0;

                Direction[,] visited = new Direction[input[0].Count, input.Count];
                while(!exitLoop)
                {
                    if((visited[positionX, positionY] & direction) > 0)
                    {
                        positionX = tempX;
                        positionY = tempY;
                        direction = lastDirection;
                        RemoveTurn();
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
                            turned++;
                            break;
                        default:
                            exitLoop = true;
                            positionX = tempX;
                            positionY = tempY;
                            direction = lastDirection;
                            RemoveTurn();
                            startPosition.RemoveAt(startPosition.Count - 1);
                            break;
                    }
                }
                input = copyList;
            }
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
Console.WriteLine(startPosition.Count);
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

List<List<char>> Copy(List<List<char>> list)
{
    var newList = new List<List<char>>();
    foreach(var row in list)
    {
        newList.Add(new List<char>(row));
    }
    return newList;
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

void AddTurn()
{
    switch(direction)
    {
        case Direction.Up:
            input[positionY - 1][positionX] = '#';
            break;
        case Direction.Down:
            input[positionY + 1][positionX] = '#';
            break;
        case Direction.Left:
            input[positionY][positionX - 1] = '#';
            break;
        case Direction.Right:
            input[positionY][positionX + 1] = '#';
            break;
        case Direction.None:
            break;
    }
}
void RemoveTurn()
{
    switch(direction)
    {
        case Direction.Up:
            input[positionY - 1][positionX] = '.';
            break;
        case Direction.Down:
            input[positionY + 1][positionX] = '.';
            break;
        case Direction.Left:
            input[positionY][positionX - 1] = '.';
            break;
        case Direction.Right:
            input[positionY][positionX + 1] = '.';
            break;
        case Direction.None:
            break;
    }
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

bool TurnToRight()
{
    switch(direction)
    {
        case Direction.None:
            break;
        case Direction.Up:
            for(int i = positionX + 1; i < input[positionY].Count; i++)
            {
                if(input[positionY][i] == '#')
                {
                    return true;
                }
            }
            break;
        case Direction.Down:
            for(int i = positionX - 1; i >= 0; i--)
            {
                if(input[positionY][i] == '#')
                {
                    return true;
                }
            }
            break;
        case Direction.Left:
            for(int i = positionY - 1; i >= 0; i--)
            {
                if(input[i][positionX] == '#')
                {
                    return true;
                }
            }
            break;
        case Direction.Right:
            for(int i = positionY + 1; i < input.Count; i++)
            {
                if(input[i][positionX] == '#')
                {
                    return true;
                }
            }
            break;
        default:
            break;
    }
    return false;
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

