

using ConsoleApp2.Midi;
using ConsoleApp2.Plugins.Fx.Basic;
using ConsoleApp2.Plugins.Fx.Dynamic;
using ConsoleApp2.Plugins.Fx.Filter;

namespace ConsoleApp2.Plugins.Instrument
{
    public delegate double[] CreateOsc(double freq, uint sampleRate);
    public class MonoSynth : PluginBase
    {
        public Envelope Envelope = new Envelope();
        public uint Glide;
        public BiquadFilter Filter = new BiquadFilter();
        public Envelope FilterEnvelope = new Envelope();
        Fading freqGlide = new Fading();
        int curNote = 0;
        double[] osc;
        double freq;
        double oscPosition = 0;

        public void SetOsc(CreateOsc osc)
        {
            this.osc = osc(1, GetChannel().BelongMixer.SampleRate);
        }

        public override void MidiOn(MidiData data)
        {
            if (curNote != 0)
            {
                freqGlide.Start(Math.Pow(2, (curNote - 1) / 12.0), Math.Pow(2, (data.Note - 1) / 12.0), Glide);
            }
            curNote = data.Note;
            oscPosition = 0;
            Envelope.Start();
            freq = Math.Pow(2, (data.Note - 1) / 12.0);
        }

        public override void MidiOff(MidiData data)
        {
            if(curNote == data.Note)
            {
                curNote = 0;
                Envelope.End();
            }
        }

        public override void Process(ref double l, ref double r)
        {
            if(curNote != 0)
            {
                l = osc[(uint)oscPosition];
                r = osc[(uint)oscPosition];

                Envelope.Process(ref l, ref r);
                Filter.Process(ref l, ref r);
                freqGlide.Process(ref freq, ref freq);
                oscPosition += freq;
                if ((uint)oscPosition >= osc.Length)
                {
                    oscPosition = 0;
                }
            }

        }
    }
}
