namespace code
{
    public static class Day14
    {
        public static long TaskOne(string[] lines)
        {
            long result = 0;

            for (var x = 0; x < lines[0].Length; x++)
            {
                var rockValue = lines.Length;
                for (var y = 0; y < lines.Length; y++)
                {
                    if (lines[y][x] == 'O')
                    {
                        result += rockValue;
                        rockValue -= 1;
                    }

                    if (lines[y][x] == '#')
                    {
                        rockValue = lines.Length - (y + 1);
                    }
                }
            }

            return result;
        }
    }
}
