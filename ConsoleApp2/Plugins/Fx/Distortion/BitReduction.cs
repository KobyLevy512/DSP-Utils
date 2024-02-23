
namespace ConsoleApp2.Plugins.Fx.Distortion
{
    public class BitReduction : PluginBase
    {
        public double Mix;
        ushort depth;
        public ushort Depth
        {
            get => depth;
            set
            {
                depth = (ushort)(ushort.MaxValue - ushort.MaxValue * value);
            }
        }
        public override void Process(ref double l, ref double r)
        {
            if(GetChannel().BelongMixer.SamplePosition % depth == 0)
            {
                l = l - l * Mix;
                r = r - r * Mix;
            }
        }
    }
}
