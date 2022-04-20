using ExFrame.Extensions.Property;
using ExFrameNet.Validation.Validators;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace ExFrameNet.Validation.Tests.Validators
{
    public class ContainsNotVaidatorTests
    {

        [Theory]
        [InlineData("1,1248", ",")]
        [InlineData("Test", "es")]
        public void ContainsNotValidator_Should_Fail_IfStringContainsSubstring(string value, string substring)
        {
            //Arrange
            var mock = new Mock<ITestEnv>();
            mock.SetupGet(x => x.Validproperties).Returns(new HashSet<string>());
            mock.SetupGet(x => x.StringProp).Returns(value);

            //Act
            var sut = mock.Object.Property(x => x.StringProp)
                .Validate(x => x.ShouldNotContain(substring));


            //Assert
            sut.Validate().IsValid.Should().BeFalse();

        }

        [Theory]
        [InlineData("1.1248", ",")]
        [InlineData("Test", "al")]
        public void ContainsNotValidator_Should_Pass_IfStringDoesntContainSubstring(string value, string substring)
        {
            //Arrange
            var mock = new Mock<ITestEnv>();
            mock.SetupGet(x => x.Validproperties).Returns(new HashSet<string>());
            mock.SetupGet(x => x.StringProp).Returns(value);

            //Act
            var sut = mock.Object.Property(x => x.StringProp)
                .Validate(x => x.ShouldNotContain(substring));


            //Assert
            sut.Validate().IsValid.Should().BeTrue();

        }
    }
}
