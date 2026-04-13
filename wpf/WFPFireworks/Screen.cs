using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WFPFireworks
{
    internal class Screen : PictureBox
    {
        List<Particle> _particles;
        Timer _timer;
        PointF _mouseLocation;

        public Screen()
        {
            _particles = new List<Particle>();
            _timer = new Timer();
            _timer.Tick += MaJ;
            _timer.Start();
            

        }

        public static void MaJ(Object timer, EventArgs eventTickArgs)
        {

        }


    }
}
