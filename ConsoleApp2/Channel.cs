
using ConsoleApp2.Midi;
using ConsoleApp2.Plugins;
using ConsoleApp2.Utils;

namespace ConsoleApp2
{
    public enum ChannelType
    {
        Drum = 1,
        Bass = 2,
        Synth = 4,
        Instrument = 8
    }
    public enum ChannelSubType : ulong
    {
        Kick = 1u,
        Snare = 2u,
        Clap = 4u,
        ClosedHat = 8,
        OpenHat = 16,
        Shaker = 32,
        Percussion = 64,
        Crash = 128,
        OrchestraDrum = 256,
        SubBass = 512,
        Bass = 1024,
        TopBass = 2048,
        PadBass = 4096,
        FmLead = 8192,
        TbLead = 16384,
        AcidLead = 16384 << 1,
        SuperSawLead = 16384u << 2,
        PluckLead = 16384u << 3,
        TantraLead = 16384u << 4,
        FormantLead = 16384u << 5,
        ChordLead = 16384u << 6,
        ArpLead = 16384u << 7,
        BubbleFx = 16384u << 8,
        SquelchFx = 16384u << 9,
        RiserFx = 16384u << 10,
        ZapFx = 16384u << 11,
        BiteFx = 16384u << 12,
        LaserFx = 16384u << 13,
        NoiseFx = 16384u << 14,
        VocalFx = 16384u << 15,
        Pad = 16384u << 16,
        Soundscape = 2147483648u,
        Impact = 4294967296u,
        Uplift = 4294967296u << 1,
        Downlift = 4294967296u << 2,
        Piano = 4294967296u << 3,
        AccGuitar = 4294967296u << 4,
        ElectricGuitar = 4294967296u << 5,
        String = 4294967296u << 6,
        Flute = 4294967296u << 7,
        Vocal = 4294967296u << 8,
    }
    public delegate List<Channel> RequestChannels(ChannelType type, ChannelSubType sub);
    public class Channel
    {
        public List<MidiData> MidiData = new List<MidiData>();
        public EventList<PluginBase> Plugins = new EventList<PluginBase>();
        public ChannelType ChannelType;
        public ChannelSubType ChannelSubType;
        public Mixer? BelongMixer;
        public event RequestChannels? OnRequestChannels;
        public Channel()
        {
            Plugins.OnItemAdded += (PluginBase item) =>
            {
                item.OnRequestChannel += () => this;
            };
            Plugins.OnItemsAdded += (IEnumerable<PluginBase> items) =>
            {
                foreach(var item in items)
                {
                    item.OnRequestChannel += () => this;
                }
            };
        }

        /// <summary>
        /// Return a channels from the same mixer that this channel is belong.
        /// Based on type and sub type.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sub"></param>
        /// <returns></returns>
        public List<Channel>? GetChannels(ChannelType type, ChannelSubType sub)
        {
            return OnRequestChannels?.Invoke(type, sub);
        }
        public void Process(ref double l, ref double r, ulong samplePosition)
        {
            foreach(MidiData data in MidiData)
            {
                if(data.SamplePosition == samplePosition)
                {
                    if (data.Type == MidiType.None)
                    {
                        data.Type = MidiType.NoteOn;
                    }
                    foreach(PluginBase plugin in Plugins)
                    {
                        plugin.MidiOn(data);
                    }
                }
                else if(data.Type == MidiType.NoteOn && data.SamplePosition + data.SampleLength - samplePosition <= 0)
                {
                    data.Type = MidiType.NoteOff;
                    foreach (PluginBase plugin in Plugins)
                    {
                        plugin.MidiOff(data);
                    }
                    data.Type = MidiType.None;
                }
            }
            foreach(PluginBase plug in Plugins)
            {
                if(!plug.Bypass)
                    plug.Process(ref l, ref r);
            }
        }
    }
}
