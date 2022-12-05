using System.Text;
using System.Text.RegularExpressions;

const string filePath = @"input.txt";
const int initialStateStartIndex = 8;
const int instructionStartIndex = 10;

var lines = File.ReadAllLines(filePath, Encoding.UTF8);

Console.WriteLine($"Part1: {Part1()}");
Console.WriteLine($"Part2: {Part2()}");

string Part1()
{
    var stacks = Initialise();
    var regex = new Regex("move (?<move>[0-9]+) from (?<from>[0-9]+) to (?<to>[0-9]+)", RegexOptions.Compiled);
    for (var i = instructionStartIndex; i < lines.Length; i++)
    {
        var result = regex.Match(lines[i]);

        if (!result.Success) continue;

        var totalToMove = int.Parse(result.Groups["move"].ValueSpan);
        var fromStack = int.Parse(result.Groups["from"].ValueSpan);
        var toStack = int.Parse(result.Groups["to"].ValueSpan);

        for (var j = 0; j < totalToMove; j++)
        {
            stacks[toStack-1].Push(stacks[fromStack-1].Pop());
        }
    }

    return string.Join("", stacks.Select(x => x.Peek()));
}

string Part2()
{
    var stacks = Initialise();
    var regex = new Regex("move (?<move>[0-9]+) from (?<from>[0-9]+) to (?<to>[0-9]+)", RegexOptions.Compiled);
    for (var i = instructionStartIndex; i < lines.Length; i++)
    {
        var result = regex.Match(lines[i]);

        if (!result.Success) continue;

        var totalToMove = int.Parse(result.Groups["move"].ValueSpan);
        var fromStack = int.Parse(result.Groups["from"].ValueSpan);
        var toStack = int.Parse(result.Groups["to"].ValueSpan);

        var tempStack = new Stack<char>();
        for (var j = 0; j < totalToMove; j++)
        {
            tempStack.Push(stacks[fromStack-1].Pop());
        }
        for (var j = 0; j < totalToMove; j++)
        {
            stacks[toStack-1].Push(tempStack.Pop());
        }
    }

    return string.Join("", stacks.Select(x => x.Peek()));
}

Stack<char>[] Initialise()
{
    var stacks = new[]
    {
        new Stack<char>(),
        new Stack<char>(),
        new Stack<char>(),
        new Stack<char>(),
        new Stack<char>(),
        new Stack<char>(),
        new Stack<char>(),
        new Stack<char>(),
        new Stack<char>(),
    };

    for (var i = initialStateStartIndex; i >= 0; i--)
    {
        for (var j = 0; j < stacks.Length; j++)
        {
            var @char = lines[i][(j * 4) + 1];
            if (char.IsLetter(@char))
            {
                stacks[j].Push(@char);
            }
        }
    }

    return stacks;
}
