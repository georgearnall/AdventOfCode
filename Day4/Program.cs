using System.Text;

const string filePath = @"input.txt";
var lines = File.ReadAllLines(filePath, Encoding.UTF8);

Console.WriteLine($"Part1: {Part1()}");
Console.WriteLine($"Part2: {Part2()}");

int Part1() =>
    lines.Select(line => line.Split(',')
            .Select(x => x.Split('-')
                .Select(int.Parse)
                .ToArray())
            .ToArray())
        .Count(sections => SectionContainedWithinOther(sections[0], sections[1])
                           || SectionContainedWithinOther(sections[1], sections[0]));

bool SectionContainedWithinOther(IReadOnlyList<int> outerSection, IReadOnlyList<int> innerSection)
{
    return outerSection[0] <= innerSection[0] && outerSection[1] >= innerSection[1];
}

int Part2() =>
    lines.Select(line => line.Split(',')
            .Select(x => x.Split('-')
                .Select(int.Parse)
                .ToArray())
            .ToArray())
        .Count(sections => !(SectionsSeparate(sections[0], sections[1]) || SectionsSeparate(sections[1], sections[0])));

bool SectionsSeparate(IReadOnlyList<int> firstSection, IReadOnlyList<int> secondSection)
{
    return firstSection[1] < secondSection[0] && secondSection[1] > firstSection[0];
}

