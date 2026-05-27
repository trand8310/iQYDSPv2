using Newtonsoft.Json.Linq;


namespace MainClient.Extensions;


internal static class JTokenExtensions
{
    public static T? Value<T>(this JToken? node)
    {
        if (node is null)
        {
            return default;
        }

        try
        {
            return node.ToObject<T>();
        }
        catch
        {
            return default;
        }
    }



    public static T? GetValue<T>(this JToken? node)
    {
        if (node is null)
        {
            return default;
        }

        try
        {
            return node.ToObject<T>();
        }
        catch
        {
            return default;
        }
    }

    public static string ToJsonString(this JToken? node)
    {
        return node?.ToString(Newtonsoft.Json.Formatting.None) ?? string.Empty;
    }

    public static IEnumerable<JToken> Children(this JToken? node)
    {
        if (node is JArray arr)
        {
            return arr.Where(x => x is not null).Select(x => x!);
        }

        if (node is JObject obj)
        {
            return obj.Properties().Select(x => x.Value).Where(x => x is not null).Select(x => x!);
        }

        return Enumerable.Empty<JToken>();
    }

    public static JToken? SelectToken(this JToken? node, string path)
    {
        if (node is null || string.IsNullOrWhiteSpace(path))
        {
            return node;
        }

        var current = node;
        foreach (var raw in path.Split('.', StringSplitOptions.RemoveEmptyEntries))
        {
            if (current is null)
            {
                return null;
            }

            if (current is JArray arr && int.TryParse(raw, out var idx))
            {
                current = idx >= 0 && idx < arr.Count ? arr[idx] : null;
                continue;
            }

            if (current is JObject obj)
            {
                current = obj[raw];
                continue;
            }

            return null;
        }

        return current;
    }
}
