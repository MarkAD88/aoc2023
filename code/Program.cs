// See https://aka.ms/new-console-template for more information
using code;

Console.WriteLine("Hello, World!");

// Day 01
var input = File.ReadAllText(Path.Combine("inputs", "day01.txt"));
Console.WriteLine($"Day01-a: {Day01.TaskOne(input)}");
Console.WriteLine($"Day01-b: {Day01.TaskTwo(input)}");

// Day 02
input = File.ReadAllText(Path.Combine("inputs", "day02.txt"));
Console.WriteLine($"Day02: {Day02.TaskOne(input)}");
Console.WriteLine($"Day02: {Day02.TaskTwo(input)}");