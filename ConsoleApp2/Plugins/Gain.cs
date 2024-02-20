
namespace ConsoleApp2.Plugins
{
    public class Gain : PluginBase
    {
        public double Value = 1;

        public override void Process(ref double l, ref double r)
        {
            l *= Value;
            r *= Value;
        }
    }
}
