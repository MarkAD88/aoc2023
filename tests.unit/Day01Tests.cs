using FluentAssertions;

namespace tests.unit
{
    public class Day01Tests
    {
        [Fact]
        public void TaskOne()
        {
            var input = @"
                1abc2
                pqr3stu8vwx
                a1b2c3d4e5f
                treb7uchet";

            code.Day01.TaskOne(input)
                .Should()
                .Be(142);
        }

        [Fact]
        public void TaskTwo()
        {
            var input = @"
                two1nine
                eightwothree
                abcone2threexyz
                xtwone3four
                4nineeightseven2
                zoneight234
                7pqrstsixteen";

            code.Day01.TaskTwo(input)
                .Should()
                .Be(281);
        }

        [Theory]
        [InlineData("4fourztnthreeone8mqmdfour", 44)]
        public void TaskTwoFailures(string input, int expected)
        {
            code.Day01.TaskTwo(input)
                .Should()
                .Be(expected, $"{input} should have resulted in {expected}");
        }
    }
}
