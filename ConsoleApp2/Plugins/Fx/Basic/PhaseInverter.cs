
namespace ConsoleApp2.Plugins.Fx.Basic
{
    public class PhaseInverter : PluginBase
    {
        public override void Process(ref double l, ref double r)
        {
            l = -l;
            r = -r;
        }
    }
}
