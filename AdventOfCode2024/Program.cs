string[] data = Shared.Files.ReadPerLine($"{Path.Combine(Environment.CurrentDirectory, "part1.txt")}");

// Data will be presented per line, this will need to be put in a sorted list
List<int> list1 = new();
List<int> list2 = new();

// Load the data
foreach (string line in data)
{
    string[] splitData = line.Trim().Split("   ");
    list1.Add(int.Parse(splitData[0]));
    list2.Add(int.Parse(splitData[1]));
}

// Copy the item count
int itemCount = list1.Count;

// Distance
int distance = 0;

#region Part 2

distance = FindDistanceMatch(ref list1, ref list2);

Console.WriteLine($"Part 2: {distance}");

#endregion

#region Part 1
distance = 0;
// Get the lowest values
for (int i=0; i < itemCount; i++)
{
    int a = FindAndRemoveLowest(ref list1);
    int b = FindAndRemoveLowest(ref list2);

    if (a > b)
        distance += a - b;
    else if (b > a)
        distance += b - a;
}

Console.WriteLine($"Part 1: {distance}");

#endregion



static int FindAndRemoveLowest(ref List<int> list)
{
    int lowestIndex = -1;
    int lowestNumber = int.MaxValue;

    for (int i = 0; i < list.Count; i++)
    {
        int element = list.ElementAt(i);
        
        if (element < lowestNumber)
        {
            lowestIndex = i;
            lowestNumber = element;
        }
    }

    if (lowestIndex != -1)
        list.RemoveAt(lowestIndex);

    return lowestNumber;
}

static int FindDistanceMatch(ref List<int> listA, ref List<int> listB)
{
    Dictionary<int, int> lookupTable = new Dictionary<int, int>();
    int distance = 0;

    for (int i = 0; i < listA.Count; i++)
    {
        int element = listA.ElementAt(i);

        // Found existing match, no need to loop through
        if (lookupTable.TryGetValue(element, out int value))
        {
            distance += value;
            continue;
        }

        int itemCount = 0;
        for (int j = 0; j < listB.Count; j++)
        {
            if (listB.ElementAt(j) == element)
                itemCount++;
        }

        lookupTable.Add(element, itemCount);
        distance += element * itemCount;
    }

    return distance;
}

