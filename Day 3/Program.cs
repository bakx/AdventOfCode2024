using System.Diagnostics;
using System.Text.RegularExpressions;

Stopwatch stopwatch = new Stopwatch();
stopwatch.Start();

List<string> data = [.. Shared.Files.ReadPerLine($"{Path.Combine(Environment.CurrentDirectory, "input.txt")}")];

int part1 = GetValidResults(ref data);
Console.WriteLine($"Part 1: {part1}");

int part2= GetEnabledValidResults(ref data);
Console.WriteLine($"Part 2: {part2}");

static int GetValidResults(ref List<string> list)
{
    int result = 0;
    Regex regex = new Regex(@"mul\((\d+),(\d+)\)");

    foreach (string s in list)
    {
        foreach (Match match in regex.Matches(s))
        {
            int a = int.Parse(match.Groups[1].Value);
            int b = int.Parse(match.Groups[2].Value);

            result += a * b;
        }
    }

    return result;
}

static int GetEnabledValidResults(ref List<string> list)
{
    int result = 0;
    Regex regex = new Regex(@"(?:mul\((\d+),(\d+)\)|do\(\)|don't\(\))");
    bool canProcess = true;

    foreach (string s in list)
    {
        foreach (Match match in regex.Matches(s))
        {
            //Console.WriteLine(match.Value);
            if (match.Groups[0].Value.Equals("do()"))
                canProcess = true;
            else if (match.Groups[0].Value.Equals("don't()"))
                canProcess = false;
            else if (canProcess)
            {
                int a = int.Parse(match.Groups[1].Value);
                int b = int.Parse(match.Groups[2].Value);

                result += a * b;
            }
            else
            {
                //Console.WriteLine($"Ignoring match due state");
            }
        }
    }

    return result;
}

TimeSpan elapsed = stopwatch.Elapsed;

Console.WriteLine($"Elapsed time: {elapsed.TotalMilliseconds} ms");
