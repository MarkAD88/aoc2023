// Day 22

const string SLAB_NAMES = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; 

List<Slab> slabs = [ ];

using var input = Console.IsInputRedirected ? Console.OpenStandardInput() : File.OpenRead("input.txt");
using var reader = new StreamReader(input);
Console.WriteLine("PARSING:");
while (!reader.EndOfStream)
{
    var line = reader.ReadLine()!;
    Console.WriteLine($"\t{line}");
    var coords = line.Split([ '~', ',' ]).Select(int.Parse).ToArray();
    slabs.Add(new Slab(coords[0], coords[1], coords[2], coords[3], coords[4], coords[5], GetSlabName()));
}

/*
Console.WriteLine("BEFORE:");
slabs.ForEach(Console.WriteLine);
Console.WriteLine();
*/

// Drop slabs
Dictionary<(int, int), int> floor = [ ];
foreach (var slab in slabs.OrderBy(slab => slab.Zs.Min()))
{
    while (slab.TryDrop(slabs)) { }
}

/*
Console.WriteLine("AFTER:");
slabs.ForEach(Console.WriteLine);
Console.WriteLine();
*/

// Can be safely disintegrated
int disintegrated = 0;
foreach (var slab in slabs)
{
    var canBeDisintegrated = slabs
        .Except([ slab ])
        .Where(eval => eval.SupportedBy.Count == 1)
        .All(eval => !eval.SupportedBy.Contains(slab));

    if (canBeDisintegrated)
        disintegrated += 1;
}

Console.WriteLine($"Part 1 : {disintegrated}");

// Count falling bricks after disintegration
int disintegratedCount = 0;
foreach (var slab in slabs)
{
    var queue = new Queue<Slab>([ slab ]);
    
    // Keep track of which bricks have been disintegrated
    var disintegratedSlabs = new HashSet<Slab>();
    while (queue.TryDequeue(out var nextSlab))
    {
        // Disintegrate the next slab
        disintegratedSlabs.Add(nextSlab);
        
        // If the next slab supports any other slabs
        // and the supported slabs are ONLY supported
        // by slabs that have already been disintegrated
        // then index our counter and add the supported
        // slabs in to the queue for processing.
        foreach (var supported in nextSlab.SupporterOf.Where(supported => supported.SupportedBy.All(disintegratedSlabs.Contains)))
        {
            disintegratedCount += 1;
            queue.Enqueue(supported);
        }
    }
}

Console.WriteLine($"Part 2 : {disintegratedCount}");

// 3054 too low
// 70345 too high
// 68254 too high

string GetSlabName()
{
    var result = "";
    var index = slabs.Count;
    while (index >= 0)
    {
        result = SLAB_NAMES[index % SLAB_NAMES.Length] + result;
        if (index < SLAB_NAMES.Length)
        {
            break;
        }
        
        index /= SLAB_NAMES.Length;
        index -= 1;
    }

    return result;
}

internal class Slab
{
    public Slab(int x1, int y1, int z1, int x2, int y2, int z2, string name = null)
    {
        Xs = Enumerable.Range(x1, x2 - x1 + 1).ToArray();
        Ys = Enumerable.Range(y1, y2 - y1 + 1).ToArray();
        Zs = Enumerable.Range(z1, z2 - z1 + 1).ToArray();
        Name = name;
    }

    public string? Name { get; }
    
    public HashSet<Slab> SupportedBy = [ ];
    public HashSet<Slab> SupporterOf = [ ];

    public int[] Xs { get; }
    
    public int[] Ys { get; }
    
    public int[] Zs { get; internal set; }

    public bool Occupies(IEnumerable<int> x, IEnumerable<int> y, IEnumerable<int> z) =>
        Xs.Intersect(x).Any()
        && Ys.Intersect(y).Any()
        && Zs.Intersect(z).Any();

    public bool TryDrop(IEnumerable<Slab> slabs)
    {
        var testZ = Zs.Min() - 1;
        if (testZ < 1)
        {
            return false;
        }

        // Find slabs that I will be resting on
        var blockingSlabs = slabs
            .Except([ this ])
            .Where(slab => slab.Occupies(Xs, Ys, [ testZ ]))
            .ToArray();
        if (blockingSlabs.Length != 0)
        {
            foreach (var slab in blockingSlabs)
            {
                SupportedBy.Add(slab);
                slab.SupporterOf.Add(this);
            }

            return false;
        }

        Zs = Zs.Select(z => z - 1).ToArray();
        return true;
    }

    public override string ToString()
    {
        return (Name != null ? $"({Name}) " : "") +
               $"{Xs.Min()}, {Ys.Min()}, {Zs.Min()} ~ {Xs.Max()}, {Ys.Max()}, {Zs.Max()}" +
               $" : BY ({string.Join(", ", SupportedBy.Select(slab => slab.Name))})" +
               $" : SUPPORTING {SupporterOf.Count}";
    }
}
