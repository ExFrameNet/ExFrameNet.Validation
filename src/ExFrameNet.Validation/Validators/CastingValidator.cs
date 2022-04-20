using System.Net.Http.Headers;

namespace ExFrameNet.Validation.Validators
{
    public class CastingValidator<T, TTo> : AbstractValidator<T>, ICastingValidator
    {
        public Func<T, TTo> Converter { get; private set; }
        public override string DefaultMessage => $"Value can't be casted to {typeof(TTo)}";
        public override uint DefaultErrorCode => 0;
        public override bool BreaksValidationIfFaild => true;

        Func<object, object> ICastingValidator.Converter => (value) => Converter((T)value);

        public CastingValidator()
        {
            Converter = value => (TTo)Convert.ChangeType(value, typeof(TTo));
        }

        public CastingValidator(Func<T,TTo> converter)
        {
            Converter = converter;
        }

        public override bool Validate(T value)
        {
            try
            {
                Converter(value);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
