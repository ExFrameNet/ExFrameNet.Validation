namespace ExFrameNet.Validation.Validators
{
    internal interface ICastingValidator : IValidator
    {
        Func<object,object> Converter { get; }
    }
}