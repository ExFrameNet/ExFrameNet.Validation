using ExFrameNet.Utils.Property;

namespace ExFrameNet.Validation;

public class ValidationContext<T, TProperty>
    where T : class
{

    private readonly List<ValidationError> _errors;
    private ValidationOptions _validationOptions;

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


            bool isValid = validator.Validate(value);

            if (isValid)
            {
                continue;
            }

            var attachments = ValidatorAttachments[validator];
            string? msg = MessageFormatter.Fromat(attachments.Message, validator.MessageParameters);
            ValidationError? error = new ValidationError(msg, attachments.ErrorCode, value,
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
