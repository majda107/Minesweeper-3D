using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper3D
{
    class VectorXYZ
    {
        public double x { get; set; }
        public double y { get; set; }
        public double z { get; set; }
        public double theta { get; set; }
        public VectorXYZ(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public VectorXYZ(double x, double y, double z, double theta)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.theta = theta;
        }
    }
}
