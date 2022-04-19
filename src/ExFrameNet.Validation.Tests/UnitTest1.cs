using System.Collections.Generic;
using System.ComponentModel;
using ExFrame.Extensions.Property;
using ExFrameNet.Validation;
using ExFrameNet.Validation.Validators;
using Xunit;

namespace ExFrameNet.Validation.Tests
{
    public class UnitTest1
    {
        class TestClass : INotifyPropertyChanged, IValidatable
        {
            private string _testProperty;
            public HashSet<string> Validproperties { get; } = new HashSet<string>();
            public string TestProperty 
            { 
                get => _testProperty;
                set
                {
                    if (_testProperty == value)
                        return;

                    _testProperty = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TestProperty)));
                }
            }


            public event PropertyChangedEventHandler? PropertyChanged;

            public void OnPropertyValidated(ValidationResult result)
            {
                var valid = result.IsValid;
            }
        }
        [Fact]
        public void Test1()
        {
            //Arrange
            var testClass = new TestClass();
            //Act
            testClass.Property(x => x.TestProperty)
                .Changed()
                .Validate(x => x.IsNotEmpty())
                .AfterValidation(AfterVasliation);

            testClass.TestProperty = "";
            //Assert
        }


        private void AfterVasliation(ValidationResult result)
        {
            var valid = result.IsValid;
        }
    }
}