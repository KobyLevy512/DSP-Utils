using ConsoleApp2.Midi;

namespace ConsoleApp2.Plugins.Instrument
{
    public class Sampler : PluginBase
    {
        /// <summary>
        /// Audio index from pool that this sampler need to play
        /// </summary>
        public int AudioIndex;

        /// <summary>
        /// The audio position offset.
        /// </summary>
        public ulong StartPosition, EndPosition;

        double current = 0;
        double step = -1;
        double fadeOut = 0;
        bool on = false;
        public override void MidiOn(MidiData data)
        {
            on = true;
            step = Math.Pow(2, (data.Note - 36) / 12.0);
            current = StartPosition;
        }

        public override void MidiOff(MidiData data)
        {
            on = false;
            fadeOut = 1;
        }

        public override void Process(ref double l, ref double r)
        {
            if (fadeOut > 0)
            {
                if (current <= EndPosition)
                {
                    l = Pool.Audio[AudioIndex][0, (ulong)current] * fadeOut;
                    r = Pool.Audio[AudioIndex][0, (ulong)current] * fadeOut;
                    current += step;
                    fadeOut -= 0.01;
                }
                else
                {
                    fadeOut = 0;
                }
            }
            else if (on)
            {
                if (current <= EndPosition)
                {
                    l = Pool.Audio[AudioIndex][0, (ulong)current];
                    r = Pool.Audio[AudioIndex][1, (ulong)current];
                    current += step;
                }
                else
                {
                    on = false;
                }
            }
        }
    }
}
