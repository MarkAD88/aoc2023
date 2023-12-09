using System.Diagnostics;
using System.Runtime.InteropServices;

namespace code;

public static class Day09
{
    public static int TaskOne(string input)
    {
        var lines = input.ReplaceLineEndings("\n").Split('\n', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        var result = 0;
        foreach(var line in lines)
        {
            List<int[]> sequences = [line.Split(' ').Select(int.Parse).ToArray()];
            var currentSequence = sequences[0];
            while(currentSequence.Any(x => x != 0))
            {
                var diff = currentSequence[..^1].Select((number, index) => currentSequence[index + 1] - number).ToArray();
                sequences.Add(diff);
                currentSequence = diff;
            }

            result += sequences.Sum(seq => seq[^1]);
        }

        return result;
    }

    public static int TaskTwo(string input)
    {
        var lines = input.ReplaceLineEndings("\n").Split('\n', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        var result = 0;
        foreach(var line in lines)
        {
            List<int[]> sequences = [line.Split(' ').Select(int.Parse).ToArray()];
            var currentSequence = sequences[0];
            while(currentSequence.Any(x => x != 0))
            {
                var diff = currentSequence[..^1].Select((number, index) => currentSequence[index + 1] - number).ToArray();
                sequences.Add(diff);
                currentSequence = diff;
            }

            var tempresult = 0;
            for(int i = sequences.Count - 1; i >= 0; i--)
            {
                tempresult = tempresult * -1 + sequences[i][0];

            }

            result += tempresult;
        }

        return result;
    }

}