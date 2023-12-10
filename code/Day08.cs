namespace code
{
    public static class Day08
    {
        public static long TaskOne(string input)
        {
            var lines = input.ReplaceLineEndings("\n").Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var sequence = lines[0];
            var nodes = Parse(lines[1..]);

            return FindSteps(node => node == "AAA", node => node == "ZZZ", sequence, nodes);
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

        public static long FindSteps(Func<string, bool> startExpression, Func<string, bool> endExpression, string sequence, Dictionary<string, string[]> nodes)
        {
            int result = 0;
            var node = nodes.Keys.First(startExpression);
            while (!endExpression.Invoke(node))
            {
                node = sequence[result % sequence.Length] switch
                {
                    'L' => nodes[node][0],
                    'R' => nodes[node][1],
                    _ => throw new ArgumentException("Unexpected sequence value!")
                };

                result++;
            }

            return result;
        }

        /* DON'T EVER RUN THIS ON THE FULL DATA SET
         * IT WILL NEVER COMPLETE - WELL NEVER'S NOT
         * THE RIGHT WORD - IT RAN FOR 36 HOURS AND
         * WAS NO WHERE NEAR COMPLETING YET.  I'M SURE
         * IT WOULD SOONER OR LATER.
         */
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

        public static long TaskTwoChineseRemainderTheorem(string input)
        {
            var lines = input.ReplaceLineEndings("\n").Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var sequence = lines[0];
            var nodes = Parse(lines[1..]);

            var result = nodes
                .Where(node => node.Key.EndsWith('A'))
                .Select(node => FindSteps(x => x == node.Key, x => x.EndsWith('Z'), sequence, nodes))
                .OrderBy(x => x)
                .Aggregate((result, steps) => (steps * result) / FindGreatestCommonDenominator(steps, result));

            return result;
        }

        // Found this on https://stackoverflow.com/questions/18541832/c-sharp-find-the-greatest-common-divisor
        // Yes I still use StackOverflow - the thought of using AI to find code makes my skin crawl
        private static long FindGreatestCommonDenominator(long a, long b) => b == 0 ? a : FindGreatestCommonDenominator(b, a % b);
    }
}
