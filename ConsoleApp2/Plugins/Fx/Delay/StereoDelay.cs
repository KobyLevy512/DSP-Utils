

namespace ConsoleApp2.Plugins.Fx.Delay
{
    public class StereoDelay : PluginBase
    {
        public MonoDelay Left = new MonoDelay(), Right = new MonoDelay();

        
        public override void Process(ref double l, ref double r)
        {
            Left.Process(ref l);
            Right.Process(ref r);
        }
    }
}
