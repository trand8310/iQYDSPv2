using Newtonsoft.Json.Linq;

namespace MainClient.Common
{
    public static class JTokenExtensions
    {
        public static JToken? SelectToken(this JToken? node, string path)
        {
            if (node is null || string.IsNullOrWhiteSpace(path))
                return node;

            var current = node;
            foreach (var segment in path.Split('.', StringSplitOptions.RemoveEmptyEntries))
            {
                current = current switch
                {
                    JObject obj => obj[segment],
                    JArray arr when int.TryParse(segment, out var index) && index >= 0 && index < arr.Count => arr[index],
                    _ => null
                };

                if (current is null)
                    return null;
            }

            return current;
        }
    }
}
