namespace ExFrameNet.Validation.Validators
{
    public class NotNullValidator<T> : IValidator<T>
    {
        public string DefaultMessage => "Value can't be null";
        public uint DefaultErrorCode => 0;

        public bool Validate(T value)
        {
            return value is not null;
        }
    }
}
