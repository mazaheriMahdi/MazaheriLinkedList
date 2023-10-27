using FluentAssertions;
using MazaheriLinkedList;

namespace MazaheriLinkedListTests;

public class MazaheriLinkedListIntegrationTest
{
    [Fact]
    public void IntegrationTest()
    {
        var list = new CustomLinkedList<int>();
        
        list.Add(23);
        list.AddFirst(35);
        list.Insert(1, 93);
        list.Insert(0, 82);
        list.Insert(0, 20);
        list.AddLast(57);
        list.AddLast(19);
        list.Insert(0, 0);
        list.AddFirst(47);
        list.Insert(7, 89);
        list.AddFirst(80);
        list.Insert(8, 71);
        var size = list.Count;
        var isEmpty = !list.Any();
        list.MergeSort();
        list.MergeSort();
        list.Insert(1, 12);
        var thirdItem = list[2];
        var last = list.Last;
        var tenth = list[10];
        var first = list.First;


        size.Should().Be(12);
        isEmpty.Should().BeFalse();
        thirdItem.Should().Be(19);
        last.Value.Should().Be(93);
        tenth.Should().Be(82);
        first.Value.Should().Be(0);
    }
}