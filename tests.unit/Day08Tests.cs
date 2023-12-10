using code;

using FluentAssertions;
namespace tests.unit
{
    public class Day08Tests
    {
        [Fact]
        public void TaskOneSimple()
        {
            var input = @"
                RL

                AAA = (BBB, CCC)
                BBB = (DDD, EEE)
                CCC = (ZZZ, GGG)
                DDD = (DDD, DDD)
                EEE = (EEE, EEE)
                GGG = (GGG, GGG)
                ZZZ = (ZZZ, ZZZ)";
            Day08.TaskOne(input).Should().Be(2);
        }

        [Fact]
        public void TaskOneRollover()
        {
            var input = @"
                LLR

                AAA = (BBB, BBB)
                BBB = (AAA, ZZZ)
                ZZZ = (ZZZ, ZZZ)";
            Day08.TaskOne(input).Should().Be(6);
        }

        [Fact]
        public void TaskOneFull()
        {
            var input = File.ReadAllText(Path.Combine("inputs", "day08.txt"));
            Day08.TaskOne(input).Should().Be(18827);
        }

        [Fact]
        public void TaskTwoSimple()
        {
            var input = @"
                LR

                11A = (11B, XXX)
                11B = (XXX, 11Z)
                11Z = (11B, XXX)
                22A = (22B, XXX)
                22B = (22C, 22C)
                22C = (22Z, 22Z)
                22Z = (22B, 22B)
                XXX = (XXX, XXX)";
            Day08.TaskTwo(input).Should().Be(6);
        }

        [Fact]
        public void TaskTwoFull()
        {
            var input = File.ReadAllText(Path.Combine("inputs", "day08.txt"));
            Day08.TaskTwoChineseRemainderTheorem(input).Should().Be(20220305520997);
        }
    }
}
