namespace ExFrameNet.Validation.Tests.Validators
{
    public class ContainsNotValidator : AbstractValidator<string>
    {
        private readonly string _substring;

        public override bool BreaksValidationIfFaild => false;
        public override string DefaultMessage => "Value shouldn't contain '{ parameter }'";
        
        public ContainsNotValidator(string substring)
        {
            _substring = substring;
        }


        public override bool Validate(string value)
        {
            if (value is null)
                return true;
            return !value.Contains(_substring);
        }
    }
}
