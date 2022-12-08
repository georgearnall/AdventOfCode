namespace Day7;

public class Directory
{
    public Directory(string name, Directory? parent)
    {
        Name = name;
        Path = $"{parent?.Path}/" + name;
        Parent = parent;
    }
    public string Name { get; }
    public string Path { get; }
    public Directory? Parent { get; }
    public List<Directory> SubDirectories { get; } = new();
    public void AddFile(int size) => _fileOnlySize += size;
    public void NewDirectory(string directoryName) => SubDirectories.Add(new Directory(directoryName, this));
    public int GetSize()
    {
        if (_size == 0)
        {
            _size = SubDirectories.Sum(x => x.GetSize()) + _fileOnlySize;
        }
        return _size;
    }
    private int _fileOnlySize;
    private int _size;
}
