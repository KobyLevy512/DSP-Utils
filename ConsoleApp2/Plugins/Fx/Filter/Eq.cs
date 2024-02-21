using ConsoleApp2.Utils;

namespace ConsoleApp2.Plugins.Fx.Filter
{
    public class Eq : PluginBase
    {
        public List<BiquadFilter> Filters = new List<BiquadFilter>();
        public double Mix = 1;
        public override void Process(ref double l, ref double r)
        {
            double left = l;
            double right = r;
            foreach (BiquadFilter filter in Filters)
            {
                filter.Process(ref left, ref right);
            }
            l = EMath.Lerp(l, left, Mix);
            r = EMath.Lerp(r, right, Mix);
        }
    }
}
