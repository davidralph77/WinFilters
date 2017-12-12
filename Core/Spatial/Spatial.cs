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
