namespace code
{
    public static class Day08
    {
        public static int TaskOne(string input)
        {
            var lines = input.ReplaceLineEndings("\n").Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var sequence = lines[0];
            var nodes = Parse(lines[1..]);

            int step = 0;
            string node = "AAA";
            while (node != "ZZZ")
            {
                node = sequence[step % sequence.Length] switch
                {
                    'L' => nodes[node][0],
                    'R' => nodes[node][1],
                    _ => throw new ArgumentException("Unexpected sequence value!")
                };

                step++;
            }

            return step;
        }

        public static Dictionary<string, string[]> Parse(string[] lines)
        {
            Dictionary<string, string[]> results = new();
            foreach (string line in lines)
            {
                var parts = line.Split(' ', StringSplitOptions.TrimEntries);
                results[parts[0]] = [parts[2][1..4], parts[3][..3]];
            }

            return results;
        }

        public static long TaskTwo(string input)
        {
            var lines = input.ReplaceLineEndings("\n").Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var sequence = lines[0];
            var nodes = Parse(lines[1..]);

            long step = 0;
            var ghosts = nodes.Where(node => node.Key.EndsWith('A')).Select(node => node.Key).ToArray();
            while (!ghosts.All(ghost => ghost.EndsWith('Z')))
            {
                for (int i = 0; i < ghosts.Length; i++)
                {
                    ghosts[i] = sequence[(int) (step % sequence.Length)] switch
                    {
                        'L' => nodes[ghosts[i]][0],
                        'R' => nodes[ghosts[i]][1],
                        _ => throw new ArgumentException("Unexpected sequence value!")
                    };
                }

                step++;

                if (step % 1000000 == 0)
                {
                    Console.WriteLine($"{DateTime.Now:HH:mm:ss} - {step / 1000000:N0}MM");
                }
            }

            return step;
        }
    }
}
