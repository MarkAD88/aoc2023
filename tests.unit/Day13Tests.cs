using code;

using FluentAssertions;

namespace tests.unit;

public class Day13Tests
{
    [Fact]
    public void TaskOneSample()
    {
        var input = File.ReadAllLines(Path.Combine("inputs", "day13-sample.txt"));
        Day13.TaskOne(input).Should().Be(405);
    }

    [Fact]
    public void TaskOneMapFailureOne()
    {
        string[] input =
        [
            "...##...##.",
            "#......###.",
            "#.#....#..#",
            "#.#..#.###.",
            ".##..##.###",
            "#.#..#.#..#",
            ".#.##.#....",
            "#..##..#.##",
            "#..##..#.##",
            "##....##.##",
            "##....##.##",
        ];
        Day13.TaskOne(input).Should().Be(1000);
    }

    [Fact]
    public void TaskOneMapFailureTwo()
    {
        string[] input =
        [
            "#..##.#......",
            ".##...##.#.##",
            ".##.......###",
            ".....#.#####.",
            ".....#.#####.",
            ".##.......###",
            ".##...##...##",
            "#..##.#......",
            "######...#..#",
            "##########..#",
            ".....####....",
        ];
        Day13.TaskOne(input).Should().Be(2);
    }

    [Fact]
    public void TaskOne()
    {
        var input = File.ReadAllLines(Path.Combine("inputs", "day13-full.txt"));
        Day13.TaskOne(input).Should().Be(34911);
    }

    [Fact]
    public void TaskTwoMapFailure()
    {
        string[] input = [
            "#.#........",
            "##...#####.",
            "#.##..####.",
            "##..##....#",
            "#..#.......",
            "#.#####..##",
            ".....#.##.#",
            ".###..#..#.",
            "##..##....#",
            "###..######",
            "##.##.#..#.",
            "..#####..##",
            "###...#..#.",
            ".......##..",
            "#.#.##.##.#",
            ".#.##.#..#.",
            ".#.##.#..#.",
        ];

        Day13.TaskTwo(input).Should().Be(8);
    }

    [Fact]
    public void TaskTwoMap28Failure()
    {
        string[] input = [
            "#..#.##.###",
            "..###.###..",
            "###..#..###",
            "........#..",
            "###.#..##..",
            "#...#..#...",
            "#..#.##..##",
            "#...#...#..",
            ".##...##...",
            "#.##.#.....",
            "#.#..#.....",
            ".##...##...",
            "#...#...#..",
            "#..#.##..##",
            "#...#..#...",
            "###.#..##..",
            "........#..",
        ];

        Day13.TaskTwo(input).Should().Be(1000);
    }

    [Fact]
    public void TaskTwoMap29Failure()
    {
        string[] input = [
            "####..#.#...#",
            "####..#.#...#",
            "##....##.##.#",
            "...#...#####.",
            "...#..###.###",
            "#.#.#.#..#.#.",
            "##....####..#",
            "....#.#..###.",
            "####.#.#.#.##",
        ];

        Day13.TaskTwo(input).Should().Be(1);
    }

    [Fact]
    public void TaskTwoSample()
    {
        var input = File.ReadAllLines(Path.Combine("inputs", "day13-sample.txt"));
        Day13.TaskTwo(input).Should().Be(400);
    }

    [Fact]
    public void TaskTwo()
    {
        var input = File.ReadAllLines(Path.Combine("inputs", "day13-full.txt"));
        Day13.TaskTwo(input).Should().Be(33183);
    }
}
