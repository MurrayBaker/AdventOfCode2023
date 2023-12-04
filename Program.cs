using System.Numerics;

Console.WriteLine(
    DateTime.Now.Day switch
    {
        1 => File.ReadAllLines("Day1.txt")
        .Select(l => new string(l
            .Select((c, i) => char.IsDigit(c)
                ? c
                : (new string(l.Skip(i).Take(5).ToArray()) switch
                {
                    var s when s.StartsWith("one") => '1',
                    var s when s.StartsWith("two") => '2',
                    var s when s.StartsWith("three") => '3',
                    var s when s.StartsWith("four") => '4',
                    var s when s.StartsWith("five") => '5',
                    var s when s.StartsWith("six") => '6',
                    var s when s.StartsWith("seven") => '7',
                    var s when s.StartsWith("eight") => '8',
                    var s when s.StartsWith("nine") => '9',
                    _ => 'x',
                })).ToArray()))
        .Select(l => int.Parse(l.First(char.IsDigit).ToString()) * 10 + int.Parse(l.Last(char.IsDigit).ToString())).Sum(),
        2 => File.ReadAllLines("Day2.txt")
            .Select(l => new string(l.SkipWhile(c => c != ':').Skip(1).ToArray()))
            .Select(l => l.Split(";"))
            .Select(l => l.Aggregate((0, 0, 0), (dictionary, game) => 
                (
                    Math.Max(dictionary.Item1, int.Parse(game.Split(",").FirstOrDefault(g => g.Split(' ')[2] == "blue")?.Split(' ')[1] ?? "0")),
                    Math.Max(dictionary.Item2, int.Parse(game.Split(",").FirstOrDefault(g => g.Split(' ')[2] == "red")?.Split(' ')[1] ?? "0")),
                    Math.Max(dictionary.Item3, int.Parse(game.Split(",").FirstOrDefault(g => g.Split(' ')[2] == "green")?.Split(' ')[1] ?? "0"))
                )))
            .Aggregate(0, (a, b) => a + (b.Item1 * b.Item2 * b.Item3)),
        3 => (from _ in new (int, int)[] { (0, 0) }
             let lines = File.ReadAllLines("Day3.txt")
             let gearCoordinates = lines.SelectMany((l, y) => l.Select((c, x) => (c, x, y)).Where(t => t.c == '*').Select(t => (t.x, t.y)))
             let adjacentCoordinates = gearCoordinates.SelectMany(point => new int[] { point.x - 1, point.x, point.x + 1 }.SelectMany(x => new (int x, int y)[] { (x, point.y - 1), (x, point.y), (x, point.y + 1) }).Where(p => p != (point.x, point.y)).Select(p => (gear: point, p.x, p.y))).ToArray()
             let goodCoordinates = adjacentCoordinates.Where(ac => char.IsDigit(lines[ac.y][ac.x])).ToArray()
             let startCoordinates = goodCoordinates.Select(c => (c.gear, x: c.x - lines[c.y].Reverse().Skip(lines[c.y].Length - c.x).TakeWhile(char.IsDigit).Count(), c.y)).Distinct().ToArray()
             from coordinatePair in startCoordinates.GroupBy(c => c.gear).Where(g => g.Count() == 2).Select(g => g.ToList())
             select (int.Parse(new string(lines[coordinatePair[0].y].Skip(coordinatePair[0].x).TakeWhile(char.IsDigit).ToArray())) * int.Parse(new string(lines[coordinatePair[1].y].Skip(coordinatePair[1].x).TakeWhile(char.IsDigit).ToArray())))).Sum(),
        4 => File.ReadAllLines("Day4.txt")
            .Select(l => new string(l.SkipWhile(c => c != ':').Skip(2).ToArray()))
            .Select(l => l.Split("|")[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Intersect(l.Split("|")[1].Split(' ', StringSplitOptions.RemoveEmptyEntries)).Count())
            .Aggregate(0, (total, count) => total += (int)Math.Pow(2, count - 1)),
        _ => throw new NotImplementedException("Sam hasn't done this yet"),
    });

