namespace ExFrameNet.Validation.Tests.Validators
{
    public class ContainsNotValidator : AbstractParameterizedValidator<string, string>
    {
        public override bool BreaksValidationIfFaild => false;
        public override string DefaultMessage => "Value shouldn't contain '{ parameter }'";

        public override bool Validate(string? value, string parameter)
        {
            if (value is null)
                return true;
            return !value.Contains(parameter);
        }
    }
}
