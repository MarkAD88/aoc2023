// Parse lines

using System.Diagnostics;

List<(char Type, string Name, string[] Destinations)> lines = [];
using var input = Console.IsInputRedirected ? Console.OpenStandardInput() : File.OpenRead("sample2.txt");
using var reader = new StreamReader(input);
while (!reader.EndOfStream)
{
    var line = reader.ReadLine()!;
    var type = line[0];
    var name = (type == 'b' ? line : line[1..]).Split(' ')[0];
    var destinations = line
        .Split('>')[^1]
        .Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
    lines.Add((type, name, destinations));
}

// Convert lines into modules
var modules = lines.ToDictionary(
    line => line.Name,
    line => line.Type switch
    {
        '%' => (Module) new FlipFlopModule { Name = line.Name, Destinations = line.Destinations, },
        '&' => new ConjunctionModule
        {
            Name = line.Name,
            Destinations = line.Destinations,
            Sources = lines
                .Where(item => item.Destinations.Contains(line.Name))
                .Select(source => source.Name)
                .ToArray(),
        },
        'b' => new BroadcastModule { Name = line.Name, Destinations = line.Destinations, },
        _ => throw new InvalidOperationException($"Unknown module type - {line.Type}"),
    });

Console.WriteLine($"Modules : {modules.Count}");
modules.Values
    .GroupBy(module => module.GetType())
    .ToList()
    .ForEach(g => Console.WriteLine($"\t{g.Key}(s) : {g.Count()}"));
modules.Values.ToList().ForEach(Console.WriteLine);

var low = 0;
var high = 0;
Queue<(string Source, string Destination, bool Signal)> messages = new();
for (var i = 1; i <= 1000; i++)
{
    messages.Enqueue(("button", "broadcaster", false));
    while (messages.Count > 0)
    {
        var message = messages.Dequeue();
        if (message.Signal)
        {
            high += 1;
        }
        else
        {
            low += 1;
        }

        if (!modules.TryGetValue(message.Destination, out var module))
        {
            continue;
        }

        var signals = module.ProcessSignal(message.Source, message.Signal);
        foreach (var item in signals)
        {
            messages.Enqueue((module.Name, item.Destinations, item.Signal));
        }
    }
}

var part1resultexpected = 886701120;
var part1result = low * high;
Console.WriteLine("Part 1:");
Console.WriteLine($"    Low    : {low}");
Console.WriteLine($"    High   : {high}");
Console.WriteLine($"    Result : {part1result}");
Debug.Assert(part1result == part1resultexpected, $"Expected {part1resultexpected} found {part1result}");

// rx doesn't exist
// rx is fed by conjunction zh
// track signals to zh
var zhModule = (ConjunctionModule) modules["zh"];
var zhSources = modules
    .Where(kvp => zhModule.Sources.Contains(kvp.Key))
    .ToDictionary(name => name.Key, _ => 0L);

modules.Values.ToList().ForEach(module => module.Reset());
var buttonPresses = 0;
while (zhSources.Values.Any(x => x == 0))
{
    buttonPresses += 1;
    messages.Enqueue(("button", "broadcaster", false));
    while (messages.Count > 0)
    {
        var message = messages.Dequeue();

        if (message.Signal && zhSources.TryGetValue(message.Source, out var zhSignal) && zhSignal == 0)
        {
            zhSources[message.Source] = buttonPresses;
            var remaining = zhSources.Values.Count(x => x == 0);
            Console.WriteLine(
                $"{DateTime.Now:HH:mm:ss} - Resolved {message.Destination} @ {buttonPresses} - {remaining} remaining.");

            if (remaining == 0)
            {
                break;
            }
        }

        if (!modules.TryGetValue(message.Destination, out var module))
        {
            continue;
        }

        var signals = module.ProcessSignal(message.Source, message.Signal);
        foreach (var item in signals)
        {
            messages.Enqueue((module.Name, item.Destinations, item.Signal));
        }
    }
}

// Chinese remainder theory to the rescue once more!
// Thank you Day 08 Part 2! :)
var part2result = zhSources
    .Values
    .OrderBy(presses => presses)
    .Aggregate((result, presses) => presses * result / FindGreatestCommonDenominator(presses, result));

Console.WriteLine($"Part 2 : {part2result}");
return;

long FindGreatestCommonDenominator(long a, long b)
{
    while (true)
    {
        if (b == 0)
        {
            return a;
        }

        var a1 = a;
        a = b;
        b = a1 % b;
    }
}

internal interface IModule
{
    string Name { get; set; }

    void Reset() { }
}

internal class Module
{
    public string[] Destinations = [];
    public string Name;
    public virtual void Reset() { }
    public virtual IEnumerable<(string Destinations, bool Signal)> ProcessSignal(string source, bool signal) => [];
}

internal class FlipFlopModule : Module
{
    public bool IsOn;

    public override IEnumerable<(string Destinations, bool Signal)> ProcessSignal(string source, bool signal)
    {
        if (signal)
        {
            return [];
        }

        IsOn = !IsOn;
        return Destinations.Select(name => (name, IsOn));
    }

    public override void Reset()
    {
        IsOn = false;
    }

    public override string ToString() => $"%{Name} -> {string.Join(", ", Destinations)}";
}

internal class ConjunctionModule : Module
{
    public string[] Sources = [];

    public Dictionary<string, bool> SourceSignalStates = [];

    public override IEnumerable<(string Destinations, bool Signal)> ProcessSignal(string source, bool signal)
    {
        if (SourceSignalStates.Count == 0)
        {
            SourceSignalStates = Sources.ToDictionary(name => name, _ => false);
        }

        SourceSignalStates[source] = signal;

        var outboundSignal = !SourceSignalStates.Values.All(value => value);
        return Destinations.Select(name => (name, outboundSignal));
    }

    public override void Reset()
    {
        foreach (var key in SourceSignalStates.Keys)
        {
            SourceSignalStates[key] = false;
        }
    }

    public override string ToString() =>
        $"&{Name} -> {string.Join(", ", Sources)} -> {string.Join(", ", Destinations)}";
}

internal class BroadcastModule : Module
{
    public override IEnumerable<(string Destinations, bool Signal)> ProcessSignal(string source, bool signal)
    {
        return Destinations.Select(name => (name, signal));
    }

    public override string ToString() => $"{Name} -> {string.Join(", ", Destinations)}";
}