namespace ExFrameNet.Validation;
public interface IValidator<T>
{

    bool Validate(T value);

    string DefaultMessage { get; }

    uint DefaultErrorCode { get; }
}
