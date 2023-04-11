using lab_4;

var dictionary = new Dictionary<string, int>();
var text = File.ReadAllText("Sherlock.txt");

foreach (char c in text)
{
    if (c == '\n')
    {
        if (dictionary.ContainsKey("<N>"))
        {
            dictionary["<N>"]++;
        }
        else
        {
            dictionary["<N>"] = 1;
        }
    }
    else if (c == '\r')
    {
        if (dictionary.ContainsKey("<R>"))
        {
            dictionary["<R>"]++;
        }
        else
        {
            dictionary["<R>"] = 1;
        }
    }
    else if (dictionary.ContainsKey(c.ToString()))
    {
        dictionary[c.ToString()]++;
    }
    else
    {
        dictionary[c.ToString()] = 1;
    }
}


Console.ForegroundColor = ConsoleColor.Magenta;
Console.WriteLine("Frequency of all characters:");
Console.ForegroundColor = ConsoleColor.White;
foreach (KeyValuePair<string, int> pair in dictionary)
{
    Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
}

Console.ForegroundColor = ConsoleColor.Magenta;
Console.WriteLine("\nHuffman code of all characters:");
Console.ForegroundColor = ConsoleColor.White;
var code = new Huffman().HuffmanCode(dictionary);
FileStream fss = new FileStream("Table_codes.txt", FileMode.OpenOrCreate);
StreamWriter table = new StreamWriter(fss);
foreach (KeyValuePair<string, string> pair in code)
{
    Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
    table.Write("{0}: {1}\n", pair.Key, pair.Value);
    table.Flush();
}


FileStream fs = new FileStream("Sherlock_code.txt", FileMode.OpenOrCreate);
StreamWriter write = new StreamWriter(fs);
var text2 = File.ReadAllText("Sherlock.txt");

foreach (char c in text2)
{
    if (c == '\n')
    {
        write.Write(code["<N>"]);
    }
    else if (c == '\r')
    {
        write.Write(code["<R>"]);
    }
    else if (code.ContainsKey(c.ToString()))
    {
        write.Write(code[c.ToString()]);
    }
    write.Flush();
}

var text3 = File.ReadAllText("Sherlock_code.txt");
FileStream fs2 = new FileStream("Sherlock_decode.txt", FileMode.OpenOrCreate);
StreamWriter write2 = new StreamWriter(fs2);
var check = "";

foreach (var character in text3)
{
    var c = character.ToString();
    if (code.ContainsValue(check))
    {
        foreach (KeyValuePair<string, string> pair in code)
        {
            if (pair.Value == check)
            {
                if (pair.Key == "<N>")
                {
                    write2.Write('\n');
                }
                else if (pair.Key == "<R>")
                {
                    write2.Write('\r');
                }
                else
                {
                    write2.Write(pair.Key);
                }
                check = "";
                break;
            }
        }
    }
    check += c;
    write2.Flush();
}
write.Close();
write2.Close();
fs.Close();
fs2.Close();