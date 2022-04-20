using ExFrameNet.Utils.Property;

namespace ExFrameNet.Validation
{
    public abstract class ValidationContext
    {
        internal abstract ValidationContext? InnerContext { get; set; }
        internal abstract PropertyContext PropertyContext { get; }
        internal abstract ValidationResult Validate(ValidationOptions options, List<ValidationError> errors);

    }

    public class ValidationContext<T, TProperty> : ValidationContext
        where T : class
    {
        internal PropertyContext<T,TProperty> Property { get; }
        internal List<IValidator> Validators { get; }
        internal IValidator? LastValidator => Validators.LastOrDefault();
        internal Dictionary<IValidator, ValidatorAttachments> ValidatorAttachments { get; }
        internal override ValidationContext? InnerContext { get; set; }
        internal override PropertyContext PropertyContext => PropertyContext;

        internal ValidationContext(PropertyContext<T,TProperty> property)
        {
            Validators = new List<IValidator>();
            ValidatorAttachments = new Dictionary<IValidator, ValidatorAttachments>();
            Property = property;
        }

        internal override ValidationResult Validate(ValidationOptions options, List<ValidationError> errors)
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
}
