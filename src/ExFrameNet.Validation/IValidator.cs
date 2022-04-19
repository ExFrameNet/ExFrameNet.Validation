using System.ComponentModel;


namespace ExFrameNet.Validation
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IValidator
    {
        bool BreaksValidationIfFaild { get; }

        bool Validate(object? value);

        string DefaultMessage { get; }

        uint DefaultErrorCode { get; }
    }

    public interface IValidator<T> : IValidator
    {
        bool Validate(T? value);
    }
}