namespace ExFrameNet.Validation.Tests.mock;

public class CastedDummy<TProperty> : AbstractValidator<TProperty>
{

    public TProperty? Value { get; private set; }
    public override bool BreaksValidationIfFaild => false;
    public override string DefaultMessage => "";

    public override bool Validate(TProperty value)
    {
        Value = value;
        return true;
    }
}
