using System;
using System.Collections;
using System.Collections.Generic;


namespace MazaheriLinkedList;

[Serializable]
public class CustomLinkedList<T> : IList<T>
{
    [Serializable]
    public class Node
    {
        public T Value { get; set; }
        public Node Next { get; set; }
        public Node Previous { get; set; }

        public Node(T value)
        {
            Value = value;
            Next = null;
            Previous = null;
        }
    }

    private Node head;
    private Node tail;
    private int count;

    public CustomLinkedList()
    {
        head = null;
        tail = null;
        count = 0;
    }

    public Node First
    {
        get
        {
            if (count == 0)
                throw new InvalidOperationException("The list is empty.");
            return head;
        }
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            head = value;
        }
    }

    public Node Last
    {
        get
        {
            if (count == 0)
                throw new InvalidOperationException("The list is empty.");
            return tail;
        }
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            tail = value;
        }
    }

    public Node Middle
    {
        get
        {
            if (count == 0)
                throw new InvalidOperationException("The list is empty.");

            var slow = head;
            var fast = head;

            while (fast != null && fast.Next != null)
            {
                slow = slow.Next;
                fast = fast.Next.Next;
            }

            return slow;
        }
    }

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= count)
                throw new IndexOutOfRangeException();

            var current = head;
            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }

            return current.Value;
        }
        set
        {
            if (index < 0 || index >= count)
                throw new IndexOutOfRangeException();

            var current = head;
            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }

            current.Value = value;
        }
    }

    public int Count
    {
        get => count;
    }

    public bool IsReadOnly => false;

    public void Add(T item)
    {
        var newNode = new Node(item);
        if (count == 0)
        {
            head = newNode;
            tail = newNode;
        }
        else
        {
            newNode.Previous = tail;
            tail.Next = newNode;
            tail = newNode;
        }

        count++;
    }

    public void Clear()
    {
        head = null;
        tail = null;
        count = 0;
    }

    public bool Contains(T item)
    {
        var current = head;
        while (current != null)
        {
            if (EqualityComparer<T>.Default.Equals(current.Value, item))
                return true;
            current = current.Next;
        }

        return false;
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        var current = head;
        while (current != null)
        {
            array[arrayIndex++] = current.Value;
            current = current.Next;
        }
    }

    public int IndexOf(T item)
    {
        var current = head;
        int index = 0;
        while (current != null)
        {
            if (EqualityComparer<T>.Default.Equals(current.Value, item))
                return index;
            current = current.Next;
            index++;
        }

        return -1;
    }

    public void Insert(int index, T item)
    {
        if (index < 0 || index > count)
            throw new IndexOutOfRangeException();

        if (index == count)
        {
            Add(item);
            return;
        }

        var newNode = new Node(item);
        var current = head;
        for (int i = 0; i < index - 1; i++)
        {
            current = current.Next;
        }

        newNode.Next = current.Next;
        newNode.Previous = current;
        current.Next = newNode;
        newNode.Next.Previous = newNode;
        count++;
    }

    public bool Remove(T item)
    {
        if (count == 0)
            return false;

        if (EqualityComparer<T>.Default.Equals(head.Value, item))
        {
            if (count == 1)
            {
                head = null;
                tail = null;
            }
            else
            {
                head = head.Next;
                head.Previous = null;
            }

            count--;
            return true;
        }

        if (EqualityComparer<T>.Default.Equals(tail.Value, item))
        {
            tail = tail.Previous;
            tail.Next = null;
            count--;
            return true;
        }

        var current = head.Next;
        while (current != tail)
        {
            if (EqualityComparer<T>.Default.Equals(current.Value, item))
            {
                current.Previous.Next = current.Next;
                current.Next.Previous = current.Previous;
                count--;
                return true;
            }

            current = current.Next;
        }

        return false;
    }

    public void RemoveAt(int index)
    {
        if (index < 0 || index >= count)
            throw new IndexOutOfRangeException();

        if (index == 0)
        {
            if (count == 1)
            {
                head = null;
                tail = null;
            }
            else
            {
                head = head.Next;
                head.Previous = null;
            }
        }
        else if (index == count - 1)
        {
            tail = tail.Previous;
            tail.Next = null;
        }
        else
        {
            var current = head.Next;
            for (int i = 1; i < index; i++)
            {
                current = current.Next;
            }

            current.Previous.Next = current.Next;
            current.Next.Previous = current.Previous;
        }

        count--;
    }

    public void AddAfter(Node targetNode, Node nodeToAdd)
    {
        if (targetNode == Last)
        {
            Add(nodeToAdd.Value);
        }
        else
        {
            nodeToAdd.Previous = targetNode;
            nodeToAdd.Next = targetNode.Next;
            targetNode.Next.Previous = nodeToAdd;
            targetNode.Next = nodeToAdd;
            count++;
        }
    }

    public void AddFirst(Node node)
    {
        if (count == 0)
        {
            head = node;
            tail = node;
        }
        else
        {
            node.Next = head;
            head.Previous = node;
            head = node;
        }

        count++;
    }

    public void AddFirst(T value)
    {
        var node = new Node(value);
        AddFirst(node);
    }

    public void AddLast(T value)
    {
        Add(value);
    }

    public bool RangeContains(CustomLinkedList<T> range)
    {
        if (range.count > count || range.count == 0)
        {
            return false;
        }

        if (range.Equals(this))
        {
            return true;
        }

        var current = First;
        var givenRangeCurrentNode = range.First;

        var isRangeFirstElementDetected = false;

        var foundedElements = 0;
        while (current != Last.Next)
        {
            if (foundedElements == range.count)
            {
                return true;
            }

            var currentAndRangeEquality = current.Value.Equals(givenRangeCurrentNode.Value);
            if (currentAndRangeEquality && !isRangeFirstElementDetected)
            {
                givenRangeCurrentNode = givenRangeCurrentNode.Next;
                isRangeFirstElementDetected = true;
                foundedElements++;
            }
            else if (currentAndRangeEquality && isRangeFirstElementDetected)
            {
                givenRangeCurrentNode = givenRangeCurrentNode.Next;
                foundedElements++;
            }
            else if (!currentAndRangeEquality && isRangeFirstElementDetected)
            {
                isRangeFirstElementDetected = false;
                foundedElements = 0;
                givenRangeCurrentNode = range.First;
                continue;
            }

            current = current.Next;
        }

        return false;
    }

    public IEnumerator<T> GetEnumerator()
    {
        var current = head;
        while (current != null)
        {
            yield return current.Value;
            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }


    public void MergeSort()
    {
        if (count <= 1)
        {
            return; // The list is already sorted
        }

        // Split the list into two halves
        Node middle = GetMiddleNode();
        Node secondHalf = middle.Next;
        middle.Next = null;

        // Recursively sort the two halves
        CustomLinkedList<T> firstHalfList = new CustomLinkedList<T> { head = head, tail = middle, count = count / 2 };
        CustomLinkedList<T> secondHalfList = new CustomLinkedList<T>
            { head = secondHalf, tail = tail, count = count - count / 2 };

        firstHalfList.MergeSort();
        secondHalfList.MergeSort();

        // Merge the two sorted halves
        head = MergeSortedLists(firstHalfList.head, secondHalfList.head);
        Node current = head;
        while (current.Next != null)
        {
            current.Next.Previous = current;
            current = current.Next;
        }

        tail = current;
    }

    private Node GetMiddleNode()
    {
        if (head == null)
        {
            return null;
        }

        Node slow = head;
        Node fast = head;

        while (fast.Next != null && fast.Next.Next != null)
        {
            slow = slow.Next;
            fast = fast.Next.Next;
        }

        return slow;
    }

    private Node MergeSortedLists(Node first, Node second)
    {
        Node result = null;

        if (first == null)
            return second;
        if (second == null)
            return first;

        if (Comparer<T>.Default.Compare(first.Value, second.Value) <= 0)
        {
            result = first;
            result.Next = MergeSortedLists(first.Next, second);
            result.Next.Previous = result;
        }
        else
        {
            result = second;
            result.Next = MergeSortedLists(first, second.Next);
            result.Next.Previous = result;
        }

        return result;
    }
}