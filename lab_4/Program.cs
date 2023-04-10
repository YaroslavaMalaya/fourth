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


FileStream fs = new FileStream("sherlok_01.txt", FileMode.OpenOrCreate);
StreamWriter write = new StreamWriter(fs);
StreamReader reader2 = new StreamReader("sherlok.txt");
string line2 = reader2.ReadToEnd();

foreach (char c in line2)
{
    if (c == '\n')
    {
        write.Write("<N>");
    }
    else if (c == '\r')
    {
        write.Write("<R>");
    }
    else if (code.ContainsKey(c))
    {
        write.Write(code[c]);
    }
    write.Flush();
}

StreamReader reader3 = new StreamReader("sherlok_01.txt");
FileStream fs2 = new FileStream("sherlok_02.txt", FileMode.OpenOrCreate);
StreamWriter write2 = new StreamWriter(fs2);
var check = "";

foreach (var character in reader3.ReadLine())
{
    var c = character.ToString();
    if (check == "<N>")
    {
        write2.Write('\n');
        check = "";
    }
    else if (check == "<R>")
    {
        write2.Write('\r');
        check = "";
    }
    else if (code.ContainsValue(check))
    {
        foreach (KeyValuePair<char, string> pair in code)
        {
            if (pair.Value == check)
            {
                write2.Write(pair.Key);
                check = "";
                break;
            }
        }
    }
    check += c;
    write2.Flush();
}

reader.Close();
reader2.Close();
reader3.Close();
write.Close();
write2.Close();
fs.Close();
fs2.Close();