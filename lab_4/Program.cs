using lab_4;

var dictionary = new Dictionary<char, int>();
StreamReader reader = new StreamReader("sherlok.txt");
string line;

while ((line = reader.ReadLine()) != null)
{
    foreach (char c in line)
    {
        if (dictionary.ContainsKey(c))
        {
            dictionary[c]++;
        }
        else
        {
            dictionary[c] = 1;
        }
    }
}

Console.ForegroundColor = ConsoleColor.Magenta;
Console.WriteLine("Frequency of all characters:");
Console.ForegroundColor = ConsoleColor.White;
foreach (KeyValuePair<char, int> pair in dictionary)
{
    Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
}

Console.ForegroundColor = ConsoleColor.Magenta;
Console.WriteLine("\nHuffman code of all characters:");
Console.ForegroundColor = ConsoleColor.White;
var code = new Huffman().HuffmanCode(dictionary);
foreach (KeyValuePair<char, string> pair in code)
{
    Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
}