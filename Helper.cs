using System.Collections.Concurrent;

var file = File.ReadAllLines("Words.csv");

file = file.Distinct().ToArray();

var bag = new ConcurrentBag<string>();

Parallel.For(0, file.Length, i =>
{
    var current = "{ \"create\" : { \"_index\" : \"words\", \"_id\" : \"" + i+1 + "\" } }\n";
    current += "{ \"id\": \"" + i+1 + "\", \"word\" : \"" + file[i].Trim() + "\" }";

    bag.Add(current);
});

var result = string.Join("\n", bag.ToList());

await File.WriteAllTextAsync("data.txt", result);