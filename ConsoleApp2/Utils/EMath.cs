

namespace ConsoleApp2.Utils
{
    /// <summary>
    /// Extended math functions.
    /// </summary>
    public static class EMath
    {
        const double PI2 = 6.283185307179586476925286766559;
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


        public static double[] SineWave(double freq, double lenInSeconds, uint sampleRate)
        {
            double step = PI2 / sampleRate * freq;
            double cur = 0;
            double[] ret = new double[(int)(lenInSeconds * sampleRate)];

            for(int i = 0; i < ret.Length; i++)
            {
                ret[i] = Math.Sin(cur);
                cur += step;
                if(cur >= PI2)
                {
                    cur -= PI2;
                }
            }
            return ret;
        }
    }
}
