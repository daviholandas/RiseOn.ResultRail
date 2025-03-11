using RiseOn.RailResult.Upshot.Extensions;
using Shouldly;

namespace RiseOn.RailResult.Upshot.Tests;

public class ExtensionsUpshotTests
{
    [Fact]
    public void OnRail_Should_Invoke_SuccessRail_When_Upshot_Is_Success()
    {
        // Arrange
        var upshot = Upshot.Success();
        bool successRailInvoked = false;
        bool failRailInvoked = false;

        Func<IUpshot> successRail = () =>
        {
            successRailInvoked = true;
            return upshot;
        };

        Func<IUpshot> failRail = () =>
        {
            failRailInvoked = true;
            return upshot;
        };

        // Act
        var result = upshot.OnRail(successRail, failRail);

        // Assert
        successRailInvoked.ShouldBeTrue();
        failRailInvoked.ShouldBeFalse();
        result.ShouldBe(upshot);
    }

    [Fact]
    public void OnRail_Should_Invoke_FailRail_When_Upshot_Is_Failure()
    {
        // Arrange
        var upshot = RailResult.Upshot.Upshot.Fail("Error");
        bool successRailInvoked = false;
        bool failRailInvoked = false;

        Func<IUpshot> successRail = () =>
        {
            successRailInvoked = true;
            return upshot;
        };

        Func<IUpshot> failRail = () =>
        {
            failRailInvoked = true;
            return upshot;
        };

        // Act
        var result = upshot.OnRail(successRail, failRail);

        // Assert
        successRailInvoked.ShouldBeFalse();
        failRailInvoked.ShouldBeTrue();
        result.ShouldBe(upshot);
    }

    [Fact]
    public void OnRailSuccess_Should_Invoke_Action_When_Upshot_Is_Success()
    {
        // Arrange
        var upshot = Upshot.Success();
        bool actionInvoked = false;

        Func<IUpshot> action = () =>
        {
            actionInvoked = true;
            return upshot;
        };

        // Act
        var result = upshot.OnRailSuccess(action);

        // Assert
        actionInvoked.ShouldBeTrue();
        result.ShouldBe(upshot);
    }

    [Fact]
    public void OnRailFail_Should_Invoke_Action_When_Upshot_Is_Failure()
    {
        // Arrange
        var upshot = RailResult.Upshot.Upshot.Fail("Error");
        bool actionInvoked = false;

        Func<IUpshot> action = () =>
        {
            actionInvoked = true;
            return upshot;
        };

        // Act
        var result = upshot.OnRailFail(action);

        // Assert
        actionInvoked.ShouldBeTrue();
        result.ShouldBe(upshot);
    }

    [Fact]
    public void Map_Should_Return_Mapped_Value_When_Upshot_Is_Success()
    {
        // Arrange
        var upshot = Upshot.Success();
        var expectedValue = "MappedValue";

        Func<IUpshot, string> func = (u) => expectedValue;

        // Act
        var result = upshot.Map("DefaultValue", func);

        // Assert
        result.ShouldBe(expectedValue);
    }

    [Fact]
    public void Map_Should_Return_Default_Value_When_Upshot_Is_Failure()
    {
        // Arrange
        var upshot = Upshot.Fail("Error");
        var defaultValue = "DefaultValue";

        Func<IUpshot, string> func = (u) => "MappedValue";

        // Act
        var result = upshot.Map(defaultValue, func);

        // Assert
        result.ShouldBe(defaultValue);
    }
}
