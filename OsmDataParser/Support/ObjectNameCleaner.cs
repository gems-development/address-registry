namespace Gems.AddressRegistry.OsmDataParser.Support;

internal class ObjectNameCleaner
{
    internal static string Clean(string name)
    {
        var words = name.Split(' ');
        var prefixes = Enum.GetNames(typeof(Prefix)).ToList();

        for (var i = 0; i < words.Length; i++)
        {
            if (prefixes.Contains(words[i], StringComparer.OrdinalIgnoreCase))
                words[i] = "";
        }
    
        var result = string.Join(" ", words).Trim();

        return result;
    }
}