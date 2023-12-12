using code;
using FluentAssertions;

namespace tests.unit;

public class Day11Tests
{
    [Fact]
    public void TaskOneSample1()
    {
        var lines = File.ReadAllLines(Path.Combine("inputs", "day11-sample-1.txt"));
        Day11.TaskOne(lines).Should().Be(374);
    }

    [Fact]
    public void TaskOneFull()
    {
        var lines = File.ReadAllLines(Path.Combine("inputs", "day11-full.txt"));
        Day11.TaskOne(lines).Should().Be(9957702);
    }

    [Fact]
    public void TaskTwoFull()
    {
        var lines = File.ReadAllLines(Path.Combine("inputs", "day11-full.txt"));
        Day11.TaskTwo(lines).Should().Be(512240933238);
    }
}