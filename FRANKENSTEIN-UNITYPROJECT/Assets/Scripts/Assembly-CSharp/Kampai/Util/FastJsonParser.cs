using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Kampai.Util
{
    // A simple, lightweight JSON parser to replace Newtonsoft.Json for problematic data.
    public class FastJsonParser
    {
        public static object Deserialize(string json)
        {
            if (string.IsNullOrEmpty(json)) return null;
            Parser parser = new Parser(json);
            return parser.ParseValue();
        }

        public static T Deserialize<T>(string json)
        {
            object parsed = Deserialize(json);
            if (parsed == null) return default(T);
            
            // Convert to JToken first using manual conversion to avoid serializer bugs
            global::Newtonsoft.Json.Linq.JToken token = ConvertToJToken(parsed);
            if (token == null) return default(T);

            // Use Newtonsoft's ToObject mapping
            return token.ToObject<T>();
        }

        public static global::Newtonsoft.Json.Linq.JToken ConvertToJToken(object obj)
        {
            if (obj == null)
                return global::Newtonsoft.Json.Linq.JValue.CreateString(null);

            Dictionary<string, object> dict = obj as Dictionary<string, object>;
            if (dict != null)
            {
                global::Newtonsoft.Json.Linq.JObject jo = new global::Newtonsoft.Json.Linq.JObject();
                foreach (KeyValuePair<string, object> kvp in dict)
                    jo[kvp.Key] = ConvertToJToken(kvp.Value);
                return jo;
            }

            List<object> list = obj as List<object>;
            if (list != null)
            {
                global::Newtonsoft.Json.Linq.JArray ja = new global::Newtonsoft.Json.Linq.JArray();
                foreach (object item in list)
                    ja.Add(ConvertToJToken(item));
                return ja;
            }

            // primitives (long, double, bool, string)
            return new global::Newtonsoft.Json.Linq.JValue(obj);
        }

        sealed class Parser
        {
            const string WORD_BREAK = "{}[],:\"";

            public static bool IsWordBreak(char c)
            {
                return Char.IsWhiteSpace(c) || WORD_BREAK.IndexOf(c) != -1;
            }

            enum TOKEN
            {
                NONE,
                CURLY_OPEN,
                CURLY_CLOSE,
                SQUARED_OPEN,
                SQUARED_CLOSE,
                COLON,
                COMMA,
                STRING,
                NUMBER,
                TRUE,
                FALSE,
                NULL
            };

            String json;
            int index;
            StringBuilder s = new StringBuilder();

            public Parser(string jsonString)
            {
                json = jsonString;
                index = 0;
            }

            public object ParseValue()
            {
                TOKEN token = NextToken();
                switch (token)
                {
                    case TOKEN.STRING:
                        return s.ToString();
                    case TOKEN.NUMBER:
                        return ParseNumber(s.ToString());
                    case TOKEN.CURLY_OPEN:
                        return ParseObject();
                    case TOKEN.SQUARED_OPEN:
                        return ParseArray();
                    case TOKEN.TRUE:
                        return true;
                    case TOKEN.FALSE:
                        return false;
                    case TOKEN.NULL:
                        return null;
                    case TOKEN.NONE:
                        return null;
                }
                return null;
            }

            Dictionary<string, object> ParseObject()
            {
                Dictionary<string, object> table = new Dictionary<string, object>();

                // [LENIENCY] Loop until we find close curly
                while (true)
                {
                    TOKEN token = NextToken();
                    switch (token)
                    {
                        case TOKEN.NONE:
                            return null;
                        case TOKEN.COMMA:
                            continue;
                        case TOKEN.CURLY_CLOSE:
                            return table;
                        default:
                            // We expect a key (STRING)
                            string name = (token == TOKEN.STRING) ? s.ToString() : null;
                            if (name == null)
                            {
                                // [LENIENCY] If we got a number/token as key (e.g. missing quote), treat stringified
                                if(token == TOKEN.NUMBER) name = s.ToString();
                                else return null; // Abort
                            }

                            // Expect colon
                            token = NextToken();
                            if (token != TOKEN.COLON)
                            {
                                // [LENIENCY] Missing colon?
                                // If token is a value start (STRING/NUMBER/OPEN), assume missed colon
                                // Backtrack? No, simple parser. Ideally we'd error but we want to survive.
                                // Let's try to assume current token IS the value if it looks like one.
                                // simpler: just fail this key or try to parse value from current position?
                                // For now, strict on colon presence for structure sanity.
                                return null; 
                            }

                            // Value
                            object value = ParseValue();
                            table[name] = value;
                            break;
                    }
                }
            }

            List<object> ParseArray()
            {
                List<object> array = new List<object>();

                while (true)
                {
                    TOKEN token = NextToken();
                    switch (token)
                    {
                        case TOKEN.NONE:
                            return null;
                        case TOKEN.COMMA:
                            continue;
                        case TOKEN.SQUARED_CLOSE:
                            return array;
                        default:
                            // Back up so ParseValue can consume this token
                            // Since we already consumed it, we must refactor slightly or handle it.
                            // The NextToken logic consumes chars.
                            // A hack: we need to peek or support pushback.
                            // Or simply, we know the current token is the start of the value.
                            // But ParseValue calls NextToken().
                            // So we need to restructure.
                            // Actually, ParseValue calls NextToken(). If we already called it here, we have the token type and 's' content.
                            // So implementing a helper that takes the *derived* token would be best but ParseValue logic is simple.
                            
                            // Let's copy ParseValue logic inline or adapt.
                            object value = null;
                            switch (token) {
                                case TOKEN.STRING: value = s.ToString(); break;
                                case TOKEN.NUMBER: value = ParseNumber(s.ToString()); break;
                                case TOKEN.CURLY_OPEN: value = ParseObject(); break;
                                case TOKEN.SQUARED_OPEN: value = ParseArray(); break;
                                case TOKEN.TRUE: value = true; break;
                                case TOKEN.FALSE: value = false; break;
                                case TOKEN.NULL: value = null; break;
                            }
                            array.Add(value);
                            break;
                    }
                }
            }

            object ParseNumber(string numberStr)
            {
                if (numberStr.IndexOf('.') != -1 || numberStr.IndexOf('e') != -1 || numberStr.IndexOf('E') != -1)
                {
                    double d;
                    if (Double.TryParse(numberStr, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out d))
                        return d;
                }
                
                long l;
                if (long.TryParse(numberStr, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out l))
                    return l;

                return 0;
            }

            TOKEN NextToken()
            {
                EatWhitespace();

                if (index == json.Length)
                    return TOKEN.NONE;

                char c = json[index];
                index++;

                switch (c)
                {
                    case '{': return TOKEN.CURLY_OPEN;
                    case '}': return TOKEN.CURLY_CLOSE;
                    case '[': return TOKEN.SQUARED_OPEN;
                    case ']': return TOKEN.SQUARED_CLOSE;
                    case ',': return TOKEN.COMMA;
                    case '"': 
                        return ParseString();
                    case ':': return TOKEN.COLON;
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                    case '-':
                        return ParseNumberToken(c);
                }
                
                // [LENIENCY] Handle unquoted keys or values if they start with letters
                if (Char.IsLetter(c)) {
                    // check for literals
                    index--;
                    int remainingLength = json.Length - index;
                    if (remainingLength >= 5 && json.Substring(index, 5) == "false")
                    {
                        index += 5;
                        return TOKEN.FALSE;
                    }
                    if (remainingLength >= 4 && json.Substring(index, 4) == "true")
                    {
                        index += 4;
                        return TOKEN.TRUE;
                    }
                    if (remainingLength >= 4 && json.Substring(index, 4) == "null")
                    {
                        index += 4;
                        return TOKEN.NULL;
                    }
                    // unquoted string?
                    return ParseUnquotedString(); 
                }

                return TOKEN.NONE;
            }

            void EatWhitespace()
            {
                while (index < json.Length)
                {
                    char c = json[index];
                    // [LENIENCY] Treat some control characters as whitespace
                    if (char.IsWhiteSpace(c) || c <= ' ')
                    {
                        index++;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            TOKEN ParseString()
            {
                s.Length = 0;

                bool parsing = true;
                while (parsing)
                {
                    if (index == json.Length)
                        break;

                    char c = json[index++];
                    if (c == '"')
                    {
                        parsing = false;
                    }
                    else if (c == '\\')
                    {
                        if (index == json.Length) break;
                        char next = json[index++];
                        switch (next)
                        {
                            case '"': s.Append('"'); break;
                            case '\\': s.Append('\\'); break;
                            case '/': s.Append('/'); break;
                            case 'b': s.Append('\b'); break;
                            case 'f': s.Append('\f'); break;
                            case 'n': s.Append('\n'); break;
                            case 'r': s.Append('\r'); break;
                            case 't': s.Append('\t'); break;
                            case 'u':
                                if (index + 4 < json.Length)
                                {
                                    string hex = json.Substring(index, 4);
                                    s.Append((char)int.Parse(hex, System.Globalization.NumberStyles.HexNumber));
                                    index += 4;
                                }
                                break;
                        }
                    }
                    else
                    {
                        s.Append(c);
                    }
                }
                return TOKEN.STRING;
            }

            TOKEN ParseUnquotedString() {
                s.Length = 0;
                 while (index < json.Length)
                {
                    char c = json[index];
                    if (IsWordBreak(c)) break;
                    s.Append(c);
                    index++;
                }
                return TOKEN.STRING;
            }

            TOKEN ParseNumberToken(char first)
            {
                s.Length = 0;
                s.Append(first);
                while (index < json.Length)
                {
                    char c = json[index];
                    if (char.IsDigit(c) || c == '.' || c == 'e' || c == 'E' || c == '+' || c == '-')
                    {
                        s.Append(c);
                        index++;
                    }
                    else
                    {
                        // [LENIENCY] If we stopped on a quote, it's a separator effectively
                        // e.g. 123"next"
                        break;
                    }
                }
                return TOKEN.NUMBER;
            }
        }
    }
}
