using System;
using System.Collections.Generic;
using System.IO;
class Program
{   
    
    static void Main(string[] args)
    {
        
        using (StreamReader reader = new StreamReader("sherlok.txt"))
        {
            
            Dictionary<char, int> charFreq = new Dictionary<char, int>();

            
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                
                foreach (char c in line)
                {
                    
                    if (charFreq.ContainsKey(c))
                    {
                        
                        charFreq[c]++;
                    }
                    else
                    {
                        
                        charFreq[c] = 1;
                    }
                }
            }

            foreach (KeyValuePair<char, int> pair in charFreq)
            {
                Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
            }
        }
    }
    
}