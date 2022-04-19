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

        public static ValidationContext<T,TCasted> CastTo<T,TProperty,TCasted>(this ValidationContext<T,TProperty> ctx)
            where T : class
        {
            ctx.AddValidator(new CastingValidator<TProperty, TCasted>());
            TCasted? transform(T c) => (TCasted?)Convert.ChangeType(ctx.PropertyReader(c), typeof(TCasted?));

            return new ValidationContext<T, TCasted>(ctx.Validators, transform , ctx.ClassInstance);
        }

        public static ValidationContext<T, TCasted> Cast<T, TProperty, TCasted>(this ValidationContext<T, TProperty> ctx, Func<TProperty?, TCasted> cast)
            where T : class
        {
            ctx.AddValidator(new CastingFunctionValidator<TProperty, TCasted>())
                .WithParameter(cast);
            TCasted? transform(T c) => cast(ctx.PropertyReader(c));

            return new ValidationContext<T, TCasted>(ctx.Validators, transform, ctx.ClassInstance);
        }
    }
}
