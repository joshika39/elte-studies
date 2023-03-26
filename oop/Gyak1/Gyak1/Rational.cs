using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gyak1
{
    internal class Rational
    {
        public class NullDividerException : ArgumentException { }
        public class asdExceeption : ArgumentException { }
        private readonly int n;
        private readonly int d;

        public Rational(int n, int d)
        {
            if (d == 0)
            {
                throw new ArgumentException($"{nameof(d)} cannot be 0.");
            }
            this.n = n;
            this.d = d;
        }

        public Rational Add(Rational a, Rational b)
        {
            return new Rational(a.n * b.d + a.d * b.n, a.d * b.d); 
        }
        public Rational Sub(Rational a, Rational b)
        {
            return new Rational(a.n * b.d - a.d * b.n, a.d * b.d);
        }
        public Rational Mul(Rational a, Rational b)
        {
            return new Rational(a.n * b.n, a.d * b.d);
        }
        public Rational Div(Rational a, Rational b)
        {
            if(b.n == 0)
            {
                throw new ArgumentException($"{nameof(b.n)} cannot be 0");
            }
            return new Rational(a.n * b.d + a.d * b.n, a.d * b.d);
        }
    }
}
