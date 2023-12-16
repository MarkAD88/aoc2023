using code;

using FluentAssertions;
namespace tests.unit
{
    public class Day15Tests
    {
        [Fact]
        public void TaskOneSimple()
        {
            var input = "HASH";
            Day15.TaskOne(input).Should().Be(52);
        }

        [Fact]
        public void TaskOneSample()
        {
            var input = "rn=1,cm-,qp=3,cm=2,qp-,pc=4,ot=9,ab=5,pc-,pc=6,ot=7";
            Day15.TaskOne(input).Should().Be(1320);
        }

        [Fact]
        public void TaskOne()
        {
            var input = File.ReadAllText(Path.Combine("inputs", "day15.txt"));
            Day15.TaskOne(input).Should().Be(505459);
        }

        [Fact]
        public void TaskTwoSample()
        {
            var input = "rn=1,cm-,qp=3,cm=2,qp-,pc=4,ot=9,ab=5,pc-,pc=6,ot=7";
            Day15.TaskTwo(input).Should().Be(145);
        }

        [Fact]
        public void TaskTwo()
        {
            var input = File.ReadAllText(Path.Combine("inputs", "day15.txt"));
            Day15.TaskTwo(input).Should().Be(228508);
        }
    }
}
