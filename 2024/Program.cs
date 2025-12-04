
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

Stopwatch sw = Stopwatch.StartNew();
long time1 = 0;
Day9();

sw.Stop();
Console.WriteLine($"Part1 TimeElapsed: {time1}ms");
//Console.WriteLine($"Part2 TimeElapsed: {sw.ElapsedMilliseconds}ms");

//--- Day 1: Historian Hysteria ---
void Day1()
{
    var lines = File.ReadLines(@"C:\Users\karin\source\repos\AdventOfCode\2024\Inputs\Day1.txt");

    //1
    var locations1 = new List<long>();
    var locations2 = new List<long>();

    foreach (string line in lines)
    {
        var a = line.Split("  ");
        locations1.Add(Convert.ToInt64(a[0]));
        locations2.Add(Convert.ToInt64(a[1]));
    }

    var ordered1 = locations1.OrderBy(x => x).ToList();
    var ordered2 = locations2.OrderBy(x => x).ToList();

    var sum1 = 0m;
    for (int i = 0; i < ordered1.Count; i++)
    {
        sum1 += Math.Abs(ordered1[i] - ordered2[i]);
    }

    time1 = sw.ElapsedMilliseconds;
    Console.WriteLine("1: " + sum1);

    var grouped1 = ordered1.GroupBy(x => x);
    var grouped2 = ordered2.GroupBy(x => x);

    var sum2 = 0m;
    foreach (var g1 in grouped1)
    {
        var count1 = g1.Count();
        long count2 = (grouped2.SingleOrDefault(g2 => g2.Key == g1.Key))?.Count() ?? 0;
        sum2 += count1 * count2 * g1.Key;
    }

    Console.WriteLine("2: " + sum2);
}

//--- Day 2: Red-Nosed Reports ---
void Day2()
{
    var reports = File.ReadLines(@"C:\Users\karin\source\repos\AdventOfCode\2024\Inputs\Day2.txt");

    long sum1 = 0;
    long sum2 = 0;

    foreach (var report in reports)
    {
        var levels = report.Split(' ').Select(r => Convert.ToInt64(r)).ToList();
        var diffs = new List<long>();
        var incCount = 0;
        var decCount = 0;
        var stillCount = 0;
        var highDiffCount = 0;

        if (IsSafe(levels))
        {
            sum1++;
            sum2++;
            continue;
        }

        if (highDiffCount == 1)
        {
            var diffIdx = diffs.FindIndex(x => Math.Abs(x) > 3);
            if (CheckSafety(diffIdx)) continue;
        }

        else if (incCount == 1)
        {
            var diffIdx = diffs.FindIndex(y => y > 0);
            if (CheckSafety(diffIdx)) continue;
        }

        else if (decCount == 1)
        {
            var diffIdx = diffs.FindIndex(y => y < 0);
            if (CheckSafety(diffIdx)) continue;
        }

        else if (stillCount == 1)
        {
            var diffIdx = diffs.FindIndex(y => y == 0);
            if (CheckSafety(diffIdx)) continue;
        }

        bool CheckSafety(int diffIdx)
        {
            var l1 = levels.ToList();
            l1.RemoveAt(diffIdx);
            if (IsSafe(l1))
            {
                sum2++;
                return true;
            }
            if (diffIdx < levels.Count)
            {
                var l2 = levels.ToList();
                l2.RemoveAt(diffIdx + 1);
                if (IsSafe(l2))
                {
                    sum2++;
                    return true;
                }
            }
            return false;
        }

        bool IsSafe(List<long> levs)
        {
            diffs = new List<long>();

            for (int i = 0; i < levs.Count - 1; i++)
            {
                diffs.Add(levs[i + 1] - levs[i]);
            }

            incCount = diffs.Count(x => x > 0);
            decCount = diffs.Count(y => y < 0);
            stillCount = diffs.Count(y => y == 0);
            highDiffCount = diffs.Count(x => Math.Abs(x) > 3);

            return (incCount == diffs.Count || decCount == diffs.Count) && highDiffCount == 0;
        }
    }
    time1 = sw.ElapsedMilliseconds;

    Console.WriteLine("1: " + sum1);
    Console.WriteLine("2: " + sum2);
}

//-- - Day 3: Mull It Over ---
void Day3()
{
    var input = File.ReadAllText(@"C:\Users\karin\source\repos\AdventOfCode\2024\Inputs\Day3.txt");
    //var input = "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))";

    Regex rx = new Regex(@"mul\((\d+),(\d+)\)");

    long sum1 = rx.Matches(input).Select(x => Convert.ToInt32(x.Groups[1].Value) * Convert.ToInt32(x.Groups[2].Value)).Sum();
    Console.WriteLine("1: " + sum1);
    time1 = sw.ElapsedMilliseconds;

    //input = "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))";

    var dos = input.Split("do()");

    long sum2 = 0;
    foreach (var todo in dos)
    {
        var onlydo = todo.Split("don't()");
        //Console.WriteLine(onlydo[0]);
        sum2 += rx.Matches(onlydo[0]).Select(x => Convert.ToInt32(x.Groups[1].Value) * Convert.ToInt32(x.Groups[2].Value)).Sum();
    }

    Console.WriteLine("2: " + sum2);
}

//--- Day 4: Ceres Search ---
void Day4()
{
    /*
MMMSXXMASM
MSAMXMSMSA
AMXSXMAAMM
MSAMASMSMX
XMASAMXAMM
XXAMMXXAMA
SMSMSASXSS
SAXAMASAAA
MAMMMXMMMM
MXMXAXMASX

....XXMAS.  11
.SAMXMS...  1
...S..A...  
..A.A.MS.X  2
XMASAMX.MM  12
X.....XA.A  11
S.S.S.S.SS  
.A.A.A.A.A  
..M.M.M.MM  
.X.X.XMASX  1232


.M.S......
..A..MSMS.
.M.S.MAA..
..A.ASMSM.
.M.S.M....
..........
S.S.S.S.S.
.A.A.A.A..
M.M.M.M.M.
..........
     */
    var lines = File.ReadLines(@"C:\Users\karin\source\repos\AdventOfCode\2024\Inputs\Day4.txt").ToList();

    var sum1 = 0m;

    var maxX = lines.Count;
    var maxY = lines[0].Count();

    for (int y = 0; y < maxY; y++)
    {
        for (int x = 0; x < maxX; x++)
        {
            //Console.Write(lines[y][x]);
            if (lines[y][x] == 'X')
            {
                var c = AdjacentMAS(y, x);

                //Console.WriteLine($"({y},{x}): {c}");
                sum1 += c;
            }
        }
        //Console.WriteLine();
    }

    long sum2 = 0;
    for (int y = 0; y < maxY; y++)
    {
        for (int x = 0; x < maxX; x++)
        {
            //Console.Write(lines[y][x]);
            if (lines[y][x] == 'A')
            {
                var c = AdjacentMS(y, x);
                Console.WriteLine($"({y},{x}): {c}");
                sum2 += c;
            }
        }
        //Console.WriteLine();
    }

    long AdjacentMS(int x, int y)
    {
        List<int[]> DirectionsDiagonal = [[-1, -1], [1, -1], [-1, 1], [1, 1]];
        int X = 0;
        int Y = 1;

        bool IsInside(int xx, int yy)
        {
            return (xx >= 0 && yy >= 0 && xx < maxX && yy < maxY);
        }

        var newX1 = x - 1;
        var newY1 = y - 1;
        var newX2 = x + 1;
        var newY2 = y + 1;

        var newX3 = x - 1;
        var newY3 = y + 1;
        var newX4 = x + 1;
        var newY4 = y - 1;

        if (!IsInside(newX1, newY1) || !IsInside(newX2, newY2) || !IsInside(newX3, newY3) || !IsInside(newX4, newY4))
        {
            return 0;
        }

        if (
            ((lines[newX1][newY1] == 'M' && lines[newX2][newY2] == 'S') || (lines[newX1][newY1] == 'S' && lines[newX2][newY2] == 'M')) &&
            ((lines[newX3][newY3] == 'M' && lines[newX4][newY4] == 'S') || (lines[newX3][newY3] == 'S' && lines[newX4][newY4] == 'M'))
            )
        {
            return 1;
        }

        return 0;
    }

    long AdjacentMAS(int x, int y)
    {
        //List<int[]> Directions = [[1, 0], [-1, 0], [0, 1], [0, -1]];
        List<int[]> DirectionsDiagonal = [[-1, -1], [0, -1], [1, -1], [-1, 0], [1, 0], [-1, 1], [0, 1], [1, 1]];

        int X = 0;
        int Y = 1;

        var count = 0;
        foreach (var dir in DirectionsDiagonal)
        {
            var newX = x + dir[X];
            var newY = y + dir[Y];

            if (!(newX >= 0 && newY >= 0 && newX < maxX && newY < maxY))
                continue;

            if (lines[newX][newY] == 'M')
            {
                newX = newX + dir[X];
                newY = newY + dir[Y];

                if (!(newX >= 0 && newY >= 0 && newX < maxX && newY < maxY))
                    continue;

                if (lines[newX][newY] == 'A')
                {
                    newX = newX + dir[X];
                    newY = newY + dir[Y];

                    if (!(newX >= 0 && newY >= 0 && newX < maxX && newY < maxY))
                        continue;

                    if (lines[newX][newY] == 'S')
                    {
                        count++;
                    }
                }
            }
        }
        return count;
    }

    Console.WriteLine("1: " + sum1);
    Console.WriteLine("2: " + sum2);
}

//--- Day 5: Print Queue ---
void Day5()
{
    var lines = File.ReadLines(@"C:\Users\karin\source\repos\AdventOfCode\2024\Inputs\Day5.txt");

    var isRules = true;
    var rules = new ConcurrentDictionary<int, List<int>>();
    var updates = new List<List<int>>();

    foreach (var line in lines)
    {
        if (string.IsNullOrEmpty(line))
        {
            isRules = false;
            continue;
        }
        //Parse rules
        if (isRules)
        {
            var r = line.Split('|');
            rules.AddOrUpdate(int.Parse(r[0]), new List<int> { int.Parse(r[1]) }, (k, v) => { v.Add(int.Parse(r[1])); return v; });
        }
        //Parse updates
        else
        {
            var u = line.Split(",").Select(l => int.Parse(l)).ToList();
            updates.Add(u);
        }
    }

    var visited = new List<int>();
    var incorrectUpdates = new List<Queue<int>>();

    long sum1 = 0;
    foreach (var update in updates)
    {
        var printed = new List<int>();
        var allClear = true;
        foreach (var page in update)
        {
            printed.Add(page);
            var pageRules = rules.Where(r => r.Key == page).SingleOrDefault().Value;
            if (pageRules == null)
            {
                continue;
            }

            if (printed.Intersect(pageRules).Any())
            {
                allClear = false;
                break;
            }
        }

        //All rules are good
        if (allClear)
        {
            var mid = update[update.Count / 2];
            Console.Write($"{mid}, ");
            sum1 += update[update.Count / 2];
        }
        else
        {
            var queue = new Queue<int>(update);
            incorrectUpdates.Add(queue);
        }
    }

    Console.WriteLine();
    Console.WriteLine("1: " + sum1);


    long sum2 = 0;
    foreach (var update in incorrectUpdates)
    {
        var orderedUpdate = new List<int>();
        var rules2 = rules.Where(r => (update.ToList()).Contains(r.Key)).ToList();

        while (update.Count() > 0)
        {
            //try page
            var page = update.Dequeue();
            var otherPageRules = rules2.Where(r => r.Key != page).ToList();
            if (!otherPageRules.Any(x => x.Value.Contains(page)))
            {
                //success
                Console.Write(page + ", ");
                orderedUpdate.Add(page);
                rules2 = otherPageRules;
            }
            else
            {
                //fail
                update.Enqueue(page);
            }
        }
        //All ordered
        var mid = orderedUpdate[orderedUpdate.Count / 2];
        Console.WriteLine($"mid: {mid}");
        sum2 += mid;
    }

    Console.WriteLine();
    Console.WriteLine("2: " + sum2);
}

//--- Day 6: Guard Gallivant ---
void Day6()
{
    /*
    ....#.....
    .........#
    ..........
    ..#.......
    .......#..
    ..........
    .#..^.....
    ........#.
    #.........
    ......#...
    */
    var lines = File.ReadLines(@"C:\Users\karin\source\repos\AdventOfCode\2024\Inputs\Day6.txt").ToList();

    long sum1 = 0;
    (int Y, int X) start = lines.Select((l, i) => (y: i, x: l.IndexOf('^'))).First(q => q.x >= 0);

    (int x, int y) dir = (0, -1);
    var guardPosition = (start.X, start.Y);

    var visitedPos = new List<(int, int)> { guardPosition };

    while (true)
    {
        //move in dir
        (int X, int Y) newPos = Move(guardPosition, dir);

        if (newPos.X < 0 || newPos.Y < 0 || newPos.X >= lines[0].Length || newPos.Y >= lines.Count)
        { break; }
        //if # - turn right
        var newChar = lines[newPos.Y][newPos.X];
        if (newChar == '#')
        {
            dir = MoveRight(dir);
            continue;
        }

        //if not X ++
        if (!visitedPos.Contains(newPos))
        {
            sum1++;
            visitedPos.Add(newPos);
        }

        guardPosition = newPos;
    }

    (int, int) Move((int x, int y) pos, (int dx, int dy) dir)
    {
        return (pos.x + dir.dx, pos.y + dir.dy);
    }

    (int, int) MoveRight((int x, int y) dir)
    {
        switch (dir)
        {
            case (1, 0): return (0, 1); //Right -> Down
            case (0, 1): return (-1, 0); //Down -> Left
            case (-1, 0): return (0, -1); //Left - > Up
            case (0, -1): return (1, 0); //Up -> Right
            default: throw new Exception();
        }
    }
    Console.WriteLine("1: " + sum1);

    long sum2 = 0;

    for (int i = 0; i < lines.Count; i++)
    {
        var lines2 = lines.ToList();
        var line = lines2[i];
        for (int j = 0; j < line.Count(); j++)
        {
            var guardPosition2 = (start.X, start.Y);
            (int x, int y) dir2 = (0, -1);
            //No obstacle on start position
            if ((i, j) == (start.X, start.Y))
            {
                continue;
            }

            StringBuilder sb = new StringBuilder(line);
            sb[j] = '#';
            lines2[i] = sb.ToString();

            var visitedPos2 = new List<((int, int) pos, (int, int) dir)>() { (guardPosition2, dir2) };

            while (true)
            {
                //move in dir
                (int X, int Y) newPos = Move(guardPosition2, dir2);

                if (newPos.X < 0 || newPos.Y < 0 || newPos.X >= lines[0].Length || newPos.Y >= lines.Count)
                { break; }
                //if # - turn right
                var newChar = lines2[newPos.Y][newPos.X];
                if (newChar == '#')
                {
                    dir2 = MoveRight(dir2);
                    continue;
                }

                //if visited before with same direction = loop
                if (visitedPos2.Contains((newPos, dir2)))
                {
                    //Console.WriteLine($"#pos: ({i},{j}), loop-pos: {newPos}, dir: {dir2}, visited: {string.Join(',', visitedPos2)}");
                    sum2++;
                    break;
                }

                visitedPos2.Add((newPos, dir2));
                guardPosition2 = newPos;
            }
        }
    }

    Console.WriteLine("2: " + sum2);
}

void Day7()
{
    /*
190: 10 19
3267: 81 40 27
83: 17 5
156: 15 6
7290: 6 8 6 15
161011: 16 10 13
192: 17 8 14
21037: 9 7 18 13
292: 11 6 16 20
     */
    var lines = File.ReadLines(@"C:\Users\karin\source\repos\AdventOfCode\2024\Inputs\Day7.txt").ToList();
    long sum1 = 0;

    foreach (var line in lines)
    {
        var a = line.Split(':');
        var testValue = long.Parse(a[0]);
        var numbers = (a[1].Split(' ', StringSplitOptions.RemoveEmptyEntries)).Select(int.Parse).ToList();

        var results = new List<long> { numbers[0] };

        for (int i = 1; i < numbers.Count; i++)
        {
            var nextResults = new List<long>();

            foreach (var result in results)
            {
                var addResult = result + numbers[i];
                var multResult = result * numbers[i];
                var concatResult = long.Parse($"{result}{numbers[i]}");

                nextResults.Add(addResult);
                nextResults.Add(multResult);
                nextResults.Add(concatResult);
            }

            results = nextResults;
        }
        if (results.Contains(testValue))
        {
            sum1 += testValue;
            Console.WriteLine($"Hit!! {testValue}, ");
        }
    }

    Console.WriteLine("1: " + sum1);

    long sum2 = 0;
    Console.WriteLine("2: " + sum2);
}

//--- Day 8: Resonant Collinearity ---
void Day8()
{
    /*
............
........0...
.....0......
.......0....
....0.......
......A.....
............
............
........A...
.........A..
............
............
     */
    var lines = File.ReadLines(@"C:\Users\karin\source\repos\AdventOfCode\2024\Inputs\Day8.txt").ToList();

    var frequencies = lines.SelectMany(x => x).Distinct().Where(x => x != '.');

    var antinodes = new HashSet<(int x, int y)>();

    //För varje typ av frekvens/bokstav
    foreach (var freq in frequencies)
    {
        var freqIndeces = lines.Select((l, i) => (y: i, x: l.IndexOf(freq))).Where(q => q.x >= 0).ToList();
        for (int i = 0; i < freqIndeces.Count; i++)
        {
            (int x, int y) currentIndex = freqIndeces[i];
            var otherIndeces = freqIndeces.Where(x => x != freqIndeces[i]).ToList();

            //För varje index, hitta dx, dy till nästa och räkna ut antinoderna
            foreach ((int x, int y) other in otherIndeces)
            {
                var antinode = (2 * currentIndex.x - other.x, 2 * currentIndex.y - other.y);

                antinodes.Add(antinode);
                //Console.WriteLine(antinode);
            }
        }
    }

    var sum1 = antinodes.Count(i => i.x >= 0 && i.y >= 0 && i.x < lines.Count && i.y < lines.Count);

    Console.WriteLine("sum1: " + sum1);

    var antinodes2 = new HashSet<(int x, int y)>();

    foreach (var freq in frequencies)
    {
        var freqIndeces = lines.Select((l, i) => (y: i, x: l.IndexOf(freq))).Where(q => q.x >= 0).ToList();
        for (int i = 0; i < freqIndeces.Count; i++)
        {
            (int x, int y) currentIndex = freqIndeces[i];
            var otherIndeces = freqIndeces.Where(x => x != freqIndeces[i]).ToList();

            //För varje index, hitta dx, dy till nästa och räkna ut antinoderna
            foreach ((int x, int y) other in otherIndeces)
            {
                var dx = other.x - currentIndex.x;
                var dy = other.y - currentIndex.y;

                var startIndex = currentIndex;
                while (true)
                {
                    var antinode = (startIndex.x + dx, startIndex.y + dy);
                    if (!InsideGrid(antinode, lines.Count))
                    {
                        break;
                    }
                    antinodes2.Add(antinode);
                    startIndex = antinode;
                }
            }
        }
    }

    var sum2 = antinodes2.Count(i => i.x >= 0 && i.y >= 0 && i.x < lines.Count && i.y < lines.Count);
    foreach (var antinode in antinodes2.OrderBy(x => x))
    {
        Console.WriteLine(antinode);
    }
    Console.WriteLine("sum2: " + sum2);
}

bool InsideGrid((int x, int y) node, int side)
{
    return node.x >= 0 && node.y >= 0 && node.x < side && node.y < side;
}

//---Day 9: Disk Fragmenter ---
void Day9()
{
    /*
     2333133121414131402
    */

    var line = "2333133121414131402".ToList();
    //var line = File.ReadAllText(@"C:\Users\karin\source\repos\AdventOfCode\2024\Inputs\Day9.txt").ToList();

    var even = true;
    var blocks = new List<int>();
    var blocks2 = new List<(int count, int value)>();

    int currIdx = 0;
    int evenIdx = 0;
    for (int i = 0; i < line.Count; i++)
    {
        var count = line[i] - '0';
        for (int e = 0; e < count; e++)
        {
            if (even)
            {
                blocks.Add(evenIdx);
            }
            else
            {
                blocks.Add(-1);
            }
            currIdx++;
        }
        if (even)
        {
            blocks2.Add((count, evenIdx));
            evenIdx++;
        }
        else
        {
            blocks2.Add((count, -1));
        }
        even = !even;
    }

    var blockList = blocks.ToList();
    var lastBlockIdx = blockList.FindLastIndex(x => x != -1);
    var firstGapIdx = blockList.FindIndex(x => x == -1);

    //while (firstGapIdx < lastBlockIdx)
    //{
    //    blockList[firstGapIdx] = blockList[lastBlockIdx];
    //    blockList[lastBlockIdx] = -1;

    //    lastBlockIdx = blockList.FindLastIndex(x => x != -1);
    //    firstGapIdx = blockList.FindIndex(x => x == -1);
    //}

    long sum1 = 0;
    for (int i = 0; i < blockList.Where(b => b != -1).Count(); i++)
    {
        sum1 += i * blockList[i];
    }

    Console.WriteLine("sum1: " + sum1);
    time1 = sw.ElapsedMilliseconds;

    var blockList2 = new LinkedList<(int count, int value)>(blocks2);

    var block = blockList2.Last;

    while (true)
    {
        //Find next block
        while (block.Value.value == -1)
        {
            block = block.Next;
        }

        var gap = blockList2.First;
        //Find first eligable gap or go to next block
        while (gap.Value.value != -1 && gap.Value.count < block.Value.count && gap != null)
        {
            gap = gap.Next;
        }

        //No match
        if (gap == null)
        {
            block = block.Next;
        }
        //gap match
        else
        {
            var remainingGap = gap.Value.count - block.Value.count;
            //Insert before
            blockList2.AddBefore(gap, block);
            if (remainingGap > 0)
            {
                var newGap = (-1, gap.Value.count - block.Value.count);
                blockList2.AddBefore(gap, newGap);
            }

        }
    }

    /*
    2333133121414131402
    00...111...2...333.44.5555.6666.777.888899
    0*0 + 1*0 + 2*...
    */
    //var a = new (int count, int value)[]{ (2, 0), (3, -1), (3, 1), (3, -1), (1, 2), (3, -1), 
    //    (3, 3), (1, -1), (2, 4), (1, -1), (4, 5),(1, -1), (4, 6), (1, -1), (3, 7), (1, -1), (3, 8), (0, -1), (2, 9)};

    ////var b = "2313244342".Reverse().ToList();
    ////var g = "333111110";

    //(int count, int value) lastBlock = a.First(x => x.value != -1);
    //(int count, int value) firstGap = a.Last(x => x.value == -1);


    //hitta första lastblock som får plast i firstGap
    //Lägg till på defragmentedstring
    //

    long sum2;
}

void Day10()
{
    /*
89010123
78121874
87430965
96549874
45678903
32019012
01329801
10456732
     */
    var lines = File.ReadLines(@"C:\Users\karin\source\repos\AdventOfCode\2024\Inputs\Day10.txt").ToList();

    //För varje head
    IEnumerable<(int y, int x)> heads = lines.Select((l, i) => (y: i, x: l.IndexOf('^'))).Where(q => q.x >= 0);

    long sum1 = 0;
    foreach (var head in heads)
    {
        //hitta alla paths
        //kolla grannar
        //Om grannar denna + 1 fortsätt
        //Om granne/ar = 9 så räkna och hoppa ur
    }

    int Adjacent(int x, int y)
    {
        var headcount = 0;
        List<int[]> directions = [[-1, 0], [1, 0], [0, 1], [0, -1]];
        int X = 0;
        int Y = 1;

        foreach (var dir in directions)
        {
            var neighbour = (x + dir[X], y + dir[Y]);
            if (InsideGrid(neighbour, lines.Count) && lines([x + dir[X]][y + dir[Y]])
            {
                return headcount += Adjacent(neighbour);
            }
        }
    }



    long sum2;
}

void Day11()
{
    var lines = File.ReadLines(@"C:\Users\karin\source\repos\AdventOfCode\2024\Inputs\Day11.txt").ToList();
    long sum1 = 0;
    long sum2;
}