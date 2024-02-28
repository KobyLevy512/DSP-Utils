
using ConsoleApp2.Utils;

namespace ConsoleApp2.Plugins.Fx.Delay
{
    public class MonoDelay : PluginBase
    {
        public double Feedback = 0.3, Mix = 1;
        Quantize quantize = Quantize.Q1_8D;
        double[] buffer;
        int bufferFillIndex;
        ulong bufferOffset;
        public Quantize Quantize
        {
            get=>quantize;
            set
            {
                quantize = value;
                bufferOffset = Measure.SamplesAmount(GetChannel().BelongMixer.Bpm, Quantize, GetChannel().BelongMixer.SampleRate);
                buffer = new double[bufferOffset * 2];
                bufferFillIndex = 0;
            }
        }
        public MonoDelay(Quantize q)
        {
            this.quantize = q;
            buffer = new double[Measure.SamplesAmount(GetChannel().BelongMixer.Bpm, Quantize, GetChannel().BelongMixer.SampleRate)];
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
