

namespace ConsoleApp2.Plugins.Instrument
{
    public class MonoSynth : PluginBase
    {
        double[] osc;

        public MonoSynth()
        {
            osc = new double[GetChannel().BelongMixer.SampleRate / 20];
        }
    }
}
