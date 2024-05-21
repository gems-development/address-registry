using Microsoft.Extensions.Configuration;

namespace Gems.AddressRegistry.OsmDataParser.Support;

internal static class ObjectNameCleaner
{
    private static readonly IConfiguration Configuration;
    private static readonly Lazy<List<string>> Prefixes = new Lazy<List<string>>(GetPrefixes);
    
    static ObjectNameCleaner()
    {
        Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
    }
    
    internal static string Clean(string name)
    {
        var words = name.Split(' ');

        for (var i = 0; i < words.Length; i++)
        {
            if (Prefixes.Value.Any(prefix => prefix.Split(' ')
                    .Contains(words[i], StringComparer.OrdinalIgnoreCase)))
                words[i] = "";
        }
    
        var result = string.Join(" ", words).Trim();

        return result;
    }
    
    private static List<string> GetPrefixes()
    {
        var allPrefixes = new List<string>();

        var levelsSection = Configuration.GetSection("Levels");
        foreach (var section in levelsSection.GetChildren())
        {
            var values = section.GetChildren().Select(x => x.Value);
            allPrefixes.AddRange(values!);
        }

        return allPrefixes;
    }
}