// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace LoggingMadeEasy;

public static class MiscExtenders
{
    public static void Do<T>(
        this T value, Func<T, bool> canDo, Action<T> action)
    {
        if (canDo(value))
            action(value);
    }

    public static bool IsEnumValue<T>(this T value)
        where T : struct, Enum
    {
        return Enum.IsDefined(value);
    }

    public static void Do<T>(
        this T value, Action<T> action) => action(value);

    public static R Convert<T, R>(
        this T value, Func<T, R> getValue) => getValue(value);

    public static bool IsDefaultValue<T>(this T value) =>
        Equals(value, default(T));

}