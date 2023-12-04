namespace code
{
    public static class Day04
    {
        public static int TaskOne(string input)
        {
            var lines = input.Replace("\r", "").Split('\n');

            var results = new List<int>();
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                var parts = line.Split(':')[1].Split('|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                var winning = parts[0].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse);
                var picked = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse);
                var matches = winning.Intersect(picked).ToArray();
                results.Add((int) Math.Pow(2, matches.Length - 1));
            }

            return results.Sum();
        }

        public static int TaskTwo(string input)
        {
            var cards = input.Replace("\r", "").Split('\n').Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
            var counts = cards.Select(x => 1).ToList();

            for (int i = 0; i < cards.Length; i++)
            {
                var card = cards[i];
                var parts = card.Split(':')[1].Split('|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                var winning = parts[0].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse);
                var picked = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse);
                var matches = winning.Intersect(picked).ToArray();

                var incrementor = counts[i];
                for (int next = 1; next <= matches.Length; next++)
                {
                    counts[i + next] += incrementor;
                }
            }

            return counts.Sum();
        }
    }
}
