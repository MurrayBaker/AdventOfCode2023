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
            .Select(l => (new string(l.SkipWhile(c => c != ':').Skip(1).ToArray()), int.Parse(l.Skip(5).TakeWhile(c => c != ':').ToArray())))
            .Where(game => !game.Item1
                .Split(";")
                .Any(g => g.Split(",")
                    .Any(pull => pull switch
                    {
                        var s when s.Split(' ')[2].First() == 'r' => int.Parse(s.Split(' ')[1]) > 12,
                        var s when s.Split(' ')[2].First() == 'g' => int.Parse(s.Split(' ')[1]) > 13,
                        var s when s.Split(' ')[2].First() == 'b' => int.Parse(s.Split(' ')[1]) > 14,
                        _ => throw new Exception("Parsed this wrong"),
                    })))
            .Select(game => game.Item2)
            .Sum(),
        _ => throw new NotImplementedException("Sam hasn't done this yet"),
    });
    
