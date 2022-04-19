using ExFrame.Extensions.Property;
using ExFrameNet.Utils.Property;
using System.ComponentModel;

namespace ExFrameNet.Validation
{
    public static class PropertyContextExtensions
    {

        public static ValidationPropertyContext<T, TProperty> Validate<T, TProperty>
            (this PropertyContext<T, TProperty> ctx, Action<ValidationContext<T, TProperty>> validation, ValidationOptions? options = null)
            where T : class, IValidatable
        {
            if (options is null)
                options = ValidationOptions.Default;


            var valCtx = new ValidationContext<T, TProperty>(ctx.PropertyReader, ctx.ClassInstance);
            validation(valCtx);
            var result = valCtx.Validate(options, ctx.Name);
            IValiudatbaleActions(ctx, result);

            return new ValidationPropertyContext<T, TProperty>(ctx, result);
        }

        public static ValidationPropertyChangedContext<T, TProperty> Validate<T, TProperty>
            (this PropertyChangedContext<T, TProperty> ctx, Action<ValidationContext<T, TProperty>> validation, ValidationOptions? options = null)
            where T : class, INotifyPropertyChanged, IValidatable
        {
            if (options is null)
                options = ValidationOptions.Default;

            var valCtx = new ValidationContext<T, TProperty>(ctx.PropertyReader, ctx.ClassInstance);
            validation(valCtx);
            var newCtx = new ValidationPropertyChangedContext<T, TProperty>(ctx);

            ctx.Subscribe(x =>
            {
                var result = valCtx.Validate(options, ctx.Name);
                IValiudatbaleActions(ctx,result);
                foreach (var action in newCtx.AfterValidationActions)
                {
                    action(result);
                }
            });

            return newCtx;
        }

        private static void IValiudatbaleActions<T,TProperty>(PropertyContext<T,TProperty> ctx, ValidationResult result)
            where T :class, IValidatable
        {
            if (result.IsValid)
            {
                ctx.ClassInstance.Validproperties.Add(ctx.Name);
            }
            else
            {
                ctx.ClassInstance.Validproperties.Remove(ctx.Name);
            }
            ctx.ClassInstance.OnPropertyValidated(result);
        }

    }
}
