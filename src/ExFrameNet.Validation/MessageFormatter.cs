using System.Text.RegularExpressions;

namespace ExFrameNet.Validation
{
    internal static class MessageFormatter
    {
        private static readonly Regex _templateRegex = new Regex("{([^{}:]+)(?::([^{}]+))?}", RegexOptions.Compiled);

        public static string Fromat(string messageWithTemplate, Dictionary<string, object?> keyValues)
        {
            return _templateRegex.Replace(messageWithTemplate, m =>
                {
                    var key = m.Groups[1].Value;

                    if (!keyValues.TryGetValue(key, out var value))
                        return m.Value;

                    var format = m.Groups[2].Success
                            ? $"{{0:{m.Groups[2].Value}}}"
                            : null;
                    return format is null
                        ? value.ToString()
                        : string.Format(format, value);
                });
        }
    }
}
