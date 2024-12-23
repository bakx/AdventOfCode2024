using System.Diagnostics;

Stopwatch stopwatch = new Stopwatch();
stopwatch.Start();

string[] data = Shared.Files.ReadPerLine($"{Path.Combine(Environment.CurrentDirectory, "input.txt")}");

// Data will be presented per line, this will need to be put in sorted lists
List<List<int>> list = [];

// Load and parse the data
foreach (string line in data)
{
    List<int> listData = [];
    foreach (string s in line.Trim().Split(" "))
    {
        listData.Add(int.Parse(s));
    }

    list.Add(listData);
}

int safetyPart1 = CalculateSafetyPart1(ref list);
Console.WriteLine($"Part 1: {safetyPart1}");

int safetyPart2 = CalculateSafetyPart2(ref list);
Console.WriteLine($"Part 2: {safetyPart2}");

static int CalculateSafetyPart1(ref List<List<int>> list)
{
    int safety = 0;

    for (int i = 0; i < list.Count; i++)
    {
        List<int> workItem = list[i];
        safety += CheckSafety(ref workItem);
    }

    return safety;
}

static int CalculateSafetyPart2(ref List<List<int>> list)
{
    int safety = 0;
    int result = 0;

    for (int i = 0; i < list.Count; i++)
    {
        // Process list item
        for (int j = 0; j < list[i].Count; j++)
        {
            List<int> workList = new List<int>(list[i]);
            workList.RemoveAt(j);

            result = CheckSafety(ref workList);

            if (result == 1)
            {
                safety++;
                break;
            }
        }
    }

    return safety;
}

TimeSpan elapsed = stopwatch.Elapsed;

Console.WriteLine($"Elapsed time: {elapsed.TotalMilliseconds} ms");

static int CheckSafety(ref List<int> list, int maxLevel = 3)
{
    // 0 = invalid, 1 is up, 2 is down
    int direction;
    bool isSafe = true;

    if (list[0] > list[0 + 1])
    {
        direction = 1;
    }
    else if (list[0] < list[0 + 1])
    {
        direction = 2;
    }
    else
    {
        return 0;
    }

    // Each item here, should either go up or down
    for (int j = 0; j < list.Count - 1; j++)
    {
        if (
            list[j] == list[j + 1] ||                    // Not going up or down
            direction == 1 && list[j] < list[j + 1] ||   // Direction is invalid
            direction == 2 && list[j] > list[j + 1] ||   // Direction is invalid
            Math.Abs(list[j] - list[j + 1]) > maxLevel
            )
        {
            isSafe = false;
            break;
        }
    }

    return isSafe ? 1 : 0;
}