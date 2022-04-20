using ExFrameNet.Utils.Property;
using System.ComponentModel;

namespace ExFrameNet.Validation
{
    public class ValidationContext
    {
        internal ValidationContext? InnerContext { get; set; }
        internal PropertyContext Property { get; }

        internal List<IValidator> Validators { get; }
        internal IValidator? LastValidator => Validators.LastOrDefault();
        internal Dictionary<IValidator, ValidatorAttachments> ValidatorAttachments { get; }

        internal ValidationContext(PropertyContext property)
        {
            Validators = new List<IValidator>();
            ValidatorAttachments = new Dictionary<IValidator, ValidatorAttachments>();
            Property = property;
        }

        internal  ValidationResult Validate(ValidationOptions options, List<ValidationError> errors)
        {
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
                errors.Add(error);

                if (options.BreakAfterFirstFail || validator.BreaksValidationIfFaild)
                {
                    breaked = true;
                    break;
                }

            }

            if (InnerContext is not null && !breaked)
                InnerContext.Validate(options, errors);

            return new ValidationResult(Property.Name, errors);
        }



    }

    public class ValidationContext<T, TProperty> : ValidationContext
        where T : class
    {
        internal new PropertyContext<T, TProperty> Property { get; }
        
        

        internal ValidationContext(PropertyContext<T, TProperty> property)
            :base(property)
        {
            Property = property;
        }
    }
}
