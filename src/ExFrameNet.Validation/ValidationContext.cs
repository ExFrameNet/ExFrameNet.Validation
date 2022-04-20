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

        internal ValidationContext(Func<T, TProperty> propertyReader, T classInstance)
        {
            Validators = new List<IValidator>();
            ValidatorAttachments = new Dictionary<IValidator, ValidatorAttachments>();
            PropertyReader = propertyReader;
            ClassInstance = classInstance;
        }

        internal ValidationContext(IEnumerable<IValidator> validators, Func<T, TProperty?> propertyReader, T classInstance)
        {
            Validators = new List<IValidator>(validators);
            ValidatorAttachments = new Dictionary<IValidator, ValidatorAttachments>();
            PropertyReader = propertyReader;
            ClassInstance = classInstance;
        }


        internal ValidationResult Validate(ValidationOptions options, string propertyName)
        {
            var errors = new List<ValidationError>();
            foreach (var validator in Validators)
            {
                validator.MessageParameters.Add("propertyname", propertyName);

                var attempted = PropertyReader(ClassInstance);
                
                

                if (validator.Validate(attempted))
                    continue;

                var attachments = ValidatorAttachments[validator];
                var msg = MessageFormatter.Fromat(attachments.Message, validator.MessageParameters);
                var error = new ValidationError(msg, attachments.ErrorCode, attempted,
                    attachments.Severity, attachments.CustomError);
                errors.Add(error);
                if (options.BreakAfterFirstFail || validator.BreaksValidationIfFaild)
                    break;

            }

            return new ValidationResult(propertyName, errors);
        }
    }
}
