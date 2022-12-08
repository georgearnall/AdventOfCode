using System.Text;

const string filePath = @"input.txt";

var line = File.ReadLines(filePath, Encoding.UTF8).First();

Console.WriteLine($"Part1: {Part1()}");
Console.WriteLine($"Part2: {Part2()}");

int Part1() => SearchLength(line, 4);

int Part2() => SearchLength(line, 14);

int SearchLength(string s, int uniqueLength)
{
    for (var i = 0; i < s.Length; i++)
    {
        var marker = s.Substring(i, uniqueLength);
        if (marker.Distinct().Count() == uniqueLength)
        {
            return i + uniqueLength;
        }
    }

    return -1;
}
