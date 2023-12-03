namespace code
{
    public static class Day03
    {
        public static int TaskOne(string input)
        {
            var symbols = input.Where(c => !char.IsDigit(c) && c != '.' && c != '\r' && c != '\n').ToArray();
            var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(line => "." + line + ".").ToList();

            // Pad lines to avoid any bounds checking
            var lineLength = lines[0].Length;
            lines.Insert(0, new string('.', lineLength));
            lines.Add(new string('.', lineLength));

            var values = new List<string>();
            for (var y = 0; y < lines.Count; y++)
            {
                for (var x = 0; x < lines[y].Length; x++)
                {
                    if (!symbols.Contains(lines[y][x]))
                    {
                        continue;
                    }

                    values.AddRange(FindNumbers(lines[y - 1], x));
                    values.AddRange(FindNumbers(lines[y + 1], x));
                    values.AddRange(FindNumbers((lines[y][..(x)] + '.' + lines[y][(x + 1)..]).ToString(), x));
                }
            }

            return values.Select(value => int.Parse(value)).Sum();
        }

        public static int TaskTwo(string input)
        {
            var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(line => "." + line + ".").ToList();

            // Pad lines to avoid any bounds checking
            var lineLength = lines[0].Length;
            lines.Insert(0, new string('.', lineLength));
            lines.Add(new string('.', lineLength));

            var results = new List<int>();
            for (var y = 0; y < lines.Count; y++)
            {
                for (var x = 0; x < lines[y].Length; x++)
                {
                    if (lines[y][x] != '*')
                    {
                        continue;
                    }

                    var allValues = FindNumbers(lines[y - 1], x)
                        .Concat(FindNumbers(lines[y + 1], x))
                        .Concat(FindNumbers((lines[y][..(x)] + '.' + lines[y][(x + 1)..]).ToString(), x))
                        .ToArray();
                    if (allValues.Length == 2)
                    {
                        results.Add(int.Parse(allValues[0]) * int.Parse(allValues[1]));
                    }
                }
            }

            return results.Sum();
        }

        static string[] FindNumbers(string line, int x)
        {
            int xmin = x - 1;
            while (char.IsDigit(line[xmin]))
            {
                xmin--;
            }

            var xmax = x + 1;
            while (char.IsDigit(line[xmax]))
            {
                xmax++;
            }

            return line[xmin..xmax].Split('.', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        }
    }
}
