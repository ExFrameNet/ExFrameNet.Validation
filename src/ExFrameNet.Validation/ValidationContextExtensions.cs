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

        public static ValidationContext<T,TProperty> AddMessageParameter<T,TProperty>(this ValidationContext<T,TProperty> ctx, string key, object? value)
            where T : class
        {
            if(ctx.LastValidator is null)
                throw new InvalidOperationException("Validator must be addded first");

            ctx.LastValidator.MessageParameters.Add(key, value);

            return ctx;
        }

        public static ValidationContext<T, TProperty> PassesWhenNull<T, TProperty>(this ValidationContext<T, TProperty> ctx, bool value)
            where T : class
        {
            if (ctx.LastValidator is null)
                throw new InvalidOperationException("Validator must be addded first");

            ctx.LastValidator.PassesWhenNull = value;

            return ctx;
        }
    }
}
