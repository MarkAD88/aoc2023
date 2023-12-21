// Day 21

List<string> grid = [];
using var input = Console.IsInputRedirected ? Console.OpenStandardInput() : File.OpenRead("sample.txt");
using var reader = new StreamReader(input);
while (!reader.EndOfStream)
{
    var line = reader.ReadLine()!;
    grid.Add(line);
}

var startY = grid.IndexOf(grid.First(line => line.Contains('S')));
var startX = grid.First(line => line.Contains('S')).IndexOf('S');

var gridWidth = grid[0].Length;
var gridHeight = grid.Count;

var maxMoves = int.Parse(args[0]);

Queue<(int X, int Y, int Moves)> queue = new();
List<(int, int)> gardenPlots = [];
var seen = new bool[gridWidth, gridHeight];
queue.Enqueue((startX, startY, 0));
while (queue.Count > 0)
{
    var (x, y, moves) = queue.Dequeue();
    if (x < 0 || x >= gridWidth || y < 0 || y >= gridHeight || moves > maxMoves)
    {
        // don't process anything out of bounds
        continue;
    }

    if (grid[y][x] == '#')
    {
        // don't process an obstacle
        continue;
    }

    if (seen[x, y])
    {
        // don't process anything twice
        continue;
    }

    seen[x, y] = true;

    if (moves % 2 == 0 || maxMoves % 2 == 1)
    {
        // Add a visited plot 
        // modulo ensures for even numbers we can really only
        // "land" in plots that have an even number of moves
        // to get to them.
        gardenPlots.Add((x, y));
    }

    queue.Enqueue((x - 1, y + 0, moves + 1));
    queue.Enqueue((x + 1, y + 0, moves + 1));
    queue.Enqueue((x + 0, y - 1, moves + 1));
    queue.Enqueue((x + 0, y + 1, moves + 1));
}

if (args.Skip(1).FirstOrDefault()?.Equals("print", StringComparison.OrdinalIgnoreCase) == true)
{
    Console.WriteLine();
    for (var y = 0; y < gridHeight; y++)
    {
        for (var x = 0; x < gridWidth; x++)
        {
            if (seen[x, y])
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }

            if (gardenPlots.Contains((x, y)))
            {
                Console.Write('O');
            }
            else
            {
                Console.Write(grid[y][x]);
            }

            Console.ResetColor();
        }

        Console.WriteLine();
    }
}

Console.WriteLine($"Part 1 : {gardenPlots.Count}");
