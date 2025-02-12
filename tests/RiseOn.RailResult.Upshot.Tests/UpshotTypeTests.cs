
using Newtonsoft.Json.Linq;
using Shouldly;
using System;
using Xunit.Sdk;

namespace RiseOn.RailResult.Upshot.Tests;

public class UpshotTypeTests
{
    [Fact]
    public void Success_ShouldCreateSuccessfulUpshot()
    {
        // Arrange
        var value = 2.9;

        // Act
        var result = Upshot<double>.Success(value);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.IsFailure.ShouldBeFalse();
        result.Value.ShouldBe(value);
    }

    [Fact]
    public void Fail_WithException_ShouldCreateFailedUpshot()
    {
        // Arrange
        Upshot<double?> result;
        var exceptionMessage = "Test exception";

        // Act
        try
        {
            throw new InvalidOperationException(exceptionMessage);
        }
        catch (Exception e)
        {
             result = Upshot<double?>.Fail(e);
        }
        
        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.IsFailure.ShouldBeTrue();
        result.Error.Message.ShouldBe(exceptionMessage);
        result.Value.ShouldBeNull();
    }

    [Fact]
    public void Fail_WithMessage_ShouldCreateFailedUpshot()
    {
        // Arrange
        var message = "Test failure message";

        // Act
        var result = Upshot<int>.Fail(message);

        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.IsFailure.ShouldBeTrue();
        result.Error.Message.ShouldBe(message);
    }
}