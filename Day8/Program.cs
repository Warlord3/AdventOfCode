List<string> lines = [];
using(StreamReader reader = new(Environment.CurrentDirectory + "/input.txt"))
{
    while(!reader.EndOfStream)
    {
        lines.Add(reader.ReadLine()!);
    }
}
char[][] inputData = new char[lines[0].Length][];
for(int i = 0; i < lines.Count; i++)
{
    inputData[i] = lines[i].ToCharArray();
}

Dictionary<char, List<Point>> Antennas = [];

for(int x = 0; x < inputData.Length; x++)
{
    for(int y = 0; y < inputData[x].Length; y++)
    {
        char data = inputData[x][y];
        if(char.IsLetterOrDigit(data))
        {
            if(Antennas.ContainsKey(data))
            {
                Antennas[data].Add(new Point(x, y));
            }
            else
            {
                Antennas.Add(data, [new Point(x, y)]);
            }
        }
    }
}
int uniqueAntinode = 0;

foreach(var Antenna in Antennas)
{
    foreach(var startPoint in Antenna.Value)
    {
        foreach(var endPoint in Antenna.Value)
        {
            if(startPoint == endPoint)
            {
                continue;
            }
            var vector = startPoint - endPoint;
            var pointA = startPoint + vector;
            var pointB = endPoint - vector;
            if(CoordinateValid(pointA))
            {
                if(inputData[pointA.X][pointA.Y] != '#')
                {
                    uniqueAntinode++;
                    inputData[pointA.X][pointA.Y] = '#';
                }
            }
            if(CoordinateValid(pointB))
            {
                if(inputData[pointB.X][pointB.Y] != '#')
                {
                    uniqueAntinode++;
                    inputData[pointB.X][pointB.Y] = '#';

                }
            }
        }
    }
}

Console.WriteLine(uniqueAntinode);

for(int i = 0; i < lines.Count; i++)
{
    inputData[i] = lines[i].ToCharArray();
}
int uniqueAntinode2 = 0;



foreach(var Antenna in Antennas)
{
    foreach(var startPoint in Antenna.Value)
    {
        foreach(var endPoint in Antenna.Value)
        {
            if(startPoint == endPoint)
            {
                continue;
            }
            var vector = endPoint - startPoint;

            var pointA = startPoint - vector;
            var pointB = endPoint + vector;
            var valid = false;
            while(CoordinateValid(pointA))
            {
                if(inputData[pointA.X][pointA.Y] != '#')
                {
                    uniqueAntinode2++;
                    inputData[pointA.X][pointA.Y] = '#';
                }
                pointA -= vector;
                valid = true;
            }
            while(CoordinateValid(pointB))
            {
                if(inputData[pointB.X][pointB.Y] != '#')
                {
                    uniqueAntinode2++;
                    inputData[pointB.X][pointB.Y] = '#';
                }
                pointB += vector;

                valid = true;
            }
            if(inputData[startPoint.X][startPoint.Y] != '#')
            {
                uniqueAntinode2++;
                inputData[startPoint.X][startPoint.Y] = '#';
            }
            if(inputData[endPoint.X][endPoint.Y] != '#')
            {
                uniqueAntinode2++;
                inputData[endPoint.X][endPoint.Y] = '#';
            }

        }
    }
}
Draw(inputData);
Console.WriteLine(uniqueAntinode2);

bool CoordinateValid(Point point)
{

    return point.X >= 0 && point.X < inputData.Length && point.Y >= 0 && point.Y < inputData[0].Length;
}

void Draw(char[][] array)
{
    using(StreamWriter sw = new StreamWriter(Environment.CurrentDirectory + "/output.txt"))
    {

        for(int i = 0; i < array.Length; i++)
        {
            sw.Write(string.Join("", array[i]));
            sw.Write('\n');
        }
    }
}
public class Point
{
    public int X { get; private set; }
    public int Y { get; private set; }

    public int Length => (int)Math.Sqrt(X * X + Y * Y);
    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);
    public static Point operator -(Point a, Point b) => new(a.X - b.X, a.Y - b.Y);
    public static Point operator -(Point a) => new(-a.X, -a.Y);

    public override bool Equals(object? obj) => Equals(obj as Point);

    public override string ToString()
    {
        return $"({X}:{Y})";
    }
    public bool Equals(Point? p)
    {
        if(p is null)
        {
            return false;
        }
        if(Object.ReferenceEquals(this, p))
        {
            return true;
        }
        if(GetType() != p.GetType())
        {
            return false;
        }
        return (X == p.X) && (Y == p.Y);
    }

    public static bool operator ==(Point? lhs, Point? rhs)
    {
        if(lhs is null)
        {
            if(rhs is null)
            {
                return true;
            }

            // Only the left side is null.
            return false;
        }
        if(rhs is null)
        {
            return false;
        }
        // Equals handles case of null on right side.
        return lhs.Equals(rhs);
    }
    public static bool operator !=(Point? lhs, Point? rhs)
    {
        if(lhs is null)
        {
            if(rhs is null)
            {
                return false;
            }

            // Only the left side is null.
            return true;
        }
        if(rhs is null)
        {
            return true;
        }
        // Equals handles case of null on right side.
        return !lhs.Equals(rhs);
    }
}