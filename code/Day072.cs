namespace code
{
    public static class Day072
    {
        const string CARD_VALUES = "0J23456789TQKA";
        //                           1234567891111
        //                                    0123

        public static long TaskTwo(string input)
        {
            var lines = input.ReplaceLineEndings("\n").Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var hands = lines.Select(Hand.Parse);
            var rank = hands.OrderBy(hand => hand).ToArray();

            long result = 0;
            for (int i = 0; i < rank.Length; i++)
            {
                result += (i + 1) * rank[i].Bid;
            }
            return result;
        }

        public class Hand(int[] cards, long bid) : IComparer<Hand>, IComparable<Hand>
        {
            public override string ToString()
            {
                return new string(Cards
                    .GroupBy(c => c)
                    .OrderByDescending(g => g.Count())
                    .SelectMany(g => g)
                    .Select(c => CARD_VALUES[c])
                    .ToArray());
            }

            private int? _handValue;

            public int[] Cards => cards;

            public long Bid => bid;

            public int HandValue
            {
                get
                {
                    if (_handValue == null)
                    {
                        var groups = cards.Where(c => c != 1).ToLookup(c => c);
                        var five = groups.FirstOrDefault(g => g.Count() == 5)?.Key;
                        var four = groups.FirstOrDefault(g => g.Count() == 4)?.Key;
                        var three = groups.FirstOrDefault(g => g.Count() == 3)?.Key;
                        var two = groups.Where(g => g.Count() == 2).OrderByDescending(g => g.Key).Select(g => g.Key).ToArray();
                        var high = groups.OrderByDescending(g => g.Key).FirstOrDefault(g => g.Count() == 1)?.Key;
                        var jokers = cards.Count(c => c == 1);

                        if (jokers == 5)
                        {
                            five = 13;
                        }
                        else if (jokers == 4)
                        {
                            five = high;
                            high = null;
                        }
                        else if (jokers == 3)
                        {
                            if (two.Length == 1)
                            {
                                five = two[0];
                                two = [];
                            }
                            else if (high.HasValue)
                            {
                                four = high;
                                high = null;
                            }
                        }
                        else if (jokers == 2)
                        {
                            if (three.HasValue)
                            {
                                five = three;
                                three = null;
                            }
                            else if (two.Length == 1)
                            {
                                four = two.Last();
                                two = [];
                            }
                            else if (high.HasValue)
                            {
                                three = high;
                                high = null;
                            }
                        }
                        else if (jokers == 1)
                        {
                            if (four.HasValue)
                            {
                                five = four;
                                four = null;
                            }
                            else if (three.HasValue)
                            {
                                four = three;
                                three = null;
                            }
                            else if (two.Length == 2)
                            {
                                three = two.First();
                                two = [two.Last()];
                            }
                            else if (two.Length == 1)
                            {
                                three = two.Last();
                                two = [];
                            }
                            else if (high.HasValue)
                            {
                                two = [high.Value];
                                high = null;
                            }
                        }

                        var fullhouse = three.HasValue && two.Length == 1 ? 1 : 0;

                        string stringHandValue =
                            (five.HasValue ? "1" : "0") +
                            (four.HasValue ? "1" : "0") +
                            (fullhouse.ToString()) +
                            (three.HasValue ? "1" : "0") +
                            (two.Length);

                        _handValue = int.Parse(stringHandValue);
                    }

                    return _handValue.Value;
                }
            }

            public static Hand Parse(string input)
            {
                var parts = input.Split(' ');
                var cards = parts[0].Take(5).Select(c => CARD_VALUES.IndexOf(c)).ToArray();
                var bid = long.Parse(parts[1]);
                return new(cards, bid);
            }

            public int Compare(Hand? x, Hand? y)
            {
                if (x.HandValue != y.HandValue)
                {
                    return x.HandValue - y.HandValue;
                }

                for (int i = 0; i < cards.Length; i++)
                {
                    if (x.Cards[i] == y.Cards[i])
                    {
                        continue;
                    }

                    return x.Cards[i] - y.Cards[i];
                }

                return 0;
            }

            public int CompareTo(Hand? other)
            {
                return Compare(this, other);
            }
        }
    }
}
