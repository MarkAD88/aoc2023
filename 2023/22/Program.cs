// Day 22

List<Slab> slabs = [ ];
Dictionary<(int, int), int> floor = [ ];

using var input = Console.IsInputRedirected ? Console.OpenStandardInput() : File.OpenRead("sample.txt");
using var reader = new StreamReader(input);
Console.WriteLine("PARSING:");
while (!reader.EndOfStream)
{
    var line = reader.ReadLine()!;
    Console.WriteLine($"\t{line}");
    var coords = line.Split([ '~', ',' ]).Select(int.Parse).ToArray();
    var cubes = (
            from x in Enumerable.Range(coords[0], coords[3] - coords[0] + 1)
            from y in Enumerable.Range(coords[1], coords[4] - coords[1] + 1)
            from z in Enumerable.Range(coords[2], coords[5] - coords[2] + 1)
            select new Cube(x, y, z))
        .ToList();
    slabs.Add(new Slab(cubes));

    // Establish map Z floors
    foreach (var cube in cubes)
    {
        floor[cube.XY] = Math.Min(cube.Z, floor.GetValueOrDefault(cube.XY, cube.Z));
    }

    Console.WriteLine(slabs.Last());
}

Console.WriteLine("BEFORE:");
slabs.ForEach(Console.WriteLine);
Console.WriteLine();

// Drop slabs
foreach (var slab in slabs.OrderBy(slab => slab.Cubes.Min(cube => cube.Z)))
{
    if (slab.MinZ == 1)
    {
        // already at minimum level
        continue;
    }

    var z = slab.MinZ;
    while (z > 1)
    {
        z -= 1;
        if (slab.Cubes.Any(cube => floor[cube.XY] == z))
        {
            break;
        }

        foreach (var cube in slab.Cubes)
        {
            cube.Z -= 1;
            floor[cube.XY] = Math.Min(cube.Z, floor.GetValueOrDefault(cube.XY, cube.Z));
        }
    }
}

Console.WriteLine("AFTER:");
slabs.ForEach(Console.WriteLine);
Console.WriteLine();

internal class Slab(IEnumerable<Cube> cubes)
{
    public IEnumerable<Cube> Cubes => cubes;

    public int MinZ => Cubes.Min(cube => cube.Z);

    public override string ToString() => string.Join(" ~ ", Cubes);
}

internal class Cube(int x, int y, int z)
{
    public (int, int) XY => (X, Y);
    public int X => x;
    public int Y => y;
    public int Z { get; set; } = z;
    public override string ToString() => $"{X}, {Y}, {Z}";
}
