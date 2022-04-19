namespace ExFrameNet.Validation
{
    public static class ValidationContextExtensions
    {
        public static ValidationContext<T,TProperty> AddValidator<T,TProperty>(this ValidationContext<T,TProperty> ctx, IValidator<TProperty> validator)
            where T : class
        {
            ctx.AddValidator(validator);
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
    }
}
