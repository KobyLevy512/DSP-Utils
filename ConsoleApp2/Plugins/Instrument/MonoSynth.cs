

using ConsoleApp2.Midi;
using ConsoleApp2.Plugins.Fx.Dynamic;

namespace ConsoleApp2.Plugins.Instrument
{
    public delegate double[] CreateOsc(double freq, uint sampleRate);
    public class MonoSynth : PluginBase
    {
        public Envelope Envelope = new Envelope();

        double[] osc;
        int note;
        double freq;
        double oscPosition = 0;

        public void SetOsc(CreateOsc osc)
        {
            this.osc = osc(freq, GetChannel().BelongMixer.SampleRate);
        }

        public override void MidiOn(MidiData data)
        {
            if(data.Type == MidiType.Pitchbend)
            {
                freq = note + data.Velocity;
            }
            else
            {
                note = (int)(Math.Pow(2, (data.Note - 69) / 12.0) * 440.0);
                oscPosition = 0;
                Envelope.Start();
                freq = note;
            }
        }

        public override void MidiOff(MidiData data)
        {
            if (data.Type == MidiType.Pitchbend)
            {
                freq = note;
            }
            else
            {
                Envelope.End();
            }
        }

        public override void Process(ref double l, ref double r)
        {
            l = osc[(uint)oscPosition];
            r = osc[(uint)oscPosition];

            Envelope.Process(ref l, ref r);

            oscPosition += freq;
            if((uint)oscPosition >= osc.Length)
            {
                oscPosition = 0;
            }
        }
    }
}
