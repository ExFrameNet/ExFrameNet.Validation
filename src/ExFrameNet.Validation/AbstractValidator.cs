using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExFrameNet.Validation
{
    public abstract class AbstractValidator<T> : IValidator<T>
    {
        public abstract bool BreaksValidationIfFaild { get; }
        public abstract string DefaultMessage { get; }
        public virtual uint DefaultErrorCode { get; } = 0;

        public abstract bool Validate(T? value);

        public bool Validate(object? value)
        {
            return Validate((T?)value);
        }
    }
}
