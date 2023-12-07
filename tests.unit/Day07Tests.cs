using code;

using FluentAssertions;

namespace tests.unit
{
    public class Day07Tests
    {
        [Fact]
        public void TaskOne()
        {
            var input = @"
                32T3K 765
                T55J5 684
                KK677 28
                KTJJT 220
                QQQJA 483";

            Day07.TaskOne(input).Should().Be(6440);
        }

        [Fact]
        public void SortingTest()
        {
            List<Day07.Hand> hands = [
                Day07.Hand.Parse("AAJJJ 200"),
                Day07.Hand.Parse("QQQJ4 436"),
                Day07.Hand.Parse("QQQ83 885"),
            ];

            var sort = hands.OrderBy(hand => hand.HandValue).ToArray();
            hands.Select(hand => hand.ToString()).Should().BeEquivalentTo(new[]
            {
                "QQQ83",
                "QQQJ4",
                "JJJAA",
            });
        }

        [Fact]
        public void TaskTwo()
        {
            var input = @"
                32T3K 765
                T55J5 684
                KK677 28
                KTJJT 220
                QQQJA 483";

            Day072.TaskTwo(input).Should().Be(5905);
        }

        [Fact]
        public void JokerSortingTests()
        {
            List<Day072.Hand> hands = [
                Day072.Hand.Parse("8883J 0"),
                Day072.Hand.Parse("33JJT 0"),
                Day072.Hand.Parse("3333T 0"),
                Day072.Hand.Parse("QQQQ3 0"),
                Day072.Hand.Parse("44JJA 0"),
                Day072.Hand.Parse("44442 0"),
                Day072.Hand.Parse("444JT 0"),
                Day072.Hand.Parse("44447 0"),
            ];

            hands = [
                Day072.Hand.Parse("99998 0"),
                Day072.Hand.Parse("999JA 0"),
                Day072.Hand.Parse("9998J 0"),
                Day072.Hand.Parse("9999K 0"),
                Day072.Hand.Parse("9999A 0"),
                Day072.Hand.Parse("9999T 0"),
                Day072.Hand.Parse("9999Q 0"),
                Day072.Hand.Parse("QQQQ9 0"),
                Day072.Hand.Parse("999KJ 0"),
                Day072.Hand.Parse("TTJJ7 0"),
                Day072.Hand.Parse("TTT3J 0"),
                Day072.Hand.Parse("TTTT9 0"),
                Day072.Hand.Parse("TTTT2 0"),
                Day072.Hand.Parse("TTT6J 0"),
                Day072.Hand.Parse("TTTT6 0"),
                Day072.Hand.Parse("TTTT8 0"),
                Day072.Hand.Parse("TTTTQ 0"),
                Day072.Hand.Parse("QQQJ9 0"),
                Day072.Hand.Parse("999QJ 0"),
                Day072.Hand.Parse("QQQTJ 0"),
                Day072.Hand.Parse("QQQJ3 0"),
            ];

            var sorted = hands.OrderBy(hand => hand).ToArray();



        }
    }
}
