using System;
using System.Drawing;

namespace WFPFireworks
{
    internal class Particle
    {
        public static Random random = new Random();
        private static int howMany = 0;

        private PointF _location;
        private PointF _origin;
        private PointF _speed;
        private TimeSpan _lifespan;

        public Particle(PointF posOrign)
        {
            howMany++;
            _origin = posOrign;
            _speed = new Point(random.Next() * 10 - 5, random.Next() * 10 - 5);
        }

        public PointF Location { get => _location; }
        public PointF Orign { get => _origin; }



    }
}
