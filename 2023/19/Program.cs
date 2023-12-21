// Day 19

Dictionary<string, Workflow> workflows = [];
List<Part> parts = [];

var readingWorkflows = true;

using var input = Console.IsInputRedirected ? Console.OpenStandardInput() : File.OpenRead("sample.txt");
using var reader = new StreamReader(input);
while (!reader.EndOfStream)
{
    var line = reader.ReadLine()!;
    if (line == string.Empty)
    {
        readingWorkflows = false;
        continue;
    }

    if (readingWorkflows)
    {
        var workflow = Workflow.Parse(line);
        workflows[workflow.Name] = workflow;
    }
    else
    {
        parts.Add(Part.Parse(line));
    }
}

Console.WriteLine($"Parsed {workflows.Count:N0} workflows");
Console.WriteLine($"Parsed {parts.Count:N0} parts");

foreach (var part in parts)
{
    var workflow = workflows["in"];
    while (!part.IsAccepted.HasValue)
    {
        foreach (var rule in workflow.Rules)
        {
            string nextWorkflow = null;
            if (rule != workflow.Rules.Last())
            {
                var partValue = rule[0] switch
                {
                    'x' => part.X,
                    'm' => part.M,
                    'a' => part.A,
                    's' => part.S,
                    'A' => 0,
                    'R' => -1,
                    _ => throw new InvalidOperationException($"Unknown input encountered - {rule[0]}"),
                };

                if (partValue == 0)
                {
                    part.IsAccepted = true;
                    break;
                }

                if (partValue == -1)
                {
                    part.IsAccepted = false;
                    break;
                }

                var segments = rule[2..].Split(':');
                var ruleValue = int.Parse(segments[0]);
                nextWorkflow = segments[1];

                var result = rule[1] switch
                {
                    '<' => partValue < ruleValue,
                    '>' => partValue > ruleValue,
                    _ => throw new InvalidOperationException($"Unknown operator encountered - {rule[1]}"),
                };

                if (!result)
                {
                    continue;
                }
            }
            else
            {
                nextWorkflow = rule;
            }

            if (nextWorkflow == "R")
            {
                part.IsAccepted = false;
                break;
            }

            if (nextWorkflow == "A")
            {
                part.IsAccepted = true;
                break;
            }

            workflow = workflows[nextWorkflow];
            break;
        }
    }
}

var part1Result = parts.Where(part => part.IsAccepted == true).Sum(part => part.X + part.M + part.A + part.S);
Console.WriteLine($"Part 1 = {part1Result}");

internal record Part(int X, int M, int A, int S)
{
    public bool? IsAccepted;

    public static Part Parse(string line)
    {
        var parts = line[1..^1].Split(',');
        return new Part(
            int.Parse(parts[0][2..]),
            int.Parse(parts[1][2..]),
            int.Parse(parts[2][2..]),
            int.Parse(parts[3][2..]));
    }
}

internal class Workflow
{
    public string Name;

    public string[] Rules;

    public static Workflow Parse(string line)
    {
        var parts = line.Split(['{', '}',]);
        var rules = parts[1].Split(',');

        return new Workflow
        {
            Name = parts[0],
            Rules = parts[1].Split(','),
        };
    }
}
