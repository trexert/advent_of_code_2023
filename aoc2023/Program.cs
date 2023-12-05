string[] cliArgs = Environment.GetCommandLineArgs();
if (cliArgs.Length != 2)
{
    Console.WriteLine("Usage: dotnet run <day>");
    return;
}

int day = int.Parse(cliArgs[1]);

(string part1, string? part2) = day switch
{
    1 => (Day1.Part1(), Day1.Part2()),
    2 => (Day2.Part1(), Day2.Part2()),
    3 => (Day3.Part1(), Day3.Part2()),
    _ => throw new NotImplementedException($"Day {day} is not implemented yet"),
};

Console.WriteLine($"Day{day} part1 - {part1}, part2 - {part2}");
