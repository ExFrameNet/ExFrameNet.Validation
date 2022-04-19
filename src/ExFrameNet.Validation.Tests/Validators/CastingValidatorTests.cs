﻿using ExFrame.Extensions.Property;
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
                .Validate(x => x.CastTo<ITestEnv,string,int>());

            //Assert
            sut.ValidationResult.IsValid.Should().Be(false);
            
        }

        [Fact]
        public void CastingValidator_Passes_IfCanCast()
        {
            //Arrange
            //Arrange
            var mock = new Mock<ITestEnv>();

            mock.SetupGet(x => x.Validproperties).Returns(new HashSet<string>());
            mock.SetupGet(x => x.StringProp).Returns("15");

            //Act
            var sut =
                mock.Object.Property(x => x.StringProp)
                .Validate(x => x.CastTo<ITestEnv, string, int>());

            sut.ValidationResult.IsValid.Should().Be(true);

        }
    }
}