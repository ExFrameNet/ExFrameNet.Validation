using Moq;
using System.Collections.Generic;
using Xunit;
using ExFrame.Extensions.Property;
using ExFrameNet.Validation.Validators;
using FluentAssertions;

namespace ExFrameNet.Validation.Tests.Validators
{
    public class NotNullValidatorTests
    {
        [Fact]
        public void NotNullValidator_Fails_IfPropertyIsNull()
        {
            //Arrange
            var mock = new Mock<ITestEnv>();

            mock.SetupGet(x => x.Validproperties).Returns(new HashSet<string>());

            //Act
            var sut =
                mock.Object.Property(x => x.StringProp)
                .Validate(x => x.IsNotNull());

            //Assert
            sut.Validate().IsValid.Should().Be(false);
        }


        [Fact]
        public void NotNullValidator_Passes_IfPropertyIsNotNull()
        {
            //Arrange
            var mock = new Mock<ITestEnv>();

            mock.SetupGet(x => x.Validproperties).Returns(new HashSet<string>());
            mock.SetupGet(x => x.StringProp).Returns("");

            //Act
            var sut =
                mock.Object.Property(x => x.StringProp)
                .Validate(x => x.IsNotNull());

            //Assert
            sut.Validate().IsValid.Should().Be(true);
        }
    }
}
