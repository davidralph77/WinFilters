/* 
 Copyright <2017> <David L Ralph>

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal 
in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies 
of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS 
FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER 
IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

namespace Core.SpatialCore
{
    /// <summary>
    /// Spatial coordinates apply to drivers and the "mic" (listening position).
    /// </summary>
    public class Spatial
    {
        // Cartesian coordinates
        public double xAxis = 0.0;
        public double yAxis = 0.0;
        public double zAxis = 1.0;

        public Spatial() { }

        /// <summary>
        /// Default constructor. Sets the coordinates to (0.0, 0.0, Listening Distance)
        /// </summary>
        public Spatial(double xAxis, double yAxis, double zAxis)
        {
            this.xAxis = xAxis; // Form control
            this.yAxis = yAxis; // Form control
            this.zAxis = zAxis; // Form control
        }

        /// <summary>
        /// Sets the coordinates to the (x, y, z) passed in.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void SetSpatialValues(double x, double y, double z)
        {
            this.xAxis = x; // Form control
            this.yAxis = y; // Form control
            this.zAxis = z; // Form control
        }

    }
}
