# Advent of Code 2023

C# implementations and tests using .NET 8 for the [Advent of Code 2023](https://adventofcode.com/2023/) event.

```text
      --------Part 1---------   --------Part 2---------
Day       Time    Rank  Score       Time    Rank  Score
 15       >24h   31971      0       >24h   28402      0
 14       >24h   32685      0       >24h   26902      0
 13       >24h   31355      0       >24h   29258      0
 11   03:12:35   13331      0   16:34:32   33500      0
 10   04:02:42   12311      0          -       -      -
  9   01:30:02   10494      0   02:28:42   12495      0
  8   00:59:01   11313      0       >24h   52028      0
  7   03:00:54   15487      0   04:19:09   15254      0
  6   00:25:50    8248      0   00:35:54    8356      0
  5   01:46:03   12160      0          -       -      -
  4   00:34:02   10896      0   01:06:03    9967      0
  3   00:59:18    6791      0   01:06:38    4879      0
  2   19:52:51   93446      0   20:00:53   89269      0
  1       >24h  175197      0       >24h  131096      0
  ```

## Day 05 Part Two
A complete and utter code disaster.  My solution for Part One while accurate was grossly inefficient.  When I tried to brute force my way through part two it just fell over and wouldn't complete.  

## Day 07 Part One
Got started an hour late on this one and got really hung up on how to generate values for each poker hand.  Had a hell of a time getting the first answer right.  The root cause of my problem was that I was not performing tie-breaker logic the way the instructions described but was instead using standard poker tie-breaking logic.  Once I realized the issue I had it solved.

## Day 07 Part Two
Throwing Jokers into the mix really caused me some grief.  I couldn't nudge my Part One solution into place so I ended up with copy-pasta.  Not horrible but felt like a minor defeat.  I spent over an hour tracking down a bug in my sorting logic that was caused by me tracking a flag indicating if a high card exists.  Once I saw it I understood how dumb it was.

## Day 07 Refactor
I hated that the code was still written using my initial bad assumption that I would have to rank hands using actual poker rules instead of the Camel rules.  I refactored it all with that assumption removed and the code is MUCH cleaner now.

## Day 08
Much like Day 05 the first part was easy but when I tried using it to solve the second part it was not up to the task.  I used a stright forward but brute-force approach on the problem and then problem set was just too big in Part Two.  I've had the calculations running for nearly 24 hours and it still hasn't solved it yet.  I heard mention on-line that it should use the Chinese Remainder Theorem.  I might research that and see if I can't implement it.  Maybe learn something in the process.

### Chinese Remainder Theorem
Wow.  Refactored my step finder, implemented the Chinese Remainder Theorem, and added a custom aggregator.  Lightning fast.  Code is super simple to read now.  Shout out to [@jonathanpaulson5053](https://github.com/jonathanpaulson) and his [Day 8 video](https://www.youtube.com/watch?v=07AMCU8Xyg4) that pointed me at the theorem.

## Day 09
Got started late and I'm still trying to get use to developing on my Mac.  Decades of muscle memory isn't easy to overcome but I'm getting more comfortable with VSCode vs. Visual Studio.  I still feel its a big step down.  Might need to grab Rider and see how I feel with it.  The first part of the puzzle wasn't that hard.  The second part tripped me up - I just couldn't recognize the pattern for predicting the before state of the numbers.  I must have stared at it and jotted down notes for 45 minutes before I stumbled upon a mathmatical pattern.  There has to be a simpler way than the hack I used.

## Day 10
Part one tripped me up.  I don't have a clue how to accomplish Part 2.

## Day 11 
Had an epiphany this morning and refactored the code to improve the solve speed.  Sleeping on issues really is my magic power.

## Day 13
Finally got around to this while travelling.  Hard problem to solve if for no other reason than I misread the instructions regarding how to calculate the values.  Part two was a beast.  I got 90% of the way there but kept coming up with the wrong answer still.  The issue was introduced by my approach to the problem.  I was accidentally ignoring valid results if the horizontal non-smudged index was the same as the smudged vertical index.  So much trial and error and manual string checking to troubleshoot.

## Day 14
Part one of this was super easy - solved it in about 15 minutes.  Too bad I started it nearly a day late.  Part two was painful.  Brute force takes way too long so we had to figure out how cyclical a result was in order to reduce the number of iterations that need to be run.  Off by one errors probably cost me at least 2 hours.  Math is hard.