
using ConsoleApp2.Plugins.Instrument;
using ConsoleApp2.Utils;

namespace ConsoleApp2.Plugins.Fx.Distortion
{
    public class WaveShaper : PluginBase
    {
        public CreateOsc OscilatorFunction;
        double[] buffer;
        uint bufferPosition;
        public WaveShaper() 
        {
            OscilatorFunction = EMath.SineWave;
            buffer = OscilatorFunction(1, 44100);
        }
        public double Freq
        {
            set
            {
                bufferPosition = 0;
                buffer = OscilatorFunction(value, GetChannel().BelongMixer.SampleRate);
            }
        }
        public override void Process(ref double l, ref double r)
        {
            l *= buffer[bufferPosition];
            r *= buffer[bufferPosition];
            bufferPosition++;
            bufferPosition %= (uint)buffer.Length;
        }
    }
}
