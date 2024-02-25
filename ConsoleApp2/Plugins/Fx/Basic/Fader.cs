
using ConsoleApp2.Utils;

namespace ConsoleApp2.Plugins.Fx.Basic
{
    public class Fader : PluginBase
    {
        double dst, src, mix, mixStep;
        public void Start(double src, double dst, uint duration)
        {
            this.src = src;
            this.dst = dst;
            mix = 0.0;
            mixStep = 1.0 / duration; 
        }

        public override void Process(ref double l, ref double r)
        {
            if(mix < 1.0)
            {
                double ret = EMath.Lerp(src, dst, mix);
                l = ret;
                r = ret;
                mix += mixStep;
            }
            else
            {
                l = dst;
                r = dst;
            }
        }
    }
}
