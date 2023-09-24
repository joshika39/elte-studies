using System;

namespace ComplexNumbers
{
    public class Complex
    {
        private readonly double _x;
        private readonly double _y;
        public Complex(double x, double y)
        {
            _x = x;
            _y = y;
        }

        public static Complex operator +(Complex a, Complex b)
        {
            return new Complex(a._x + b._x, a._y + b._y);
        }
        public static Complex operator -(Complex a, Complex b)
        {
            return new Complex(a._x - b._x, a._y - b._y);
        }
        public static Complex operator *(Complex a, Complex b)
        {
            return new Complex(a._x * b._x - a._y * b._y, a._x * b._x + a._y * b._y);
        }
        public static Complex operator /(Complex a, Complex b)
        {
            if (b._x == 0 && b._y == 0)
            {
                throw new ArgumentException($"{nameof(b._x)} and {nameof(b._y)} cannot be null");
            }
            return new Complex(
                (a._x * b._x + a._y * b._y) / (Math.Pow(b._x, 2) + Math.Pow(b._y, 2)), 
                (a._y * b._x - a._x * b._y) / (Math.Pow(b._x, 2) + Math.Pow(b._y, 2)));
        }

        public override string ToString()
        {
            return $"({_x}, {_y})";
        }
    }
}
