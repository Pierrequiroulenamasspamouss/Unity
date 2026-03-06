
using System;
using System.IO;
using System.Text;

public class HexDump
{
    public static void Main()
    {
        string path = "Assets/StreamingAssets/definitions.json";
        if (File.Exists(path))
        {
            byte[] bytes = File.ReadAllBytes(path);
            Console.WriteLine("Total bytes: " + bytes.Length);
            // Print first 200 bytes
            for (int i = 0; i < 200 && i < bytes.Length; i++)
            {
                Console.Write(bytes[i].ToString("X2") + " ");
                if ((i + 1) % 16 == 0) Console.WriteLine();
            }
            Console.WriteLine();
            string text = Encoding.UTF8.GetString(bytes, 0, Math.Min(200, bytes.Length));
            Console.WriteLine("Text: " + text);
        }
        else
        {
            Console.WriteLine("File not found");
        }
    }
}
