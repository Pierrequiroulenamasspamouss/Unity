namespace Prime31
{
    using System;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public static class JsonFormatter
    {
        /// <summary>
        /// Safely pretty-prints JSON using Json.NET.
        /// Returns the original string if parsing fails.
        /// NEVER corrupts input.
        /// </summary>
        public static string prettyPrint(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            try
            {
                // Parse into a token (object OR array)
                JToken token = JToken.Parse(input);

                // Re-serialize with indentation
                return token.ToString(Formatting.Indented);
            }
            catch (Exception)
            {
                // Invalid JSON → return original string untouched
                return input;
            }
        }
    }
}
