// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using System.Drawing;
using System.Linq.Expressions;
using System.Numerics;
using System.Text.RegularExpressions;

Day16();

void Day1()
{
    // How many total Calories is that Elf carrying?
    // Read file
    var lines = File.ReadLines(@"C:\Users\karin\source\repos\AdventOfCode\2022\Inputs\Day1.txt");

    var sums = new List<int>();
    var sum = 0;
    foreach (string cal in lines)
    {
        if (string.IsNullOrEmpty(cal))
        {
            sums.Add(sum);
            sum = 0;
            continue;
        }

        sum = sum += int.Parse(cal);
    }

    Console.WriteLine("1: " + sums.Max());
    Console.WriteLine("2: " + sums.OrderByDescending(x => x).Take(3).Sum());
}

void Day2()
{

    //The score for a single round is the score for the shape you selected (1 for Rock, 2 for Paper, and 3 for Scissors)
    //plus the score for the outcome of the round (0 if you lost, 3 if the round was a draw, and 6 if you won).
    //A Y
    //B X
    //C Z

    var lines = File.ReadLines(@"C:\Users\karin\source\repos\AdventOfCode\2022\Inputs\Day2.txt");

    Dictionary<char, int> You = new Dictionary<char, int> { };
    You.Add('X', 1);
    You.Add('Y', 2);
    You.Add('Z', 3);

    Dictionary<char, int> Opp = new Dictionary<char, int> { };
    Opp.Add('A', 1);
    Opp.Add('B', 2);
    Opp.Add('C', 3);

    var sum = 0;
    var sum2 = 0;
    foreach (var line in lines)
    {
        char opp = line.First();
        char you = line.Last();

        //Calc score
        sum += OutcomeScore(Opp[opp], You[you]) + You[you];
        sum2 += OutComeScore2(Opp[opp], You[you]);
    }

    Console.WriteLine("1: " + sum);
    Console.WriteLine("2: " + sum2);

    int OutComeScore2(int opp, int strategy)
    {
        //X means you need to lose, Y means you need to end the round in a draw, and Z means you need to win

        if (strategy == 1) //lose 0
        {
            switch (opp)
            {
                case 1: return 3;
                case 2: return 1;
                case 3: return 2;
                default:
                    break;
            }
        }

        if (strategy == 2) //draw 3
        {
            return 3 + opp;
        }

        if (strategy == 3) //win 6
        {
            switch (opp)
            {
                case 1: return 6 + 2;
                case 2: return 6 + 3;
                case 3: return 6 + 1;
                default:
                    break;
            }
        }

        throw new Exception("Fel fel fel");
    }

    int OutcomeScore(int opp, int you)
    {
        // 1 for Rock, 2 for Paper, and 3 for Scissors
        //A for Rock, B for Paper, and C for Scissors
        //0 if you lost, 3 if the round was a draw, and 6 if you won

        if (opp == you)
        {
            return 3;
        }

        if (opp == 1)
        {
            return you == 2 ? 6 : 0;
        }

        if (opp == 2)
        {
            return you == 3 ? 6 : 0;
        }

        if (opp == 3)
        {
            return you == 1 ? 6 : 0;
        }

        throw new Exception("Fel fel fel");
    }
}

void Day3()
{
    var lines = File.ReadLines(@"C:\Users\karin\source\repos\AdventOfCode\2022\Inputs\Day3.txt");

    var lines2 = new List<string> {
    "vJrwpWtwJgWrhcsFMMfFFhFp",
"jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL",
"PmmdzqPrVvPwwTWBwg",
"wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn",
"ttgJtRGJQctTZtZT",
"CrZsJsPPZsGzwwsLwLmpwMDw"};

    var sum = 0;
    var sum2 = 0;
    foreach (var rucksack in lines)
    {
        //Split into 2
        var item1 = rucksack.Take(rucksack.Length / 2);
        var item2 = rucksack.Skip(rucksack.Length / 2);
        //compare
        var s = item1.Intersect(item2).Single();
        var duplicate = (int)item1.Intersect(item2).Single();

        //find priority
        var priority = duplicate > 96 ? duplicate - 96 : duplicate - 64 + 26;
        sum += priority;
    }

    var lines3 = lines.ToList();
    for (int i = 0; i < lines.Count(); i += 3)
    {
        var item1 = lines3[i];
        var item2 = lines3[i + 1];
        var item3 = lines3[i + 2];

        var a = item1.Intersect(item2);
        var duplicate = a.Intersect(item3).Single();

        var priority = duplicate > 96 ? duplicate - 96 : duplicate - 64 + 26;
        sum2 += priority;
    }

    Console.WriteLine("1: " + sum);
    Console.WriteLine("2: " + sum2);
}

void Day4()
{
    var lines = File.ReadLines(@"C:\Users\karin\source\repos\AdventOfCode\2022\Inputs\Day4.txt");

    var lines2 = new List<string>
    {
        "2-4,6-8",
        "2-3,4-5",
        "5-7,7-9",
        "2-8,3-7",
        "6-6,4-6",
        "2-6,4-8"
    };

    var sum = 0;
    var sum2 = 0;
    foreach (var line in lines)
    {
        var span1 = line.Split(',').First().Split('-').Select(int.Parse);
        var span2 = line.Split(',').Last().Split('-').Select(int.Parse);

        if ((span1.First() <= span2.First() && span1.Last() >= span2.Last()) || (span2.First() <= span1.First() && span2.Last() >= span1.Last()))
        {
            sum++;
        }

        if (!(span1.Last() < span2.First() || span2.Last() < span1.First()))
        {
            sum2++;
        }
    }

    Console.WriteLine("1: " + sum);
    Console.WriteLine("2: " + sum2);
}

void Day5()
{
    var lines = File.ReadLines(@"C:\Users\karin\source\repos\AdventOfCode\2022\Inputs\Day5.txt").ToList();

    //    [D]
    //    [N][C]
    //    [Z][M][P]
    //    1   2   3

    //move 1 from 2 to 1
    //move 3 from 1 to 3
    //move 2 from 2 to 1
    //move 1 from 1 to 2

    //[B]                     [N]     [H]
    //[V]         [P] [T]     [V]     [P]
    //[W]     [C] [T] [S]     [H]     [N]
    //[T]     [J] [Z] [M] [N] [F]     [L]
    //[Q]     [W] [N] [J] [T] [Q] [R] [B]
    //[N] [B] [Q] [R] [V] [F] [D] [F] [M]
    //[H] [W] [S] [J] [P] [W] [L] [P] [S]
    //[D] [D] [T] [F] [G] [B] [B] [H] [Z]
    // 1   2   3   4   5   6   7   8   9 

    var stacks = new List<LinkedList<char>>
    {
        new LinkedList<char> {},
        new LinkedList<char> {},
        new LinkedList<char> {},
        new LinkedList<char> {},
        new LinkedList<char> {},
        new LinkedList<char> {},
        new LinkedList<char> {},
        new LinkedList<char> {},
        new LinkedList<char> {}
    };

    // Every layer from 7 -> 0
    for (int i = 7; i >= 0; i--)
    {
        // Every crate populate
        var line = lines[i];
        for (int j = 0; j < 9; j++)
        {
            var crate = line[1 + j * 4];
            if (crate != ' ')
            {
                stacks[j].AddFirst(line[1 + j * 4]);
            }
        }
    }

    foreach (var line in lines.Skip(10))
    {
        //move 2 from 8 to 1
        var commands = line.Split(new[] { "move ", " from ", " to " }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
        //Move9000(commands[0], commands[1], commands[2]);
        Move9001(commands[0], commands[1], commands[2]);
    }

    var sum = "";
    foreach (var stack in stacks)
    {
        sum += stack.First();
    }

    Console.WriteLine("1: " + sum);

    void Move9001(int times, int fromList, int toList)
    {
        var val = new LinkedList<char>();
        for (int i = 0; i < times; i++)
        {
            //Pop
            val.AddFirst(stacks[fromList - 1].First());
            stacks[fromList - 1].RemoveFirst();
        }

        for (int i = 0; i < times; i++)
        {
            //push
            stacks[toList - 1].AddFirst(val.First());
            val.RemoveFirst();
        }
    }

    void Move9000(int times, int fromList, int toList)
    {
        for (int i = 0; i < times; i++)
        {
            //Pop
            var val = stacks[fromList - 1].First();
            stacks[fromList - 1].RemoveFirst();

            //push
            stacks[toList - 1].AddFirst(val);
        }
    }
}

void Day6()
{
    var lines = File.ReadLines(@"C:\Users\karin\source\repos\AdventOfCode\2022\Inputs\Day6.txt").First(); //.ToCharArray();

    //var lines = "nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg";

    int take = 14;
    int skip = 0;
    while (true)
    {
        var a = lines.Substring(skip, take);
        if (a.Distinct().Count() == take)
        {
            break;
        }
        skip++;
    }

    var sum = skip + take;

    Console.WriteLine("1: " + sum);
}

void Day7()
{
    var lines = File.ReadLines(@"C:\Users\karin\source\repos\AdventOfCode\2022\Inputs\Day7.txt").ToList();

    //$ cd /
    //$ ls
    //dir a
    //14848514 b.txt
    //8504156 c.dat
    //dir d
    //$ cd a
    //$ ls
    //dir e
    //29116 f
    //2557 g
    //62596 h.lst
    //$ cd e
    //$ ls
    //584 i
    //$ cd..
    //$ cd..
    //$ cd d
    //$ ls
    //4060174 j
    //8033020 d.log
    //5626152 d.ext
    //7214296 k

    //Find all of the directories with a total size of at most 100000. What is the sum of the total sizes of those directories?

    //$ ls
    //dir a 
    //14848514 b.txt
    //$ cd a
    //$ cd..

    for (int i = 0; i < lines.Count; i++)
    {
        var a = lines[i].Split(' ');

        if (a[0] == "§")
        {
            if (a[1] == "cd")
            {
                //Add dir a[2]
            }
            if (a[1] == "ls")
            {

            }
            if (a[1] == "cd..")
            {

            }
        }

        //if (line == "§ ls")
        //{

        //}

        //if (line == "§ ls")
        //{

        //}

        //if (line == "§ ls")
        //{

        //}

        //if (line == "§ ls")
        //{

        //}
    }

    //var sum = 0;
    //var sum2 = 0;

    //Console.WriteLine("1: " + sum);
    //Console.WriteLine("2: " + sum2);
}

void Day8()
{
    var lines = File.ReadLines(@"C:\Users\karin\source\repos\AdventOfCode\2022\Inputs\Day8.txt");

    //30373
    //25512
    //65332
    //33549
    //35390

    var lines2 = new List<string> { "30373", "25512", "65332", "33549", "35390" };

    //how many trees are visible from outside the grid?



    var sum = 0;
    var sum2 = 0;

    Console.WriteLine("1: " + sum);
    Console.WriteLine("2: " + sum2);
}

//**
void Day9()
{
    var lines = File.ReadLines(@"C:\Users\karin\source\repos\AdventOfCode\2022\Inputs\Day9.txt");
    var tails = new HashSet<Point>();

    //Del 1
    //var knots = new List<Point> { new Point(0, 0), new Point(0, 0) };

    //Del2
    var knots = new List<Point> { new Point(0, 0), new Point(0, 0), new Point(0, 0), new Point(0, 0), new Point(0, 0), new Point(0, 0), new Point(0, 0), new Point(0, 0), new Point(0, 0), new Point(0, 0) };

    foreach (var line in lines)
    {
        var a = line.Split(' ');

        var dir = a[0];
        var times = int.Parse(a[1]);

        for (int i = 0; i < times; i++)
        {
            MoveHead(dir);
            MoveTails();

            //Add tail to tails
            tails.Add(knots.Last());
        }
    }

    //How many positions does the tail of the rope visit at least once?
    var sum = tails.Count;

    Console.WriteLine("1: " + sum);

    void MoveHead(string dir)
    {
        var dx = 0;
        var dy = 0;

        var head = knots.First();

        //   U
        //L  -  R
        //   D
        //Move head
        if (dir == "R") { dx = 1; }
        if (dir == "U") { dy = 1; }
        if (dir == "L") { dx = -1; }
        if (dir == "D") { dy = -1; }

        head.Offset(dx, dy);
        knots[0] = head;
    }

    void MoveTails()
    {
        //Move tails
        for (int i = 0; i < knots.Count - 1; i++)
        {
            var head = knots[i];
            var tail = knots[i + 1];

            var diffX = head.X - tail.X;
            var diffY = head.Y - tail.Y;

            if (Math.Abs(diffX) > 1 && Math.Abs(diffY) == 0)
            {
                tail.X += Math.Sign(diffX);
            }

            else if (Math.Abs(diffY) > 1 && Math.Abs(diffX) == 0)
            {
                tail.Y += Math.Sign(diffY);
            }

            else if (Math.Abs(diffX) > 1 || Math.Abs(diffY) > 1)
            {
                tail.X += Math.Sign(diffX);
                tail.Y += Math.Sign(diffY);
            }

            knots[i + 1] = tail;
        }
    }
}

//**
void Day10()
{
    var lines = File.ReadLines(@"C:\Users\karin\source\repos\AdventOfCode\2022\Inputs\Day10.txt").ToList();

    var newLine = new List<int> { 1 };
    var x = 1;
    var spritePos = new List<int> { };

    var nr = 0;
    foreach (var line in lines)
    {
        if (line == "noop")
        {
            newLine.Add(0);
            spritePos.Add(x);
            continue;
        }

        var a = line.Split(' ');

        //1
        newLine.Add(0);
        spritePos.Add(x);

        //2
        newLine.Add(int.Parse(a[1]));
        spritePos.Add(x);
        x += int.Parse(a[1]);
    }

    //Find the signal strength during the 20th, 60th, 100th, 140th, 180th, and 220th cycles. What is the sum of these six signal strengths?
    var sum = 20 * newLine.Take(20).Sum()
        + 60 * newLine.Take(60).Sum()
        + 100 * newLine.Take(100).Sum()
        + 140 * newLine.Take(140).Sum()
        + 180 * newLine.Take(180).Sum()
        + 220 * newLine.Take(220).Sum();

    Console.WriteLine("1: " + sum);

    var iPos = Enumerable.Range(0, spritePos.Count).ToList();

    int f = 0;
    for (int h = 0; h < 6; h++)
    {
        for (int w = 0; w < 40; w++)
        {
            var drawPos = iPos[f];
            var xPos = spritePos[f];
            var horDrawPos = drawPos - h * 40;
            if (horDrawPos >= Min(xPos) && horDrawPos <= Max(xPos))
            {
                Console.Write('#');
            }
            else
            {
                Console.Write('.');
            }
            f++;
        }
        Console.WriteLine();
        //ZUPRFECL
        //####.#..#.###..###..####.####..##..#....
        //...#.#..#.#..#.#..#.#....#....#..#.#....
        //..#..#..#.#..#.#..#.###..###..#....#....
        //.....#..#.###..###..#....#....#....#....
        //#....#..#.#....#.#..#....#....#..#.#....
        //####..##..#....#..#.#....####..##..####.
    }


    int Min(int xPos)
    {
        return xPos % 40 == 0 ? xPos : xPos - 1;
    }

    int Max(int xPos)
    {
        return xPos % 39 == 0 ? xPos : xPos + 1;
    }
}

//*
//--- Day 11: Monkey in the Middle ---
void Day11()
{
    var lines = File.ReadLines(@"C:\Users\karin\source\repos\AdventOfCode\2022\Inputs\Day11.txt").ToList();

    var monkeys1 = new List<Monkey>();
    var monkeys2 = new List<Monkey>();

    for (int i = 0; i < lines.Count; i += 7)
    {
        //Monkey 0:
        //Starting items: 79, 98
        //Operation: new = old * 19
        //Test: divisible by 23
        //  If true: throw to monkey 2
        //  If false: throw to monkey 3
        var m = lines[i].Split(' ');
        //Monkey 0:
        if (m[0] == "Monkey")
        {
            monkeys1.Add(new Monkey
            {
                //Starting items: 79, 98
                Items = lines[i + 1].Split(':').Last().Split(',', StringSplitOptions.RemoveEmptyEntries).Select(BigInteger.Parse).ToList(),
                //Operation: new = old * 19
                Operation = lines[i + 2].Split("=").Last(),
                //Test: divisible by 23
                DivisionalTest = int.Parse(lines[i + 3].Split(' ').Last()),
                //  If true: throw to monkey 2
                TrueMonkey = int.Parse(lines[i + 4].Split(' ').Last()),
                //  If false: throw to monkey 3
                FalseMonkey = int.Parse(lines[i + 5].Split(' ').Last())
            });

            var monkey = new Monkey
            {
                //Starting items: 79, 98
                Items = lines[i + 1].Split(':').Last().Split(',', StringSplitOptions.RemoveEmptyEntries).Select(BigInteger.Parse).ToList(),
                //Operation: new = old * 19
                Operation = lines[i + 2].Split("=").Last(),
                //Test: divisible by 23
                DivisionalTest = int.Parse(lines[i + 3].Split(' ').Last()),
                //  If true: throw to monkey 2
                TrueMonkey = int.Parse(lines[i + 4].Split(' ').Last()),
                //  If false: throw to monkey 3
                FalseMonkey = int.Parse(lines[i + 5].Split(' ').Last())
            };
            monkey.PrimeItems = monkey.Items.Select(x => GeneratePrimes(x)).ToList();

            monkeys2.Add(monkey);
        }
    }

    static List<int> GeneratePrimes(BigInteger number)
    {
        var primes = new List<int>();

        for (int div = 2; div <= number; div++)
            while (number % div == 0)
            {
                primes.Add(div);
                number = number / div;
            }

        return primes;
    }

    static BigInteger GenerateFromPrimes(List<int> primes)
    {
        var a = primes.First();
        for (int i = 1; i < primes.Count; i++)
        {
            a *= primes[i];
        }
        return a;
    }

    //Del 1
    for (int i = 0; i < 20; i++)
    {
        var relief = 3;

        monkeyPlay(monkeys1, relief);
    }

    //Count the total number of times each monkey inspects items over 20 rounds
    var t = monkeys1.OrderByDescending(x => x.Inspections).Take(2).ToList();
    var sum = t[0].Inspections * t[1].Inspections;
    Console.WriteLine("1: " + sum);


    //Del2
    for (int i = 0; i < 1000; i++)
    {
        monkeyPlay2(monkeys2);
        Console.WriteLine(i);
    }

    //Count the total number of times each monkey inspects items over 20 rounds
    var t2 = monkeys2.OrderByDescending(x => x.Inspections).Take(2).ToList();
    var sum2 = t2[0].Inspections * t2[1].Inspections;
    Console.WriteLine("2: " + sum2);
    //464968899

    void monkeyPlay(List<Monkey> monkeys, int relief)
    {
        //För varje apa
        foreach (var monkey in monkeys)
        {
            //För varje grej
            foreach (var item in monkey.Items)
            {
                //Monkey 0:
                //Monkey inspects an item with a worry level of 79.
                //  Worry level is multiplied by 19 to 1501.
                var a = PerformOperation(item, monkey.Operation);

                //  Monkey gets bored with item. Worry level is divided by 3 to 500.
                var newWorryLevel = a / relief;
                //  Current worry level is not divisible by 23.
                if (newWorryLevel % monkey.DivisionalTest == 0)
                {
                    monkeys[monkey.TrueMonkey].Items.Add(newWorryLevel);
                }
                else
                {
                    monkeys[monkey.FalseMonkey].Items.Add(newWorryLevel);
                }
                //  Item with worry level 500 is thrown to monkey 3.

                monkey.Inspections++;
            }
            monkey.Items = new List<BigInteger>();

            BigInteger PerformOperation(BigInteger item, string operation)
            {
                //old * 19
                //old * old
                var a = operation.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var number2 = new BigInteger(0);
                if (a.Last() == "old")
                {
                    number2 = item;
                }
                else
                {
                    number2 = long.Parse(a.Last());
                }

                var op = a[1];
                if (op == "+")
                {
                    return item + number2;
                }
                else if (op == "*")
                {
                    return item * number2;
                }
                return 0;
            }
        }
    }

    void monkeyPlay2(List<Monkey> monkeys)
    {
        //För varje apa
        foreach (var monkey in monkeys)
        {
            //För varje primetalsgrej
            foreach (var primeItems in monkey.PrimeItems)
            {
                //Monkey 0:
                //Monkey inspects an item with a worry level of 79.
                //  Worry level is multiplied by 19 to 1501.
                if (!primeItems.Any())
                {
                    continue;
                }

                PerformOperationWithResidue();

                if (monkey.Residue == monkey.DivisionalTest)
                {

                }

                //  Current worry level is not divisible by 23.
                //if (newPrimes.Contains(monkey.DivisionalTest))
                //{
                //    monkeys[monkey.TrueMonkey].PrimeItems.Add(monkey.PrimeItems);

                //}
                //else
                //{
                //    monkeys[monkey.FalseMonkey].PrimeItems.Add(newPrimes);
                //}
                //  Item with worry level 500 is thrown to monkey 3.

                monkey.Inspections++;
            }
            monkey.PrimeItems = new List<List<int>>();

            void PerformOperationWithResidue()
            {
                //old * 19
                //old * old
                var a = monkey.Operation.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (a.Last() == "old")
                {
                    // old * old
                    // (43 + 2,5) * (43 + 2,5) = 43¨^2 + 2* 43*2,5 + 2,5¨2

                    //primeItems.Add(2);
                }
                else
                {
                    var number2 = decimal.Parse(a.Last());
                    var op = a[1];

                    if (op == "+")
                    {
                        // (43 + 2,5) + 3 = 43 + 5,5
                        // (43 + 22,5) + 3 = ???
                        var newResidue = monkey.Residue + number2;

                    }
                    else if (op == "*")
                    {
                        // (43 + 2,5) * 19 = 43 * 19 + 2,5 * 19 = 43 * 19 ???
                        //monkey.PrimeItems.Add(number2);
                    }
                }
            }
        }
    }
}

// **
//---Day 12: Hill Climbing Algorithm ---
void Day12()
{
    var lines = File.ReadLines(@"C:\Users\karin\source\repos\AdventOfCode\2022\Inputs\Day12.txt").ToList();

    var startx = 0;
    var starty = 0;
    var i = 0;
    foreach (var line in lines)
    {
        var a = line.IndexOf('S');
        if (a != -1)
        {
            startx = i;
            starty = a;
            break;
        }
        i++;
    }


    lines[startx] = lines[startx].Replace('S', 'a');
    var tree = new Queue<(Point, int)>();
    tree.Enqueue((new Point(startx, starty), 0));

    Console.WriteLine($"1: {GetPath(tree)}");

    //Find all a
    var tree2 = new Queue<(Point, int)>();

    var y = 0;
    foreach (var line in lines)
    {
        var x = 0;
        foreach (var item in line)
        {
            if (item == 'a')
            {
                tree2.Enqueue((new Point(y, x), 0));
            }
            x++;
        }
        y++;
    }

    Console.WriteLine($"2: {GetPath(tree2)}");

    int GetPath(Queue<(Point, int)> tree)
    {
        var visited = new HashSet<Point>();
        var ymax = lines[1].Length;
        var xmax = lines.Count;

        var done = false;
        var pathLength = 0;

        while (!done)
        {
            (Point currCoord, int steps) b = tree.Dequeue();

            // lägg till ställen man får gå till
            var point = b.currCoord;

            var currHeight = lines[b.currCoord.X][b.currCoord.Y];

            if (point.X < xmax - 1)
            {
                TryEnqueue(new Point(point.X + 1, point.Y), currHeight);
            }

            if (point.X > 0)
            {
                TryEnqueue(new Point(point.X - 1, point.Y), currHeight);
            }

            if (point.Y < ymax - 1)
            {
                TryEnqueue(new Point(point.X, point.Y + 1), currHeight);
            }

            if (point.Y > 0)
            {
                TryEnqueue(new Point(point.X, point.Y - 1), currHeight);
            }

            void TryEnqueue(Point p, char curr)
            {
                var dest = lines[p.X][p.Y];

                if (dest == 'E')
                {
                    if (curr >= 'y')
                    {
                        pathLength = b.steps + 1;
                        Console.WriteLine($"1: {b.steps + 1}");
                        done = true;
                    }
                }
                else if (dest <= currHeight + 1)
                {
                    if (!visited.Contains(p))
                    {
                        tree.Enqueue((p, b.steps + 1));
                        visited.Add(p);
                    }
                }
            }
        }

        return pathLength;
    }
}

//--- Day 13: Distress Signal ---
void Day13()
{
    var lines = File.ReadLines(@"C:\Users\karin\source\repos\AdventOfCode\2022\Inputs\Day13.txt").ToList();

    //[1,1,3,1,1]
    //[1,1,5,1,1]

    //[[1],[2,3,4]]
    //[[1],4]

    //[9]
    //[[8,7,6]]

    //[[4,4],4,4]
    //[[4,4],4,4,4]

    //[7,7,7,7]
    //[7,7,7]

    //[]
    //[3]

    //[[[]]]
    //[[]]

    //[1,[2,[3,[4,[5,6,7]]]],8,9]
    //[1,[2,[3,[4,[5,6,0]]]],8,9]

    var rightOrderSum = 0;

    for (int i = 0; i < lines.Count; i += 2)
    {
        var left = lines[i];
        var right = lines[i + 1];

        var leftPackets = ParsePackets(lines[i]);

        if (Compare(left, right))
        {
            // Find fisrt integers to compare           
            var leftComp = FindFirst(left);
            var rightComp = FindFirst(right);

            //Same - find next to compare
            if (leftComp == rightComp)
            {
                //left = 
                //var leftComp = FindFirst(leftComp);
                //var rightComp = FindFirst(rightComp);
            }

            //-Left side is smaller, so inputs are in the right order
            //-Left side ran out of items, so inputs are in the right order
            if (leftComp == null || leftComp < rightComp)
            {
                rightOrderSum++;
            }

            //- Right side ran out of items, so inputs are not in the right order
            if (rightComp == null && rightComp != null)
            {
                continue;
            }
        };
    }

    bool Compare(string a, string b)
    {
        var right = a.Split(',');
        var left = b.Split(',');


        return false;
    }

    int? FindFirst(string a)
    {
        if (a.First() == '[')
        {
            //remove braces and find new a
            return 0;
        }

        if (a == null)
        {
            return 0;
        }

        //if (int.TryParse(a.First(), out a))
        //{
        //    return 0;
        //}

        return 0;
    }
    //Determine which pairs of packets are already in the right order. What is the sum of the indices of those pairs?
    Console.WriteLine("1: " + rightOrderSum);

    var sum2 = 0;
    Console.WriteLine("2: " + sum2);
}

Packet ParsePackets(string v)
{
    var d = new Packet();
    //[1,[2,[3,[4,[5,6,7]]]],8,9]
    for (int i = 0; i < v.Length; i++)
    {
        //List
        if (v[i] == '[')
        {

            d.packets.Add(new Packet());
        }
    }
    var a = v.Split(']', StringSplitOptions.RemoveEmptyEntries);
    //var b = a.Split(',');
    return null;
}

//--- Day 14: Regolith Reservoir ---
void Day14()
{
    var lines = File.ReadLines(@"C:\Users\karin\source\repos\AdventOfCode\2022\Inputs\Day14.txt");

    var sum = 0;
    var sum2 = 0;

    Console.WriteLine("1: " + sum);
    Console.WriteLine("2: " + sum2);
}

//--- Day 15: Beacon Exclusion Zone ---
void Day15()
{
    var lines = File.ReadLines(@"C:\Users\karin\source\repos\AdventOfCode\2022\Inputs\Day15.txt").ToList();

    //Sensor at x = 2, y = 18: closest beacon is at x=-2, y = 15
    //Sensor at x=9, y = 16: closest beacon is at x=10, y = 16
    //Sensor at x=13, y = 2: closest beacon is at x=15, y = 3
    //Sensor at x=12, y = 14: closest beacon is at x=10, y = 16
    //Sensor at x=10, y = 20: closest beacon is at x=10, y = 16
    //Sensor at x=14, y = 17: closest beacon is at x=10, y = 16
    //Sensor at x=8, y = 7: closest beacon is at x=2, y = 10
    //Sensor at x=2, y = 0: closest beacon is at x=2, y = 10
    //Sensor at x=0, y = 11: closest beacon is at x=2, y = 10
    //Sensor at x=20, y = 14: closest beacon is at x=25, y = 17
    //Sensor at x=17, y = 20: closest beacon is at x=21, y = 22
    //Sensor at x=16, y = 7: closest beacon is at x=15, y = 3
    //Sensor at x=14, y = 3: closest beacon is at x=15, y = 3
    //Sensor at x=20, y = 1: closest beacon is at x=15, y = 3

    Regex rx = new Regex(@"Sensoratx=(?<sensorX>(-?\d+)),y=(?<sensorY>(-?\d+)):closestbeaconisatx=(?<beaconX>(-?\d+)),y=(?<beaconY>(-?\d+))");

    //Del 1
    var area = new HashSet<Point>();
    var beacons = new HashSet<Point>();
    var rownum = 2000000;

    foreach (var line in lines)
    {
        var b = line.Replace(" ", "");
        Match match = rx.Match(b);

        int sx = int.Parse(match.Groups["sensorX"].Value);
        int sy = int.Parse(match.Groups["sensorY"].Value);
        int bx = int.Parse(match.Groups["beaconX"].Value);
        int by = int.Parse(match.Groups["beaconY"].Value);

        beacons.Add(new Point(bx, by));
        //Markera o-beaconställen i matrixen
        var manhattan = Math.Abs(sx - bx) + Math.Abs(sy - by);

        var dx1 = manhattan - Math.Abs(sy - rownum);
        for (int x = sx - dx1; x <= sx + dx1; x++)
        {
            area.Add(new Point(x, rownum));
        }
    }

    //Consult the report from the sensors you just deployed. In the row where y=2000000, how many positions cannot contain a beacon?
    var sum = area.Where(x => x.Y == rownum).Count() - beacons.Where(x => x.Y == rownum).Count();
    Console.WriteLine("1: " + sum);

    //Del 2
    for (int y = 0; y < 4000000; y++)
    {
        foreach (var line in lines)
        {
            var b = line.Replace(" ", "");
            Match match = rx.Match(b);

            int sx = int.Parse(match.Groups["sensorX"].Value);
            int sy = int.Parse(match.Groups["sensorY"].Value);
            int bx = int.Parse(match.Groups["beaconX"].Value);
            int by = int.Parse(match.Groups["beaconY"].Value);

            var manhattan = Math.Abs(sx - bx) + Math.Abs(sy - by);

            var dx2 = manhattan - Math.Abs(sy - y);
            var xmin = Math.Max(sx - dx2, 0);
            var xmax = Math.Min(sx + dx2, 4000000);

            //No beacon
            if (xmin <= 0 && xmax >= 40000000)
            {

            }
        }
    }

    //Tuning frequency, which can be found by multiplying its x coordinate by 4000000 and then adding its y coordinate
    //var sum2 = a.X * 4000000 + a.Y;
    //Console.WriteLine("2: " + sum2);
}

//--- Day 16: Proboscidea Volcanium ---
void Day16()
{
    //Valve AA has flow rate = 0; tunnels lead to valves DD, II, BB
    //Valve BB has flow rate = 13; tunnels lead to valves CC, AA
    //Valve CC has flow rate = 2; tunnels lead to valves DD, BB
    //Valve DD has flow rate = 20; tunnels lead to valves CC, AA, EE
    //Valve EE has flow rate = 3; tunnels lead to valves FF, DD
    //Valve FF has flow rate = 0; tunnels lead to valves EE, GG
    //Valve GG has flow rate = 0; tunnels lead to valves FF, HH
    //Valve HH has flow rate = 22; tunnel leads to valve GG
    //Valve II has flow rate = 0; tunnels lead to valves AA, JJ
    //Valve JJ has flow rate = 21; tunnel leads to valve II

    var lines = File.ReadLines(@"C:\Users\karin\source\repos\AdventOfCode\2022\Inputs\Day16.txt").ToList();

    Regex rx = new Regex(@"Valve(?<valve>(\w+))hasflowrate=(?<flowrate>(\d+));tunnels?leads?tovalves?(?<childvalves>((\w+,)+))");
    var valves = new Dictionary<string, (int, List<string>)>();

    foreach (var line in lines)
    {
        var b = line.Replace(" ", "") + ",";
        Match match = rx.Match(b);

        var v = match.Groups["valve"].Value;
        var f = int.Parse(match.Groups["flowrate"].Value);
        var c = match.Groups["childvalves"].Value.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();

        valves.Add(v, (f, c));
    }

    var s = new Stack<(string name, int flow, int time, bool open)>();
    s.Push(("AA", 0, 0, false));

    while(true)
    {
        //Where are we now
        var state = s.Peek();
        //Add options + state
        //Open if closed
        if (!state.open)
        {
            s.Push((state.name, state.flow * (30 - state.time), state.time++, true));
        }
        //Add children
        var options = valves[state.name].Item2;
        foreach (var child in options)
        {
            s.Push((child, state.flow, state.time, false));
        }
        
    }
    //This approach lets you release the most pressure possible in 30 minutes with this valve layout, 1651.
    //Work out the steps to release the most pressure in 30 minutes.What is the most pressure you can release?

    var sum = 0;
    var sum2 = 0;

    Console.WriteLine("1: " + sum);
    Console.WriteLine("2: " + sum2);
}

//--- Day 17: Pyroclastic Flow ---
void Day17()
{
    var lines = File.ReadLines(@"C:\Users\karin\source\repos\AdventOfCode\2022\Inputs\Day17.txt");

    var sum = 0;
    var sum2 = 0;

    Console.WriteLine("1: " + sum);
    Console.WriteLine("2: " + sum2);
}

//--- Day 18: Boiling Boulders ---
void Day18()
{
    var lines = File.ReadLines(@"C:\Users\karin\source\repos\AdventOfCode\2022\Inputs\Day18.txt");

    var sum = 0;
    var sum2 = 0;

    Console.WriteLine("1: " + sum);
    Console.WriteLine("2: " + sum2);
}
void Day19()
{
    var lines = File.ReadLines(@"C:\Users\karin\source\repos\AdventOfCode\2022\Inputs\Day19.txt");

    var sum = 0;
    var sum2 = 0;

    Console.WriteLine("1: " + sum);
    Console.WriteLine("2: " + sum2);
}
void Day20()
{
    var lines = File.ReadLines(@"C:\Users\karin\source\repos\AdventOfCode\2022\Inputs\Day20.txt");

    var sum = 0;
    var sum2 = 0;

    Console.WriteLine("1: " + sum);
    Console.WriteLine("2: " + sum2);
}
void Day21()
{
    var lines = File.ReadLines(@"C:\Users\karin\source\repos\AdventOfCode\2022\Inputs\Day21.txt");

    var sum = 0;
    var sum2 = 0;

    Console.WriteLine("1: " + sum);
    Console.WriteLine("2: " + sum2);
}
void Day22()
{
    var lines = File.ReadLines(@"C:\Users\karin\source\repos\AdventOfCode\2022\Inputs\Day22.txt");

    var sum = 0;
    var sum2 = 0;

    Console.WriteLine("1: " + sum);
    Console.WriteLine("2: " + sum2);
}
void Day23()
{
    var lines = File.ReadLines(@"C:\Users\karin\source\repos\AdventOfCode\2022\Inputs\Day23.txt");

    var sum = 0;
    var sum2 = 0;

    Console.WriteLine("1: " + sum);
    Console.WriteLine("2: " + sum2);
}
void Day24()
{
    var lines = File.ReadLines(@"C:\Users\karin\source\repos\AdventOfCode\2022\Inputs\Day24.txt");

    var sum = 0;
    var sum2 = 0;

    Console.WriteLine("1: " + sum);
    Console.WriteLine("2: " + sum2);
}
class Monkey
{
    public int DivisionalTest { get; set; }
    public decimal Residue { get; set; }
    public long Inspections { get; set; }
    public string Operation { get; set; }
    public int TrueMonkey { get; set; }
    public int FalseMonkey { get; set; }
    public List<List<int>> PrimeItems { get; set; }
    public List<BigInteger> Items { get; set; }

}
class Packet
{
    public List<Packet> packets { get; set; }
}

