using System.Text;
using static System.IO.File;
using Directory = Day7.Directory;

const string filePath = @"input.txt";

var lines = ReadAllLines(filePath, Encoding.UTF8);

var rootDirectory = BuildDirectory();
var flatDirectories = FlattenDirectories(rootDirectory);

Console.WriteLine($"Part1: {Part1(flatDirectories)}");
Console.WriteLine($"Part2: {Part2(flatDirectories)}");

Directory BuildDirectory()
{
    var root = new Directory("root", null);
    var cwd = root;
    var inListMode = false;

    foreach (var line in  lines)
    {
        switch (line.Split(' '))
        {
            case ["$", "cd", "/"]:
                cwd = root;
                inListMode = false;
                break;
            case ["$", "cd", ".."] when cwd.Parent is not null:
                cwd = cwd.Parent;
                inListMode = false;
                break;
            case ["$", "cd", var directoryName]:
                cwd = cwd.SubDirectories.Single(x => x.Name == directoryName);
                inListMode = false;
                break;
            case ["$", "ls"] when !inListMode:
                inListMode = true;
                break;
            case ["dir", var directoryName] when inListMode:
                cwd.NewDirectory(directoryName);
                break;
            case [var bytes, _] when inListMode && int.TryParse(bytes, out var parsedBytes):
                cwd.AddFile(parsedBytes);
                break;
            default:
                throw new InvalidOperationException($"Unknown command \"{line}\"");
        }
    }
    return root;
}

int Part1(IReadOnlyCollection<Directory> directories)
{
    const int directorySizeLimit = 100000;

    return directories
        .Select(x => x.GetSize())
        .Where(size => size <= directorySizeLimit)
        .Sum(size => size);
}

int Part2(IReadOnlyCollection<Directory> directories)
{
    const int fileSystemTotalSize = 70000000;
    const int fileSystemTargetFreeSpace = 30000000;

    var fileSystemSize = directories.First(x => x.Name == "root").GetSize();
    var currentFreeSpace = fileSystemTotalSize - fileSystemSize;
    var targetDirectorySize = fileSystemTargetFreeSpace - currentFreeSpace;

    return directories
        .Select(x => x.GetSize())
        .Where(size => size >= targetDirectorySize)
        .MinBy(size => size);
}

List<Directory> FlattenDirectories(Directory directory)
{
    var flattenedDirectories = new List<Directory>
    {
        directory
    };

    flattenedDirectories.AddRange(directory.SubDirectories.SelectMany(FlattenDirectories));

    return flattenedDirectories;
}
