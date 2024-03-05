

using ConsoleApp2.Utils;

namespace ConsoleApp2.Plugins.Fx.Distortion
{
    public class Noiser : PluginBase
    {
        public double Mix;
        Random rand;
        public Noiser()
        {
            rand = new Random();
        }
        public override void Process(ref double l, ref double r)
        {
            double rnd = rand.NextDouble() * Mix + (1-Mix);
            l *= rnd;
            r *= rnd;
        }
    }
}
