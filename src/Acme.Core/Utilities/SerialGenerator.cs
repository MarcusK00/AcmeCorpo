namespace Acme.Core.Utilities;

public static class SerialGenerator
{
    private const string Chars = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";

    public static string Generate()
    {
        var random = new Random();
        string Segment()
            => new string(Enumerable.Range(0, 4)
                .Select(_ => Chars[random.Next(Chars.Length)])
                .ToArray());

        return $"{Segment()}-{Segment()}-{Segment()}-{Segment()}";
    }

    public static List<string> GenerateMany(int amount)
    {
        var set = new HashSet<string>();

        while (set.Count < amount)
        {
            set.Add(Generate());
        }

        return set.ToList();
    }
}