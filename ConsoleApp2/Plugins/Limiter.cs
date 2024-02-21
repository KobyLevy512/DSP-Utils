

namespace ConsoleApp2.Plugins
{
    public class Limiter : PluginBase
    {
        public double Input = 1, Output = 1;

        public override void Process(ref double l, ref double r)
        {
            l *= Input;
            r *= Output;
            if(l > Output) l = Output;
            if(r > Output) r = Output;
        }
    }
}
