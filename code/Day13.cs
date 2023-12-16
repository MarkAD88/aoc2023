using System.Text;

namespace code;

public static class Day13
{
    public static long TaskOne(string[] lines)
    {
        List<long> values = [];

        var maps = GetMaps(lines);

        foreach (var map in maps)
        {
            var value = FindMirrorLine(map, 0) * 100;
            if (value != null)
            {
                values.Add(value.Value);
            }

            value = FindMirrorLine(Rotate(map), 0);
            if (value != null)
            {
                values.Add(value.Value);
            }
        }

        return values.Sum();
    }

    public static long TaskTwo(string[] lines)
    {
        List<long> values = [];

        var maps = GetMaps(lines);

        foreach (var map in maps)
        {
            var horizontal = FindMirrorLine(map, 0);
            var vertical = FindMirrorLine(Rotate(map), 0);

            var smudgedHorizontal = FindMirrorLine(map, 1, horizontal);
            var smudgedVertical = FindMirrorLine(Rotate(map), 1, vertical);

            values.Add(
                smudgedHorizontal * 100
                ?? smudgedVertical
                ?? horizontal * 100
                ?? vertical
                ?? throw new InvalidOperationException("No grid value!"));
        }

        return values.Sum();
    }

    private static List<string[]> GetMaps(string[] lines)
    {
        List<string[]> results = [];
        List<string> map = [];
        foreach (var line in lines)
        {
            if (line != string.Empty)
            {
                map.Add(line);

                continue;
            }

            results.Add([.. map,]);
            map.Clear();
        }

        if (map.Any())
        {
            results.Add([.. map,]);
        }

        return results;
    }

    private static string[] Rotate(string[] lines)
    {
        List<string> results = [];
        StringBuilder builder = new();
        for (var x = 0; x < lines[0].Length; x++)
        {
            builder.Clear();
            foreach (var line in lines)
            {
                builder.Append(line[x]);
            }

            results.Add(builder.ToString());
        }

        return [.. results,];
    }

    private static long? FindMirrorLine(string[] lines, int tolerance, long? ignoreIndex = null)
    {
        var line = 0;
        var mirror = false;
        var toleranceRemaining = tolerance;
        for (var i = 0; i < lines.Length - 1; i++)
        {
            if (i == ignoreIndex - 1)
            {
                continue;
            }

            (var isMatch, _) = IsMatch(lines[i], lines[i + 1], toleranceRemaining);
            if (!isMatch)
            {
                continue;
            }

            if (i == 0)
            {
                return 1;
            }

            if (i == lines.Length - 2)
            {
                return lines.Length - 1;
            }

            line = i + 1;
            var top = i;
            mirror = true;
            var bottom = top + 1;
            var innerToleranceRemaining = toleranceRemaining;
            while (top >= 0 && bottom < lines.Length)
            {
                (isMatch, innerToleranceRemaining) = IsMatch(lines[top], lines[bottom], innerToleranceRemaining);
                if (!isMatch)
                {
                    mirror = false;
                    break;
                }

                top -= 1;
                bottom += 1;
            }

            if (mirror)
            {
                break;
            }
        }

        return mirror ? line : null;
    }

    private static (bool, int) IsMatch(string line1, string line2, int tolerance)
    {
        if (line1 == line2)
        {
            return (true, tolerance);
        }

        if (tolerance == 0)
        {
            return (false, tolerance);
        }

        var toleranceRemaining = tolerance;
        for (var x = 0; x < line1.Length; x++)
        {
            if (line1[x] != line2[x])
            {
                toleranceRemaining -= 1;
                if (toleranceRemaining < 0)
                {
                    return (false, tolerance);
                }
            }
        }

        return (true, toleranceRemaining);
    }
}
