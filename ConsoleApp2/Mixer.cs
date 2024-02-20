using ConsoleApp2.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class Mixer
    {
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
            };

            Channels.OnItemsAdded += (IEnumerable<Channel> ch) =>
            {
                foreach(Channel c in ch)
                {
                    c.OnRequestChannels += (ChannelType type, ChannelSubType sub) =>
                    {
                        return Channels.Where(c => c.ChannelType == type && c.ChannelSubType == sub).ToList();
                    };
                }
            };
        }
        public double[,] ProcessBuffer()
        {
            double[,] ret = new double[2, BufferSize];
            for(int i = 0; i < BufferSize; i++)
            {
                double l = 0;
                double r = 0;
                foreach(Channel c in Channels)
                {
                    c.Process(ref l, ref r, SamplePosition);
                    ret[0, i] += l;
                    ret[1, i] += r;
                }
                SamplePosition++;
            }
            return ret;
        }
    }
}
