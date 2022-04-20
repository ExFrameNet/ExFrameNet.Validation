using System.Collections;

namespace ExFrameNet.Validation.Validators
{
    public class NotEmptyValidator<T> : AbstractValidator<T>
    {
        public override string DefaultMessage => "{propertyName} can't be empty";
        public override uint DefaultErrorCode { get; }
        public override bool BreaksValidationIfFaild => false;

        public override bool Validate(T? value)
        {
            switch (value)
            {
                case null:
                case string s when string.IsNullOrWhiteSpace(s):
                case ICollection { Count: 0 }:
                case Array { Length: 0 }:
                case IEnumerable e when !e.GetEnumerator().MoveNext():
                    return false;
            }
            return true;
        }
    }
}
