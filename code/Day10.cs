using System.Drawing;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace code;

public static class Day10
{
    public static long TaskOne(string[] lines)
    {
        var (start, vector1, vector2) = FindStartPointAndInitialVectors(lines);

        var forward = start + vector1;
        var backward = start + vector2;

        int moves = 1;
        do
        {
            Move(forward, vector1, lines, out vector1);
            forward += vector1;
            Move(backward, vector2, lines, out vector2);
            backward += vector2;
            moves++;
        } while (forward != backward);

        return moves;
    }

    private static void Move(Point point, Size vector, string[] lines, out Size nextVector)
    {
        nextVector = vector;
        if (vector == new Size(0, -1))
        {
            MoveUp(point, lines, out nextVector);
        }
        else if (vector == new Size(0, 1))
        {
            MoveDown(point, lines, out nextVector);
        }
        else if (vector == new Size(-1, 0))
        {
            MoveLeft(point, lines, out nextVector);
        }
        else if (vector == new Size(1, 0))
        {
            MoveRight(point, lines, out nextVector);
        }
    }

    private static void MoveUp(Point point, string[] lines, out Size nextVector)
    {
        var next = GetMapValue(point.X, point.Y, lines);
        nextVector = next switch
        {
            '|' => new (0, -1),
            '7' => new (-1, 0),
            'F' => new (1, 0),
            _ => throw new ArgumentNullException($"Invalid character enountered - {next}")
        };
    }

    private static void MoveDown(Point point, string[] lines, out Size nextVector)
    {
        var next = GetMapValue(point.X, point.Y, lines);
        nextVector = next switch
        {
            '|' => new (0, 1),
            'L' => new (1, 0),
            'J' => new (-1, 0),
            _ => throw new ArgumentNullException($"Invalid character enountered - {next}")
        };
    }

    private static void MoveLeft(Point point, string[] lines, out Size nextVector)
    {
        var next = GetMapValue(point.X, point.Y, lines);
        nextVector = next switch
        {
            '-' => new (-1, 0),
            'L' => new (0, -1),
            'F' => new (0, 1),
            _ => throw new ArgumentNullException($"Invalid character enountered - {next}")
        };
    }

    private static void MoveRight(Point point, string[] lines, out Size nextVector)
    {
        var next = GetMapValue(point.X, point.Y, lines);
        nextVector = next switch
        {
            '-' => new (1, 0),
            'J' => new (0, -1),
            '7' => new (0, 1),
            _ => throw new ArgumentNullException($"Invalid character enountered - {next}")
        };
    }

    public static long TaskTwo(string[] lines)
    {
        return 0;
    }

    private static (Point, Size, Size) FindStartPointAndInitialVectors(string[] lines)
    {
        Point start = default;
        for (int y = 0; y < lines.Length; y++)
        {
            var x = lines[y].IndexOf('S');
            if (x >= 0)
            {
                start = new (x, y);
            }
        }

        var up = GetMapValue(start.X, start.Y - 1, lines);
        var down = GetMapValue(start.X, start.Y + 1, lines);
        var left = GetMapValue(start.X - 1, start.Y, lines);
        var right = GetMapValue(start.X + 1, start.Y, lines);

        List<Size> vectors = [];
        if ("|7F".Contains(up))
        {
            vectors.Add(new(0, -1));
        }
        
        if ("|LJ".Contains(down))
        {
            vectors.Add(new(0, 1));
        }

        if ("-LF".Contains(left))
        {
            vectors.Add(new(-1, 0));
        }

        if("-J7".Contains(right))
        {
            vectors.Add(new(1, 0));
        }

        return (start, vectors.FirstOrDefault(), vectors.Skip(1).FirstOrDefault());
    }

    private static char GetMapValue(int x, int y, string[] lines)
    {
        if (x < 0 || x >= lines[0].Length || y < 0 || y >= lines.Length)
        {
            return char.MinValue;
        }

        return lines[y][x];
    }
}