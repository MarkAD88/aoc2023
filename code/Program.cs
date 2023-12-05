﻿// See https://aka.ms/new-console-template for more information
using code;

Console.WriteLine("Hello, World!");

// Day 01
var input = File.ReadAllText(Path.Combine("inputs", "day01.txt"));
Console.WriteLine($"Day01-a: {Day01.TaskOne(input)}");
Console.WriteLine($"Day01-b: {Day01.TaskTwo(input)}");

// Day 02
input = File.ReadAllText(Path.Combine("inputs", "day02.txt"));
Console.WriteLine($"Day02-a: {Day02.TaskOne(input)}");
Console.WriteLine($"Day02-b: {Day02.TaskTwo(input)}");

// Day 03
input = File.ReadAllText(Path.Combine("inputs", "day03.txt"));
Console.WriteLine($"Day03-a: {Day03.TaskOne(input)}");
Console.WriteLine($"Day03-b: {Day03.TaskTwo(input)}");

// Day 04
input = File.ReadAllText(Path.Combine("inputs", "day04.txt"));
Console.WriteLine($"Day04-a: {Day04.TaskOne(input)}");
Console.WriteLine($"Day04-b: {Day04.TaskTwo(input)}");

// Day 05
input = File.ReadAllText(Path.Combine("inputs", "day05.txt"));
Console.WriteLine($"Day05-a: {Day05.TaskOne(input)}");
Console.WriteLine($"Day05-b: {Day05.TaskTwo(input)}");
