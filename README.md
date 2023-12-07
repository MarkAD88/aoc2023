# Advent of Code 2023

C# implementations and tests using .NET 8 for the [Advent of Code 2023](https://adventofcode.com/2023/) event.

```text
      --------Part 1---------   --------Part 2---------
Day       Time    Rank  Score       Time    Rank  Score
  7   03:00:54   15487      0   04:19:09   15254      0
  6   00:25:50    8248      0   00:35:54    8356      0
  5   01:46:03   12160      0          -       -      -
  4   00:34:02   10896      0   01:06:03    9967      0
  3   00:59:18    6791      0   01:06:38    4879      0
  2   19:52:51   93446      0   20:00:53   89269      0
  1       >24h  175197      0       >24h  131096      0
```

## Day 05 Part Two
A complete and utter code disaster.  My solution for Part One
while accurate was grossly inefficient.  When I tried to brute
force my way through part two it just fell over and wouldn't
complete.  

## Day 07 Part One
Got started an hour late on this one and got really hung up
on how to generate values for each poker hand.  Had a hell
of a time getting the first answer right.  The root cause of
my problem was that I was not performing tie-breaker logic
the way the instructions described but was instead using
standard poker tie-breaking logic.  Once I realized the issue
I had it solved.

## Day 07 Part Two
Throwing Jokers into the mix really caused me some grief.  I
couldn't nudge my Part One solution into place so I ended up
with copy-pasta.  Not horrible but felt like a minor defeat.
I spent over an hour tracking down a bug in my sorting logic
that was caused by me tracking a flag indicating if a high
card exists.  Once I saw it I understood how dumb it was.
