using code;
using FluentAssertions;

namespace tests.unit;

public class Day09Tests
{
    [Fact]
    public void TaskOneSample()
    {
        var input = File.ReadAllText(Path.Combine("inputs", "day09-sample.txt"));
        Day09.TaskOne(input).Should().Be(114);
    }

    [Fact]
    public void TaskOneFull()
    {
        var input = File.ReadAllText(Path.Combine("inputs", "day09-full.txt"));
        Day09.TaskOne(input).Should().Be(1916822650);
    }

    [Fact]
    public void TaskTwoSample()
    {
        var input = File.ReadAllText(Path.Combine("inputs", "day09-sample.txt"));
        Day09.TaskTwo(input).Should().Be(2);
    }

    [Fact]
    public void TaskTwoFull()
    {
        var input = File.ReadAllText(Path.Combine("inputs", "day09-full.txt"));
        Day09.TaskTwo(input).Should().Be(966);
    }
}