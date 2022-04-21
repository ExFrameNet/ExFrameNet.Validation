using ExFrameNet.Utils.Property;

namespace ExFrameNet.Validation
{

    public class ValidationContext<T, TProperty>
        where T : class
    {

        private List<ValidationError> _errors;
        private ValidationOptions _validationOptions { get; }

        internal List<IValidator<TProperty>> Validators { get; }
        internal IValidator<TProperty>? LastValidator => Validators.LastOrDefault();
        internal Dictionary<IValidator<TProperty>, ValidatorAttachments> ValidatorAttachments { get; }

        internal PropertyContext<T, TProperty> Property { get; }

        internal ValidationContext(PropertyContext<T, TProperty> property, ValidationOptions options)
        {
            _errors = new List<ValidationError>();
            _validationOptions = options;
            
            ValidatorAttachments = new Dictionary<IValidator<TProperty>, ValidatorAttachments>();
            Validators = new List<IValidator<TProperty>>();

            Property = property;
        }

        internal ValidationResult Validate()
        {
            _errors.Clear();
            var value = Property.Value;
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

                if (_validationOptions.BreakAfterFirstFail || validator.BreaksValidationIfFaild)
                {
                    break;
                }

            }
            return new ValidationResult(Property.Name, _errors);
        }
    }
}
