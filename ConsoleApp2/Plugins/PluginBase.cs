
namespace ConsoleApp2.Plugins
{
    public delegate Channel RequestChannel();

    /// <summary>
    /// Base class for plugins.
    /// </summary>
    public class PluginBase
    {
        public event RequestChannel? OnRequestChannel;

        /// <summary>
        /// Return the channel that this plug is on.
        /// </summary>
        /// <returns></returns>
        public Channel? GetChannel()
        {
            return OnRequestChannel?.Invoke();
        }

        /// <summary>
        /// This function occured when midi note is on.
        /// </summary>
        /// <param name="data"></param>
        public virtual void MidiOn(Midi.MidiData data) { }
        /// <summary>
        /// This function occured when midi note is off.
        /// </summary>
        /// <param name="data"></param>
        public virtual void MidiOff(Midi.MidiData data) { }
        /// <summary>
        /// Here is the magic.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        public virtual void Process(ref double l, ref double r){}
    }
}
