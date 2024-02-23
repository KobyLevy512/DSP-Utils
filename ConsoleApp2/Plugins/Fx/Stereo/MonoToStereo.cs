
using ConsoleApp2.Utils;

namespace ConsoleApp2.Plugins.Fx.Stereo
{
    public class MonoToStereo : PluginBase
    {
        double depth;
        double stereo;
        bool left = false;
        public double Depth
        {
            get => depth;
            set
            {
                depth = value;
                stereo = 1 - 1 * depth;
            }
        }
        public override void Process(ref double l, ref double r)
        {
            if (left)
            {
                r *= stereo;
            }
            else
            {
                l *= stereo;
            }
            left = !left;
        }
    }
}
