
using ConsoleApp2.Utils;

namespace ConsoleApp2.Plugins.Fx.Delay
{
    public class MonoDelay : PluginBase
    {
        public double Feedback = 0.3, Mix = 1;
        protected double[] buffer;
        protected int bufferFillIndex;
        protected ulong bufferOffset;
        public Quantize Quantize
        {
            set
            {
                bufferOffset = Measure.SamplesAmount(GetChannel().BelongMixer.Bpm, value, GetChannel().BelongMixer.SampleRate);
                buffer = new double[bufferOffset * 2];
                bufferFillIndex = 0;
            }
        }

        public override void Process(ref double mono)
        {
            ulong curIndex = ((ulong)bufferFillIndex + bufferOffset) % (ulong)buffer.Length;
            double result = mono + buffer[curIndex] * Feedback;
            buffer[bufferFillIndex++] = result;
            bufferFillIndex %= buffer.Length;
            mono += result * Mix;
        }
        public override void Process(ref double l, ref double r)
        {
            ulong curIndex = ((ulong)bufferFillIndex + bufferOffset) % (ulong)buffer.Length;
            double result = l + buffer[curIndex] * Feedback;
            buffer[bufferFillIndex++] = result;
            bufferFillIndex %= buffer.Length;
            l += result * Mix;
            r += result * Mix;
        }
    }
}
