using System.Text;

const string filePath = @"input.txt";
var lines = File.ReadAllLines(filePath, Encoding.UTF8);

Console.WriteLine($"Part1: {Part1()}");
Console.WriteLine($"Part2: {Part2()}");

int Part1() => lines.Select(line =>
{
    // Convert to priorities
    var priorities = ConvertToPriorities(line);

    // Split into compartments
    var middle = priorities.Length / 2;
    var compartment1 = priorities[..middle];
    var compartment2 = priorities[middle..];

    // Find Common Priority
    return compartment1.Intersect(compartment2).Single();
}).Sum();

int Part2() => lines
    .Select(ConvertToPriorities)
    .Chunk(3)
    .Select(priorities => priorities[0]
        .Intersect(priorities[1])
        .Intersect(priorities[2])
        .Single())
    .Sum();

int[] ConvertToPriorities(string bag)
{
    return bag.Select(x => (int)x switch
    {
        >= 97 and <= 122 => x - 96,
        >= 65 and <= 90 => x - 38,
        _ => throw new ArgumentOutOfRangeException(nameof(x), x, null)
    }).ToArray();
}
