
using ConsoleApp2.Utils;

namespace ConsoleApp2.Plugins.Fx.Stereo
{
    public class StereoEnhancer : PluginBase
    {
        const double PI2 = Math.PI * 2;
        public double Depth = 0, Color = 0.1;
        double curValue = 0;
        public override void Process(ref double l, ref double r)
        {
            double left = l;
            double right = r;

            left *= Math.Sin(curValue);
            right *= Math.Cos(curValue);

            l = EMath.Lerp(l, left, Depth);
            r = EMath.Lerp(r, right, Depth);

            curValue += Color;
            if(curValue > PI2)
            {
                curValue = 0;
            }
        }
    }
}
