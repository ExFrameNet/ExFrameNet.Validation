namespace ExFrameNet.Validation.Validators
{
    public class LengthValidator : AbstractValidator<string>
    {
        private readonly uint _min;
        private readonly uint _max;

        public override bool BreaksValidationIfFaild { get; }
        public override string DefaultMessage => "Lenght must be inbetween {min} and {max}";


        public LengthValidator(uint min, uint max)
        {
            _min = min;
            _max = max;
        }

        
        public override bool Validate(string value)
        {
            MessageParameters.Add("min", _min);
            MessageParameters.Add("max", _max);
            if (value is null)
                return true;

            return value.Length >= _min && value.Length <= _max;
        }
    }
}
