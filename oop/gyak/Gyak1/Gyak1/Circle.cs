using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gyak1
{
    internal class Circle
    {
        private readonly Point _center;
        private readonly double _radius;

        public class WrongRadiusException : ArgumentException
        {
            public WrongRadiusException(string msg)
            {
                
            }
        }

        public Circle(Point center, double radius)
        {
            _center = center;
            if (radius < 0)
            {
                throw new WrongRadiusException($"{nameof(radius)} cannot be smaller then 0");
            }
            _radius = radius;
        }

        public bool Contains(Point p)
        {
            return _center.Distance(p) <= _radius;
        }
    }
}
