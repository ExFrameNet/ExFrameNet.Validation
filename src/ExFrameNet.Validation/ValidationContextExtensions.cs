namespace ExFrameNet.Validation
{
    public static class ValidationContextExtensions
    {
        public static ValidationContext<T,TProperty> AddValidator<T,TProperty>(this ValidationContext<T,TProperty> ctx, IValidator<TProperty> validator)
            where T : class
        {
            ctx.Validators.Add(validator);
            ctx.ValidatorAttachments[validator] = new ValidatorAttachments(validator.DefaultMessage, validator.DefaultErrorCode)
            {
                Message = validator.DefaultMessage,
                ErrorCode = validator.DefaultErrorCode
            };
            return ctx;
        }

        public static ValidationContext<T,TProperty> WithMessage<T,TProperty>(this ValidationContext<T,TProperty> ctx, string message)
            where T : class
        {
            if (ctx.LastValidator is null)
                throw new InvalidOperationException("Add Validator first");

            ctx.ValidatorAttachments[ctx.LastValidator].Message = message;
            return ctx;
        }

        public static ValidationContext<T,TProperty> WithParameter<T,TProperty,TParameter>(this ValidationContext<T,TProperty>ctx , TParameter parameter)
            where T : class
        {
            if(parameter is null)
                throw new ArgumentNullException(nameof(parameter));

            if (ctx.LastValidator is null)
                throw new InvalidOperationException("Validator must be addded first");

            if (ctx.LastValidator is not IParameterizedValidator<TProperty, TParameter> validator)
                throw new InvalidOperationException("Validator didn't except a parameter or this type of Parameter");

            ctx.ValidatorParameters[validator] = parameter;

            return ctx;
        }

        public static ValidationContext<T,TProperty> AddMessageParameter<T,TProperty>(this ValidationContext<T,TProperty> ctx, string key, object? value)
            where T : class
        {
            if(ctx.LastValidator is null)
                throw new InvalidOperationException("Validator must be addded first");

            ctx.LastValidator.MessageParameters.Add(key, value);

            return ctx;
        }
    }
}
