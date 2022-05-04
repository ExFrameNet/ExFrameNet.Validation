namespace ExFrameNet.Validation.Validators;

public class GreaterThenValidator<T> : AbstractValidator<T>
    where T : IComparable<T>
{
    private readonly T _value;

    public override bool BreaksValidationIfFaild => false;
    public override string DefaultMessage => "Value must be greater then {comparison}";

    public GreaterThenValidator(T value)
    {
        _value = value;
        MessageParameters.Add("comparison", value);
    }

    public override bool Validate(T value)
    {
        int res = _value.CompareTo(value);
        return res > 0;
    }
}
