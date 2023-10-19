namespace MazaheriLinkedList;

using System;
using System.Collections;
using System.Collections.Generic;

public class LinkedListSorter<T> : ILinkedListSorter<T>
{
    public void Sort(CustomLinkedList<T> list)
    {
        list.First = MergeSort(list.First);
    }

    private CustomLinkedList<T>.Node<T> MergeSort(CustomLinkedList<T>.Node<T> head)
    {
        if (head == null || head.Next == null)
            return head;

        var middle = GetMiddle(head);
        var nextToMiddle = middle.Next;

        middle.Next = null;

        var left = MergeSort(head);
        var right = MergeSort(nextToMiddle);

        return Merge(left, right);
    }

    private CustomLinkedList<T>.Node<T> GetMiddle(CustomLinkedList<T>.Node<T> head)
    {
        if (head == null)
            return head;

        var slow = head;
        var fast = head;

        while (fast.Next != null && fast.Next.Next != null)
        {
            slow = slow.Next;
            fast = fast.Next.Next;
        }

        return slow;
    }

    private CustomLinkedList<T>.Node<T> Merge(CustomLinkedList<T>.Node<T> left, CustomLinkedList<T>.Node<T> right)
    {
        CustomLinkedList<T>.Node<T> result = null;

        if (left == null)
            return right;
        if (right == null)
            return left;

        if (Comparer<T>.Default.Compare(left.Value, right.Value) <= 0)
        {
            result = left;
            result.Next = Merge(left.Next, right);
        }
        else
        {
            result = right;
            result.Next = Merge(left, right.Next);
        }

        return result;
    }

    
}