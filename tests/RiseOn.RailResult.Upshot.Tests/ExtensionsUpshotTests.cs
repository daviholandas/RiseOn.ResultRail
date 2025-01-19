using System.Linq.Expressions;
using RiseOn.RailResult.Upshot.Extensions;

namespace RiseOn.RailResult.Upshot.Tests;

public class ExtensionsUpshotTests
{
    [Fact]
    public void OnSuccessRail_Should_Invoke_SuccessAction_When_Upshot_Is_Success()
    {
        // Arrange
        var upshot = Upshot.Success();
        bool successActionInvoked = false;
        bool failActionInvoked = false;
       

        // Act
        upshot.StartRailWay(onSuccess: () => successActionInvoked ? Upshot.Success() : Upshot.Fail("Error"),
            onFail: () => failActionInvoked ? Upshot.Success() : Upshot.Fail("Take fail rail way"));

        // Assert
        Assert.True(successActionInvoked);
        Assert.False(failActionInvoked);
    }
}
