using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExFrameNet.Validation.Validators
{
    public class CastingFunctionValidator<TFrom, TTo> : AbstractParameterizedValidator<TFrom, Func<TFrom?, TTo>>
    {
        public override bool BreaksValidationIfFaild => true;
        public override string DefaultMessage => $"Can't cast to {typeof(TTo)}";

        public override bool Validate(TFrom? value, Func<TFrom?, TTo> parameter)
        {
            try
            {
                parameter(value);
            }
            catch 
            {
                return false;
            }
            return true;
        }
    }
}
