
namespace ConsoleApp2.Utils
{
    /// <summary>
    /// Class for measure and converters
    /// </summary>
    public static class Measure
    {
        const double hertzConvertMagic = 20000.0 / (2 * Math.PI);

        /// <summary>
        /// Convert seconds to amount of samples.
        /// </summary>
        /// <param name="seconds"></param>
        /// <param name="sampleRate"></param>
        /// <returns></returns>
        public static ulong SecondsToSamplesAmount(double seconds, uint sampleRate)
        {
            return (uint)(seconds * sampleRate);
        }
        /// <summary>
        /// Return the measured length of a specific beat in bpm.
        /// For example to get 1/16 length pass to the barDividor 16.
        /// </summary>
        /// <param name="bpm"></param>
        /// <param name="barDividor"></param>
        /// <returns></returns>
        public static double BpmToSecond(byte bpm, byte barDividor)
        {
            if (barDividor < 1) throw new ArgumentException("Cant divide by 0.\nMinimum is 1");
            double root = 240000.0 / barDividor;
            return root / bpm / 1000.0;
        }

        /// <summary>
        /// Return the amount of samples needed for a beat measured by bpm.
        /// For example to get 1/16 length pass to the barDividor 16.
        /// </summary>
        /// <param name="bpm"></param>
        /// <param name="barDividor"></param>
        /// <param name="sampleRate"></param>
        /// <returns></returns>
        public static ulong SamplesAmount(byte bpm, byte barDividor, uint sampleRate)
        {
            double mul = BpmToSecond(bpm, barDividor);
            return (ulong)(sampleRate * mul);
        }

        /// <summary>
        /// Return the amount of samples needed for a beat measured by bpm.
        /// </summary>
        /// <param name="bpm"></param>
        /// <param name="barDividor"></param>
        /// <param name="sampleRate"></param>
        /// <returns></returns>
        public static ulong SamplesAmount(byte bpm, Quantize quantize, uint sampleRate)
        {
            switch (quantize)
            {
                case Quantize.Q1_128:
                    return SamplesAmount(bpm, 128, sampleRate);
                case Quantize.Q1_128D:
                    ulong samples = SamplesAmount(bpm, 128, sampleRate);
                    return samples + (samples / 2);
                case Quantize.Q1_128T:
                    return SamplesAmount(bpm, 96, sampleRate);
                case Quantize.Q1_64:
                    return SamplesAmount(bpm, 64, sampleRate);
                case Quantize.Q1_64D:
                    samples = SamplesAmount(bpm, 64, sampleRate);
                    return samples + (samples / 2);
                case Quantize.Q1_64T:
                    return SamplesAmount(bpm, 48, sampleRate);
                case Quantize.Q1_32:
                    return SamplesAmount(bpm, 32, sampleRate);
                case Quantize.Q1_32D:
                    samples = SamplesAmount(bpm, 32, sampleRate);
                    return samples + (samples / 2);
                case Quantize.Q1_32T:
                    return SamplesAmount(bpm, 24, sampleRate);
                case Quantize.Q1_16:
                    return SamplesAmount(bpm, 16, sampleRate);
                case Quantize.Q1_16D:
                    samples = SamplesAmount(bpm, 16, sampleRate);
                    return samples + (samples / 2);
                case Quantize.Q1_16T:
                    return SamplesAmount(bpm, 24, sampleRate);
                case Quantize.Q1_8:
                    return SamplesAmount(bpm, 8, sampleRate);
                case Quantize.Q1_8D:
                    samples = SamplesAmount(bpm, 8, sampleRate);
                    return samples + (samples / 2);
                case Quantize.Q1_8T:
                    return SamplesAmount(bpm, 12, sampleRate);
                case Quantize.Q1_4:
                    return SamplesAmount(bpm, 4, sampleRate);
                case Quantize.Q1_4T:
                    return SamplesAmount(bpm, 6, sampleRate);
                case Quantize.Q1_2:
                    return SamplesAmount(bpm, 2, sampleRate);
                default:
                case Quantize.Q1_1:
                    return SamplesAmount(bpm, 1, sampleRate);
            }
        }

        /// <summary>
        /// Return the sample position based on a grid value.
        /// For example to get the second 1/16 at bar 3:
        /// SamplePosition(bpm, sampleRate, 3, 16, 2)
        /// </summary>
        /// <param name="bpm"></param>
        /// <param name="sampleRate"></param>
        /// <param name="bar"></param>
        /// <param name="barDividor"></param>
        /// <param name="dividorOffset"></param>
        /// <returns></returns>
        public static ulong SamplePosition(byte bpm, uint sampleRate, ulong bar, byte barDividor, ushort dividorOffset)
        {
            ulong barsAmt = SamplesAmount(bpm, 1, sampleRate) * bar;
            ulong beatAmt = SamplesAmount(bpm, barDividor, sampleRate) * dividorOffset;
            return barsAmt + beatAmt;
        }

        /// <summary>
        /// Return db value from a sample value.
        /// </summary>
        /// <param name="vol"></param>
        /// <returns></returns>
        public static double ToDb(double vol)
        {
            return 20 * Math.Log10(Math.Abs(vol));
        }

        /// <summary>
        /// Convert theta(radians) to hertz.
        /// </summary>
        /// <param name="theta"></param>
        /// <param name="sampleRate"></param>
        /// <returns></returns>
        public static double ToHertz(double theta, ushort sampleRate)
        {
            return theta * hertzConvertMagic / sampleRate;
        }

        /// <summary>
        /// Convert hertz to theta
        /// </summary>
        /// <param name="freq"></param>
        /// <param name="sampleRate"></param>
        /// <returns></returns>
        public static double FromHertz(double freq, ushort sampleRate)
        {
            return 2 * Math.PI * freq / sampleRate;
        }
    }
}
