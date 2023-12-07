using System.Diagnostics;
using System.Text;

namespace code
{
    public static class Day073
    {
        public static long TaskOne(string input)
        {
            var lines = input.ReplaceLineEndings("\n").Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var hands = lines.Select(line => Hand.Parse(line, false));
            var rank = hands.OrderBy(hand => hand.Weight).ToArray();

            long result = 0;
            for (int i = 0; i < rank.Length; i++)
            {
                result += (i + 1) * rank[i].Bid;
            }
            return result;
        }

        public static long TaskTwo(string input)
        {
            var lines = input.ReplaceLineEndings("\n").Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var hands = lines.Select(line => Hand.Parse(line, true));
            var rank = hands.OrderBy(hand => hand.Weight).ToArray();

            long result = 0;
            for (int i = 0; i < rank.Length; i++)
            {
                result += (i + 1) * rank[i].Bid;
            }
            return result;
        }

        [DebuggerDisplay("{Cards} - {Bid}")]
        public class Hand(string cards, long bid, bool jacksWild)
        {
            public Hand(string cards, long bid)
                : this(cards, bid, false)
            { }

            public string Cards { get; } = cards;

            public long Bid { get; } = bid;

            public (int, string) Weight { get; } = CalculateWeight(cards, jacksWild);

            private static (int, string) CalculateWeight(string cards, bool jacksWild)
            {
                int[] groups = [];
                if (jacksWild)
                {
                    var jacks = cards.Count(c => c == 'J');
                    groups = [.. cards.Where(c => c != 'J').GroupBy(c => c).Select(c => c.Count()).OrderByDescending(count => count)];
                    if (groups.Length == 0)
                    {
                        groups = [5];
                    }
                    else
                    {
                        groups[0] += jacks;
                    }
                }
                else
                {
                    groups = [.. cards.GroupBy(c => c).Select(c => c.Count()).OrderByDescending(count => count)];
                }

                var weight = groups switch
                {
                    [5] => 10,
                    [4, 1] => 9,
                    [3, 2] => 8,
                    [3, 1, 1] => 7,
                    [2, 2, 1] => 6,
                    [2, 1, 1, 1] => 5,
                    [1, 1, 1, 1, 1] => 4,
                    _ => -1
                };

                return (weight, ParseCards(cards, jacksWild));
            }

            private static string ParseCards(string cards, bool jacksWild)
            {
                var builder = new StringBuilder(5);
                for (int i = 0; i < cards.Length; i++)
                {
                    var replacement = cards[i] switch
                    {
                        'T' => (char) 65,
                        'J' => (char) (jacksWild ? 49 : 66),
                        'Q' => (char) 67,
                        'K' => (char) 68,
                        'A' => (char) 69,
                        _ => (char) (48 + int.Parse(cards[i].ToString())),
                    };
                    builder.Append(replacement);
                }

                return builder.ToString();
            }

            public static Hand Parse(string input, bool jacksWild)
            {
                var parts = input.Split(' ');
                var cards = parts[0];
                var bid = long.Parse(parts[1]);
                return new(cards, bid, jacksWild);
            }
        }
    }
}
