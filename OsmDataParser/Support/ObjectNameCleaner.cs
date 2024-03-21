using Microsoft.Extensions.Configuration;

namespace Gems.AddressRegistry.OsmDataParser.Support;

internal static class ObjectNameCleaner
{
    private static readonly IConfiguration Configuration;
    
    static ObjectNameCleaner()
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var projectDirectory = Directory.GetParent(currentDirectory)?.Parent?.Parent?.FullName;

        Configuration =new ConfigurationBuilder()
            .SetBasePath(projectDirectory!)
            .AddJsonFile("appsettings.json")
            .Build();
    }
    
    internal static string Clean(string name)
    {
        var words = name.Split(' ');
        var prefixes = GetPrefixes();

        for (var i = 0; i < words.Length; i++)
        {
            if (prefixes.Any(prefix => prefix.Split(' ')
                    .Contains(words[i], StringComparer.OrdinalIgnoreCase)))
                words[i] = "";
        }
    
        var result = string.Join(" ", words).Trim();

        return result;
    }
    
    private static List<string> GetPrefixes()
    {
        var prefixes = new List<string>();

        var levelsSection = Configuration.GetSection("Levels");
        foreach (var section in levelsSection.GetChildren())
        {
            var values = section.GetChildren().Select(x => x.Value);
            prefixes.AddRange(values!);
        }

        return prefixes;
    }
}