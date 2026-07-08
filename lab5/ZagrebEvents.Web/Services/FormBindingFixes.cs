using System.Globalization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace ZagrebEvents.Web.Services
{
    // ============================================================
    // Popravci model bindinga za forme (globalno):
    //
    // 1. InvariantNumberModelBinder — hr kultura koristi zarez kao decimalni
    //    separator pa je "45.8" iz <input type=number> postajalo 458!
    //    Ovaj binder prihvaća i točku i zarez (normalizira pa parsira invariant).
    //
    // 2. EmptyStringMetadataProvider — prazan string iz forme MVC pretvara u null,
    //    a non-nullable string svojstva onda padnu na implicitnom [Required]
    //    (npr. prazan PosterUrl je rušio kreiranje eventa). Ovim "" ostaje "".
    // ============================================================

    public class InvariantNumberModelBinder : IModelBinder
    {
        private readonly Type _type;
        public InvariantNumberModelBinder(Type type) => _type = type;

        public Task BindModelAsync(ModelBindingContext ctx)
        {
            var result = ctx.ValueProvider.GetValue(ctx.ModelName);
            if (result == ValueProviderResult.None) return Task.CompletedTask;

            ctx.ModelState.SetModelValue(ctx.ModelName, result);
            var raw = result.FirstValue;

            if (string.IsNullOrWhiteSpace(raw))
            {
                // nullable tipovi smiju biti prazni
                if (Nullable.GetUnderlyingType(_type) != null)
                    ctx.Result = ModelBindingResult.Success(null);
                return Task.CompletedTask;
            }

            // prihvati "45.8" i "45,8"
            raw = raw.Trim().Replace(',', '.');
            var target = Nullable.GetUnderlyingType(_type) ?? _type;

            object? value = null;
            bool ok = false;
            if (target == typeof(decimal)) { ok = decimal.TryParse(raw, NumberStyles.Number, CultureInfo.InvariantCulture, out var d); value = d; }
            else if (target == typeof(double)) { ok = double.TryParse(raw, NumberStyles.Number | NumberStyles.Float, CultureInfo.InvariantCulture, out var d); value = d; }
            else if (target == typeof(float)) { ok = float.TryParse(raw, NumberStyles.Number | NumberStyles.Float, CultureInfo.InvariantCulture, out var f); value = f; }

            if (ok)
                ctx.Result = ModelBindingResult.Success(value);
            else
                ctx.ModelState.TryAddModelError(ctx.ModelName, $"Vrijednost '{result.FirstValue}' nije valjan broj.");

            return Task.CompletedTask;
        }
    }

    public class InvariantNumberModelBinderProvider : IModelBinderProvider
    {
        private static readonly Type[] Types =
        {
            typeof(decimal), typeof(decimal?),
            typeof(double),  typeof(double?),
            typeof(float),   typeof(float?)
        };

        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            return Types.Contains(context.Metadata.ModelType)
                ? new InvariantNumberModelBinder(context.Metadata.ModelType)
                : null;
        }
    }

    public class EmptyStringMetadataProvider : IDisplayMetadataProvider
    {
        public void CreateDisplayMetadata(DisplayMetadataProviderContext context)
        {
            if (context.Key.MetadataKind == ModelMetadataKind.Property &&
                context.Key.ModelType == typeof(string))
            {
                context.DisplayMetadata.ConvertEmptyStringToNull = false;
            }
        }
    }
}
