// Day 24

using System.Numerics;

var sample = new MemoryStream("""
                              19, 13, 30 @ -2,  1, -2
                              18, 19, 22 @ -1, -1, -2
                              20, 25, 34 @ -2, -2, -4
                              12, 31, 28 @ -1, -2, -1
                              20, 19, 15 @  1, -5, -3
                              """u8.ToArray());

List<Hailstone> stones = [ ];

using var input = Console.IsInputRedirected ? Console.OpenStandardInput() : sample;
using var reader = new StreamReader(input);
while (!reader.EndOfStream)
{
    stones.Add(Hailstone.Parse(reader.ReadLine()!));
}

var min = float.Parse(args[0]);
var max = float.Parse(args[1]);
var useZ = bool.Parse(args[2]);

// Find where the vectors intercept on X&Y (use args 7 27 false)
var part1 = stones.SelectMany(stone =>
    stones
        .Skip(stones.IndexOf(stone) + 1)
        .Select(other =>
        (
            A: stone.Position,
            AV: stone.Vector,
            B: other.Position,
            BV: other.Vector,
            Intersect: FindIntersection(
                stone.Position with { Z = 0 },
                stone.Vector with { Z = 0 },
                other.Position with { Z = 0 },
                other.Vector with { Z = 0 })))
        .Where(result => result.Intersect.HasValue)
        .Where(result => result.Intersect.Value.X >= min && result.Intersect.Value.X <= max)
        .Where(result => result.Intersect.Value.Y >= min && result.Intersect.Value.Y <= max)
        .Where(result => !useZ || (result.Intersect.Value.Z >= min && result.Intersect.Value.Z <= max))
        .Where(result =>
        {
            switch (result.AV.X)
            {
                // Make sure the movement was FORWARD in time and not BACKWARD
                case < 0 when !(result.Intersect.Value.X <= result.A.X):
                case > 0 when !(result.Intersect.Value.X >= result.A.X):
                    return false;
            }

            switch (result.BV.X)
            {
                case < 0 when !(result.Intersect.Value.X <= result.B.X):
                case > 0 when !(result.Intersect.Value.X >= result.B.X):
                    return false;
            }

            return true;
        })
        .Select(result => result)
).ToArray();

Console.WriteLine($"Part 1 : {part1.Length}");

return;

Vector3? FindIntersection(Vector3 point1, Vector3 vector1, Vector3 point2, Vector3 vector2)
{
    var cross = Vector3.Cross(vector1, vector2);
    var denominator = cross.GetMagnitude() * cross.GetMagnitude();
    if (denominator == 0)
    {
        return null;
    }

    var p = point2 - point1;
    var t1 = Vector3.Dot(Vector3.Cross(p, vector2), cross) / denominator;
    var t2 = Vector3.Dot(Vector3.Cross(p, vector1), cross) / denominator;

    var intersection = point1 + vector1 * t1;
    return intersection;
}

internal class Hailstone(Vector3 position, Vector3 vector)
{
    private static readonly char[] separators = { ',', '@' };

    public Vector3 Position => position;

    public Vector3 Vector => vector;

    public static Hailstone Parse(string line)
    {
        var parts = line
            .Split(separators, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Select(float.Parse)
            .ToArray();

        return new Hailstone(
            new Vector3(parts[0], parts[1], parts[2]),
            new Vector3(parts[3], parts[4], parts[5]));
    }
}

internal static class Vector3Extensions
{
    public static float GetMagnitude(this Vector3 input) =>
        MathF.Sqrt(input.X * input.X + input.Y * input.Y + input.Z * input.Z);
}
