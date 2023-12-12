using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public partial class Day9
{
    public static string Part1()
    {
        var input = ParseInput();
        long i = 0;
        foreach (var seq in input)
        {
            var diffs = BuildDiffs(seq);
            long nextValue = 0;
            diffs.Reverse();
            foreach (var diffSeq in diffs)
            {
                nextValue += diffSeq[^1];
            }
            i += nextValue;
        }
        return i.ToString();
    }

    public static string Part2()
    {
        var input = ParseInput();
        long i = 0;
        foreach (var seq in input)
        {
            var diffs = BuildDiffs(seq);
            long nextValue = 0;
            diffs.Reverse();
            foreach (var diffSeq in diffs)
            {
                nextValue = diffSeq[0] - nextValue;
            }
            i += nextValue;
        }
        return i.ToString();
    }

    private static List<List<long>> BuildDiffs(List<long> seq)
    {
        List<List<long>> diffs = [seq];
        while (!diffs[^1].All(value => value == 0))
        {
            List<long> newDiffs = [];
            foreach (var pair in diffs[^1].Windows(2))
            {
                newDiffs.Add(pair[1] - pair[0]);
            }
            diffs.Add(newDiffs);
        }
        return diffs;
    }

    private static List<List<long>> ParseInput()
    {
        List<List<long>> parsedInput = [];
        using (var sr = new StreamReader("inputs/Day9.txt"))
        {
            for (var line = sr.ReadLine(); line != null; line = sr.ReadLine())
            {
                parsedInput.Add(line.Split(" ").Select(long.Parse).ToList());
            }
        }
        return parsedInput;
    }
}