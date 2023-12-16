using code;

using FluentAssertions;
namespace tests.unit
{
    public class Day14Tests
    {
        [Fact]
        public void TaskOneSample()
        {
            string[] input = [
                "O....#....",
                "O.OO#....#",
                ".....##...",
                "OO.#O....O",
                ".O.....O#.",
                "O.#..O.#.#",
                "..O..#O..O",
                ".......O..",
                "#....###..",
                "#OO..#....",
            ];

            Day14.TaskOne(input).Should().Be(136);
        }

        [Fact]
        public void TaskOne()
        {
            var input = File.ReadAllLines(Path.Combine("inputs", "day14-full.txt"));
            Day14.TaskOne(input).Should().Be(110090);
        }
    }
}
