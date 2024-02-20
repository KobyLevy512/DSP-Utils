

namespace ConsoleApp2.Midi
{
    public class MidiData
    {
        /// <summary>
        /// The position and length of this midi data.
        /// </summary>
        public ulong SamplePosition, SampleLength;
        /// <summary>
        /// The note value.
        /// </summary>
        public int Note;
        /// <summary>
        /// Velocity of the note.
        /// </summary>
        public double Velocity;
        /// <summary>
        /// The type of this midi data.
        /// </summary>
        public MidiType Type = MidiType.None;
    }
}
