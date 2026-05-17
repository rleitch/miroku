namespace Miroku.Core.Extensions;

public static class ValidationExtensions
{
    /// <summary>
    /// Throws an ArgumentNullException if the given value is null.
    /// </summary>
    public static T ThrowIfNull<T>(this T? value, string? parameterName = null)
    {
        if (value != null)
        {
            return value;
        }

        // This is much better: it tells the user *what* was null.
        if (parameterName != null)
        {
            throw new ArgumentNullException(parameterName, $"{parameterName} cannot be null.");
        }
        else
        {
            throw new ArgumentNullException(nameof(value), "Value cannot be null.");
        }
    }
}