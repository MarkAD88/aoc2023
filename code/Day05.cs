namespace code
{
    public static class Day05
    {
        public static long TaskOne(string input)
        {
            var lines = input.ReplaceLineEndings("\n").Split('\n', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            var seeds = lines[0]
                .Split(':')[1]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.RemoveEmptyEntries)
                .Select(long.Parse)
                .ToArray();
            var locations = GetLocationsFromSeeds(lines, seeds);
            return locations.Min();
        }

        public static long TaskTwo(string input)
        {
            return -1;

            // THIS CODE IS GROSSLY INEFFICIENT
            // IT WILL EAT UP EVERY AVAILABLE BYTE OF RAM AND CPU
            // AND STILL NEVER COMPLETE
            // MY IMPLEMENTATION IS JUST BAD
            // NOT SURE HOW TO SOLVE IT PROPERLY
            // LEAVING IT HERE AS A CAUTIONARY TALE FOR FUTURE GENERATIONS.
            var lines = input.ReplaceLineEndings("\n").Split('\n', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            var seedPairs = lines[0]
                .Split(':')[1]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.RemoveEmptyEntries)
                .Select(long.Parse)
                .ToArray();

            var seeds = new List<long>();
            for (int index = 0; index < seedPairs.Length; index += 2)
            {
                var start = seedPairs[index];
                var range = seedPairs[index + 1];
                for (int i = 0; i < range; i++)
                {
                    seeds.Add(start + i);
                }
            }

            var locations = GetLocationsFromSeeds(lines, [.. seeds]);
            return locations.Min();
        }

        private static long[] GetLocationsFromSeeds(string[] lines, long[] seeds)
        {
            // Read seed to soil mappings
            long lineNumber = 2;
            var seedToSoilMaps = LoadMaps(lines, ref lineNumber);
            lineNumber++;
            var soilToFertilizerMaps = LoadMaps(lines, ref lineNumber);
            lineNumber++;
            var fertilizerToWaterMaps = LoadMaps(lines, ref lineNumber);
            lineNumber++;
            var waterToLightMaps = LoadMaps(lines, ref lineNumber);
            lineNumber++;
            var lightToTemperatureMaps = LoadMaps(lines, ref lineNumber);
            lineNumber++;
            var temperatureToHumidityMaps = LoadMaps(lines, ref lineNumber);
            lineNumber++;
            var humidityToLocationMaps = LoadMaps(lines, ref lineNumber);

            // Follow the path for each seed
            var seedToLocationMaps = seeds.Select(seed =>
            {
                var soil = seedToSoilMaps.FirstOrDefault(map => map.ContainsMap(seed)).GetMap(seed);
                var fertilizer = soilToFertilizerMaps.FirstOrDefault(map => map.ContainsMap(soil)).GetMap(soil);
                var water = fertilizerToWaterMaps.FirstOrDefault(map => map.ContainsMap(fertilizer)).GetMap(fertilizer);
                var light = waterToLightMaps.FirstOrDefault(map => map.ContainsMap(water)).GetMap(water);
                var temperature = lightToTemperatureMaps.FirstOrDefault(map => map.ContainsMap(light)).GetMap(light);
                var humidity = temperatureToHumidityMaps.FirstOrDefault(map => map.ContainsMap(temperature)).GetMap(temperature);
                var location = humidityToLocationMaps.FirstOrDefault(map => map.ContainsMap(humidity)).GetMap(humidity);
                return location;
            }).ToArray();

            // Find the lowest value
            return seedToLocationMaps;
        }

        private static Map[] LoadMaps(string[] lines, ref long index)
        {
            var results = new List<Map>();
            while (index < lines.Length && char.IsDigit(lines[index][0]))
            {
                results.Add(Map.Parse(lines[index]));
                index++;
            }

            return [.. results];
        }

        private struct Map
        {
            public long SourceStart;
            public long DestinationStart;
            public long Range;

            public readonly bool ContainsMap(long source) => source >= SourceStart && source - SourceStart <= Range;

            public readonly long GetMap(long source)
            {
                if (Range != default && ContainsMap(source))
                {
                    return DestinationStart + (source - SourceStart);
                }

                return source;
            }

            public static Map Parse(string line)
            {
                var parts = line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                return new Map
                {
                    DestinationStart = long.Parse(parts[0]),
                    SourceStart = long.Parse(parts[1]),
                    Range = long.Parse(parts[2])
                };
            }
        }
    }
}
