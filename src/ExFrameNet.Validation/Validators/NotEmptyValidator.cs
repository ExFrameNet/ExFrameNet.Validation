using System.Collections;

namespace ExFrameNet.Validation.Validators
{
    public class NotEmptyValidator<T> : IValidator<T>
    {
        public string DefaultMessage => "Value can't be empty";
        public uint DefaultErrorCode { get; }

        public bool Validate(T value)
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
