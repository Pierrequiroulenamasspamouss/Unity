
using System;
using System.IO;
using Newtonsoft.Json.Linq;

public class TestJsonParsing
{
    public static void Main()
    {
        string path = "C:/Unity/FRANKENSTEIN-UNITYPROJECT/Assets/StreamingAssets/definitions.json";
        if (File.Exists(path))
        {
            try
            {
                string json = File.ReadAllText(path);
                Console.WriteLine("Read " + json.Length + " chars");
                // Print char at line 5 pos 13 approx
                string[] lines = File.ReadAllLines(path);
                if (lines.Length >= 5)
                {
                    Console.WriteLine("Line 5: [" + lines[4] + "]");
                }
                JToken.Parse(json);
                Console.WriteLine("Parse Success");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Parse Error: " + ex.Message);
            }
        }
        else
        {
            Console.WriteLine("File not found");
        }
    }
}
