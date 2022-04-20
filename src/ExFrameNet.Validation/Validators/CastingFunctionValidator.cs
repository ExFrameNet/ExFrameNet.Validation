namespace ExFrameNet.Validation.Validators
{
    public class CastingFunctionValidator<TFrom, TTo> : AbstractValidator<TFrom>
    {

        private readonly Func<TFrom, TTo> _converter;

        public CastingFunctionValidator(Func<TFrom, TTo> converter)
        {
            _converter = converter;
        }

        public override bool BreaksValidationIfFaild => true;
        public override string DefaultMessage => $"Can't cast to {typeof(TTo)}";




        public override bool Validate(TFrom value)
        {
            try
            {
                _converter(value);
            }
            catch 
            {
                return false;
            }
            return true;
        }
    }
}
