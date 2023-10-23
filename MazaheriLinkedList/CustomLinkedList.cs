using System.Collections;

namespace MazaheriLinkedList;

public class CustomLinkedList<T> : IList<T>
{
    public class Node<T>
    {
        public T Value { get; set; }
        public Node<T> Next { get; set; }

        public Node(T value)
        {
            Value = value;
            Next = null;
        }
    }

    private Node<T> head;
    private Node<T> tail;
    private int count;

    public CustomLinkedList()
    {
        head = null;
        tail = null;
        count = 0;
    }

    public Node<T> First
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

    public Node<T> Last
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

    public Node<T> Middle
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
        set => count = value;
        get => count;
    }

    public bool IsReadOnly => false;

    public void Add(T item)
    {
        var newNode = new Node<T>(item);
        if (count == 0)
        {
            head = newNode;
            tail = newNode;
        }
        else
        {
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

    public IEnumerator<T> GetEnumerator()
    {
        var current = head;
        while (current != null)
        {
            yield return current.Value;
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

        var newNode = new Node<T>(item);
        if (index == 0)
        {
            newNode.Next = head;
            head = newNode;
        }
        else
        {
            var current = head;
            for (int i = 0; i < index - 1; i++)
            {
                current = current.Next;
            }

            newNode.Next = current.Next;
            current.Next = newNode;
        }

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
            }

            count--;
            return true;
        }

        var current = head;
        while (current.Next != null)
        {
            if (EqualityComparer<T>.Default.Equals(current.Next.Value, item))
            {
                if (current.Next == tail)
                {
                    tail = current;
                }

                current.Next = current.Next.Next;
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
            }
        }
        else
        {
            var current = head;
            for (int i = 0; i < index - 1; i++)
            {
                current = current.Next;
            }

            if (current.Next == tail)
            {
                tail = current;
            }

            current.Next = current.Next.Next;
        }

        count--;
    }

    public void AddAfter(Node<T> targetNode, Node<T> nodeToAdd)
    {
        if (targetNode == Last)
        {
            targetNode.Next = nodeToAdd;
            Last = nodeToAdd;
        }
        else
        {
            var temp = targetNode.Next;
            targetNode.Next = nodeToAdd;
            nodeToAdd.Next = temp;
        }

        count++;
    }
    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public override bool Equals(object? obj)
    {
        var givenObject = (CustomLinkedList<T>)obj;

        if (givenObject.Count != this.Count)
        {
            return false;
        }

        var givenObjectFirst = givenObject.First;
        var baseClassFirst = First;

        for (int index = 0; index < Count; index++)
        {
            if (givenObjectFirst != baseClassFirst)
            {
                return false;
            }

            baseClassFirst = baseClassFirst.Next;
            givenObjectFirst = givenObjectFirst.Next;
        }

        return true;
    }
}