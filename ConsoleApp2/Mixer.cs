using ConsoleApp2.Utils;

namespace ConsoleApp2
{
    /// <summary>
    /// Base class for manipulating and mix between multy channels.
    /// </summary>
    public class Mixer
    {
        /// <summary>
        /// All the channels in this mixer.
        /// </summary>
        public EventList<Channel> Channels = new EventList<Channel>();

        public ushort BufferSize = 2048;
        public uint SampleRate = 44100;
        public byte Bpm = 145;
        public ulong SamplePosition = 0;

        public Mixer()
        {
            Channels.OnItemAdded += (Channel c) =>
            {
                c.OnRequestChannels += (ChannelType type, ChannelSubType sub) =>
                {
                    return Channels.Where(c => (c.ChannelType & type) != 0 && (c.ChannelSubType & sub) != 0).ToList();
                };
                c.BelongMixer = this;
            };

            Channels.OnItemsAdded += (IEnumerable<Channel> ch) =>
            {
                foreach(Channel c in ch)
                {
                    c.OnRequestChannels += (ChannelType type, ChannelSubType sub) =>
                    {
                        return Channels.Where(c => c.ChannelType == type && c.ChannelSubType == sub).ToList();
                    };
                    c.BelongMixer = this;
                }
            };
        }
        
        /// <summary>
        /// Process the buffer from the current sample position and return the result.
        /// </summary>
        /// <returns></returns>
        public double[] ProcessBuffer()
        {
            double[] ret = new double[BufferSize * 2];
            for(int i = 0; i < ret.Length; i++)
            {
                double l = 0;
                double r = 0;
                foreach(Channel c in Channels)
                {
                    c.Process(ref l, ref r, SamplePosition);
                    ret[i] += l;
                    ret[i + 1] += r;
                }
                SamplePosition++;
            }
            return ret;
        }
    }
}
