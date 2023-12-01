Console.WriteLine(
    File.ReadAllLines("Day1.txt")
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
        .Select(l => int.Parse(l.First(char.IsDigit).ToString()) * 10 + int.Parse(l.Last(char.IsDigit).ToString())).Sum());
