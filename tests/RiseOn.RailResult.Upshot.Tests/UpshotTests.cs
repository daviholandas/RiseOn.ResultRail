using RiseOn.RailResult.Upshot.Extensions;
using Shouldly;

namespace RiseOn.RailResult.Upshot.Tests;

public class UpshotTests
{
    [Fact]
    public void Success_ShouldReturnSuccessOperation()
    {
        // Arrange
        var operation = Upshot.Success();

        // Act
        var result = operation.IsSuccess;
        
        // Assert
        result.ShouldBeTrue();
    }
    
    [Fact]
    public void Success_ShouldReturnSuccessOperationAndValue()
    {
        // Arrange
        var operation = Upshot<int>.Success(10);
       
        // Act
        var result = operation.Value;
        
        // Assert
        result.ShouldBe(10);
    }
    
    [Fact]
    public void Success_ShouldReturnErrorOperation()
    {
        // Arrange
        var operation = Upshot.Fail(new ArgumentException("Exception"));
        
        // Act & Assert
        operation.Error?.Exception.ShouldBeOfType<ArgumentException>();
        operation.Message.ShouldBe(operation.Error?.Exception?.Message);
    }

    [Fact]
    public void StartRailWay_ShouldWrapOperationAndReturnUpshotMessageError()
    {
        // Arrange
        var resultOperation = 100;
        var initialValue = 10;
        Func<Upshot> operation = () => Upshot.Success();

        var re = resultOperation.OnRail(
            r => Upshot<int>.Success(r * 10)
            .OnRailSuccess(t => Upshot<int>.Success(t.Value * 10)));

        // Act
        var result = operation();
        result.OnRail(result => Upshot.Fail("TEST")
        .OnRailFail(re => Console.WriteLine(re.Message)));

        // Assert
        result.IsFailure.ShouldBeTrue();
        result.Error?.Message.ShouldBe("error");
    }
}