using _2023;
using System.Drawing;

Day8();

//*
void Day1()
{
    var lines = File.ReadLines(@"C:\Users\karin\source\repos\AdventOfCode\2023\Inputs\Day1.txt");

    //1
    var sum = 0;
    var sum2 = 0;

    foreach (string line in lines)
    {
        var digits = line.Where(x => char.IsDigit(x));
        sum += (digits.First() - '0') * 10 + (digits.Last() - '0');


        sum2 += 0;
    }

    Console.WriteLine("1: " + sum);

    Console.WriteLine("2: " + sum);
}

//---Day 2: Cube Conundrum ---
void Day2()
{
    //Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
    //Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
    //Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
    //Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
    //Game5:6red,1blue,3green;2blue,1red,2green

    var lines = File.ReadLines(@"C:\Users\karin\source\repos\AdventOfCode\2023\Inputs\Day2.txt");

    var sum = 0;
    var sum2 = 0;
    var maxred = 1;
    var maxblue = 1;
    var maxgreen = 1;
    foreach (string line in lines)
    {
        //Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
        var replacedline = line.Replace(" ", "");
        var a = replacedline.Split(':');

        var games = a[1].Split(';');
        var possible = true;

        foreach (string game in games)
        {
            //3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
            var hands = game.Split(',');
            foreach (string hand in hands)
            {
                //6red,1blue,3green
                switch (hand.Substring(hand.Length - 3))
                {
                    //only 12 red cubes, 13 green cubes, and 14 blue cubes
                    case "lue":
                        //<=14 blue cubes
                        if (int.Parse(hand.Substring(0, hand.Length - 4)) > 14)
                        {
                            possible = false;
                            break;
                        }
                        else
                        { continue; }
                    case "red":
                        //<=12 red cubes
                        if (int.Parse(hand.Substring(0, hand.Length - 3)) > 12)
                        {
                            possible = false;
                            break;
                        }
                        else
                        { continue; }
                    case "een":
                        //13 green cubes
                        if (int.Parse(hand.Substring(0, hand.Length - 5)) > 13)
                        {
                            possible = false;
                            break;
                        }
                        else
                        { continue; }
                }
            }
            foreach (string hand2 in hands)
            {
                switch (hand2.Substring(hand2.Length - 3))
                {
                    //only 12 red cubes, 13 green cubes, and 14 blue cubes
                    case "lue":
                        var bluecount = int.Parse(hand2.Substring(0, hand2.Length - 4));
                        if (bluecount > maxblue)
                        {
                            maxblue = bluecount;
                        }
                        continue;
                    case "red":
                        var redcount = (int.Parse(hand2.Substring(0, hand2.Length - 3)));
                        if (redcount > maxred)
                        {
                            maxred = redcount;
                        }
                        continue;
                    case "een":
                        var greencount = (int.Parse(hand2.Substring(0, hand2.Length - 5)));
                        if (greencount > maxgreen)
                        {
                            maxgreen = greencount;
                        }
                        continue;
                }
            }
        }
        var gameId = new string(a[0].Skip(4).ToArray());
        if (possible)
        {
            sum += int.Parse(gameId);
            //Console.WriteLine("possiblegame: " + ad);
        }

        Console.WriteLine($"possiblegame{gameId}: red:{maxred} green:{maxgreen} blue:{maxblue}");
        sum2 += (maxred * maxgreen * maxblue);
        maxred = 1;
        maxblue = 1;
        maxgreen = 1;
    }

    Console.WriteLine("1: " + sum);
    Console.WriteLine("2: " + sum2);
}

//--- Day 3: Gear Ratios ---
void Day3()
{
    var lines = File.ReadLines(@"C:\Users\karin\source\repos\AdventOfCode\2023\Inputs\Day3.txt");


}

//--- Day 4: Scratchcards ---
void Day4()
{
    //Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
    var cards = File.ReadLines(@"C:\Users\karin\source\repos\AdventOfCode\2023\Inputs\Day4.txt");

    var point = 0d;
    var magnitudeList = Enumerable.Repeat(1, cards.Count()).ToList();
    var linePos = 0;

    foreach (var card in cards)
    {
        var nums = card.Split(':')[1].Split('|').ToArray();
        var wins = nums[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList().Intersect(nums[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList());

        var matches = wins.Count();
        var currentMagnuitude = magnitudeList[linePos];

        for (int i = linePos; i < linePos + matches; i++)
        {
            try
            {
                magnitudeList[i + 1] += currentMagnuitude;
            }
            catch (Exception)
            {
            }
        }

        if (matches > 0)
        {
            var cardPoint = Math.Pow(2, wins.Count() - 1);
            point += cardPoint;
        }

        linePos++;
    }

    Console.WriteLine("1: " + point);
    var point2 = magnitudeList.Sum();
    Console.WriteLine("2: " + point2);
}

//--- Day 5: If You Give A Seed A Fertilizer ---
void Day5()
{
    var lines = File.ReadLines(@"C:\Users\karin\source\repos\AdventOfCode\2023\Inputs\Day5.txt");


}

//--- Day 6: Wait For It ---
void Day6()
{
    var lines = File.ReadLines(@"C:\Users\karin\source\repos\AdventOfCode\2023\Inputs\Day6.txt").ToList();

    var times = (lines[0].Split(':'))[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
    var distances = (lines[1].Split(':'))[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

    var score = 1;

    //race
    for (int i = 0; i < times.Length; i++)
    {
        //alternatives
        var successfulRaces = 0;
        var raceTime = int.Parse(times[i]);
        var recordDistance = int.Parse(distances[i]);
        for (int j = 0; j <= raceTime; j++)
        {
            var holdTime = j;
            var timeLeftToTravel = raceTime - holdTime;
            var distanceTravelled = timeLeftToTravel * holdTime;
            if (distanceTravelled > recordDistance)
            {
                //Console.WriteLine($"Time: {j} Travelled {distanceTravelled}");
                successfulRaces++;
            }
        }
        score *= successfulRaces;
    }

    Console.WriteLine("1: " + score);

    //alternatives
    var successfulRaces2 = 0;
    var raceTime2 = 34908986;
    long recordDistance2 = 204171312101780;
    //Lower limit when 
    for (int j = 0; j <= raceTime2; j++)
    {
        var holdTime = j;
        long timeLeftToTravel = raceTime2 - holdTime;
        long distanceTravelled = timeLeftToTravel * holdTime;
        if (distanceTravelled > recordDistance2)
        {
            successfulRaces2++;
        }
    }

    Console.WriteLine("2: " + successfulRaces2);
}

void Day7()
{
    var lines = File.ReadLines(@"C:\Users\karin\source\repos\AdventOfCode\2023\Inputs\Day7.txt");

    //32T3K 765
    //T55J5 684
    //KK677 28
    //KTJJT 220
    //QQQJA 483

    //string
    //var hands = lines
    //    .Select(l => l.Split(' '))
    //    .Select(h => (new String(h[0].OrderBy(h => h).ToArray()), int.Parse(h[1])));

    var comparer = new CardComparer();

    var hands = lines
    .Select(l => l.Split(' '))
    .Select(h => (h[0].ToCharArray().OrderBy(h => h, comparer), int.Parse(h[1])));

    var orderedBids = new List<int>();

    //Five of a kind, where all five cards have the same label: AAAAA
    var one = hands.Where(h => h.Item1.Distinct().Count() == 1);
    //one.orderby(h => h.Item1);
    orderedBids.AddRange(one.Select(h => h.Item2).ToList());

    var two = hands.Where(h => h.Item1.Distinct().Count() == 2);
    //Four of a kind, where four cards have the same label and one card has a different label: AA8AA
    //var fourOfaKind = two.Where(h => Count(h => h.Item1.First()));

    //Full house, where three cards have the same label, and the remaining two cards share a different label: 23332
    var fullHouse = two;

    //Three of a kind, where three cards have the same label, and the remaining two cards are each different from any other card in the hand: TTT98
    //Two pair, where two cards share one label, two other cards share a second label, and the remaining card has a third label: 23432
    var three = hands.Where(h => h.Item1.Distinct().Count() == 3);


    //One pair, where two cards share one label, and the other three cards have a different label from the pair and each other: A23A4
    var four = hands.Where(h => h.Item1.Distinct().Count() == 4).OrderBy(h => h.Item1);
    orderedBids.AddRange(four.Select(h => h.Item2).ToList());


    //High card, where all cards' labels are distinct: 23456
    var five = hands.Where(h => h.Item1.Distinct().Count() == 5).OrderBy(h => h.Item1);
    orderedBids.AddRange(five.Select(h => h.Item2).ToList());

}

//--- Day 8: Haunted Wasteland ---
void Day8()
{
    var lines = File.ReadLines(@"C:\Users\karin\source\repos\AdventOfCode\2023\Inputs\Day8.txt").ToList();

    //    LLR
    var instructions = lines[0];

    //AAA = (BBB, BBB)
    //BBB = (AAA, ZZZ)
    //ZZZ = (ZZZ, ZZZ)
    var lookup = new Dictionary<string, (string left, string right)>();

    for (int i = 2; i < lines.Count; i++)
    {
        var a = lines[i].Split('=', StringSplitOptions.TrimEntries);
        var key = a[0];

        var b = a[1].Split(',', StringSplitOptions.TrimEntries);
        var left = b[0].Substring(1, 3);
        var right = b[1].Substring(0, 3);
        lookup.Add(key, (left, right));
    }

    var stepCount = 0;
    var current = "AAA";
    while (current != "ZZZ")
        foreach (var instruction in instructions)
        {
            if (instruction == 'R')
            {
                current = lookup[current].right;
            }
            else
            {
                current = lookup[current].left;
            }

            stepCount++;
        }

    Console.WriteLine($"1: {stepCount}");

    var current2List = lookup.Where(x => x.Key.Last() == 'A').Select(x => x.Key).ToArray();
    var counts = new List<long>();

    foreach (var current2 in current2List)
    {
        var step2 = 0;
        var curr = current2;
        while (curr.Last() != 'Z')
        {
            foreach (var instruction in instructions)
            {
                if (instruction == 'R')
                {
                    curr = lookup[curr].right;
                }
                else
                {
                    curr = lookup[curr].left;
                }

                step2++;
            }
        }
        counts.Add(step2);
    }

    var stepCount2 = counts.Sum();

    var stepCount3 = 0;
    var current3List = lookup.Where(x => x.Key.Last() == 'A').Select(x => x.Key).ToArray();
    while (!current3List.All(c => c.Last() == 'Z'))
    {
        foreach (var instruction in instructions)
        {
            if (instruction == 'R')
            {
                current3List = current3List.Select(c => lookup[c].right).ToArray();
            }
            else
            {
                current3List = current3List.Select(c => lookup[c].left).ToArray();
            }

            stepCount3++;
            if (current3List.Any(x => x.Last() == 'Z'))
            {
                Console.WriteLine($"stepCount: {stepCount3}, {string.Join('.', current3List.Where(x => x.Last() == 'Z'))}");
            }
        }
    }

    Console.WriteLine($"2: {stepCount3}");
}

//--- Day 9: Mirage Maintenance ---
void Day9()
{
    var lines = File.ReadLines(@"C:\Users\karin\source\repos\AdventOfCode\2023\Inputs\Day9.txt").ToList();

    //0 3 6 9 12 15
    //1 3 6 10 15 21
    //10 13 16 21 30 45

    var score = 0;
    var score2 = 0;
    foreach (var line in lines)
    {
        var currentSeries = line.Split(' ', StringSplitOptions.TrimEntries).Select(x => int.Parse(x)).ToList();
        var lastNumbers = new List<int>();
        var firstNumbers = new List<int>();

        while (!currentSeries.All(x => x == 0))
        {
            var newSeries = new List<int>();
            lastNumbers.Add(currentSeries.Last());
            firstNumbers.Add(currentSeries.First());

            for (int i = 0; i < currentSeries.Count() - 1; i++)
            {
                newSeries.Add(currentSeries[i + 1] - currentSeries[i]);
            }
            currentSeries = newSeries;
        }
        score += lastNumbers.Sum();
        firstNumbers.Reverse();
        var sub = 0;
        var diff = 0;
        foreach (var num in firstNumbers)
        {
            diff = num - sub;
            //Console.WriteLine($"{num} - {sub} = {diff}");          
            sub = diff;
        }
        score2 += diff;
    }

    Console.WriteLine($"1: {score}");
    Console.WriteLine($"2: {score2}");
}

//--- Day 10: Pipe Maze ---
void Day10()
{
    var lines = File.ReadLines(@"C:\Users\karin\source\repos\AdventOfCode\2023\Inputs\Day10.txt").ToList();

    //Construct map
    var map = new Dictionary<Point, char>();
    var Xmax = lines[0].Length;
    var Ymax = lines.Count();

    // X ->
    // Y V
    for (int y = 0; y < Ymax; y++)
    {
        var line = lines[y];
        for (int x = 0; x < Xmax; x++)
        {
            map.Add(new Point(x, y), line[x]);
        }
    }

    //Find loop
    var startPoint = map.First(x => x.Value == 'S').Key;
    var loop = new HashSet<Point>
    {
        startPoint
    };

    var nextPoint = FindFirst(startPoint);

    while (nextPoint.point != startPoint)
    {
        if (loop.Add(nextPoint.point) == false)
        {
            throw new Exception($"Kunde inte lägga till: {nextPoint.point} {map[nextPoint.point]}");
        }
        var prevPoint = nextPoint;
        nextPoint = FindConnectingPipe(prevPoint);
    }

    //Plot
    for (int y = 0; y < Ymax; y++)
    {
        var line = lines[y];
        for (int x = 0; x < Xmax; x++)
        {
            if (loop.Contains(new Point(x, y)))
            {
                Console.Write(map[new Point(x, y)]);
                //Console.Write('*');
            }
            else
            {
                Console.Write('.');
                //Console.Write('.');
            }
        }
        Console.WriteLine();
    }

    Console.WriteLine($"1: {loop.Count / 2}");
    //Console.WriteLine($"2: {loop}");

    (Point point, char dir) FindFirst(Point start)
    {
        //7 - F7 -
        //.FJ | 7
        //SJLL7
        //| F--J
        //LJ.LJ

        //--7
        //-SL
        //.|F

        char? right = start.X < Xmax ? map[new Point(start.X + 1, start.Y)] : null;
        char? left = start.X > 0 ? map[new Point(start.X - 1, start.Y)] : null;
        char? up = start.Y > 0 ? map[new Point(start.X, start.Y - 1)] : null;
        char? down = start.Y < Ymax ? map[new Point(start.X, start.Y + 1)] : null;

        //höger - 7 J
        if (right != null && (right == '-' || right == '7' || right == 'J'))
            return (new Point(start.X + 1, start.Y), 'L');

        //vänster - F L
        if (left != null && (left == '-' || left == 'F' || left == 'L'))
            return (new Point(start.X - 1, start.Y), 'R');

        //under | J L
        if (down != null && (down == '|' || down == 'J' || down == 'L'))
            return (new Point(start.X, start.Y + 1), 'U');

        //över | F 7
        if (up != null && (up == '|' || up == 'F' || up == '7'))
            return (new Point(start.X, start.Y - 1), 'D');

        throw new NotImplementedException();
    }

    (Point point, char dir) FindConnectingPipe((Point point, char? dir) prevPoint)
    {
        var me = map[prevPoint.point];
        switch (me)
        {
            //| is a vertical pipe connecting north and south.
            case '|':
                if (prevPoint.dir == 'U')
                {
                    return Down(prevPoint);
                }
                else if (prevPoint.dir == 'D')
                {
                    return Up(prevPoint);
                }
                else throw new Exception("sämst");

            //- is a horizontal pipe connecting east and west.
            case '-':
                if (prevPoint.dir == 'R')
                {
                    return Left(prevPoint);
                }
                else if (prevPoint.dir == 'L')
                {
                    return Right(prevPoint);
                }
                else throw new Exception("sämst");
            //L is a 90 - degree bend connecting north and east.
            case 'L':
                if (prevPoint.dir == 'U')
                {
                    return Right(prevPoint);
                }
                else if (prevPoint.dir == 'R')
                {
                    return Up(prevPoint);
                }
                else throw new Exception("sämst");
            //J is a 90 - degree bend connecting north and west.
            case 'J':
                if (prevPoint.dir == 'U')
                {
                    return Left(prevPoint);
                }
                else if (prevPoint.dir == 'L')
                {
                    return Up(prevPoint);
                }
                else throw new Exception("sämst");
            //7 is a 90 - degree bend connecting south and west.
            case '7':
                if (prevPoint.dir == 'D')
                {
                    return Left(prevPoint);
                }
                else if (prevPoint.dir == 'L')
                {
                    return Down(prevPoint);
                }
                else throw new Exception("sämst");
            //F is a 90 - degree bend connecting south and east.
            case 'F':
                if (prevPoint.dir == 'R')
                {
                    return Down(prevPoint);
                }
                else if (prevPoint.dir == 'D')
                {
                    return Right(prevPoint);
                }
                else throw new Exception("sämst");
            case 'S':
            case '.':
            default:
                throw new NotImplementedException();
        }
    }

    (Point, char) Down((Point point, char? dir) prevPoint)
    {
        return (new Point(prevPoint.point.X, prevPoint.point.Y + 1), 'U');
    }

    (Point, char) Up((Point point, char? dir) prevPoint)
    {
        return (new Point(prevPoint.point.X, prevPoint.point.Y - 1), 'D');
    }

    (Point, char) Right((Point point, char? dir) prevPoint)
    {
        return (new Point(prevPoint.point.X + 1, prevPoint.point.Y), 'L');
    }

    (Point, char) Left((Point point, char? dir) prevPoint)
    {
        return (new Point(prevPoint.point.X - 1, prevPoint.point.Y), 'R');
    }
}

//--- Day 11: Cosmic Expansion ---
void Day11()
{
    var lines = File.ReadLines(@"C:\Users\karin\source\repos\AdventOfCode\2023\Inputs\Day11.txt").ToList();

    //  0123456789 -> X cols
    //0 ...#......
    //1 .......#..
    //2 #.........
    //3 ..........
    //4 ......#...
    //5 .#........
    //6 .........#
    //7 ..........
    //8 .......#..
    //9 #...#.....
    //|
    //V
    //Y rows

    var mapY = lines.Count;
    var mapX = lines[0].Count();

    var emptyRows = new List<int>();
    var emptyCols = Enumerable.Range(0, mapX).ToList();
    var galaxies = new List<Point>();

    for (int y = 0; y < mapX; y++)
    {
        var line = lines[y];

        for (int x = 0; x < mapY; x++)
        {
            if (line[x] == '#')
            {
                emptyCols.Remove(x);
                galaxies.Add(new Point(x, y));
            }
        }

        if (line.All(x => x == '.'))
        {
            emptyRows.Add(y);
        }
    }

    var sum = 0;
    long sum2 = 0;

    for (int a = 0; a < galaxies.Count - 1; a++)
        for (int b = a + 1; b < galaxies.Count; b++)
        {
            var startGalaxy = galaxies[a];
            var endGalaxy = galaxies[b];
            var xdist = Math.Abs(endGalaxy.X - startGalaxy.X);
            var ydist = Math.Abs(endGalaxy.Y - startGalaxy.Y);

            // Add extra
            var extraY = Enumerable.Range(Math.Min(startGalaxy.Y, endGalaxy.Y), ydist).Intersect(emptyRows).Count();
            var extraX = Enumerable.Range(Math.Min(startGalaxy.X, endGalaxy.X), xdist).Intersect(emptyCols).Count();

            var distance = xdist + ydist + extraX + extraY;
            var distance2 = xdist + ydist + (1_000_000 - 1) * extraX + (1_000_000 - 1) * extraY;

            sum += distance;
            sum2 += distance2;
            //Console.WriteLine($"start {a + 1}: ({startGalaxy}) end {b + 1}: {endGalaxy}, sum: {distance}, x: {xdist}, y: {ydist}, extrax: {extraX}, extray: {extraY}");
        }

    Console.WriteLine($"1: {sum}");
    Console.WriteLine($"2: {sum2}");
}

//--- Day 12: Hot Springs ---
void Day12()
{
    var lines = File.ReadLines(@"C:\Users\karin\source\repos\AdventOfCode\2023\Inputs\Day12.txt").ToList();

    //???.### 1,1,3
    //.?? .. ?? ...?##. 1,1,3
    //?#?#?#?#?#?#?#? 1,3,1,6
    //????.#...#... 4,1,1
    //????.######..#####. 1,6,5
    //?###???????? 3,2,1


}

void Day13()
{
    var lines = File.ReadLines(@"C:\Users\karin\source\repos\AdventOfCode\2023\Inputs\Day13.txt").ToList();


}

void Day14()
{
    var lines = File.ReadLines(@"C:\Users\karin\source\repos\AdventOfCode\2023\Inputs\Day14.txt").ToList();


}

void Day15()
{
    var lines = File.ReadLines(@"C:\Users\karin\source\repos\AdventOfCode\2023\Inputs\Day15.txt").ToList();


}

void Day16()
{
    var lines = File.ReadLines(@"C:\Users\karin\source\repos\AdventOfCode\2023\Inputs\Day16.txt").ToList();


}