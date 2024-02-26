namespace ConsoleApp2.Plugins.Fx.Dynamic
{
    public class Limiter : PluginBase
    {
        public double Input = 1, Output = 1;

        public override void Process(ref double l, ref double r)
        {
            l *= Input;
            r *= Input;
            if (l > Output) l = Output;
            if (r > Output) r = Output;
        }
    }
}
