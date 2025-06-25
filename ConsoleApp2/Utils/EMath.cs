

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

        //Return lerp hermite between values.
        public static float Hermite(float y0, float y1, float y2, float y3, float x)
        {
            float c0 = y1;
            float c1 = 0.5f * (y2 - y0);
            float c2 = y0 - 2.5f * y1 + 2.0f * y2 - 0.5f * y3;
            float c3 = 0.5f * (y3 - y0) + 1.5f * (y1 - y2);
            return ((c3 * x + c2) * x + c1) * x + c0;
        }
        /// <summary>
        /// Return a 1 second of sinewave.
        /// </summary>
        /// <param name="freq"></param>
        /// <param name="sampleRate"></param>
        /// <returns></returns>
        public static double[] SineWave(double freq, uint sampleRate)
        {
            double step = PI2 / sampleRate * freq;
            double cur = 0;
            double[] ret = new double[sampleRate];

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
