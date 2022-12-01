using System.Text;

const string filePath = @"/Users/georgearnall/source/AdventOfCode/Day1/input.txt";
var lines = File.ReadLines(filePath, Encoding.UTF8);

var elfCalories = new List<int>();

var currentElfCalories = 0;

foreach (var line in lines)
{
    if (string.IsNullOrEmpty(line))
    {
        elfCalories.Add(currentElfCalories);
        currentElfCalories = 0;
        continue;
    }

    currentElfCalories += int.Parse(line);
}

Console.WriteLine("Top Three Elves");
var top3 = elfCalories.OrderByDescending(x => x).Take(3).ToArray();
foreach (var calories in top3)
{
    Console.WriteLine($"Elf Calories {calories}");
}
Console.WriteLine($"Total: {top3.Sum()}");
