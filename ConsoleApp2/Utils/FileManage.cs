
using NAudio.Wave;

namespace ConsoleApp2.Utils
{
    public static class FileManage
    {
        /// <summary>
        /// Load a wave file and return his samples.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static double[,] LoadWaveFromFile(string fileName)
        {
            WaveFileReader reader = new WaveFileReader(fileName);
            int bits = reader.WaveFormat.BitsPerSample / 8;
            double[,] ret = new double[2, reader.Length / bits];
            int position = 0;
            float[] ret1 = reader.ReadNextSampleFrame();
            while(ret1 != null)
            {
                for(int i = 0; i < ret1.Length; i++)
                {
                    ret[i, position] = ret1[i];
                }
                position++;
                ret1 = reader.ReadNextSampleFrame();
            }
            return ret;
        }

        /// <summary>
        /// Save wav file from a mixer.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="m"></param>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public static bool SaveWaveFromMixer(string name, Mixer m, double seconds)
        {

            try
            {
                ulong len = Measure.SecondsToSamplesAmount(seconds, m.SampleRate);
                WaveFileWriter w = new WaveFileWriter(name + ".wav", new WaveFormat((int)m.SampleRate, 16, 2));
                while (m.SamplePosition < len)
                {
                    float[] buf = m.ProcessBuffer();
                    w.WriteSamples(buf, 0, buf.Length);
                }
                w.Close();
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
