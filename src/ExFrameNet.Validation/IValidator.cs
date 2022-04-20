using System.ComponentModel;
using System.Security;

namespace ExFrameNet.Validation
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IValidator
    {
        Dictionary<string, object?> MessageParameters { get; }
        bool BreaksValidationIfFaild { get; }

        bool PassesWhenNull { get; set; }

        bool Validate(object? value);

        string DefaultMessage { get; }

        uint DefaultErrorCode { get; }
    }

    public interface IValidator<T> : IValidator
    {
        bool Validate(T value);
    }
}