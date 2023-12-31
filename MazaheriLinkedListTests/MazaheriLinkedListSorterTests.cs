using FluentAssertions;
using MazaheriLinkedList;

namespace MazaheriLinkedListTests;

public class MazaheriLinkedListSorterTests
{
    
    [Fact]
    public void Sort_ShouldSortTheArray_WhenEver()
    {
        // Arrange 
        var linkedList = new CustomLinkedList<int>();
        linkedList.Add(1);
        linkedList.Add(3);
        linkedList.Add(2);
        linkedList.Add(20);
        linkedList.Add(4);
        linkedList.Add(15);

        var expected = new CustomLinkedList<int>();
        expected.Add(1);
        expected.Add(2);
        expected.Add(3);
        expected.Add(4);
        expected.Add(15);
        expected.Add(20);

        // Act
        linkedList.MergeSort();

        // Assert
        linkedList.Should().BeEquivalentTo(expected);
    }
}