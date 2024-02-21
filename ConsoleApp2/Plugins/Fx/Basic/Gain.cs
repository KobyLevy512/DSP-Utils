namespace ConsoleApp2.Plugins.Fx.Basic
{
    public class Gain : PluginBase
    {
        /// <summary>
        /// The gain value in linear.
        /// </summary>
        public double Value = 1;

        public override void Process(ref double l, ref double r)
        {
            l *= Value;
            r *= Value;
        }
    }
}
