using ExFrameNet.Utils.Property;
using ExFrameNet.Validation.Tests.mock;
using ExFrameNet.Validation.Validators;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace ExFrameNet.Validation.Tests.Validators
{
    public class CastingValidatorTests
    {

        [Fact]
        public void CastingValidator_Failed_IfCantCast()
        {
            //Arrange
            var mock = new Mock<ITestEnv>();

            mock.SetupGet(x => x.Validproperties).Returns(new HashSet<string>());
            mock.SetupGet(x => x.StringProp).Returns("hallo");

            //Act
            var sut =
                mock.Object.Property(x => x.StringProp)
                .Validate(x => x.CanBeCastedTo<ITestEnv,string,int>());

            //Assert
            sut.Validate().IsValid.Should().Be(false);
            
        }

        [Fact]
        public void CastingValidator_Passes_IfCanCast()
        {
            //Arrange
            var mock = new Mock<ITestEnv>();

            mock.SetupGet(x => x.Validproperties).Returns(new HashSet<string>());
            mock.SetupGet(x => x.StringProp).Returns("15");

            //Act
            var sut =
                mock.Object.Property(x => x.StringProp)
                .Validate(x => x.CanBeCastedTo<ITestEnv, string, int>());

            sut.Validate().IsValid.Should().Be(true);

        }

        [Fact]
        public void CastingValidator_Should_ReturnCastedValue()
        {
            var mock = new Mock<ITestEnv>();

            mock.SetupGet(x => x.Validproperties).Returns(new HashSet<string>());
            mock.SetupGet(x => x.StringProp).Returns("15");

            //Act
            var dummyValidator = new CastedDummy<int>();
            var sut =
                mock.Object.Property(x => x.StringProp)
                .Validate(x => x.CanBeCastedTo<ITestEnv, string, int>())
                .Transform(x => int.Parse(x))
                .Validate(x => x.AddValidator(dummyValidator));

            sut.Validate();
            dummyValidator.Value.Should().Be(15);
        }
    }
}
