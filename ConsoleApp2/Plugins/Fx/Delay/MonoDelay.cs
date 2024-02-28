
using ConsoleApp2.Utils;

namespace ConsoleApp2.Plugins.Fx.Delay
{
    public class MonoDelay : PluginBase
    {
        public Quantize Quantize = Quantize.Q1_8D;
        double[] buffer;

        public MonoDelay(Quantize q)
        {

        }
        public override void Process(ref double l, ref double r)
        {
            base.Process(ref l, ref r);
        }
    }
}
