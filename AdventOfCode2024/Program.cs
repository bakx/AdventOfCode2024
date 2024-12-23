using System.Diagnostics;

Stopwatch stopwatch = new Stopwatch();
stopwatch.Start();

string[] data = Shared.Files.ReadPerLine($"{Path.Combine(Environment.CurrentDirectory, "input.txt")}");

// Data will be presented per line, this will need to be put in sorted lists
List<int> list1 = [];
List<int> list2 = [];

// Load and parse the data
foreach (string line in data)
{
    string[] splitData = line.Trim().Split("   ");
    list1.Add(int.Parse(splitData[0]));
    list2.Add(int.Parse(splitData[1]));
}

// Sort the lists upfront to simplify operations
list1.Sort();
list2.Sort();

int distancePart1 = CalculateDistancePart1(list1, list2);
Console.WriteLine($"Part 1: {distancePart1}");

int distancePart2 = CalculateDistancePart2(list1, list2);
Console.WriteLine($"Part 2: {distancePart2}");

static int CalculateDistancePart1(List<int> sortedList1, List<int> sortedList2)
{
    int distance = 0;

    // Since both lists are sorted, directly compute distance
    for (int i = 0; i < sortedList1.Count; i++)
    {
        distance += Math.Abs(sortedList1[i] - sortedList2[i]);
    }

    return distance;
}

static int CalculateDistancePart2(List<int> sortedList1, List<int> sortedList2)
{
    int distance = 0;
    var frequency = new Dictionary<int, int>();

    // Build frequency table for list2
    foreach (int number in sortedList2)
    {
        if (!frequency.ContainsKey(number))
            frequency[number] = 0;

        frequency[number]++;
    }

    // Calculate the distance
    foreach (int number in sortedList1)
    {
        if (frequency.TryGetValue(number, out int count) && count > 0)
        {
            distance += number * count;
            frequency[number]--;
        }
    }

    return distance;
}

TimeSpan elapsed = stopwatch.Elapsed;

Console.WriteLine($"Elapsed time: {elapsed.TotalMilliseconds} ms");