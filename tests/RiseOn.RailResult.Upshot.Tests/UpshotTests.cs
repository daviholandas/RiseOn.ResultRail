using Shouldly;

namespace RiseOn.RailResult.Upshot.Tests;

public class UpshotTests
{
    
    [Fact]
    public void Fail_WithMessageAndException_ShouldReturnFailureOperation()
    {
        // Arrange
        var message = "Error occurred";
        var operation = Upshot.Fail(message);

        // Act
        var result = operation.IsFailure;

        // Assert
        result.ShouldBeTrue();
        operation.Error.Message.ShouldBe(message);
    }

    [Fact]
    public void Fail_WithException_ShouldReturnFailureOperation()
    {
        // Arrange
        var exception = new InvalidOperationException("Error occurred");
        var operation = Upshot.Fail(exception);

        // Act
        var result = operation.IsFailure;

        // Assert
        result.ShouldBeTrue();
        operation.Error.Exception.ShouldBeOfType<InvalidOperationException>();
        operation.Error.Message.ShouldBe(exception.Message);
    }

    [Fact]
    public void Fail_WithMessage_ShouldReturnFailureOperation()
    {
        // Arrange
        var message = "Error occurred";
        var operation = RailResult.Upshot.Upshot.Fail(message);

        // Act
        var result = operation.IsFailure;

        // Assert
        result.ShouldBeTrue();
        operation.Error.Message.ShouldBe(message);
    }
}
