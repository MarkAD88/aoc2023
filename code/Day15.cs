namespace code
{
    public static class Day15
    {
        public static long TaskOne(string input)
        {
            input = input.ReplaceLineEndings("");
            var parts = input.Split(',');

            List<long> results = [];
            foreach (var part in parts)
            {
                var bytes = System.Text.Encoding.ASCII.GetBytes(part);
                var current = 0;
                foreach (var ascii in bytes)
                {
                    current += ascii;
                    current *= 17;
                    current %= 256;
                }
                results.Add(current);
            }

            return results.Sum();
        }

        public static long TaskTwo(string input)
        {
            input = input.ReplaceLineEndings("");
            var parts = input.Split(',');

            Dictionary<int, List<Lens>> boxes = [];
            foreach (var part in parts)
            {
                var operationIndex = part.IndexOfAny(['-', '=']);
                var label = part[..operationIndex];
                var operation = part[operationIndex];
                var focalLength = operation == '=' ? int.Parse(part[operationIndex + 1].ToString()) : 0;

                var bytes = System.Text.Encoding.ASCII.GetBytes(label);
                var boxNumber = 0;
                foreach (var ascii in bytes)
                {
                    boxNumber += ascii;
                    boxNumber *= 17;
                    boxNumber %= 256;
                }

                if (!boxes.TryGetValue(boxNumber, out var box))
                {
                    box = [];
                    boxes[boxNumber] = box;
                }

                if (operation == '-')
                {
                    box.RemoveAll(lens => lens.Label == label);
                }
                else if (operation == '=')
                {
                    var existing = box.SingleOrDefault(lens => lens.Label == label);
                    if (existing != null)
                    {
                        existing.FocalLength = focalLength;
                    }
                    else
                    {
                        box.Add(new Lens { Label = label, FocalLength = focalLength });
                    }
                }
            }

            var result = 0;
            foreach (var (boxNumber, lenses) in boxes)
            {
                for (var i = 0; i < lenses.Count; i++)
                {
                    var lens = lenses[i];
                    result += (boxNumber + 1) * (i + 1) * lens.FocalLength;
                }
            }
            return result;
        }

        private class Lens
        {
            public string Label { get; set; }
            public int FocalLength { get; set; }
        }
    }
}
