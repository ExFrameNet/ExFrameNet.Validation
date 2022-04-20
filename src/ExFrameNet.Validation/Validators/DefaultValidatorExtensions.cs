using ExFrameNet.Validation.Tests.Validators;

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

        public static ValidationContext<T,string> ShouldNotContain<T>(this ValidationContext<T,string> ctx, string substring)
            where T : class
        {
            ctx.AddValidator(new ContainsNotValidator())
                .WithParameter(substring);

            return ctx;
        }

        public static ValidationContext<T,string> Lenght<T>(this ValidationContext<T,string> ctx, uint min, uint max)
            where T : class
        {
            ctx.AddValidator(new LengthValidator())
                .WithParameter((min, max));
            return ctx;
        }

        public static ValidationContext<T,string> MinLength<T>(this ValidationContext<T,string> ctx, uint min)
            where T : class
        {
            ctx.AddValidator(new LengthValidator())
                .WithParameter((min, uint.MaxValue));
               
            return ctx;
        }

        public static ValidationContext<T, string> MaxLength<T>(this ValidationContext<T, string> ctx, uint max)
            where T : class
        {
            ctx.AddValidator(new LengthValidator())
                .WithParameter((uint.MinValue, max));

            return ctx;
        }
    }
}
