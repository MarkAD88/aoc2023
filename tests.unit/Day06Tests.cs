using code;

using FluentAssertions;

namespace tests.unit
{
    public class Day06Tests
    {
        [Fact]
        public void TaskOne()
        {
            var input = @"
                Time:      7  15   30
                Distance:  9  40  200";

            // 3 races
            // 4 ways to bet the first one
            // 8 ways to beat the second one
            // 9 ways to beat the third one
            // 4 * 8 * 9 = 288
            Day06.TaskOne(input).Should().Be(288);
        }

        [Fact]
        public void TaskTwo()
        {
            var input = @"
                Time:      7  15   30
                Distance:  9  40  200";

            // 1 race
            // 71503 ways to beat it
            Day06.TaskTwo(input).Should().Be(71503);
        }
    }
}
