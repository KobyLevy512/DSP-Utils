
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
            int channels = reader.WaveFormat.Channels;
            double[,] ret = new double[2, reader.Length / bits];
            byte[] buffer = new byte[bits];
            int position = 0;
            while(reader.Read(buffer, 0, bits * channels) != 0)
            {
                if(channels == 2)
                {
                    if (bits == 1)
                    {
                        ret[0, position] = buffer[0] / 255.0;
                        ret[1, position] = buffer[1] / 255.0;
                    }
                    else if(bits == 2)
                    {
                        ret[0, position] = ((buffer[1] << 8) | (ushort)buffer[0]) / 65535.0;
                        ret[1, position] = ((buffer[3] << 8) | (ushort)buffer[2]) / 65535.0;
                    }
                    else if (bits == 3)
                    {
                        ret[0, position] = (((uint)buffer[2] << 16) | ((uint)buffer[1] << 8) | buffer[0]) / 16777215.0;
                        ret[1, position] = (((uint)buffer[5] << 16) | ((uint)buffer[4] << 8) | buffer[3]) / 16777215.0;
                    }
                    else
                    {
                        ret[0, position] = (((uint)buffer[3] << 24) | ((uint)buffer[2] << 16) | ((uint)buffer[1] << 8) | buffer[0]) / 4294967295.0;
                        ret[1, position] = (((uint)buffer[7] << 24) | ((uint)buffer[6] << 16) | ((uint)buffer[5] << 8) | buffer[4]) / 4294967295.0;
                    }
                }
                else
                {
                    if (bits == 1)
                    {
                        ret[0, position] = buffer[0] / 255.0;
                        ret[1, position] = ret[0, position];
                    }
                    else if (bits == 2)
                    {
                        ret[0, position] = ((buffer[1] << 8) | (ushort)buffer[0]) / 65535.0;
                        ret[1, position] = ret[0, position];
                    }
                    else if (bits == 3)
                    {
                        ret[0, position] = (((uint)buffer[2] << 16) | ((uint)buffer[1] << 8) | buffer[0]) / 16777215.0;
                        ret[1, position] = ret[0, position];
                    }
                    else
                    {
                        ret[0, position] = (((uint)buffer[3] << 24) | ((uint)buffer[2] << 16) | ((uint)buffer[1] << 8) | buffer[0]) / 4294967295.0;
                        ret[1, position] = ret[0, position];
                    }
                }
                position++;
            }

            return ret;
        }
    }
}
