using ExFrameNet.Utils.Property;
using ExFrameNet.Validation.Validators;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace ExFrameNet.Validation.Tests.Validators;

public class NotEmptyValidatorTests
{
    [Theory]
    [MemberData(nameof(NotEmptyFailsData))]
    public void NotEmptyValidator_Fails_IfPropertyIsEmpty(object data)
    {
        //Arrange
        Mock<ITestEnv>? mock = new Mock<ITestEnv>();

        mock.SetupGet(x => x.Validproperties).Returns(new HashSet<string>());
        mock.SetupGet(x => x.ObjProp).Returns(data);

        //Act
        var sut =
            mock.Object.Property(x => x.ObjProp)
            .Validate(x => x.IsNotEmpty());

        //Assert
        sut.Validate().IsValid.Should().Be(false);
    }

    [Theory]
    [MemberData(nameof(NotEmptyPassesData))]
    public void NotEmptyValidator_Passes_IfPropertyIsNotEmpty(object data)
    {
        //Arrange
        Mock<ITestEnv>? mock = new Mock<ITestEnv>();

        mock.SetupGet(x => x.Validproperties).Returns(new HashSet<string>());
        mock.SetupGet(x => x.ObjProp).Returns(data);

        //Act
        var sut =
            mock.Object.Property(x => x.ObjProp)
            .Validate(x => x.IsNotEmpty());

        //Assert
        sut.Validate().IsValid.Should().Be(true);
    }

    public static IEnumerable<object[]> NotEmptyFailsData()
    {
        yield return new[] { "" };
        yield return new[] { " " };
        yield return new[] { new List<string>() };
    }

    public static IEnumerable<object[]> NotEmptyPassesData()
    {
        yield return new[] { "1" };
        yield return new[] { (object)15 };
        yield return new[] { new List<string>() { "Test" } };
    }
}
