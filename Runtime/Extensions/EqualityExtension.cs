using System;

namespace Lab5Games
{
    public static class EqualityExtension
    {
        public static bool IsNullable(this object obj)
        {
            return Nullable.GetUnderlyingType(obj.GetType()) != null;
        }

        public static bool IsNull(this object obj)
        {
            if (!obj.IsNullable())
                return false;

            return ReferenceEquals(obj, null);
        }

        public static bool SafeEquals(this object a, object b)
        {
            if (a.IsNull() && b.IsNull())
                return true;

            return ReferenceEquals(a, b);
        }

        public static bool FloatEquals(this float a, float b, float epsilon = float.Epsilon)
        {
            float absA = Math.Abs(a);
            float absB = Math.Abs(b);
            float diff = Math.Abs(a - b);

            if(a == b)
            {
                return true;
            }
            else if(a == 0 || b == 0 || (absA + absB) < float.MinValue)
            {
                return diff < (epsilon * float.MinValue);
            }
            else
            {
                return diff / (absA + absB) < epsilon;
            }
        }

        public static bool DoubleEquals(this double a, double b, double epsilon = float.Epsilon)
        {
            const double MinNormal = 2.2250738585072014E-308d;
            double absA = Math.Abs(a);
            double absB = Math.Abs(b);
            double diff = Math.Abs(a - b);

            if (a.Equals(b))
            { // shortcut, handles infinities
                return true;
            }
            else if (a == 0 || b == 0 || absA + absB < MinNormal)
            {
                // a or b is zero or both are extremely close to it
                // relative error is less meaningful here
                return diff < (epsilon * MinNormal);
            }
            else
            { // use relative error
                return diff / (absA + absB) < epsilon;
            }
        }
    }
}