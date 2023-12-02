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
        _ => throw new NotImplementedException("Sam hasn't done this yet"),
    });

