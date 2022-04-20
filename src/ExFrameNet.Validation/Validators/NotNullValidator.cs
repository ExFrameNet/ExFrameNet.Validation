namespace ExFrameNet.Validation.Validators
{
    public class NotNullValidator<T> : AbstractValidator<T>
    {
        public override string DefaultMessage => "{propertyName} can't be null";
        public override uint DefaultErrorCode => 0;

        public override bool BreaksValidationIfFaild => false;

        public override bool Validate(T? value)
        {
            return value is not null;
        }
    }
}
