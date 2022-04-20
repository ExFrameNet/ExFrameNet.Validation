namespace ExFrameNet.Validation
{
    public abstract class AbstractValidator<T> : IValidator<T>
    {
        public abstract bool BreaksValidationIfFaild { get; }
        public abstract string DefaultMessage { get; }
        public virtual uint DefaultErrorCode { get; } = 0;
        public virtual bool PassesWhenNull { get; set; } = true;
        public Dictionary<string, object?> MessageParameters { get; } = new Dictionary<string, object?>();

        public abstract bool Validate(T value);

        public bool Validate(object? value)
        {
            MessageParameters.Add("value", value);

            if(value is null)
                return PassesWhenNull;

            return Validate((T)value);
        }
    }
}
