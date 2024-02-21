namespace ConsoleApp2.Plugins.Fx.Distortaion
{
    public class Bitcrush : PluginBase
    {
        public double Divider;
        double curDivide;
        byte bitDepth = 64;
        double maxValue = Math.Pow(2, 64) - 1;
        public byte BitDepth
        {
            get => bitDepth;
            set
            {
                bitDepth = value;
                maxValue = Math.Pow(2, value) - 1;
            }
        }
        public override void Process(ref double l, ref double r)
        {
            if ((ulong)curDivide % GetChannel().BelongMixer.SamplePosition == 0)
            {
                l = Math.Round(l * maxValue) / maxValue;
                r = Math.Round(r * maxValue) / maxValue;
            }
            curDivide += Divider;
        }
    }
}
