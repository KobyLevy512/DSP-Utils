using ConsoleApp2.Utils;

namespace ConsoleApp2.Plugins.Fx.Dynamic
{
    public class Smoother : PluginBase
    {
        /// <summary>
        /// From when to start smooth the samples.
        /// </summary>
        public double ThreshHold = 0.1;
        /// <summary>
        /// Amount of the smoothness.
        /// </summary>
        public double Intensity = 0.5;
        double lastLeft, lastRight;
        public override void Process(ref double l, ref double r)
        {
            if (Math.Abs(lastLeft - l) >= ThreshHold)
            {
                l = EMath.Lerp(l, lastLeft, Intensity);
            }
            if (Math.Abs(lastRight - r) >= ThreshHold)
            {
                r = EMath.Lerp(r, lastRight, Intensity);
            }
            lastLeft = l;
            lastRight = r;
        }
    }
}
