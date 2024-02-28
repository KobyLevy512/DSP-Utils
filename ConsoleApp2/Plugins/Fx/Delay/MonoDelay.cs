
using ConsoleApp2.Utils;

namespace ConsoleApp2.Plugins.Fx.Delay
{
    public class MonoDelay : PluginBase
    {
        public double Feedback = 0.3, Mix = 1;
        double[] buffer;
        int bufferFillIndex;
        ulong bufferOffset;
        public Quantize Quantize
        {
            set
            {
                bufferOffset = Measure.SamplesAmount(GetChannel().BelongMixer.Bpm, value, GetChannel().BelongMixer.SampleRate);
                buffer = new double[bufferOffset * 2];
                bufferFillIndex = 0;
            }
        }
        public MonoDelay(Quantize q)
        {
            bufferOffset = Measure.SamplesAmount(GetChannel().BelongMixer.Bpm, q, GetChannel().BelongMixer.SampleRate);
            buffer = new double[bufferOffset * 2];
            bufferFillIndex = 0;
        }
        public MonoDelay(double sec)
        {
            bufferOffset = Measure.SecondsToSamplesAmount(sec, GetChannel().BelongMixer.SampleRate);
            buffer = new double[bufferOffset * 2];
            bufferFillIndex = 0;
        }
        public override void Process(ref double mono)
        {
            buffer[bufferFillIndex] += mono;
            bufferFillIndex %= buffer.Length;
            ulong curIndex = ((ulong)bufferFillIndex + bufferOffset) % (ulong)buffer.Length;
            mono = EMath.Lerp(mono, buffer[curIndex] * Feedback, Mix);
        }
        public override void Process(ref double l, ref double r)
        {
            buffer[bufferFillIndex] += l;
            bufferFillIndex %= buffer.Length;
            ulong curIndex = ((ulong)bufferFillIndex + bufferOffset) % (ulong)buffer.Length;
            l = EMath.Lerp(l, buffer[curIndex] * Feedback, Mix);
            r = EMath.Lerp(r, buffer[curIndex] * Feedback, Mix);
        }
    }
}
