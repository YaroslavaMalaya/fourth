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

FileStream fs4 = new FileStream("Sherlock_byte.txt", FileMode.OpenOrCreate);
StreamWriter write4 = new StreamWriter(fs4);
var text4 = File.ReadAllText("Sherlock_code.txt");

var bytes = GetBytes(text4);
fs4.Write(bytes);

write.Close();
write2.Close();
write4.Close();
fs.Close();
fs2.Close();
fs4.Close();

byte[] GetBytes(string line)
{
    string[] byteStrings = new string[line.Length / 8];
    for (int i = 0; i < line.Length-8; i += 8)
    {
        byteStrings[i / 8] = line.Substring(i, 8);
        //Console.WriteLine(byteStrings[i / 8]);
    }
    byteStrings[^1] = line.Substring(byteStrings.Length*8);
    while (byteStrings[^1].Length != 8 )
    {
        byteStrings[^1] += "0";
    }
    //Console.WriteLine(byteStrings[^1]);
    byte[] bytes = new byte[byteStrings.Length];
    for (int i = 0; i < byteStrings.Length; i++)
    {
        bytes[i] = Convert.ToByte(byteStrings[i], 2);
    }
    return bytes;
}
