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

        Func<int, Upshot<double>> failFun = value => Upshot<double>.Success(value / 10);
        Func<int, Upshot<double>> successFun = value => Upshot<double>.Success(value * 10);

        // Act
        var result = initialValue
            .OnRail(x => x > 10,
                failRail: failFun,
                successRail: successFun);
    }
}
