using ExFrameNet.Utils.Property;

namespace ExFrameNet.Validation
{
    public class ValidationContext<T, TProperty>
        where T : class
    {
        private List<IValidator<TProperty>> _validators { get; }
        internal IValidator<TProperty>? LastValidator => _validators.LastOrDefault();
        internal Dictionary<IValidator<TProperty>, ValidatorAttachments> ValidatorAttachments { get; }

        internal ValidationContext()
        {
            _validators = new List<IValidator<TProperty>>();
            ValidatorAttachments = new Dictionary<IValidator<TProperty>, ValidatorAttachments>();
        }

        internal ValidationResult Validate(ValidationOptions options, PropertyContext<T,TProperty> ctx)
        {
            var errors = new List<ValidationError>();
            foreach (var validator in _validators)
            {
                var isValid = validator.Validate(ctx.Value);
                
                if (isValid)
                    continue;

                var attachments = ValidatorAttachments[validator];
                var error = new ValidationError(attachments.Message, attachments.ErrorCode, ctx.Value,
                    attachments.Severity, attachments.CustomError);
                errors.Add(error);
                if (options.BreakAfterFirstFail) 
                    break;

            }

            return new ValidationResult(ctx.Name, errors);
        }
        
        internal void AddValidator(IValidator<TProperty> validator)
        {
            _validators.Add(validator);
            ValidatorAttachments[validator] = new ValidatorAttachments()
            {
                Message = validator.DefaultMessage,
                ErrorCode = validator.DefaultErrorCode
            };
        }
    }
}
