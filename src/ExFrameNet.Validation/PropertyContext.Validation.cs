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


            var valCtx = new ValidationContext<T, TProperty>(ctx);
            validation(valCtx);

            var result = valCtx.Validate(options, new List<ValidationError>());
            if(typeof(T).IsAssignableTo(typeof(IValidatable)))
                IValidtableActions(ctx, result);

            return new ValidationPropertyContext<T, TProperty>(ctx, result);
        }

        public static ValidationPropertyChangedContext<T, TProperty> Validate<T, TProperty>
            (this PropertyChangedContext<T, TProperty> ctx, Action<ValidationContext<T, TProperty>> validation, ValidationOptions? options = null)
            where T : class, INotifyPropertyChanged, IValidatable
        {
            if (options is null)
                options = ValidationOptions.Default;

            var valCtx = new ValidationContext<T, TProperty>(ctx);
            validation(valCtx);
            var newCtx = new ValidationPropertyChangedContext<T, TProperty>(ctx);

            ctx.Subscribe(x =>
            {
                var result = valCtx.Validate(options, new List<ValidationError>());
                IValidtableActions(ctx,result);
                foreach (var action in newCtx.AfterValidationActions)
                {
                    action(result);
                }
            });

            return newCtx;
        }

        private static void IValidtableActions<T,TProperty>(PropertyContext<T,TProperty> ctx, ValidationResult result)
            where T : class
        {
            var instance = (IValidatable)ctx.ClassInstance;
            if (result.IsValid)
            {
                instance.Validproperties.Add(ctx.Name);
            }
            else
            {
                instance.Validproperties.Remove(ctx.Name);
            }
            instance.OnPropertyValidated(result);
        }

    }
}
