using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Numerics;

Stopwatch sw = Stopwatch.StartNew();
Day2();

sw.Stop();
Console.WriteLine($"TimeElapsed: {sw.ElapsedMilliseconds}ms");

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