

namespace ConsoleApp2.Utils
{
    public static class EMath
    {
        /// <summary>
        /// Return linear interpolate between values
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="ratio"></param>
        /// <returns></returns>
        public static double Lerp(double a, double b, double ratio)
        {
            return a + (b - a) * ratio;
        }
    }
}
