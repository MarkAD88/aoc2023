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
            var value = FindMirrorLine(map) * 100;
            if (value != null)
            {
                values.Add(value.Value);
            }

            value = FindMirrorLine(Rotate(map));
            if (value != null)
            {
                values.Add(value.Value);
            }
        }

        return values.Sum();
    }

    public static long TaskTwo(string[] lines) => 0;

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

            results.Add([..map,]);
            map.Clear();
        }

        if (map.Any())
        {
            results.Add([..map,]);
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

        return [..results,];
    }

    private static long? FindMirrorLine(string[] lines)
    {
        var line = 0;
        var spread = 0;
        var mirror = true;
        for (var i = 0; i < lines.Length - 1; i++)
        {
            if (lines[i] != lines[i + 1])
            {
                continue;
            }

            line = i + 1;
            var top = i;
            mirror = true;
            var bottom = top + 1;
            spread = 1;
            while (top >= 0 && bottom < lines.Length)
            {
                if (lines[top] != lines[bottom])
                {
                    mirror = false;
                    break;
                }

                top -= 1;
                bottom += 1;
                spread += 1;
            }

            if (mirror)
            {
                break;
            }
        }

        return mirror ? line : null;
    }
}
