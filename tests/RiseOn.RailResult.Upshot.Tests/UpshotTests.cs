using Newtonsoft.Json;
using Shouldly;
using Xunit.Abstractions;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace RiseOn.RailResult.Upshot.Tests;

public class UpshotTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public UpshotTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

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
        var operation = Upshot.Fail(message);

        // Act
        var result = operation.IsFailure;

        // Assert
        result.ShouldBeTrue();
        operation.Error.Message.ShouldBe(message);
    }
}
