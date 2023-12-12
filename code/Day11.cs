using System.Drawing;

namespace code;

public static class Day11
{
    public static long TaskOne(string[] lines) => Calculate(lines, 2);

    private static (long[], long[]) FindExpansions(string[] lines)
    {
        List<long> yExpansions = [];
        for (var y = 0; y < lines.Length; y++)
        {
            if (lines[y].Contains('#'))
            {
                continue;
            }

            yExpansions.Add(y);
        }

        List<long> xExpansions = [];
        var width = lines[0].Length;
        for (var x = 0; x < width; x++)
        {
            if (lines.Any(line => line[x] == '#'))
            {
                continue;
            }

            xExpansions.Add(x);
        }

        return ([..xExpansions,], [..yExpansions,]);
    }

    private static (Point, Point)[] GetGalaxyPairs(string[] lines)
    {
        List<Point> points = [];
        for (var y = 0; y < lines.Length; y++)
        {
            if (!lines[y].Contains('#'))
            {
                continue;
            }

            var line = lines[y];
            for (var x = 0; x < line.Length; x++)
            {
                if (line[x] == '#')
                {
                    points.Add(new Point(x, y));
                }
            }
        }

        List<(Point, Point)> pairs = [];
        for (var i = 0; i < points.Count; i++)
        for (var j = i + 1; j < points.Count; j++)
        {
            pairs.Add((points[i], points[j]));
        }

        return [..pairs,];
    }

    public static long TaskTwo(string[] lines) => Calculate(lines, 1000000);

    private static long Calculate(string[] lines, int expansion)
    {
        var pairs = GetGalaxyPairs(lines);
        var expansions = FindExpansions(lines);

        List<long> distances = [];
        foreach (var pair in pairs)
        {
            var xRange = Enumerable
                .Range(Math.Min(pair.Item1.X, pair.Item2.X), Math.Abs(pair.Item1.X - pair.Item2.X))
                .Select(x => (long) x);
            var xExpansions = expansions.Item1.Intersect(xRange).Count();
            var xDistance = Math.Abs(pair.Item1.X - pair.Item2.X) - xExpansions + xExpansions * expansion;

            var yRange = Enumerable
                .Range(Math.Min(pair.Item1.Y, pair.Item2.Y), Math.Abs(pair.Item1.Y - pair.Item2.Y))
                .Select(x => (long) x);
            var yExpansions = expansions.Item2.Intersect(yRange).Count();
            var yDistance = Math.Abs(pair.Item1.Y - pair.Item2.Y) - yExpansions + yExpansions * expansion;

            distances.Add(xDistance + yDistance);
        }

        return distances.Sum();
    }
}
