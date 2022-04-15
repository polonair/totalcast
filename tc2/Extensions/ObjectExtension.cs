namespace tc2
{
    static class ObjectExtension
    {
        public static bool Is<T>(this object o) => typeof(T).IsAssignableFrom(o.GetType());
        public static T As<T>(this object o) => (T)o;
    }
}
