using ExFrameNet.Validation.Tests.Validators;

namespace ExFrameNet.Validation.Validators;

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

    public static ValidationContext<T, TProperty> CanBeCastedTo<T, TProperty, TCasted>(this ValidationContext<T, TProperty> ctx)
        where T : class
    {
        return ctx.AddValidator(new CastingValidator<TProperty, TCasted>());
    }

    public static ValidationContext<T, TProperty> CanBeCastedBy<T, TProperty, TCasted>(this ValidationContext<T, TProperty> ctx, Func<TProperty, TCasted> cast)
        where T : class
    {
        return ctx.AddValidator(new CastingValidator<TProperty, TCasted>(cast));
    }

    public static ValidationContext<T, string> ShouldNotContain<T>(this ValidationContext<T, string> ctx, string substring)
        where T : class
    {
        ctx.AddValidator(new ContainsNotValidator(substring));

        return ctx;
    }

    public static ValidationContext<T, string> Lenght<T>(this ValidationContext<T, string> ctx, uint min, uint max)
        where T : class
    {
        ctx.AddValidator(new LengthValidator(min, max));
        return ctx;
    }

    public static ValidationContext<T, string> MinLength<T>(this ValidationContext<T, string> ctx, uint min)
        where T : class
    {
        ctx.AddValidator(new LengthValidator(min, uint.MaxValue));

        return ctx;
    }

    public static ValidationContext<T, string> MaxLength<T>(this ValidationContext<T, string> ctx, uint max)
        where T : class
    {
        ctx.AddValidator(new LengthValidator(uint.MinValue, max));

        return ctx;
    }

    public static ValidationContext<T, TProperty> IsGreaterThen<T, TProperty>(this ValidationContext<T, TProperty> ctx, TProperty value)
        where T : class
        where TProperty : IComparable<TProperty>
    {
        ctx.AddValidator(new GreaterThenValidator<TProperty>(value));
        return ctx;
    }
}
