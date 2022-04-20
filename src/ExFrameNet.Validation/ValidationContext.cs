using ExFrameNet.Utils.Property;
using System.ComponentModel;

namespace ExFrameNet.Validation
{
    public class ValidationContext
    {
        private List<ValidationError> _errors = new List<ValidationError>();
        internal ValidationContext? InnerContext { get; set; }
        internal PropertyContext Property { get; }

        internal List<IValidator> Validators { get; }
        internal IValidator? LastValidator => Validators.LastOrDefault();
        internal Dictionary<IValidator, ValidatorAttachments> ValidatorAttachments { get; }
        
        public ValidationOptions ValidationOptions { get; }

        internal ValidationContext(PropertyContext property, ValidationOptions options)
        {
            Validators = new List<IValidator>();
            ValidatorAttachments = new Dictionary<IValidator, ValidatorAttachments>();
            Property = property;
            ValidationOptions = options;
        }

        internal  ValidationResult Validate()
        {
            _errors.Clear();
            var value = Property.Value;
            var breaked = false;
            foreach (var validator in Validators)
            {
                validator.MessageParameters.Add("propertyname", Property.Name);


                var isValid = validator.Validate(value);

                if (isValid)
                    continue;

                var attachments = ValidatorAttachments[validator];
                var msg = MessageFormatter.Fromat(attachments.Message, validator.MessageParameters);
                var error = new ValidationError(msg, attachments.ErrorCode, value,
                    attachments.Severity, attachments.CustomError);
                _errors.Add(error);

                if (ValidationOptions.BreakAfterFirstFail || validator.BreaksValidationIfFaild)
                {
                    breaked = true;
                    break;
                }

            }

            if (InnerContext is not null && !breaked)
            {
                var innerresult = InnerContext.Validate();
                _errors.AddRange(innerresult.Errors);
            }

            return new ValidationResult(Property.Name, _errors);
        }



    }

    public class ValidationContext<T, TProperty> : ValidationContext
        where T : class
    {
        internal new PropertyContext<T, TProperty> Property { get; }
        
        

        internal ValidationContext(PropertyContext<T, TProperty> property, ValidationOptions options)
            :base(property, options)
        {
            Property = property;
        }
    }
}
