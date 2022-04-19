namespace ExFrameNet.Validation.Validators
{
    public static class DefaultValidatorExtensions
    {
        public static ValidationContext<T, TProperty> IsNotNull<T, TProperty>(this ValidationContext<T, TProperty> ctx)
            where T : class
        {
            ctx.AddValidator(new NotNullValidator<TProperty>());
            return ctx;
        }

        public static ValidationContext<T, TProperty> IsNotEmpty<T, TProperty>(this ValidationContext<T, TProperty> ctx)
            where T : class
        {
            ctx.AddValidator(new NotEmptyValidator<TProperty>());
            return ctx;
        }
    }
}
