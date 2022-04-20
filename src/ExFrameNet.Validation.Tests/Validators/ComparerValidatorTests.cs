using ExFrame.Extensions.Property;
using ExFrameNet.Validation.Validators;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace ExFrameNet.Validation.Tests.Validators
{
    public class ComparerValidatorTests
    {
        [Theory]
        [InlineData(15,16,true)]
        [InlineData(15,15,false)]
        [InlineData(15,14,false)]
        public void GreaterThenValidator_ShouldComparseCorrect(int input, int comparision, bool expected)
        {
            //Arrang
            var mock = new Mock<ITestEnv>();
            mock.SetupGet(x => x.IntProp).Returns(input);
            mock.SetupGet(x => x.Validproperties).Returns(new HashSet<string>());

            //Act
            var sut = mock.Object.Property(x => x.IntProp)
                .Validate(x => x.IsGreaterThen(comparision));
            //Assert

            sut.Validate().IsValid.Should().Be(expected);
        }
    }
}
