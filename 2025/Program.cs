using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Numerics;

Stopwatch sw = Stopwatch.StartNew();
Day5();

sw.Stop();
Console.WriteLine($"TimeElapsed: {sw.ElapsedMilliseconds}ms");

//--- Day 5: Cafeteria ---
void Day5()
{
    var lines = ReadInputOfDay(5).ToList();

    var freshIds = new HashSet<BigInteger>();
    var freshRanges = new List<(BigInteger, BigInteger)>();
    var ingredients = new HashSet<BigInteger>();

    var i = 0;
    for (; i < lines.Count; i++)
    {
        var idRange = lines[i];
        if (string.IsNullOrWhiteSpace(idRange))
            continue;
        if (!idRange.Contains('-'))
        {
            ingredients.Add(BigInteger.Parse(idRange));
            continue;
        }
        var ids = idRange.Split('-', StringSplitOptions.RemoveEmptyEntries).Select(s => BigInteger.Parse(s)).ToList();
        freshRanges.Add((ids[0], ids[1]));
    }

    foreach (var ingredient in ingredients)
    {
        foreach (var range in freshRanges)
        {
            if (ingredient >= range.Item1 && ingredient <= range.Item2)
            {
                freshIds.Add(ingredient);
                break;
            }
        }
    }

    var count1 = freshIds.Count();
    var count2 = 0;

    Console.WriteLine($"Day 1: part 1 = {count1}");
    Console.WriteLine($"Day 1: part 2 = {count2}");

}

//--- Day 4: Printing Department ---
void Day4()
{
    var rollLines = ReadInputOfDay(4).ToList();
    var rollCords = new List<(int x, int y)>();
    var xMax = rollLines.Count;
    var yMax = rollLines[0].Length;

    for (int x = 0; x < xMax; x++)
    {
        for (int y = 0; y < yMax; y++)
        {
            if (rollLines[x][y] == '@')
            {
                rollCords.Add((x, y));
            }
        }
    }

    var count1 = 0;
    foreach (var rollCord in rollCords)
    {
        var neighbours = GetNeighbours(rollCord);

        if (neighbours.Intersect(rollCords).Count() < 4)
        {
            count1++;
        }
    }

    List<(int x, int y)> GetNeighbours((int x, int y) cord)
    {
        var neighbours = new List<(int x, int y)>
        {
            (cord.x - 1, cord.y),
            (cord.x + 1, cord.y),

            (cord.x - 1, cord.y - 1),
            (cord.x, cord.y - 1),
            (cord.x + 1, cord.y - 1),

            (cord.x - 1, cord.y + 1),
            (cord.x, cord.y + 1),
            (cord.x + 1, cord.y + 1)
        };

        return neighbours.ToList();
    }

    var count2 = 0;
    bool somethingChanged = true;
    var copy = rollCords.ToList();
    while (somethingChanged)
    {
        somethingChanged = false;
        foreach (var rollCord in rollCords)
        {
            var neighbours = GetNeighbours(rollCord);

            if (neighbours.Intersect(rollCords).Count() < 4)
            {
                copy.Remove(rollCord);
                somethingChanged = true;
                count2++;
            }
        }
        rollCords = copy.ToList();
    }

    Console.WriteLine($"Day 1: part 1 = {count1}");
    Console.WriteLine($"Day 1: part 2 = {count2}");
}

//--- Day 3: Lobby ---
void Day3()
{
    var banks = ReadInputOfDay(3);

    var count1 = 0;
    foreach (var bank in banks)
    {
        var first = bank.Take(bank.Length - 1).Max();
        var index = bank.IndexOf(first);
        var remainder = bank.Skip(index + 1);
        var second = remainder.Max();
        count1 += (first - '0') * 10 + (second - '0');
    }

    BigInteger count2 = 0;
    foreach (var bank in banks)
    {
        var joltage = new List<char>();
        var rem = bank.ToList();
        for (int i = 12; i > 0; i--)
        {
            var first = rem.Take(rem.Count + 1 - i).Max();
            var index = rem.IndexOf(first);
            rem = rem.Skip(index + 1).ToList();

            joltage.Add(first);
        }

        Console.WriteLine(new String(joltage.ToArray()));
        count2 += BigInteger.Parse(new String(joltage.ToArray()));
    }

    Console.WriteLine($"Day 1: part 1 = {count1}");
    Console.WriteLine($"Day 1: part 2 = {count2}");
}

//--- Day 2: Gift Shop ---
void Day2()
{
    var line = ReadInputOfDay(2).First();

    var ranges = line.Split(',', StringSplitOptions.RemoveEmptyEntries);

    long count1 = 0;
    foreach (var range in ranges)
    {
        var ids = (range.Split('-', StringSplitOptions.RemoveEmptyEntries)).Select(s => long.Parse(s)).ToList();
        for (var i = ids[0]; i < ids[1]; i++)
        {
            var id = i.ToString();
            var middle = id.Length / 2;
            var part1 = id.Take(middle);
            var part2 = id.Skip(middle);
            if (part1.SequenceEqual(part2))
            {
                Console.WriteLine($"Found part1: {id}");
                count1 += long.Parse(id);
            }
        }
    }

    long count2 = 0;
    foreach (var range in ranges)
    {
        var ids = (range.Split('-', StringSplitOptions.RemoveEmptyEntries)).Select(s => long.Parse(s)).ToList();
        for (var i = ids[0]; i <= ids[1]; i++)
        {
            var divs = 2;
            var id = i.ToString();

            while (id.Length >= divs)
            {
                if (id.Length % divs == 0)
                {
                    var parts = new List<string>();
                    var divLength = id.Length / divs;
                    while (parts.Count < divs)
                    {
                        var part = id.Skip(parts.Count * divLength).Take(divLength);
                        parts.Add(new String(part.ToArray()));
                    }
                    if (parts.Distinct().Count() == 1)
                    {
                        Console.WriteLine($"Found part2: {id}");
                        count2 += long.Parse(id);
                        break;
                    }
                }
                divs++;
            }
        }
    }

    Console.WriteLine($"Day 1: part 1 = {count1}");
    Console.WriteLine($"Day 1: part 2 = {count2}");
}

//--- Day 1: Secret Entrance ---
void Day1()
{
    var lines = ReadInputOfDay(1);
    var dialPos = 50;
    var zeroCount1 = 0;
    var zeroCount2 = 0;

    foreach (var line in lines)
    {
        var ticks = int.Parse(line.Substring(1));
        var rotations = ticks / 100;
        var remainder = ticks % 100; //0 -> 99
        var direction = line[0];

        zeroCount2 += rotations;
        var startedAtZero = dialPos == 0;

        if (direction == 'L')
        {
            dialPos -= remainder;
            if (dialPos < 0)
            {
                dialPos += 100;
                if (!startedAtZero)
                    zeroCount2++;
            }
        }
        else if (direction == 'R')
        {
            dialPos += remainder;
            if (dialPos > 99)
            {
                dialPos -= 100;
                if (dialPos != 0)
                    zeroCount2++;
            }
        }
        if (dialPos == 0)
        {
            zeroCount1++;
            zeroCount2++;
        }
    }

    Console.WriteLine($"Day 1: part 1 = {zeroCount1}");
    Console.WriteLine($"Day 1: part 2 = {zeroCount2}");
}

IEnumerable<string> ReadInputOfDay(int day)
{
    return File.ReadLines(@$"C:\Users\karin\source\repos\AdventOfCode\2025\Inputs\Day{day}.txt");
}