using System.Diagnostics;
using System.Numerics;

Stopwatch sw = Stopwatch.StartNew();
Day3();

sw.Stop();
Console.WriteLine($"TimeElapsed: {sw.ElapsedMilliseconds}ms");

void Day3()
{
    var banks = ReadInputOfDay(3);

    var count = 0;
    foreach (var bank in banks)
    {
        var first = bank.Take(bank.Length - 1).Max();
        var index = bank.IndexOf(first);
        var remainder = bank.Skip(index + 1);
        var second = remainder.Max();
        count += (first- '0') * 10 + (second - '0');
    }

    BigInteger count2 = 0;
    foreach (var bank in banks)
    {
        var joltage = new List<char>();
        var rem = bank;
        for (int i = 12; i > 0; i--)
        {
            var first = rem.Take(rem.Length - i).Max();
            var index = rem.IndexOf(first);
            rem = rem.Substring(index + 1);

            joltage.Add(first);
        }

        count2 = 0;
        for (int i = 0; i < joltage.Count; i++)
        {
            BigInteger potence = Math.Pow(10, (12 - i));
            var multiplier = (joltage[i] - '0');
            BigInteger internalcount = (int)potence * multiplier;
            Console.WriteLine(internalcount);
            count2 += (int)potence * multiplier;
        }
    }

    Console.WriteLine($"Day 1: part 1 = {count}");
    Console.WriteLine($"Day 1: part 2 = {count2}");
}

void Day2()
{
    var line = "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124";
    //var line = ReadInputOfDay(2);

    var ranges = line.Split(',', StringSplitOptions.RemoveEmptyEntries);
    var ids = ranges.SelectMany(r => r.Split('-', StringSplitOptions.RemoveEmptyEntries));
    foreach (var id in ids)
    {
        var i = 1;
        var first = id.Substring(0, i);
        var rest = id.Substring(i);
        var next = rest.Substring(0, i);
        var count = 0;
        while (first == next)
        {
            first = next;
            next = next.Substring(i);
            if (!next.Any())
            {
                count+= int.Parse(id);
                break;
            }
        }
    }
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