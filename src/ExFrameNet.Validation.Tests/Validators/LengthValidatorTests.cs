﻿using ExFrame.Extensions.Property;
using ExFrameNet.Validation.Validators;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace ExFrameNet.Validation.Tests.Validators
{
    public class LengthValidatorTests
    {
        [Theory]
        [InlineData("123456789", 0,20,true)]
        [InlineData("123456789", 0,9,true)]
        [InlineData("123456789", 0,8,false)]
        [InlineData("123",3,15, true)]
        [InlineData("123",2,15, true)]
        [InlineData("123",4,15, false)]
        [InlineData(null,5,15, true)]
        public void LengthValidator_Shloud_ValidateLength(string value, uint min, uint max, bool expectedResult)
        {

            //Arrange
            var mock = new Mock<ITestEnv>();
            mock.SetupGet(x => x.Validproperties).Returns(new HashSet<string>());
            mock.SetupGet(x => x.StringProp).Returns(value);

            //Act
            var sut = mock.Object.Property(x => x.StringProp)
                .Validate(x => x.Lenght(min,max));


            //Assert
            sut.ValidationResult.IsValid.Should().Be(expectedResult);
        }
    }
}