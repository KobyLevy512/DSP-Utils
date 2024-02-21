using ConsoleApp2.Midi;

namespace ConsoleApp2.Plugins.Instrument
{
    public class Sampler : PluginBase
    {
        /// <summary>
        /// Audio index from pool that this sampler need to play
        /// </summary>
        public int AudioIndex;


        ulong startPosition, endPosition;

        double current = 0;
        double step = -1;
        double fadeOut = 0;
        bool on = false;

        /// <summary>
        /// The start audio position offset.
        /// </summary>
        public ulong StartPosition
        {
            get => startPosition;
            set
            {
                if(startPosition >= (ulong)Pool.Audio[AudioIndex].GetLength(1))
                {
                    startPosition = (ulong)Pool.Audio[AudioIndex].GetLength(1) - 1;
                }
                else startPosition = value;
            }
        }

        /// <summary>
        /// The end audio position offset.
        /// </summary>
        public ulong EndPosition
        {
            get => endPosition;
            set
            {
                if (endPosition >= (ulong)Pool.Audio[AudioIndex].GetLength(1))
                {
                    endPosition = (ulong)Pool.Audio[AudioIndex].GetLength(1) - 1;
                }
                else endPosition = value;
            }
        }
        public override void MidiOn(MidiData data)
        {
            if(data.Type == MidiType.Pitchbend)
            {
                step += Math.Pow(2, (data.Velocity - 36) / 12.0);
            }
            else
            {
                on = true;
                step = Math.Pow(2, (data.Note - 36) / 12.0);
                current = startPosition;
            }
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
                if (current <= endPosition)
                {
                    l = Pool.Audio[AudioIndex][0, (ulong)current] * fadeOut;
                    r = Pool.Audio[AudioIndex][1, (ulong)current] * fadeOut;
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
                if (current <= endPosition)
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
