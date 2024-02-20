
namespace ConsoleApp2.Plugins
{
    public delegate Channel RequestChannel();
    public class PluginBase
    {
        public event RequestChannel? OnRequestChannel;
        public Channel? GetChannel()
        {
            return OnRequestChannel?.Invoke();
        }
        public virtual void MidiOn(Midi.MidiData data) { }
        public virtual void MidiOff(Midi.MidiData data) { }
        public virtual void Process(ref double l, ref double r){}
    }
}
