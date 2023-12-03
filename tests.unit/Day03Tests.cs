using code;

using FluentAssertions;

namespace tests.unit
{
    public class Day03Tests
    {
        [Fact]
        public void TaskOne()
        {
            var input = @"
                467..114..
                ...*......
                ..35..633.
                ......#...
                617*......
                .....+.58.
                ..592.....
                ......755.
                ...$.*....
                .664.598..";

            Day03.TaskOne(input)
                .Should()
                .Be(4361);
        }

        [Fact]
        public void TaskTwo()
        {
            var input = @"
                467..114..
                ...*......
                ..35..633.
                ......#...
                617*......
                .....+.58.
                ..592.....
                ......755.
                ...$.*....
                .664.598..";

            Day03.TaskTwo(input)
                .Should()
                .Be(467835);
        }
    }
}
