namespace ExFrameNet.Validation;

public interface IValidatable
{
    HashSet<string> Validproperties { get; }

    void OnPropertyValidated(ValidationResult result);
}
