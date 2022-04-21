using System.ComponentModel;
using System.Security;

namespace ExFrameNet.Validation
{

    public interface IValidator<T>
    {
        Dictionary<string, object?> MessageParameters { get; }
        bool BreaksValidationIfFaild { get; }
        bool PassesWhenNull { get; set; }
        string DefaultMessage { get; }
        uint DefaultErrorCode { get; }

        bool Validate(T value);
    }
}