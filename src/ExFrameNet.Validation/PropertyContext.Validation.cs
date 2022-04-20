using ExFrame.Extensions.Property;
using ExFrameNet.Utils.Property;
using System.ComponentModel;

namespace ExFrameNet.Validation
{
    public static class PropertyContextExtensions
    {

        public static ValidationPropertyContext<T, TProperty> Validate<T, TProperty>
            (this PropertyContext<T, TProperty> ctx, Action<ValidationContext<T, TProperty>> validation, ValidationOptions? options = null)
            where T : class
        {
            if (options is null)
                options = ValidationOptions.Default;


            var valCtx = new ValidationContext<T, TProperty>(ctx, options);
            validation(valCtx);

            return new ValidationPropertyContext<T, TProperty>(ctx, valCtx);
        }

        public static ValidationPropertyChangedContext<T, TProperty> Validate<T, TProperty>
            (this PropertyChangedContext<T, TProperty> ctx, Action<ValidationContext<T, TProperty>> validation, ValidationOptions? options = null)
            where T : class, INotifyPropertyChanged
        {
            if (options is null)
                options = ValidationOptions.Default;

            var valCtx = new ValidationContext<T, TProperty>(ctx, options);
            validation(valCtx);
            var newCtx = new ValidationPropertyChangedContext<T, TProperty>(ctx);

            ctx.Subscribe(x =>
            {
                var result = valCtx.Validate();
                if(typeof(T).IsAssignableTo(typeof(IValidatable)))
                    IValidtableActions(valCtx, result);
                foreach (var action in newCtx.AfterValidationActions)
                {
                    action(result);
                }
            });

            return newCtx;
        }

        private static void IValidtableActions<T,TProperty>(ValidationContext<T,TProperty> ctx, ValidationResult? result)
            where T : class
        {
            if (result is null)
                result = ctx.Validate();

            var instance = (IValidatable)ctx.Property.ClassInstance;
            if (result.IsValid)
            {
                instance.Validproperties.Add(ctx.Property.Name);
            }
            else
            {
                instance.Validproperties.Remove(ctx.Property.Name);
            }
            instance.OnPropertyValidated(result);
            instance.ValidationContexts.Add(ctx);
        }

    }
}
