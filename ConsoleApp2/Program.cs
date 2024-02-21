using ConsoleApp2.Utils;

Console.WriteLine(Measure.FromHertz(20000, 44100));

//using NAudio.Wave;
//WaveFormat fmt = new WaveFormat(44100, 32, 2);
//BufferedWaveProvider buffer = new BufferedWaveProvider(fmt);
//WaveOutEvent e = new WaveOutEvent();
//buffer.AddSamples(Sine(), 0, 4 * 44100 * 2);
//e.Init(buffer);
//e.Play();
//while(true)
//{
//    Console.WriteLine(e.GetPosition());
//    Thread.Sleep(1000);
//    buffer.AddSamples(Sine(), 0, 4 * 44100 * 2);
//}

//byte[] Sine()
//{
//    Random rnd = new Random();
//    byte[] arr = new byte[44100 * 2 * 4];
//    for(int i = 0; i < arr.Length; i++)
//    {
//        arr[i] = (byte)rnd.Next(0, 255);
//    }
//    return arr;
//}