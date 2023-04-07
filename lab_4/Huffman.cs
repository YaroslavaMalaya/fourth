namespace lab_4;

public class Huffman
{
    public Dictionary<char, string> dict = new Dictionary<char, string>();
    private MinHeap minHeap = new MinHeap();

    public Dictionary<char, string> HuffmanCode(Dictionary<char, int> dictionary)
    {
        foreach (var element in dictionary)
        {
            minHeap.Add(new MinHeapNode(element.Key, element.Value));
        }
        
        while (minHeap.Count() != 1)
        {
            var left = minHeap.Min();
            minHeap.Remove(left);
            var right = minHeap.Min();
            minHeap.Remove(right);
            var main = new MinHeapNode((char)(left.Letter + right.Letter), left.Frequency + right.Frequency);
            main.left = left;
            main.right = right;

            minHeap.Add(main);
        }

        
        var min = minHeap.Min();
        var code = "";
        foreach (var element in dictionary)
        {
            dict[element.Key] = Travel(min, element.Key);
        }
        return dict;
    }

    public string? Travel(MinHeapNode current, char goal)
    {
        if (current.Letter == goal)
        {
            return "";
        }
        if (current.left == null && current.right == null)
        {
            return null;
        }

        var element = Travel(current.left, goal);
        if (element != null)
        {
            return "0" + element;
        }
        element = Travel(current.right, goal);
        if (element != null)
        {
            return "1" + element;
        }
        return null;
    }
}