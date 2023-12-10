using code;
using FluentAssertions;

namespace tests.unit;

public class Day10Tests
{
    [Fact]
    public void TaskOneSample1()
    {
        var lines = File.ReadAllLines(Path.Combine("inputs", "day10-sample-1.txt"));
        Day10.TaskOne(lines).Should().Be(4);
    }

   [Fact]
    public void TaskOneSample2()
    {
        var lines = File.ReadAllLines(Path.Combine("inputs", "day10-sample-2.txt"));
        Day10.TaskOne(lines).Should().Be(8);
    }

    [Fact]
    public void TaskOneFull()
    {
        var lines = File.ReadAllLines(Path.Combine("inputs", "day10-full.txt"));
        Day10.TaskOne(lines).Should().Be(6942);
    }
}