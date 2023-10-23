using FluentAssertions;
using MazaheriLinkedList;

namespace MazaheriLinkedListTests;

public class MazaheriLinkedListTests
{
    [Fact]
    public void ContainsRange_ShouldReturnFalse_WhenLenghtOfGivenListIsMoreThanBaseList()
    {
        // Arrange
        var mainList = new CustomLinkedList<int>();
        mainList.Add(20);
        mainList.Add(30);
        mainList.Add(4);
        mainList.Add(2);
        mainList.Add(1);
        mainList.Add(3);
        mainList.Add(50);

        var targetRange = new CustomLinkedList<int>();
        targetRange.Add(4);
        targetRange.Add(4);
        targetRange.Add(4);
        targetRange.Add(4);
        targetRange.Add(4);
        targetRange.Add(2);
        targetRange.Add(1);
        targetRange.Add(3);

        // Act
        var result = mainList.RangeContains(targetRange);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void ContainsRange_ShouldReturnTrue_WhenGivenRangeIsFound()
    {
        // Arrange
        var mainList = new CustomLinkedList<int>();
        mainList.Add(22);
        mainList.Add(3);
        mainList.Add(6);
        mainList.Add(10);
        mainList.Add(1);
        mainList.Add(2);
        mainList.Add(1);
        mainList.Add(3);
        mainList.Add(6);
        mainList.Add(10);
        mainList.Add(1);
        mainList.Add(2);
        mainList.Add(99);

        var targetRange = new CustomLinkedList<int>();
        targetRange.Add(1);
        targetRange.Add(3);
        targetRange.Add(6);
        targetRange.Add(10);
        targetRange.Add(1);
        targetRange.Add(2);

        // Act
        var result = mainList.RangeContains(targetRange);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void ContainsRange_ShouldReturnFalse_WhenNoMatchingRangeFound()
    {
        // Arrange
        var mainList = new CustomLinkedList<int>();
        mainList.Add(20);
        mainList.Add(30);
        mainList.Add(4);
        mainList.Add(2);
        mainList.Add(1);
        mainList.Add(3);
        mainList.Add(50);

        var targetRange = new CustomLinkedList<int>();
        targetRange.Add(4);
        targetRange.Add(4);
        targetRange.Add(1);
        targetRange.Add(3);

        // Act
        var result = mainList.RangeContains(targetRange);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void ContainsRange_ShouldReturnFalse_WhenRagetListIsNull()
    {
        // Arrange
        var mainList = new CustomLinkedList<int>();
        mainList.Add(20);
        mainList.Add(30);
        mainList.Add(4);
        mainList.Add(2);
        mainList.Add(1);
        mainList.Add(3);
        mainList.Add(50);

        var targetRange = new CustomLinkedList<int>();

        // Act
        var result = mainList.RangeContains(targetRange);

        // Assert
        result.Should().BeFalse();
    }
}