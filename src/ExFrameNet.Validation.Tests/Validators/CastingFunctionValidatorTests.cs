using ExFrame.Extensions.Property;
using ExFrameNet.Validation.Validators;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ExFrameNet.Validation.Tests.Validators
{
    public class CastingFunctionValidatorTests
    {
        [Fact]
        public void Validation_Should_Fail_IfFunctionCantCast()
        {
            //Arrange
            var mock = new Mock<ITestEnv>();

            mock.SetupGet(x => x.Validproperties).Returns(new HashSet<string>());
            mock.SetupGet(x => x.StringProp).Returns("hallo");

            //Act
            var sut =
                mock.Object.Property(x => x.StringProp)
                .Validate(x => x.Cast(x => int.Parse(x)));

            //Assert
            sut.ValidationResult.IsValid.Should().Be(false);
        }


        [Fact]
        public void Validation_Should_Pass_IfFunctionCanCast()
        {
            //Arrange
            var mock = new Mock<ITestEnv>();

            mock.SetupGet(x => x.Validproperties).Returns(new HashSet<string>());
            mock.SetupGet(x => x.StringProp).Returns("15");

            //Act
            var sut =
                mock.Object.Property(x => x.StringProp)
                .Validate(x => x.Cast(x => int.Parse(x)));

            //Assert
            sut.ValidationResult.IsValid.Should().Be(true);
        }
    }
}
