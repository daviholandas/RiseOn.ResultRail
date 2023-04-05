using FluentAssertions;
using ResultRail;
using ResultRail.Extensions;

namespace RiseOn.ResultRail.Tests;

public class SolutionTests
{
    [Fact]
    public void Success_ShouldReturnSuccessOperation()
    {
        // Arrange
        var operation = Solution.Success();
       
        // Act
        var result = operation.IsSuccess;
        
        // Assert
        result.Should().BeTrue();
    }
    
    [Fact]
    public void Success_ShouldReturnSuccessOperationAndValue()
    {
        // Arrange
        var operation = Solution<int>.Success(10);
       
        // Act
        var result = operation.Value;
        
        // Assert
        result.Should().Be(10);
    }
    
    [Fact]
    public void Success_ShouldReturnErrorOperation()
    {
        // Arrange
        var operation = Solution.Fail(new ArgumentException("Exception"));
        
        // Act & Assert
        operation.Error?.Exception.Should().BeOfType<ArgumentException>();
        operation.Message.Should().Be(operation.Error?.Exception?.Message);
    }

    [Fact]
    public void RailWay_ShouldResultOfOperation()
    {
        // Arrange
        var resultOperation = 100;
        var initialValue = 10;
        
        // Act
        var result = Solution<int>
            .RailWay(() => initialValue.ToPure())
            .Map(x => x * 10)
            .Finally(f => f.IsSuccess ? f.Value : 0);

        // Assert
        result.Should().Be(resultOperation);

    }
}