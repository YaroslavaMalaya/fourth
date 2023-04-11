namespace lab_4;

public class MinHeapNode
{
    public string Letter;
    public int Frequency; // частота
    public MinHeapNode? left, right; // типу діти

    public MinHeapNode(string letter, int frequency)
    {
        left = right = null;
        Letter = letter;
        Frequency = frequency;
    }
}

public class MinHeap
{
    private List<MinHeapNode> heap = new List<MinHeapNode>();
    public int Count()
    {
        return heap.Count;
    }
    
    private void Swap(int i, int j)
    {
        (heap[i], heap[j]) = (heap[j], heap[i]);
    }

    public void Add(MinHeapNode node)
    {
        heap.Add(node);
        var index = heap.Count - 1;
        var parent = (index - 1) / 2;
        
        while (index > 0 && heap[index].Frequency < heap[parent].Frequency)
        {
            Swap(index, parent);
            index = parent;
        }
    }
    
    private void MinHeapify(int index)
    {
        var left = 2 * index + 1;
        var right = 2 * index + 2;
        var min = index;
        
        if (left < heap.Count && heap[left].Frequency < heap[min].Frequency)
        {
            min = left;
        }
        if (right < heap.Count && heap[right].Frequency < heap[min].Frequency)
        {
            min = right;
        }
        if (min != index)
        {
            Swap(index, min);
            MinHeapify(min);
        }
    }

    public MinHeapNode? Min() // зі знаком питання, бо може повертати ще й null
    {
        if (heap.Count == 0)
        {
            Console.WriteLine("The list is empty :(");
            return null;
        }
        
        var min = heap[0];
        heap.Remove(min);
        MinHeapify(0); // починаємо знову сортувати спочатку (з 0 індексу завжди)
        return min;
    }

    public void Remove(MinHeapNode node)
    {
        if (heap.Contains(node))
        {
            heap.Remove(node);
        }
    }
}