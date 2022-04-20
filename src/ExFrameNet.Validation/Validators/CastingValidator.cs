using System.Net.Http.Headers;

namespace ExFrameNet.Validation.Validators
{
    public class CastingValidator<T, TTo> : AbstractValidator<T>
    {
        public override string DefaultMessage => $"Value can't be casted to {typeof(TTo)}";
        public override uint DefaultErrorCode => 0;
        public override bool BreaksValidationIfFaild => true;

        public override bool Validate(T? value)
        {
            try
            {
                Convert.ChangeType(value, typeof(TTo));
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
