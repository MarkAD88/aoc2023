namespace code
{
    public static class Day06
    {
        public static int TaskOne(string input)
        {
            var lines = input.ReplaceLineEndings("\n").Split('\n', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            var times = lines[0].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse).ToArray();
            var distances = lines[1].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse).ToArray();
            var results = new List<List<int>>();

            for (int race = 0; race < times.Length; race++)
            {
                results.Add([]);
                int time = times[race];
                int record = distances[race];

                for (int press = time - 1; press >= 1; press--)
                {
                    var distance = (time - press) * press;
                    if (distance > record)
                    {
                        results[race].Add(press);
                    }
                }
            }

            int result = 1;
            foreach (var race in results)
            {
                result *= race.Count;
            }

            return result;
        }

        public static int TaskTwo(string input)
        {
            var lines = input.ReplaceLineEndings("\n").Split('\n', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            var time = lines[0].Split(':', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(x => long.Parse(x.Replace(" ", ""))).First();
            var record = lines[1].Split(':', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(x => long.Parse(x.Replace(" ", ""))).First();

            var result = 0;
            for (long press = time - 1; press >= 1; press--)
            {
                long distance = (time - press) * press;
                if (distance > record)
                {
                    result += 1;
                }
            }

            return result;
        }
    }
}
