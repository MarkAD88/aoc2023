namespace code
{
    public static class Day01
    {
        public static int TaskOne(string input)
        {
            var lines = input.Split('\n', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            var codes = lines.Select(line =>
            {
                var digits = line.Where(c => char.IsDigit(c)).ToArray();
                if (digits.Length == 0)
                {
                    return 0;
                }
                else if (digits.Length == 1)
                {
                    return int.Parse(new string(digits[0], 2));
                }
                else
                {
                    return int.Parse(digits.First().ToString() + digits.Last().ToString());
                }
            });

            return codes.Sum();
        }

        public static int TaskTwo(string input)
        {
            var lines = input.Split('\n', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            List<string> names = ["zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"];
            List<string> numbers = ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"];
            List<string> namesAndNumbers = [.. names, .. numbers];

            var codes = lines.Select(line =>
            {
                var hits = namesAndNumbers
                    .SelectMany(item =>
                    {
                        return new[]
                        {
                            new { Index = line.IndexOf(item), Text = item },
                            new { Index = line.LastIndexOf(item), Text = item },
                        };
                    })
                    .Where(item => item.Index >= 0)
                    .OrderBy(item => item.Index)
                    .ToArray();
                if (hits.Length == 0)
                {
                    return 0;
                }
                else if (hits.Length == 1)
                {
                    return int.Parse(ToNumber(hits[0].Text) + ToNumber(hits[0].Text));
                }
                else
                {
                    return int.Parse(ToNumber(hits.First().Text) + ToNumber(hits.Last().Text));
                }
            }).ToArray();

            return codes.Sum();

            string ToNumber(string value)
            {
                if (numbers.Contains(value))
                {
                    return value;
                }

                return numbers[names.IndexOf(value)];
            }
        }
    }
}
