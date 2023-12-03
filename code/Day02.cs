namespace code
{
    public static class Day02
    {
        public static int TaskOne(string input)
        {
            // Allow for 12 red, 13 green, and 14 blue

            var games = ParseInput(input);
            var valid = games.Where(game => game.Samples.All(sample => sample.Red <= 12 && sample.Green <= 13 && sample.Blue <= 14));
            return valid.Sum(game => game.Number);
        }

        public static int TaskTwo(string input)
        {
            // Get max Red, Green, and Blue cubes per game (to determine how many of each would have to be have been present to make the games valid)
            // Multiply these numbers together to get the POWER of the game
            // Sum up the per-game powers

            var games = ParseInput(input);
            var powers = games.Select(game =>
            {
                var maxRed = game.Samples.Max(sample => sample.Red);
                var maxGreen = game.Samples.Max(sample => sample.Green);
                var maxBlue = game.Samples.Max(sample => sample.Blue);
                return maxRed * maxGreen * maxBlue;
            });

            var result = powers.Sum();
            return result;
        }

        static Game[] ParseInput(string input)
        {
            var lines = input.Split('\n');
            var results = lines.Where(line => line != "\r").Select(line => new Game(line)).ToArray();
            return results;
        }
    }

    class Game
    {
        public Game(string record)
        {
            var parts = record.Split(':', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            Number = int.Parse(parts[0][5..]);

            parts = parts[1].Split(";", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            Samples = parts.Select(part => new Sample(part)).ToArray();
        }

        public int Number { get; set; }

        public Sample[] Samples { get; set; }
    }

    class Sample
    {
        public Sample(string record)
        {
            var parts = record.Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            foreach (var part in parts)
            {
                if (part.EndsWith("blue"))
                {
                    Blue = int.Parse(part[..^4]);
                }
                else if (part.EndsWith("red"))
                {
                    Red = int.Parse(part[..^3]);
                }
                else if (part.EndsWith("green"))
                {
                    Green = int.Parse(part[..^5]);
                }
            }
        }

        public int Green { get; set; }

        public int Red { get; set; }

        public int Blue { get; set; }
    }
}
