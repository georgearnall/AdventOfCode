using System.Text;

const string filePath = @"input.txt";
var lines = File.ReadAllLines(filePath, Encoding.UTF8);

Console.WriteLine($"Part1: {Part1(lines)}");
Console.WriteLine($"Part2: {Part2(lines)}");

int Part1(IEnumerable<string> lines)
{
    var score = 0;
    foreach (var line in lines)
    {
        if (string.IsNullOrWhiteSpace(line)) continue;
        var plays = line.Split(' ');
        var opponent = (OpponentInput)plays[0][0];
        var player = (PlayerInput)plays[1][0];

        score += opponent switch
        {
            OpponentInput.Rock when player is PlayerInput.Rock => (int)Outcome.Draw + (int)Score.Rock,
            OpponentInput.Rock when player is PlayerInput.Paper => (int)Outcome.Win + (int)Score.Paper,
            OpponentInput.Rock when player is PlayerInput.Scissors => (int)Outcome.Lose + (int)Score.Scissors,

            OpponentInput.Paper when player is PlayerInput.Rock => (int)Outcome.Lose + (int)Score.Rock,
            OpponentInput.Paper when player is PlayerInput.Paper => (int)Outcome.Draw + (int)Score.Paper,
            OpponentInput.Paper when player is PlayerInput.Scissors => (int)Outcome.Win + (int)Score.Scissors,

            OpponentInput.Scissors when player is PlayerInput.Rock => (int)Outcome.Win + (int)Score.Rock,
            OpponentInput.Scissors when player is PlayerInput.Paper => (int)Outcome.Lose + (int)Score.Paper,
            OpponentInput.Scissors when player is PlayerInput.Scissors => (int)Outcome.Draw + (int)Score.Scissors,
            _ => throw new ArgumentOutOfRangeException($"{opponent} {player}, {line}")
        };
    }

    return score;
}

int Part2(IEnumerable<string> lines)
{
    var score = 0;
    foreach (var line in lines)
    {
        if (string.IsNullOrWhiteSpace(line)) continue;
        var plays = line.Split(' ');
        var opponent = (OpponentInput)plays[0][0];
        var requiredOutcome = (RequiredOutcome)plays[1][0];

        score += opponent switch
        {
            OpponentInput.Rock when requiredOutcome is RequiredOutcome.Draw => (int)Outcome.Draw + (int)Score.Rock,
            OpponentInput.Rock when requiredOutcome is RequiredOutcome.Win => (int)Outcome.Win + (int)Score.Paper,
            OpponentInput.Rock when requiredOutcome is RequiredOutcome.Lose => (int)Outcome.Lose + (int)Score.Scissors,

            OpponentInput.Paper when requiredOutcome is RequiredOutcome.Lose => (int)Outcome.Lose + (int)Score.Rock,
            OpponentInput.Paper when requiredOutcome is RequiredOutcome.Draw => (int)Outcome.Draw + (int)Score.Paper,
            OpponentInput.Paper when requiredOutcome is RequiredOutcome.Win => (int)Outcome.Win + (int)Score.Scissors,

            OpponentInput.Scissors when requiredOutcome is RequiredOutcome.Win => (int)Outcome.Win + (int)Score.Rock,
            OpponentInput.Scissors when requiredOutcome is RequiredOutcome.Lose => (int)Outcome.Lose + (int)Score.Paper,
            OpponentInput.Scissors when requiredOutcome is RequiredOutcome.Draw => (int)Outcome.Draw + (int)Score.Scissors,
            _ => throw new ArgumentOutOfRangeException($"{opponent} {requiredOutcome}, {line}")
        };
    }

    return score;
}

internal enum OpponentInput
{
    Rock = 'A',
    Paper = 'B',
    Scissors = 'C'
}

internal enum PlayerInput
{
    Rock = 'X',
    Paper = 'Y',
    Scissors = 'Z'
}
internal enum Score
{
    Rock = 1,
    Paper = 2,
    Scissors = 3
}

internal enum Outcome
{
    Win = 6,
    Draw = 3,
    Lose = 0
}

internal enum RequiredOutcome
{
    Lose = 'X',
    Draw = 'Y',
    Win = 'Z'
}
