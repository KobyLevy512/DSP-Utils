

namespace ConsoleApp2.Midi
{
    public class MidiData
    {
        public ulong SamplePosition, SampleLength;
        public int Note;
        public double Velocity;
        public MidiType Type = MidiType.None;
    }
}
