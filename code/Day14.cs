using System.Text;

namespace code
{
    public static class Day14
    {
        public static long TaskOne(string[] lines)
        {
            var map = CreateMap(lines);
            RollNorth(map);
            var dump = DumpMap(map);
            return CalculateNorthernLoad(map);
        }

        public static long TaskTwo(string[] lines)
        {
            var map = lines.CreateMap();

            var repeats = new Dictionary<string, int>();
            var iterations = 1000000000;
            for (var i = 1; i <= iterations; i++)
            {
                var dump = map.RollNorth().RollWest().RollSouth().RollEast().DumpMap();
                if (repeats.TryGetValue(dump, out var repeat))
                {
                    // If we find a duplicate map we know how often it occurs
                    // based on the last time it occured.  We can use this value
                    // to reduce the total number of iterations we need to run.
                    //
                    // The formula is iterations - ((iterations - i - 1) % (i - repeat)) - 1;

                    var iterationsRemaining = iterations - i - 1;
                    var frequncyOfOccurance = i - repeat;
                    var numberOfOccurancesRemaining = iterationsRemaining % frequncyOfOccurance;
                    i = iterations - numberOfOccurancesRemaining - 1;
                }

                repeats[dump] = i;

                if (i % 1000000 == 0)
                {
                    Console.WriteLine($"{DateTime.Now:HH:mm:ss} - {i % 1000000}MM iterations...");
                }
            }

            var result = map.CalculateNorthernLoad();
            return result;
        }

        private static char[,] RollNorth(this char[,] map)
        {
            for (var x = 0; x < map.GetLength(0); x++)
            {
                var yStop = 0;
                for (var y = 0; y < map.GetLength(1); y++)
                {
                    if (map[x, y] == 'O')
                    {
                        map[x, y] = '.';
                        map[x, yStop] = 'O';
                        yStop++;
                    }

                    if (map[x, y] == '#')
                    {
                        yStop = y + 1;
                    }
                }
            }

            return map;
        }

        private static char[,] RollSouth(this char[,] map)
        {
            for (var x = 0; x < map.GetLength(0); x++)
            {
                var yStop = map.GetLength(1) - 1;
                for (var y = map.GetLength(1) - 1; y >= 0; y--)
                {
                    if (map[x, y] == 'O')
                    {
                        map[x, y] = '.';
                        map[x, yStop] = 'O';
                        yStop--;
                    }

                    if (map[x, y] == '#')
                    {
                        yStop = y - 1;
                    }
                }
            }

            return map;
        }

        private static char[,] RollWest(this char[,] map)
        {
            for (var y = 0; y < map.GetLength(1); y++)
            {
                var xStop = 0;
                for (var x = 0; x < map.GetLength(0); x++)
                {
                    if (map[x, y] == 'O')
                    {
                        map[x, y] = '.';
                        map[xStop, y] = 'O';
                        xStop++;
                    }

                    if (map[x, y] == '#')
                    {
                        xStop = x + 1;
                    }
                }
            }

            return map;
        }

        private static char[,] RollEast(this char[,] map)
        {
            for (var y = 0; y < map.GetLength(1); y++)
            {
                var xStop = map.GetLength(0) - 1;
                for (var x = map.GetLength(0) - 1; x >= 0; x--)
                {
                    if (map[x, y] == 'O')
                    {
                        map[x, y] = '.';
                        map[xStop, y] = 'O';
                        xStop--;
                    }

                    if (map[x, y] == '#')
                    {
                        xStop = x - 1;
                    }
                }
            }

            return map;
        }

        private static long CalculateNorthernLoad(this char[,] map)
        {
            var result = 0;
            for (var y = 0; y < map.GetLength(1); y++)
            {
                for (var x = 0; x < map.GetLength(0); x++)
                {
                    if (map[x, y] == 'O')
                    {
                        result += map.GetLength(1) - y;
                    }
                }
            }
            return result;
        }

        public static char[,] CreateMap(this string[] lines)
        {
            var results = new char[lines.Length, lines[0].Length];
            for (var y = 0; y < lines.Length; ++y)
            {
                for (var x = 0; x < lines[0].Length; x++)
                {
                    results[x, y] = lines[y][x];
                }
            }

            return results;
        }

        public static string DumpMap(this char[,] map)
        {
            var builder = new StringBuilder();
            for (var y = 0; y < map.GetLength(1); y++)
            {
                for (var x = 0; x < map.GetLength(0); x++)
                {
                    builder.Append(map[x, y]);
                }
                builder.Append(Environment.NewLine);
            }

            return builder.ToString();
        }
    }
}
