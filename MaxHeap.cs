using System;
using System.Collections.Generic;
using System.IO;

class MaxHeap
{
    private List<Tuple<int, string>> heap;  // Tupla (prioridade, elemento)

    public MaxHeap()
    {
        heap = new List<Tuple<int, string>>();
    }

    public int GetSize() => heap.Count;

    public bool IsEmpty() => heap.Count == 0;

    public void Insert(int priority, string value)
    {
        heap.Add(new Tuple<int, string>(priority, value));
        HeapifyUp(heap.Count - 1);
    }

    public Tuple<int, string> ExtractMax()
    {
        if (IsEmpty()) throw new InvalidOperationException("Heap is empty");

        var max = heap[0];
        heap[0] = heap[heap.Count - 1];
        heap.RemoveAt(heap.Count - 1);
        HeapifyDown(0);

        return max;
    }

    public void PrintHeap()
    {
        for (int i = 0; i < heap.Count; i++)
        {
            Console.WriteLine($"Priority: {heap[i].Item1}, Element: {heap[i].Item2}");
        }
    }

    public void ChangePriority(int index, int newPriority)
    {
        if (index < 0 || index >= heap.Count)
            throw new ArgumentOutOfRangeException(nameof(index), "Invalid index.");

        var element = heap[index];
        heap[index] = new Tuple<int, string>(newPriority, element.Item2);
        HeapifyUp(index);
        HeapifyDown(index);
    }

    private void HeapifyUp(int index)
    {
        while (index > 0 && heap[Parent(index)].Item1 < heap[index].Item1)
        {
            Swap(index, Parent(index));
            index = Parent(index);
        }
    }

    private void HeapifyDown(int index)
    {
        int left = LeftChild(index);
        int right = RightChild(index);
        int largest = index;

        if (left < heap.Count && heap[left].Item1 > heap[largest].Item1)
            largest = left;

        if (right < heap.Count && heap[right].Item1 > heap[largest].Item1)
            largest = right;

        if (largest != index)
        {
            Swap(index, largest);
            HeapifyDown(largest);
        }
    }

    private void Swap(int i, int j)
    {
        var temp = heap[i];
        heap[i] = heap[j];
        heap[j] = temp;
    }

    private int Parent(int index) => (index - 1) / 2;
    private int LeftChild(int index) => 2 * index + 1;
    private int RightChild(int index) => 2 * index + 2;
}