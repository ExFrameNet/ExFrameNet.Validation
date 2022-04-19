using ExFrameNet.Utils.Property;
using System.ComponentModel.DataAnnotations;

namespace ExFrameNet.Validation
{
    public class ValidationContext<T, TProperty>
        where T : class
    {
        internal Func<T, TProperty?> PropertyReader { get; }
        internal T ClassInstance { get; }
        internal List<IValidator> Validators { get; }
        internal IValidator? LastValidator => Validators.LastOrDefault();
        internal Dictionary<IValidator, ValidatorAttachments> ValidatorAttachments { get; }
        internal Dictionary<IParameterizedValidator<TProperty>, object> ValidatorParameters { get; }

        internal ValidationContext(Func<T, TProperty> propertyReader, T classInstance)
        {
            Validators = new List<IValidator>();
            ValidatorAttachments = new Dictionary<IValidator, ValidatorAttachments>();
            ValidatorParameters = new Dictionary<IParameterizedValidator<TProperty>, object>();
            PropertyReader = propertyReader;
            ClassInstance = classInstance;
        }

        internal ValidationContext(IEnumerable<IValidator> validators, Func<T, TProperty?> propertyReader, T classInstance)
        {
            Validators = new List<IValidator>(validators);
            ValidatorAttachments = new Dictionary<IValidator, ValidatorAttachments>();
            ValidatorParameters = new Dictionary<IParameterizedValidator<TProperty>, object>();
            PropertyReader = propertyReader;
            ClassInstance = classInstance;
        }


        internal ValidationResult Validate(ValidationOptions options, string propertyName)
        {
            var errors = new List<ValidationError>();
            foreach (var validator in Validators)
            {
                var attempted = PropertyReader(ClassInstance);
                bool isValid = false;
                if (validator is IParameterizedValidator<TProperty> pvalidator && ValidatorParameters.TryGetValue(pvalidator, out var param))
                    isValid = pvalidator.Validate(attempted, param);
                else
                    isValid = validator.Validate(attempted);

                if (isValid)
                    continue;

                var attachments = ValidatorAttachments[validator];
                var error = new ValidationError(attachments.Message, attachments.ErrorCode, attempted,
                    attachments.Severity, attachments.CustomError);
                errors.Add(error);
                if (options.BreakAfterFirstFail || validator.BreaksValidationIfFaild)
                    break;

            }

            return new ValidationResult(propertyName, errors);
        }
    }
}
