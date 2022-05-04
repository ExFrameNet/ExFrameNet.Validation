namespace ExFrameNet.Validation.Validators;

public class CastingValidator<T, TTo> : AbstractValidator<T>
{
    public Func<T, TTo?> Converter { get; private set; }
    public override string DefaultMessage => $"Value can't be casted to {typeof(TTo)}";
    public override uint DefaultErrorCode => 0;
    public override bool BreaksValidationIfFaild => true;

    public CastingValidator()
    {
        Converter = value => (TTo?)Convert.ChangeType(value, typeof(TTo));
    }

    public CastingValidator(Func<T, TTo> converter)
    {
        Converter = converter;
    }

    public override bool Validate(T value)
    {
        try
        {
            var casted = Converter(value);
            return casted is not null;
        }
        catch
        {
            return false;
        }

    }
}
