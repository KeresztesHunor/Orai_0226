namespace Orai_0226
{
    internal static class ExtensionMethods
    {
        public static void ForEach<T>(this IEnumerable<T> values, Action<T, int> action)
        {
            int i = 0;
            foreach (T value in values)
            {
                action(value, i++);
            }
        }

        public static void ForEach<T>(this IEnumerable<T> values, Action<T> action)
        {
            foreach (T value in values)
            {
                action(value);
            }
        }
    }
}
