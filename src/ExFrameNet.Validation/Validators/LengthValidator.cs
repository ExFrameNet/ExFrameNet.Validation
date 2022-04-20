﻿namespace ExFrameNet.Validation.Validators
{
    public class LengthValidator : AbstractParameterizedValidator<string, (uint min, uint max)>
    {
        public override bool BreaksValidationIfFaild { get; }
        public override string DefaultMessage => "Lenght must be inbetween {min} and {max}";

        public override bool Validate(string? value, (uint min,uint max) parameter)
        {
            MessageParameters.Add("min", parameter.min);
            MessageParameters.Add("max", parameter.max);
            if (value is null)
                return true;

            return value.Length >= parameter.min && value.Length <= parameter.max;
        }
    }
}